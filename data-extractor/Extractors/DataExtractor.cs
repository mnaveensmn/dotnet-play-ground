using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Microsoft.Office.Interop.Excel;
namespace data_extractor;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

public class DataExtractor
{
    public void Extract()
    {
        string fileloc = @"../data-extractor/Files";
        string scenarioPattern = @"^build	[0-9]{2}-[a-zA-Z]{3}-[0-9]{4} [0-9]{2}:[0-9]{2}:[0-9]{2}	[0-9]{1,3}\) Scenario: ";
        string failedStepPattern = @"^build	[0-9]{2}-[a-zA-Z]{3}-[0-9]{4} [0-9]{2}:[0-9]{2}:[0-9]{2}	\s*✖ ";
        Regex scenarioRegEx = new(scenarioPattern);
        Regex failedStepRegEx = new(failedStepPattern);
        SortedDictionary<int, FailedScenario> failedScenarios = new();
        var failedDuplicateCount = 0;

        foreach (string fileLocation in Directory.EnumerateFiles(fileloc, "*.log"))
        {
            var filename = Path.GetFileName(fileLocation);
            Console.WriteLine($"Processing {filename}");
            IEnumerable<string> lines = File.ReadLines(fileLocation);
            var failedScenarioFound = false;
            var failedScenario = new FailedScenario
            {
                BuildNumbers = []
            };


            foreach (var line in lines)
            {
                var result = Regex.IsMatch(line, scenarioPattern);
                if (result)
                {
                    failedScenarioFound = true;
                    failedScenario.Scenario = ExtractMatchedData(scenarioRegEx, line);
                }

                result = Regex.IsMatch(line, failedStepPattern);
                if (result && failedScenarioFound)
                {
                    failedScenario.Step = ExtractMatchedData(failedStepRegEx, line);
                    failedScenarioFound = false;
                }

                if (!string.IsNullOrEmpty(failedScenario.Scenario) && !string.IsNullOrEmpty(failedScenario.Step))
                {
                    if (failedScenarios.ContainsKey(failedScenario.GetHashCode()))
                    {
                        List<string> buildNumbers = failedScenarios[failedScenario.GetHashCode()].BuildNumbers;
                        buildNumbers.Add(GetBuildNumber(filename));
                        failedScenario.BuildNumbers = buildNumbers;
                    }
                    else
                    {
                        var buildNumbers = new List<string>() { GetBuildNumber(filename) };
                        failedScenario.BuildNumbers = buildNumbers;
                    }
                    failedScenarios[failedScenario.GetHashCode()] = failedScenario;
                    failedScenario = new FailedScenario();
                }
            }
        }
        CreateExcelFile(failedScenarios);
        Console.WriteLine($"Failed Count: {failedScenarios.Count}");
    }

    private string ExtractMatchedData(Regex regex, string line)
    {
        MatchCollection matchedScenario = regex.Matches(line);
        if (matchedScenario.Count > 1)
        {
            throw new Exception("Matched multiple scenario in single line");
        }

        var matchedText = matchedScenario[0].Value;
        return line.Replace(matchedText, "");
    }

    private string GetBuildNumber(string fileName)
    {
        string scenarioPattern = @"plan-[0-9]+-[A-Z0-9]+-([0-9]*).log";
        var match = Regex.Match(fileName, scenarioPattern);
        return match.Groups[1].Value;
    }

    private void CreateExcelFile(SortedDictionary<int, FailedScenario> failedScenariosMap)
    {
        HSSFWorkbook workbook = new HSSFWorkbook();
        HSSFFont myFont = (HSSFFont)workbook.CreateFont();
        myFont.FontHeightInPoints = 11;
        myFont.FontName = "Tahoma";

        HSSFCellStyle borderedCellStyle = (HSSFCellStyle)workbook.CreateCellStyle();

        ISheet Sheet = workbook.CreateSheet("Report");
        //Creat The Headers of the excel
        IRow HeaderRow = Sheet.CreateRow(0);

        //Create The Actual Cells
        CreateCell(HeaderRow, 0, "Step", borderedCellStyle);
        CreateCell(HeaderRow, 1, "Scenario", borderedCellStyle);
        CreateCell(HeaderRow, 2, "Build Numbers", borderedCellStyle);
        int RowIndex = 1;
        var failedScenarios = failedScenariosMap.Values.ToList().OrderBy(x => x.Step).ThenBy(x => x.Scenario);

        foreach (var failedScenario in failedScenarios)
        {
            IRow CurrentRow = Sheet.CreateRow(RowIndex);
            CreateCell(CurrentRow, 0, failedScenario.Step, borderedCellStyle);
            CreateCell(CurrentRow, 1, failedScenario.Scenario, borderedCellStyle);
            CreateCell(CurrentRow, 2, string.Join(", ", failedScenario.BuildNumbers.ToArray()), borderedCellStyle);
            RowIndex++;
        }

        using (var fileData = new FileStream(@"../data-extractor/Files/FailedScenarios.xls", FileMode.Create))
        {
            workbook.Write(fileData);
        }

    }

    private void CreateCell(IRow CurrentRow, int CellIndex, string Value, HSSFCellStyle Style)
    {
        ICell Cell = CurrentRow.CreateCell(CellIndex);
        Cell.SetCellValue(Value);
        Cell.CellStyle = Style;
    }
}

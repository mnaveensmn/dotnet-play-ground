using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using Microsoft.Office.Interop.Excel;
namespace data_extractor;

using data_extractor.Extractors;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

public class DataExtractor
{

    private ReportGenerator _reportGenerator;
    private const string _filelocation = @"../data-extractor/ConfidentialFiles";
    private const string _scenarioPattern = @"^build	[0-9]{2}-[a-zA-Z]{3}-[0-9]{4} [0-9]{2}:[0-9]{2}:[0-9]{2}	[0-9]{1,3}\) Scenario: ";
    private const string _failedStepPattern = @"^build	[0-9]{2}-[a-zA-Z]{3}-[0-9]{4} [0-9]{2}:[0-9]{2}:[0-9]{2}	\s*✖ ";
    private Regex scenarioRegEx = new(_scenarioPattern);
    private Regex failedStepRegEx = new(_failedStepPattern);

    public DataExtractor()
    {
        _reportGenerator = new ReportGenerator();
    }

    public void Extract()
    {
        SortedDictionary<int, FailedScenario> failedScenarios = new();

        foreach (string fileLocation in Directory.EnumerateFiles(_filelocation, "*.log"))
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
                var result = Regex.IsMatch(line, _scenarioPattern);
                if (result)
                {
                    failedScenarioFound = true;
                    failedScenario.Scenario = ExtractMatchedData(scenarioRegEx, line);
                }

                result = Regex.IsMatch(line, _failedStepPattern);
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
                    failedScenario.FileName = GetFeatureName(failedScenario.Scenario);
                    failedScenarios[failedScenario.GetHashCode()] = failedScenario;
                    failedScenario = new FailedScenario();
                }
            }
        }
        _reportGenerator.CreateExcelFile(failedScenarios);
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

    private string GetFeatureName(string scenario)
    {
        int startIndex = scenario.IndexOf('#') + 1;
        int endIndex = scenario.IndexOf(':');
        scenario = scenario.Substring(startIndex, (endIndex - startIndex));
        return scenario.Replace("features/", "").Trim();
    }

}

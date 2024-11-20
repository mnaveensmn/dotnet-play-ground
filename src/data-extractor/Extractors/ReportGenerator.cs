using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace data_extractor.Extractors
{
    public class ReportGenerator
    {

        public void CreateExcelFile(SortedDictionary<int, FailedScenario> failedScenariosMap)
        {

            var failedScenarios = failedScenariosMap.Values
                                            .ToList()
                                            .OrderBy(x => x.FileName)
                                            .ThenBy(x => x.Scenario)
                                            .ThenBy(x => x.Step)
                                            .ToList();

            HSSFWorkbook workbook = new HSSFWorkbook();

            CreateFailedScenario(workbook, failedScenarios);
            CreateFeaturesStats(workbook, failedScenarios);

            using (var fileData = new FileStream(@"../data-extractor/ConfidentialFiles/FailedScenarios.xls", FileMode.Create))
            {
                workbook.Write(fileData);
            }
        }

        private void CreateFailedScenario(HSSFWorkbook workbook, List<FailedScenario> failedScenarios)
        {
            HSSFCellStyle cellStyle = SetCellStyle(workbook);

            ISheet sheet = workbook.CreateSheet("Failed Scenario");
            sheet.SetColumnWidth(0, 6 * 1000);
            sheet.SetColumnWidth(1, 17 * 1000);
            sheet.SetColumnWidth(2, 17 * 1000);
            sheet.SetColumnWidth(3, 6 * 1000);
            sheet.VerticallyCenter = true;

            IRow headerRow = sheet.CreateRow(0);
            headerRow.Height = 450;

            CreateCell(headerRow, 0, "File Name", cellStyle);
            CreateCell(headerRow, 1, "Scenario", cellStyle);
            CreateCell(headerRow, 2, "Step", cellStyle);
            CreateCell(headerRow, 3, "Build Numbers", cellStyle);
            int RowIndex = 1;

            foreach (var failedScenario in failedScenarios)
            {
                IRow currentRow = sheet.CreateRow(RowIndex);
                currentRow.Height = 400;
                CreateCell(currentRow, 0, failedScenario.FileName, cellStyle);
                CreateCell(currentRow, 1, failedScenario.Step, cellStyle);
                CreateCell(currentRow, 2, failedScenario.Scenario, cellStyle);
                CreateCell(currentRow, 3, string.Join(", ", failedScenario.BuildNumbers.ToArray()), cellStyle);
                RowIndex++;
            }
        }

        private void CreateFeaturesStats(HSSFWorkbook workbook, List<FailedScenario> failedScenarios)
        {
            HSSFCellStyle cellStyle = SetCellStyle(workbook);

            ISheet sheet = workbook.CreateSheet("Feature Stats");
            sheet.SetColumnWidth(0, 6 * 1000);
            sheet.SetColumnWidth(1, 6 * 1000);
            sheet.VerticallyCenter = true;

            IRow headerRow = sheet.CreateRow(0);
            headerRow.Height = 450;

            CreateCell(headerRow, 0, "Feature", cellStyle);
            CreateCell(headerRow, 1, "Failed Count", cellStyle);
            int RowIndex = 1;

            var groupedByFiledName = failedScenarios.GroupBy(scenario => scenario.FileName).ToList();

            foreach (var group in groupedByFiledName)
            {
                IRow currentRow = sheet.CreateRow(RowIndex);
                currentRow.Height = 400;
                CreateCell(currentRow, 0, group.Key, cellStyle);
                CreateCell(currentRow, 1, group.Count().ToString(), cellStyle);
                RowIndex++;
            }
        }

        private void CreateCell(IRow CurrentRow, int CellIndex, string Value, HSSFCellStyle Style)
        {
            ICell Cell = CurrentRow.CreateCell(CellIndex);
            Cell.SetCellValue(Value);
            Cell.CellStyle = Style;
        }

        private HSSFCellStyle SetCellStyle(HSSFWorkbook workbook)
        {
            HSSFFont myFont = (HSSFFont)workbook.CreateFont();
            myFont.FontHeightInPoints = 11;

            HSSFCellStyle cellStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            cellStyle.SetFont(myFont);

            cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.VerticalAlignment = VerticalAlignment.Center;

            return cellStyle;
        }

    }
}
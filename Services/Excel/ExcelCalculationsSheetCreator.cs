using ClosedXML.Excel;
using DataModel.Model;
using DocumentFormat.OpenXml.Spreadsheet;
using Services.Abstractions.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Excel
{
    public class ExcelCalculationsSheetCreator : IExcelCalculationsSheetCreator
    {
        private Escalation? Escalation { get; set; }
        private IXLWorksheet? calculationsSheet { get; set; }
        private (
            int Year,
            int ThreeMonth,
            int Field,
            int Subfield,
            int CurrentPrice,
            int PreviousPrice,
            int Difference,
            int Proportion,
            int PriceInCurrentTimebox,
            int BaseIndex,
            int WorkingIndex,
            int EscalationCoef,
            int EscalationPrice
            ) columns =
        (
                Year: 1,
                ThreeMonth: 2,
                Field: 3,
                Subfield: 4,
                CurrentPrice: 5,
                PreviousPrice: 6,
                Difference: 7,
                Proportion: 8,
                PriceInCurrentTimebox: 9,
                BaseIndex: 10,
                WorkingIndex: 11,
                EscalationCoef: 12,
                EscalationPrice: 13
                );
        private (
            string Year,
            string ThreeMonth,
            string Field,
            string Subfield,
            string CurrentPrice,
            string PreviousPrice,
            string Difference,
            string Proportion,
            string PriceInCurrentTimebox,
            string BaseIndex,
            string WorkingIndex,
            string EscalationCoef,
            string EscalationPrice
            ) columnsChar =
        (
                Year: "A",
                ThreeMonth: "B",
                Field: "C",
                Subfield: "D",
                CurrentPrice: "E",
                PreviousPrice: "F",
                Difference: "G",
                Proportion: "H",
                PriceInCurrentTimebox: "I",
                BaseIndex: "J",
                WorkingIndex: "K",
                EscalationCoef: "L",
                EscalationPrice: "M"
                );
        public void WriteCalculationsSheet(Escalation escalation, XLWorkbook workbook)
        {
            Escalation = escalation;
            var usedRowsUpTo = 1;
            calculationsSheet = workbook.Worksheets.Add("محاسبات تعدیل");
            calculationsSheet.Rows().AdjustToContents();
            calculationsSheet.SetRightToLeft(true);
            usedRowsUpTo += WriteHeaders(usedRowsUpTo);
            var fieldGroups = escalation.Items.GroupBy(i => i.Subfield?.Field);

            for (int i = 0; i < fieldGroups.Count(); i++)
            {
                //var rows = fieldGroups[0].ToArray();
                var items = fieldGroups.ToList()[i].ToArray();
                WriteItemsOfField(usedRowsUpTo + 1, items);
            }
        }




        /// <returns>returns number of rows used</returns>
        private int WriteHeaders(int startRow)
        {
            calculationsSheet.Cell(startRow, columns.Year).Value = "دوره کارکرد";
            calculationsSheet.Range($"{columnsChar.Year}{startRow}:{columnsChar.ThreeMonth}{startRow + 1}").Row(1).Merge();

            calculationsSheet.Cell(startRow + 1, columns.Year).Value = "سال";
            calculationsSheet.Cell(startRow + 1, columns.ThreeMonth).Value = "سه ماهه";

            calculationsSheet.Cell(startRow, columns.Field).Value = "رشته";
            calculationsSheet.Range(startRow, columns.Field, startRow + 1, columns.Field).Merge();

            calculationsSheet.Cell(startRow, columns.Subfield).Value = "فصل";
            calculationsSheet.Range(startRow, columns.Subfield, startRow + 1, columns.Subfield).Merge();

            calculationsSheet.Cell(startRow, columns.CurrentPrice).Value = "مبلغ صورت وضعیت فعلی";
            calculationsSheet.Range(startRow, columns.CurrentPrice, startRow + 1, columns.CurrentPrice).Merge();

            calculationsSheet.Cell(startRow, columns.PreviousPrice).Value = "مبلغ صورت وضعیت قبلی";
            calculationsSheet.Range(startRow, columns.PreviousPrice, startRow + 1, columns.PreviousPrice).Merge();

            calculationsSheet.Cell(startRow, columns.Difference).Value = "مبلغ مابه‌التفاوت دو صورت وضعیت";
            calculationsSheet.Range(startRow, columns.Difference, startRow + 1, columns.Difference).Merge();

            calculationsSheet.Cell(startRow, columns.Proportion).Value = "نسبت مدت کارکرد در دوره به مدت کارکرد";
            calculationsSheet.Range(startRow, columns.Proportion, startRow + 1, columns.Proportion).Merge();

            calculationsSheet.Cell(startRow, columns.PriceInCurrentTimebox).Value = "مبلغ کارکرد در دوره";
            calculationsSheet.Range(startRow, columns.PriceInCurrentTimebox,
                startRow + 1, columns.PriceInCurrentTimebox).Merge();

            calculationsSheet.Cell(startRow, columns.BaseIndex).Value = "شاخص مبنا";
            calculationsSheet.Range(startRow, columns.BaseIndex, startRow + 1, columns.BaseIndex).Merge();

            calculationsSheet.Cell(startRow, columns.WorkingIndex).Value = "شاخص دوره کارکرد";
            calculationsSheet.Range(startRow, columns.WorkingIndex, startRow + 1, columns.WorkingIndex).Merge();

            calculationsSheet.Cell(startRow, columns.EscalationCoef).Value = "ضریب تعدیل";
            calculationsSheet.Range(startRow, columns.EscalationCoef, startRow + 1, columns.EscalationCoef).Merge();

            calculationsSheet.Cell(startRow, columns.EscalationPrice).Value = "مبلغ تعدیل";
            calculationsSheet.Range(startRow, columns.EscalationPrice, startRow + 1, columns.EscalationPrice).Merge();

            return 2;
        }


        /// <returns>number of rows used</returns>
        private int WriteItemsOfField(int startingRow, EscalationItem[] items)
        {
            var usedRowsCount = 0;
            var itemStartRow = startingRow;

            foreach (var item in items)
            {
                usedRowsCount += WriteItemRows(item, itemStartRow);
                itemStartRow = startingRow + usedRowsCount;
            }
            usedRowsCount += WriteSumOfField(items, startingRow: startingRow + usedRowsCount + 1);
            return usedRowsCount;
        }

        private int WriteSumOfField(EscalationItem[] items, int startingRow)
        {
            if (items is null || !items.Any()) { return 0; }
            var field = items.First().Subfield?.Field;
            var sum = items.First().Escalation.GetSumOfField(field);

            calculationsSheet.Cell(row: startingRow, column: columns.Year).Value = $"جمع تعدیل {field}";
            calculationsSheet.Range(startingRow, columns.Year, startingRow, columns.EscalationCoef).Merge();
            calculationsSheet.Cell(startingRow, columns.EscalationPrice).Value = sum;

            return 1;
        }


        private int WriteItemRows(EscalationItem item, int itemStartRow)
        {
            if (calculationsSheet is null) return 0;
            var usedRowsCount = 0;
            for (int i = 1; i <= item.Rows?.Count; i++)
            {
                var row = item.Rows[i - 1];
                var rowNo = itemStartRow + i - 1;
                if (i == 1)
                {
                    calculationsSheet.Cell(itemStartRow, columns.Field).Value = item.Subfield?.Field;
                    calculationsSheet.Range(
                        itemStartRow,
                        columns.Field,
                        itemStartRow + item.Rows.Count - 1,
                        columns.Field)
                        .Merge();

                    calculationsSheet.Cell(itemStartRow, columns.Subfield).Value = item.Subfield?.Number;
                    calculationsSheet.Range(
                        itemStartRow,
                        columns.Subfield,
                        itemStartRow + item.Rows.Count - 1,
                        columns.Subfield)
                        .Merge();

                    calculationsSheet.Cell(itemStartRow, columns.PreviousPrice).Value = item.PreviousPrice;
                    calculationsSheet.Range(
                        itemStartRow,
                        columns.PreviousPrice,
                        itemStartRow + item.Rows.Count - 1,
                        columns.PreviousPrice)
                        .Merge();

                    calculationsSheet.Cell(itemStartRow, columns.CurrentPrice).Value = item.CurrentPrice;
                    calculationsSheet.Range(
                        itemStartRow,
                        columns.CurrentPrice,
                        itemStartRow + item.Rows.Count - 1,
                        columns.CurrentPrice)
                        .Merge();

                    calculationsSheet.Cell(itemStartRow, columns.BaseIndex).Value = item.BaseIndex;
                    calculationsSheet.Range(
                        itemStartRow,
                        columns.BaseIndex,
                        itemStartRow + item.Rows.Count - 1,
                        columns.BaseIndex)
                        .Merge();
                }
                calculationsSheet.Cell(row: rowNo, columns.Year).Value = row.WorkingTimeBox?.SolarYear;
                if (row.WorkingTimeBox?.ThreeMonthNo != default)
                {
                    calculationsSheet.Cell(row: rowNo, columns.ThreeMonth).Value = row.WorkingTimeBox?.ThreeMonthNo;
                }
                else
                {
                    calculationsSheet.Cell(row: rowNo, columns.ThreeMonth).Value = row.WorkingTimeBox?.Month;
                }


                calculationsSheet.Cell(rowNo, columns.Proportion).Value = row.WorkingProportionRounded;
                calculationsSheet.Cell(rowNo, columns.PriceInCurrentTimebox).Value = row.WorkingPriceInCurrentTimebox;
                calculationsSheet.Cell(rowNo, columns.WorkingIndex).Value = row.WorkingTimeBoxIndex;
                calculationsSheet.Cell(rowNo, columns.EscalationCoef).Value = row.EscalationCoefficientRounded;
                calculationsSheet.Cell(rowNo, columns.EscalationPrice).Value = row.EscalationPriceRounded;
            }
            usedRowsCount += item.Rows?.Count ?? 0;
            return usedRowsCount;
        }
    }
}

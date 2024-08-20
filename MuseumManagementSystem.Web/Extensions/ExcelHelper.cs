using ClosedXML.Excel;
using System.Globalization;

namespace MuseumManagementSystem.Web.ExtensionMethods
{
    public static class ExcelHelper
    {
        public static XLWorkbook ExportToExcel<T>(IEnumerable<T> data,
            Dictionary<string,string> loclizedColumnNames)
        {
            var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("Sheet1");

            //Check if language is ar make direction RTl
            if (CultureInfo.CurrentCulture.Name.StartsWith("ar"))
                worksheet.RightToLeft = true;

            //Get loclized Column Names
            int col = 1;
            foreach ( var columnName in loclizedColumnNames.Values)
            {
                worksheet.Cell(1, col).Value = columnName;
                col++;
            }

            //Fill the data rows
            var row = 2;
            foreach(var item in data)
            {
                col = 1;
                foreach(var proerty in typeof(T).GetProperties())
                {
                    var value = proerty.GetValue(item);
                    worksheet.Cell(row, col).Value = Convert.ToString(value);
                    col++;
                }
                row++;
            }
            return workbook;
        }
    }
}

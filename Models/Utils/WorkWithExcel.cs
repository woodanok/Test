using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CustomrsFinder.Models.Utils
{
    public class WorkWithExcel
    {
        public IocContainer dependency = null;
        public WorkWithExcel(IocContainer dependency) => this.dependency = dependency;

        public delegate String GetCulumnName(String oldColumnName);
        public async Task<T> ReadFromExcel<T>(string path, GetCulumnName getCulumnName = null, bool hasHeader = true)
        {
            DataTable excelasTable = new DataTable();
            await Task.Run(() =>
            {
                // If you are a commercial business and have
                // purchased commercial licenses use the static property
                // LicenseContext of the ExcelPackage class:
                ExcelPackage.LicenseContext = LicenseContext.Commercial;
                var excelPack = new ExcelPackage(new FileInfo(path));

                //Lets Deal with first worksheet.(You may iterate here if dealing with multiple sheets)
                var ws = excelPack.Workbook.Worksheets[0];

                //Get all details as DataTable -because Datatable make life easy :)
                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                {
                    //Get colummn details
                    if (!string.IsNullOrEmpty(firstRowCell.Text))
                    {
                        string firstColumn = string.Format("Column {0}", firstRowCell.Start.Column);

                        if (getCulumnName != null) excelasTable.Columns.Add(hasHeader ? getCulumnName(firstRowCell.Text) : firstColumn);
                        else excelasTable.Columns.Add(hasHeader ? firstRowCell.Text : firstColumn);
                    }
                }
                
                var startRow = hasHeader ? 2 : 1;
                //Get row details
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, excelasTable.Columns.Count];
                    #region stop to read excel if row is empty
                    if (ws.Cells[rowNum, 1].Value == null)
                    {
                        rowNum = ws.Dimension.End.Row;
                        //excelasTable.Rows.RemoveAt(excelasTable.Rows.Count - 1);
                        break;
                    }
                    #endregion
                    DataRow row = excelasTable.Rows.Add();
                    foreach (var cell in wsRow)
                    {
                        if(excelasTable.Columns[cell.Start.Column - 1].ColumnName.ToLower().Contains("link") || excelasTable.Columns[cell.Start.Column - 1].ColumnName.ToLower().Contains("url")) row[cell.Start.Column - 1] = cell.Hyperlink;
                        else row[cell.Start.Column - 1] = cell.Text;
                    }
                }
            });
            //Get everything as generics and let end user decides on casting to required type
            var generatedType = JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(excelasTable));
            return (T)Convert.ChangeType(generatedType, typeof(T));
        }

    }
}

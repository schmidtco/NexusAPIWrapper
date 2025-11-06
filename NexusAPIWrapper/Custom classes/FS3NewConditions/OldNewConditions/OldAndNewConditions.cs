using ClosedXML.Excel;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;

namespace NexusAPIWrapper.Custom_classes.FS3NewConditions.OldNewConditions
{
    public class OldAndNewConditions
    {
        private readonly Dictionary<string, (string NewArea, string NewCategory, string NewCondition)> _mappings = new Dictionary<string, (string NewArea, string NewCategory, string NewCondition)>();
        /// <summary>
        /// Will not load conditions. Use .LoadExcelDataWithOLEDB or .LoadExcelDataWithoutOLEDB afterwards
        /// </summary>
        public OldAndNewConditions()
        {

        }
        /// <summary>
        /// Will load conditions using OLEDB
        /// </summary>
        /// <param name="excelFilePath"></param>
        public OldAndNewConditions(string excelFilePath)
        {
            LoadExcelData(excelFilePath, true);
        }
        /// <summary>
        /// Will load condition using OLEDB if true, and NPOI if false
        /// </summary>
        /// <param name="excelFilePath"></param>
        /// <param name="useOLEDB"></param>
        public OldAndNewConditions(string excelFilePath, bool useOLEDB)
        {
            LoadExcelData(excelFilePath,useOLEDB);
        }

        /// <summary>
        /// Looks up the new area, category, and condition based on the old values.
        /// Returns (null, null, null) if no mapping is found.
        /// Note: Lookup is case-insensitive.
        /// </summary>
        /// <param name="oldArea">Old area.</param>
        /// <param name="oldCategory">Old category.</param>
        /// <param name="oldCondition">Old condition.</param>
        /// <returns>A tuple containing the new area, category, and condition, or (null, null, null) if not found.</returns>
        public (string NewArea, string NewCategory, string NewCondition) GetNewMapping(string oldArea, string oldCategory, string oldCondition)
        {
            var key = new ConditionKey(oldArea, oldCategory, oldCondition).conditionKey;
            if (_mappings.TryGetValue(key, out var newValues))
            {
                return newValues;
            }

            return (null, null, null);
        }


        public class ConditionKey
        {
            private string _conditionKey;
            public string conditionKey { get => _conditionKey; }
            public ConditionKey(string oldArea, string oldCategory, string oldCondition)
            {
                _conditionKey = oldArea + oldCategory + oldCondition;
            }
        }
        public void LoadExcelData(string filePath, bool useOLEDB)
        {
            if (useOLEDB)
            {
                LoadExcelDataWithOLEDB(filePath);
            }
            else
            {
                LoadExcelDataWithoutOLEDB(filePath);
            }
        }
        public void LoadExcelDataWithoutOLEDB(string filePath)
        {
            // Ensure the file exists
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Excel file not found.", filePath);
            }

            using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                // Load the workbook (supports both .xlsx and .xls)
                IWorkbook workbook = WorkbookFactory.Create(fileStream);

                // Get the first worksheet
                ISheet worksheet = workbook.GetSheetAt(0);

                // Ensure the worksheet has data (at least one data row beyond the header)
                if (worksheet.LastRowNum < 1)
                {
                    throw new InvalidOperationException("The Excel file contains no data or only headers.");
                }

                // Loop through rows, starting from row 1 (0-based index, skipping header row)
                for (int rowIndex = 1; rowIndex <= worksheet.LastRowNum; rowIndex++)
                {
                    IRow row = worksheet.GetRow(rowIndex);
                    if (row == null) // Skip empty rows
                        continue;

                    // Read cell values (0-based column indices)
                    string oldArea = row.GetCell(0)?.ToString()?.Trim();
                    string oldCategory = row.GetCell(1)?.ToString()?.Trim();
                    string oldCondition = row.GetCell(2)?.ToString()?.Trim();
                    string newArea = row.GetCell(3)?.ToString()?.Trim();
                    string newCategory = row.GetCell(4)?.ToString()?.Trim();
                    string newCondition = row.GetCell(5)?.ToString()?.Trim();

                    // Skip invalid rows (e.g., empty old keys)
                    if (string.IsNullOrWhiteSpace(oldArea) || string.IsNullOrWhiteSpace(oldCategory) || string.IsNullOrWhiteSpace(oldCondition))
                    {
                        continue;
                    }

                    var key = new ConditionKey(oldArea, oldCategory, oldCondition).conditionKey;
                    _mappings[key] = (newArea, newCategory, newCondition);
                }
            }
        }
        public void LoadExcelDataWithOLEDB(string filePath)
        {
            string connString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};Extended Properties='Excel 12.0 Xml;HDR=YES;'";

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("Excel file not found.", filePath);
            }

            using (OleDbConnection conn = new OleDbConnection(connString))
            {
                conn.Open();

                // Get the first sheet name
                DataTable dtSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                string sheetName = dtSchema.Rows[0]["TABLE_NAME"].ToString();

                // Read data from the first sheet
                string query = $"SELECT * FROM [{sheetName}]";

                using (OleDbDataAdapter adapter = new OleDbDataAdapter(query, conn))
                {
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);


                    foreach (DataRow row in dt.Rows)
                    {
                        string oldArea = row.ItemArray[0].ToString();
                        string oldCategory = row.ItemArray[1].ToString();
                        string oldCondition = row.ItemArray[2].ToString();
                        string newArea = row.ItemArray[3].ToString();
                        string newCategory = row.ItemArray[4].ToString();
                        string newCondition = row.ItemArray[5].ToString();

                        // Skip invalid rows (e.g., empty old keys)
                        if (string.IsNullOrWhiteSpace(oldArea) || string.IsNullOrWhiteSpace(oldCategory) || string.IsNullOrWhiteSpace(oldCondition))
                        {
                            continue;
                        }

                        var key = new ConditionKey(oldArea, oldCategory, oldCondition).conditionKey;
                        _mappings[key] = (newArea, newCategory, newCondition);
                    }
                }

            }
        }
    }

}

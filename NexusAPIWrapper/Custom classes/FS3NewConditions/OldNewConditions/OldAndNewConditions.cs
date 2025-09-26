using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusAPIWrapper.Custom_classes.FS3NewConditions.OldNewConditions
{
    public class OldAndNewConditions
    {
        private readonly Dictionary<string, (string NewArea, string NewCategory, string NewCondition)> _mappings = new Dictionary<string, (string NewArea, string NewCategory, string NewCondition)>();
        public OldAndNewConditions(string excelFilePath)
        {
            LoadExcelData(excelFilePath);
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
        public void LoadExcelData(string filePath)
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

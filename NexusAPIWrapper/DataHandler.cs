using CsQuery.Engine.PseudoClassSelectors;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NexusAPIWrapper.Custom_classes;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace NexusAPIWrapper
{
    public class DataHandler
    {
        /*
         This class can handle all the different types of data from the API 
         */


        public DataHandler() { }
        internal enum StringObjectType
        {
            arrayJson,
            json,
            unknown
        }

        internal int GetDatePosition(string input)
        {
            string pattern = @"(((0[1-9])|([12][0-9])|(3[01]))-((0[0-9])|(1[012]))-((20[012]\d|19\d\d)|(1\d|2[0123])))";

            Regex regex = new Regex(pattern);
            var match = regex.Match(input);

            return match.Index;
        }
        /// <summary>
        /// Will split a string by the string input - default will be backslash
        /// </summary>
        /// <param name="input"></param>
        /// <param name="splitBy"></param>
        /// <returns></returns>
        internal string[] SplitStringByString(string input, string splitBy = "\\")
        {
            return input.Split(new string[] { splitBy }, StringSplitOptions.None);
        }
        internal void AddResource(SortedDictionary<string, string> dict, string strName, string strValue)
        {
            if (dict.ContainsKey(strName))
                return;
            dict.Add(strName, strValue);    
        }
        internal void AddResource(SortedDictionary<string, SortedDictionary<string, string>> dict, string strName, SortedDictionary<string, string> subDict)
        {
            if (dict.ContainsKey(strName))
                return;
            dict.Add(strName, subDict);
        }
        internal string GetElementDataFromSortedDict(SortedDictionary<string, string> dict, string elementName)
        {
            string result = string.Empty;
            foreach (var item in dict)
            {
                if (item.Key == elementName)
                {
                    result = item.Value;
                    break;
                }
                else
                {
                    result = "Element not found.";
                }
            }
            return result;
        }

        internal JObject ConvertNexusResultToJsonObject(NexusResult output)
        {
            JObject result = new JObject();
            var jsonOutput = JsonConvert.DeserializeObject<dynamic>((string)output.Result);
            //bool boolresult = false;
            switch (jsonOutput is JArray)
            {
                case true:
                    //boolresult = true;
                    //JArray arrayObj = (JArray)jsonOutput;
                    //var obj = JsonConvert.DeserializeObject<JArray>(output.Result.ToString()).ToObject<List<JObject>>().FirstOrDefault();
                    result = ConvertJArrayStringToJObject(output.Result.ToString());
                    break;
                case false:
                    //boolresult = false;
                    result = jsonOutput;
                    break;
                default:
                    break;
            }   
            //dynamic jsonOutput = JObject.Parse((string)output.Result);
            

            return result;
        }
        internal JObject ConvertJArrayStringToJObject(string jArray)
        {
            return JsonConvert.DeserializeObject<JArray>(jArray).ToObject<List<JObject>>().FirstOrDefault();
        }
        internal JObject ConvertNexusResultToJsonObject(NexusResult output, string objectName)

        {
            dynamic jsonOutput = JObject.Parse((string)output.Result);
            dynamic objectOutput = jsonOutput[objectName];
            JObject result = objectOutput;

            return result;
        }
        public SortedDictionary<string, string> ArrayJsonStringToSortedDictionary (string arrayJson)
        {
            // Duplicate document names will not be added to the dictionary and therefore only show the first occurance
            SortedDictionary<string, string> dict = new SortedDictionary<string, string>();
            SortedDictionary<string, string> subDict = new SortedDictionary<string, string>();

            //JArray  data = JArray.Parse(arrayJson);
            var data = JsonConvert.DeserializeObject<dynamic>(arrayJson);

            foreach (var item in data)
            {

                subDict.Clear();
                foreach (var itemInfo in item)
                {
                    subDict.Add(itemInfo.Name.ToString(), itemInfo.Value.ToString());
                }

                AddResource(dict, subDict["name"], JsonConvert.SerializeObject(item));
            }
            return dict;
        }
        public SortedDictionary<string, string> ArrayJsonStringToSortedDictionary(string arrayJson,string subDictNameString)
        {
            // Duplicate document names will not be added to the dictionary and therefore only show the first occurance
            SortedDictionary<string, string> dict = new SortedDictionary<string, string>();
            SortedDictionary<string, string> subDict = new SortedDictionary<string, string>();

            //JArray  data = JArray.Parse(arrayJson);
            var data = JsonConvert.DeserializeObject<dynamic>(arrayJson);

            foreach (var item in data)
            {

                subDict.Clear();
                foreach (var itemInfo in item)
                {
                    subDict.Add(itemInfo.Name.ToString(), itemInfo.Value.ToString());
                }

                AddResource(dict, subDict[subDictNameString], JsonConvert.SerializeObject(item));
            }
            // If the subdict only has 1 element, we just return that and not the dict
            if (subDict.Count == 1)
            {
                return subDict;
            }
            else
            {
                return dict;
            }
        }
        internal SortedDictionary<string, string> JsonStringToSortedDictionary(string json)
        {
            //We convert to sorted dictionary so we don't need to do .ToString() on every property.
            // Duplicate document names will not be added to the dictionary and therefore only show the first occurance
            SortedDictionary<string, string> dict = new SortedDictionary<string, string>();
            JObject jsonObject = JObject.Parse(json);
            dynamic values = (dynamic)jsonObject;

            foreach (var item in values)
            {
                string ressourceName = Convert.ToString(item.Name);
                string ressourceValue = Convert.ToString(item.Value);
                AddResource(dict, ressourceName, ressourceValue);
            }
            return dict;
        }
        internal SortedDictionary<string, string> JsonToSortedDictionary(JObject json)
        {
            //We convert to sorted dictionary so we don't need to do .ToString() on every property.
            // Duplicate document names will not be added to the dictionary and therefore only show the first occurance
            SortedDictionary<string, string> dict = new SortedDictionary<string, string>();
            
            dynamic values = (dynamic)json;

            foreach (var item in values)
            {
                string ressourceName = Convert.ToString(item.Name);
                string href = Convert.ToString(item.Value);
                AddResource(dict, ressourceName, href);
            }
            return dict;
        }
        internal StringObjectType ReturnStringObjectType (string input)
        {
            StringObjectType result = StringObjectType.unknown;
            bool parseToJson = true;
            try
            {
                JObject jsonObject = JObject.Parse(input);
                //result = "json";
                result = StringObjectType.json;
            }
            catch
            {
                parseToJson = false;
                if (!parseToJson)
                {
                    try
                    {
                        JArray arrayObject = JArray.Parse(input);
                        //result = "arrayJson";
                        result = StringObjectType.arrayJson;
                    }
                    catch 
                    {
                        //result = "Unable to convert data.";
                        result = StringObjectType.unknown;
                    }
                    
                }
            }
            return result;
        }
        public SortedDictionary<string, string> ConvertJsonToSortedDictionary(string json)
        {
            StringObjectType stringType = ReturnStringObjectType(json);
            switch (stringType)
            {
                case StringObjectType.arrayJson:
                    return ArrayJsonStringToSortedDictionary(json);
                case StringObjectType.json:
                    return JsonStringToSortedDictionary(json);
                default: 
                    return null;
            }
            
        }
        internal SortedDictionary<string, string> ConvertJsonToSortedDictionary(JObject json)//, string partToExtract)
        {
            //We only convert to sorted dictionary because we then don't need to do .ToString() on every property.
            SortedDictionary<string, string> dict = new SortedDictionary<string, string>();

            dynamic values = (dynamic)json;

            foreach (var item in values)
            {
                string ressourceName = Convert.ToString(item.Name);
                string href = Convert.ToString(item.Value);
                AddResource(dict, ressourceName, href);
            }
            return dict;
        }

        internal SortedDictionary<string, string> GetNestedData(SortedDictionary<string, string> dict)
        {
            JObject dataOutput = JObject.Parse(dict.ToString());
            return ConvertJsonToSortedDictionary(dataOutput);
        }
        public SortedDictionary<string, string> GetNestedData(SortedDictionary<string, string> dict, string dataName)
        {
            JObject dataOutput = JObject.Parse(dict[dataName]);
            return ConvertJsonToSortedDictionary(dataOutput);
        }

        internal string GetHref(SortedDictionary<string,string> dict, bool selfLink)
        {
            string result = string.Empty;
            switch (selfLink)
            {
                case true:
                    var strSelf = dict["self"];
                    if (strSelf != null)
                    {
                        var hrefDict = ConvertJsonToSortedDictionary(strSelf);
                        result = hrefDict["href"].ToString();
                    }
                    else
                    {
                        result = "";
                    }
                    break;
            
                case false:
                    result = dict["href"];
                    break;
            }
            return result;
        }
        internal string GetHref(SortedDictionary<string, string> dict, string linkName, bool selfLink)
        {
            string result = string.Empty;
            switch (selfLink)
            {
                case true:
                    string linkNameLink = dict[linkName];
                    var linkNameLinkDict = JsonStringToSortedDictionary(linkNameLink);
                    var strSelf = linkNameLinkDict["self"];
                    if (strSelf != null)
                    {
                        var hrefDict = ConvertJsonToSortedDictionary(strSelf);
                        result = hrefDict["href"].ToString();
                    }
                    else
                    {
                        result = "";
                    }
                    break;

                case false:
                    result = dict[linkName];
                    var resultDict = JsonStringToSortedDictionary(result);
                    result = resultDict["href"].ToString();
                    break;
            }
            return result;
        }
        internal string GetHref(JObject jsonObject, bool selfLink)
        {
            string result = string.Empty;
            switch (selfLink) 
            {
                case true:
                    var selfLinkObject = (dynamic)jsonObject["self"];
                    var linkStringObject = selfLinkObject["href"];
                    result = linkStringObject.Value;
                    break;
                case false:
                    var linkStringObj = (dynamic)jsonObject["href"];
                    result = linkStringObj.Value;
                    break;  
            }
            return result;
        }

        internal string GetDocumentPrototypeLink(SortedDictionary<string, string> links)
        {
            var documentPrototypeObj = links["documentPrototype"];
            var documentPrototypeDict = JsonStringToSortedDictionary(documentPrototypeObj);
            return GetHref(documentPrototypeDict, false);
        }
        internal string GetDocumentPrototypeLink(PatientDetailsSearch_Links links)
        {
            return links.DocumentPrototype.Href;
        }
        internal string GetDocumentPrototypeLink(PatientPreferences_CITIZENPATHWAY pathway)
        {
            return pathway.Links.Self.Href;
        }

        internal string SortedDictionaryToJSON(SortedDictionary<string, string> dictObject)
        {
            JObject result = new JObject();
            foreach (var link in dictObject)
            {
                result.Add(link.Key, link.Value);
            }
            return JsonConvert.SerializeObject(result);
        }

        internal DateTime GetDate(string TimeOfX)
        {
            int day = Convert.ToInt32(TimeOfX.Substring(0, 2));
            int month = Convert.ToInt32(TimeOfX.Substring(3, 2));
            int year = Convert.ToInt32(TimeOfX.Substring(6, 4));
            DateTime date = new DateTime(year, month, day);
            return date;
        }
        internal DateTime GetDateAndTime(string TimeOfX)
        {
            int day = Convert.ToInt32(TimeOfX.Substring(0, 2));
            int month = Convert.ToInt32(TimeOfX.Substring(3, 2));
            int year = Convert.ToInt32(TimeOfX.Substring(6, 4));
            string time = GetTime(TimeOfX);
            int hour = Convert.ToInt32(time.Substring(0, 2));
            int minutes = Convert.ToInt32(time.Substring(3, 2));
            DateTime date = new DateTime(year, month, day,hour,minutes,0);
            return date;
        }
        internal string GetTime(string TimeOfX)
        {
            return TimeOfX.Substring(TimeOfX.Length - 5, 5);
        }
        internal PatientWith72HourTreatmentGuarantee GetPatientWith72HoursTreatmentGuarantee(int patientId)
        {
            string queryString = "SELECT * from PatientsWithCurrent72HourTreatmentGuarantee WHERE PatientId = " + patientId.ToString();
            return Run72HourSQLQuery(queryString);
        }
        internal PatientWith72HourTreatmentGuarantee Run72HourSQLQuery(string queryString)
        {
            PatientWith72HourTreatmentGuarantee patient = null;
            string connectionString = "Data Source=RKSQL03;Initial Catalog=RKSQLRPA01;Persist Security Info=True;User ID=rpasql01;Password=Sol@1427";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(queryString, sqlConnection);
            using (sqlConnection)
            {
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    patient = new PatientWith72HourTreatmentGuarantee();
                    patient.Id = Convert.ToInt32(reader["Id"].ToString());
                    patient.PatientId = Convert.ToInt32(reader["PatientId"].ToString());
                    patient.PatientName = reader["PatientName"].ToString();
                    patient.TimeOfDischarge = Convert.ToDateTime(reader["TimeOfDischarge"].ToString());
                }
                return patient;
            }
        }
        internal void RunSQLWithoutReturnResult(string queryString)
        {
            string connectionString = "Data Source=RKSQL03;Initial Catalog=RKSQLRPA01;Persist Security Info=True;User ID=rpasql01;Password=Sol@1427";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(queryString, sqlConnection);
            using (sqlConnection)
            {
                command.Connection.Open();
                command.ExecuteNonQuery();
            }
        }
        internal string ConvertDateTmeToDbFormat(DateTime date)
        {
            string day = date.Day.ToString();
            string month = date.Month.ToString();
            string year = date.Year.ToString();

            string hour = date.Hour.ToString();
            string minute = date.Minute.ToString();
            string second = date.Second.ToString();

            return year + "-" + month + "-" + day + " " + hour + ":" + minute + ":" + second;
        }

        internal string ConvertXmlToJsonString(string xmlFilePath)
        {
            string xmlString = System.IO.File.ReadAllText(xmlFilePath);
            var xmlStringBytes = System.IO.File.ReadAllBytes(xmlFilePath);

            var inputEncoding = Encoding.GetEncoding("iso-8859-1");
            var text = inputEncoding.GetString(xmlStringBytes);
            var output = Encoding.UTF8.GetBytes(text);
            var UTF8Output = Encoding.UTF8.GetString(output);


            XmlDocument notes = new XmlDocument();
            notes.LoadXml(UTF8Output);

            return JsonConvert.SerializeXmlNode(notes);
        }

        
    }
}

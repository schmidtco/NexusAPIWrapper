using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        /// <summary>
        /// Removes the square brackets from an arrayJson in the nexus result, in order to properly convert to a class object.
        /// </summary>
        /// <param name="nexusResult"></param>
        internal string RemoveArrayBracketsFromResult(NexusResult nexusResult)
        {
            string result = nexusResult.Result.ToString();
            result = result.Substring(1);
            result = result.Substring(0, result.Length - 1);

            return result;
        }
    }
}

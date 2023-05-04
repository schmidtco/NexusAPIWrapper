using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace NexusAPIWrapper
{
    public class NexusAPI
    {
        #region Properties
        
        private NexusTokenObject _tokenObject;
        private NexusHomeRessource _ressource;
        private ClientCredentials _clientCredentials;
        private NexusResult _result;


        public NexusTokenObject tokenObject
        {
            get => _tokenObject;
            set => _tokenObject = value;
        }

        public ClientCredentials clientCredentials
        {
            get => _clientCredentials;  
            set => _clientCredentials = value;
        }

        public NexusResult result
        {
            get => _result;
            set => _result = value;
        }
        #endregion Properties

        #region Constructors
        public NexusAPI(string environment)
        {
            clientCredentials = new ClientCredentials(environment);
            tokenObject = new NexusTokenObject(clientCredentials);
            result = new NexusResult();
            GetHomeRessource();
        }
        #endregion Constructors

        #region HomeRessource methods
        public void GetHomeRessource()
        {
            NexusHomeRessource nexusHomeRessource = new NexusHomeRessource(_clientCredentials.Host, _tokenObject.AccessToken);
            _ressource = nexusHomeRessource;
        }


        public string GetHomeRessourceLink(string linkName)
        {
            if (_ressource.Links is null || _ressource.Links.Count == 0)
            {
                _ressource.GetHomeRessource();
                //string urlEndpoint = _ressource.Links[linkName];
                return _ressource.Links[linkName];
            }
            else
            {
                string urlEndpoint = _ressource.Links[linkName];
                return _ressource.Links[linkName];
            }
            
        }
        #region HomeRessource -> Links
        public string GetPatientDetailsSearchLink()
        {
            return GetHomeRessourceLink("patientDetailsSearch");
        }
        #endregion HomeRessource -> Links

        public SortedDictionary<string,string> GetPatientDetailsByCPR(string CitizenCPR)
        {
            result.GetPatientDetailsByCPR(this, CitizenCPR);
            var stringObject = GetJsonObjectFromRestResponse(result,"patient");
            var dict = ConvertJsonToSortedDictionary(stringObject);
            return dict;
        }



        #endregion HomeRessource methods

        #region Specific methods
        public SortedDictionary<string, string> GetPatientDetailsLinks_ByCPR(string CitizenCPR)
        {
            var patientDetails = GetPatientDetailsByCPR(CitizenCPR);
            var links = GetNestedData(patientDetails, "_links");
            //Here we need to convert the numbered dictionary to a real key/value pair dictionary.
            return ConvertArrayDictionaryToKeyValueDictionary(links);
        }

        

        public string GetPatientPreferencesLink_ByCPR(string CitizenCPR)
        {
            var links = GetPatientDetailsLinks_ByCPR(CitizenCPR);
            return links["patientPreferences"];
        }

        public SortedDictionary <string, string> GetPatientPreferences_ByCPR(string CitizenCPR)
        {
            result.GetPatientPreferences(this,CitizenCPR);
            var stringObject = ConvertNexusResultToJsonObject(result);
            var dict = ConvertJsonToSortedDictionary(stringObject); 
            return dict;
        }
        
        public SortedDictionary<string, SortedDictionary<string, string>> GetCitizenPathways_ByCPR(string CitizenCPR)
        {
            var preferences = GetPatientPreferences_ByCPR(CitizenCPR);
            //converting the numbered dictionary to a real key/value pair dictionary
            //return ConvertNumberedDictionaryToKeyValueDictionary(preferences);
            var pathwaysStringObject = GetElementDataFromSortedDict(preferences, "CITIZEN_PATHWAY");
            var pathways = JsonConvert.DeserializeObject<dynamic>(pathwaysStringObject);
            SortedDictionary<string, SortedDictionary<string, string>> pathwaysDict = new SortedDictionary<string, SortedDictionary<string, string>>();
            SortedDictionary<string, string> pathwayInfoDict = new SortedDictionary<string, string>();

            foreach (var item in pathways)
            {
                
                    pathwayInfoDict.Clear();
                    foreach (var itemInfo in item)
                    {
                        pathwayInfoDict.Add(itemInfo.Name.ToString(), itemInfo.Value.ToString());
                    }
                    pathwaysDict.Add(pathwayInfoDict["name"], pathwayInfoDict);
            }
            return pathwaysDict;
        }

        public string GetCitizenPathwayLink_ByCPR(string CitizenCPR, string pathwayName)
        {
            var pathways = GetCitizenPathways_ByCPR(CitizenCPR);
            var pathwayElement = GetElementDataFromSortedDict(pathways, pathwayName);

            dynamic linkObject = JObject.Parse(pathwayElement);
            var selfLinkObject = linkObject["self"];
            var linkStringObject = selfLinkObject["href"];
            string linkString = linkStringObject.Value;

            return linkString;
        }

        public SortedDictionary<string, string> GetCitizenPathwayInfo_ByCPR(string CitizenCPR, string pathwayName)
        {
            //SortedDictionary<string, string> dict = new SortedDictionary<string, string>();
            result.GetCitizenPathwayInfo_ByCPR(this, CitizenCPR, pathwayName);
            var stringObject = ConvertNexusResultToJsonObject(result);
            var dict = ConvertJsonToSortedDictionary(stringObject);
            return dict;
        }


        #endregion Specific methods
        #region Shared methods

        ////////////////////
        ///SHARED METHODS///
        ////////////////////

        public SortedDictionary<string,string> ConvertStringObjectToSortedDictionary(string objectInput)
        {
            SortedDictionary<string,string> dict = new SortedDictionary<string,string>();
            bool parseToJson=false;
            try
            {
                JObject jsonObject = JObject.Parse(objectInput);

                
            }
            catch
            {
                if (!parseToJson)
                {
                    JArray arrayObject = JArray.Parse(objectInput);

                    foreach (var item in objectInput)
                    {
                        //string key = item.Key;
                        //string value = item.Value;
                        string value = string.Empty;

                        if (value == "[]")
                        {
                            //No value in key/value pair
                        }
                        else
                        {

                            //dynamic jsonValue = JObject.Parse(value);
                            //value = Convert.ToString(jsonValue["href"]);
                            //keyValuePairs.Add(key, value);
                        }

                    }

                }
            }
            return dict;
        }
        private string GetElementDataFromSortedDict(SortedDictionary<string, SortedDictionary<string, string>> dict, string elementName)
        {
            string result = string.Empty;
            foreach (var item in dict)
            {
                if (item.Key == elementName)
                {
                    result = item.Value["_links"];
                    break;
                }
                else
                {
                    result = "Element not found.";
                }
            }
            return result;
        }
        private string GetElementDataFromSortedDict(SortedDictionary<string,string> dict, string elementName)
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
        private SortedDictionary<string, string> ConvertArrayDictionaryToKeyValueDictionary(SortedDictionary<string, string> sortedDictionary)
        {
            SortedDictionary<string,string> keyValuePairs = new SortedDictionary<string,string>();
            for (int i = 0; i < sortedDictionary.Count - 1; i++)
            {
                //var pair = ReturnKeyValuePairFromJson()
            }

            foreach (var item in sortedDictionary)
            {
                string key = item.Key;
                string value = item.Value;
                //JsonSerializer jsonSerializer = new JsonSerializer();
                //var yt = jsonSerializer.Deserialize(item.Value);
                
                if (value == "[]") 
                {
                    //No value in key/value pair
                }
                else
                {

                    dynamic jsonValue = JObject.Parse(value);
                    value = Convert.ToString(jsonValue["href"]);
                    keyValuePairs.Add(key, value);
                }

            }
            return keyValuePairs;
        }
        public KeyValuePair<string, string> ReturnKeyValuePairFromJson(JObject json, int itemNumber)
        {
            
            dynamic values = (dynamic)json;

            string key = Convert.ToString(values[itemNumber].Name);
            string value = Convert.ToString(values[itemNumber].Value);

            KeyValuePair<string, string> pair = new KeyValuePair<string, string>(key,value);

            return pair;
        }
        public SortedDictionary<string, string> GetNestedData(SortedDictionary<string, string> dict)
        {
            JObject dataOutput = JObject.Parse(dict.ToString());
            return ConvertJsonToSortedDictionary(dataOutput);
        }
        public SortedDictionary<string, string> GetNestedData(SortedDictionary<string, string> dict, string dataName)
        {
            JObject dataOutput = JObject.Parse(dict[dataName]);
            return ConvertJsonToSortedDictionary(dataOutput);
        }
        public void CheckTokens()
        {

        }
        public void AddResource(SortedDictionary<string,string> dict, string linkName, string href)
        {
            if (dict.ContainsKey(linkName))
                return;
            dict.Add(linkName, href);
        }
        internal SortedDictionary<string, string> ConvertJsonToSortedDictionary(JObject json)//, string partToExtract)
        {
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
        //internal string GetObjectFromSortedDict(SortedDictionary<string, string> dict, string objectName)
        //{
        //    var result = dict[objectName];
        //    return result;
        //}
        internal JObject GetJsonObjectFromRestResponse(NexusResult output, string objectName)
        {
            dynamic jsonOutput = JObject.Parse((string)output.Result);
            dynamic objectOutput = jsonOutput[objectName];
            return objectOutput;
        }
        internal JObject ConvertNexusResultToJsonObject(NexusResult output)
            /*
             Does this one work??
             Not testet yet.
            */
        {
            dynamic jsonOutput = JToken.Parse((string)output.Result);
            
            return jsonOutput;
        }
        //internal SortedDictionary<string, string> GetAvailableLinks(string objectInput)
        //{
        //    //The result in the Nexus output will always be JSON, as this is what is returned by the API.
        //    dynamic jsonOutput = JObject.Parse((string)output.Result);
        //    dynamic links = jsonOutput["_links"];
        //    return links;
        //}



        #endregion



    }
}

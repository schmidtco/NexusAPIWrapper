using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
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
        private DataHandler _dataHandler;
        private string _primaryOrganization = "Ringsted Kommune";


        public string primaryOrganization
        {
            get => _primaryOrganization;
        }
        public DataHandler dataHandler
        {
            get => _dataHandler;
            set => _dataHandler = value;
        }

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

        public NexusHomeRessource homeRessource
        {
            get => _ressource;
        }
        #endregion Properties

        #region Constructors
        public NexusAPI(string environment)
        {
            clientCredentials = new ClientCredentials(environment);
            tokenObject = new NexusTokenObject(clientCredentials);
            result = new NexusResult();
            dataHandler = new DataHandler();
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

        /// <summary>
        /// This returns the complete collection of organizations
        /// </summary>
        /// <returns></returns>
        public OrganizationsTree_Root GetOrganizationsTree()
        {
            string organizationsTreeEndpoint = GetHomeRessourceLink("organizationsTree");
            var result = CallAPI(this, organizationsTreeEndpoint, Method.Get);

            return JsonConvert.DeserializeObject<OrganizationsTree_Root>(result.Result.ToString());
        }
        #region HomeRessource -> Links
        // specific methods for returning home ressource links
        public string GetPatientDetailsSearchLink()
        {
            return GetHomeRessourceLink("patientDetailsSearch");
        }

        public string GetProfessionalsLink()
        {
            return GetHomeRessourceLink("professionals");
        }
        #endregion HomeRessource -> Links

        //public SortedDictionary<string,string> GetPatientDetailsByCPR(string CitizenCPR)
        //{
        //    result.GetPatientDetailsByCPR(this, CitizenCPR); 
        //    //Returns object {string} - data is JSON and can be parsed to JObject
        //    //var stringObject = GetJsonObjectFromRestResponse(result,"patient"); //Now a JSON object
        //    //var stringObject1 = ConvertNexusResultToJsonObject(result, "patient");

        //    var sortDict = dataHandler.ConvertJsonToSortedDictionary((string)result.Result);
        //    var details = sortDict["patient"]; //JSON string
        //    //var dict = ConvertJsonToSortedDictionary(stringObject);
        //    var dict = dataHandler.ConvertJsonToSortedDictionary(details);
        //    return dict;
        //}
        public PatientDetails_Root GetPatientDetails(string CitizenCPR)
        {
            result.GetPatientDetails(this, CitizenCPR);
            //Returns object {string} - data is JSON and can be parsed to JObject
            //var stringObject = GetJsonObjectFromRestResponse(result,"patient"); //Now a JSON object
            //var stringObject1 = ConvertNexusResultToJsonObject(result, "patient");

            var sortDict = dataHandler.ConvertJsonToSortedDictionary((string)result.Result);
            var details = sortDict["patient"]; //JSON string
            return JsonConvert.DeserializeObject<PatientDetails_Root>(details);
        }

        public PatientDetails_Root GetPatientDetails(int id)
        {
            string patientDetailsSearchEndpoint = GetPatientDetailsSearchLink();
            string patientDetailsSearchEndpointSubstring = patientDetailsSearchEndpoint.Substring(0, patientDetailsSearchEndpoint.Length - 6);

            var result = CallAPI(this, patientDetailsSearchEndpointSubstring + id, Method.Get);
            return JsonConvert.DeserializeObject<PatientDetails_Root> (result.Result.ToString());
        }



        #endregion HomeRessource methods

        #region Specific methods
        //public SortedDictionary<string, string> GetPatientDetailsLinks_ByCPR(string CitizenCPR)
        //{
        //    var patientDetails = GetPatientDetailsByCPR(CitizenCPR);
        //    var links = dataHandler.ConvertJsonToSortedDictionary(patientDetails["_links"]);
        //    //var links = GetNestedData(patientDetails, "_links");
        //    //Here we need to convert the numbered dictionary to a real key/value pair dictionary.
        //    return links;
        //    //return ConvertArrayDictionaryToKeyValueDictionary(links);
        //}

        public PatientDetails_Links GetPatientDetailsLinks(string CitizenCPR)
        {
            var patientDetails = GetPatientDetails(CitizenCPR);
            var links = patientDetails.Links;
            //var links = GetNestedData(patientDetails, "_links");
            //Here we need to convert the numbered dictionary to a real key/value pair dictionary.
            return links;
            //return ConvertArrayDictionaryToKeyValueDictionary(links);
        }
        public PatientDetails_Links GetPatientDetailsLinks(int id)
        {
            var patientDetails = GetPatientDetails(id);
            var links = patientDetails.Links;
            //var links = GetNestedData(patientDetails, "_links");
            //Here we need to convert the numbered dictionary to a real key/value pair dictionary.
            return links;
            //return ConvertArrayDictionaryToKeyValueDictionary(links);
        }

        internal List<ProfessionalConfigurationOrganizations_Root> GetProfessionalPrimaryOrganizations(int professionalId)
        {
            var professionalConfiguration = GetProfessionalConfiguration(professionalId);
            string organizationsEndpoint = professionalConfiguration.Links.Organizations.Href;

            var result = CallAPI(this, organizationsEndpoint, Method.Get);

            var arrayOrganizations = JsonConvert.DeserializeObject(result.Result.ToString());
            var primaryOrganizations = JsonConvert.DeserializeObject<List<ProfessionalConfigurationOrganizations_Root>>(arrayOrganizations.ToString());

            return primaryOrganizations;
        }
        internal ProfessionalConfigurationOrganizations_Root GetProfessionalPrimaryOrganization(int professionalId)
        {
            var professionalPrimaryOrganizations = GetProfessionalPrimaryOrganizations(professionalId);
            ProfessionalConfigurationOrganizations_Root primaryOrganization = null;

            foreach (var organization in professionalPrimaryOrganizations)
            {
                if (organization.Name == _primaryOrganization)
                {
                    primaryOrganization = organization;
                    break;
                }
            }

            return primaryOrganization;
        }

        internal ProfessionalConfigurationOrganizations_Root GetProfessionalPrimaryOrganization(int professionalId, string organizationName)
        {
            var professionalPrimaryOrganizations = GetProfessionalPrimaryOrganizations(professionalId);
            ProfessionalConfigurationOrganizations_Root primaryOrganization = null;

            foreach (var organization in professionalPrimaryOrganizations)
            {
                if (organization.Name == organizationName)
                {
                    primaryOrganization = organization;
                    break;
                }
            }

            return primaryOrganization;
        }

        /// <summary>
        /// Returning the professional configuration object class
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ProfessionalConfiguration_Root GetProfessionalConfiguration(int id)
        {
            Professional_Root professionalObject = GetProfessional(id);

            // configuration link
            string configurationLink = professionalObject.Links.Configuration.Href;

            var professionalConfiguration = CallAPI(this, configurationLink, Method.Get);

            // Convert to class object
            return JsonConvert.DeserializeObject<ProfessionalConfiguration_Root>(professionalConfiguration.Result.ToString());
        }
        //public string GetPatientPreferencesLink_ByCPR(string CitizenCPR)
        //{
        //    var links = GetPatientDetailsLinks_ByCPR(CitizenCPR);

        //    var link = dataHandler.ConvertJsonToSortedDictionary(links["patientPreferences"]);
        //    return dataHandler.GetHref(link, false);
        //    //return link["href"];
        //}

        public string GetPatientPreferencesLink(string CitizenCPR)
        {
            var links = GetPatientDetailsLinks(CitizenCPR);

            return links.PatientPreferences.Href;
        }

        public string GetPatientPreferencesLink(int id)
        {
            var links = GetPatientDetailsLinks(id);

            return links.PatientPreferences.Href;
        }

        //public SortedDictionary <string, string> GetPatientPreferences_ByCPR(string CitizenCPR)
        //{
        //    result.GetPatientPreferences(this,CitizenCPR);
        //    return dataHandler.ConvertJsonToSortedDictionary((string)result.Result);
        //}
        public PatientPreferences_Root GetPatientPreferences(string CitizenCPR)
        {
            result.GetPatientPreferences(this, CitizenCPR);
            return JsonConvert.DeserializeObject<PatientPreferences_Root>((string)result.Result); 
        }

        public PatientPreferences_Root GetPatientPreferences(int id)
        {
            result.GetPatientPreferences(this, id);
            return JsonConvert.DeserializeObject<PatientPreferences_Root>((string)result.Result);
        }

        //public SortedDictionary<string, SortedDictionary<string, string>> GetCitizenPathways_ByCPR(string CitizenCPR)
        //{
        //    var preferences = GetPatientPreferences_ByCPR(CitizenCPR);
        //    var pathwaysStringObject = dataHandler.GetElementDataFromSortedDict(preferences, "CITIZEN_PATHWAY");
        //    SortedDictionary<string, SortedDictionary<string, string>> pathwaysDict = new SortedDictionary<string, SortedDictionary<string, string>>();

        //    var pathwayDictArray = dataHandler.ArrayJsonStringToSortedDictionary(pathwaysStringObject);
        //    foreach (var item in pathwayDictArray)
        //    {
        //        var strValue = item.Value;
        //        var valueDict = dataHandler.ConvertJsonToSortedDictionary((string)strValue);
        //        pathwaysDict.Add(item.Key, valueDict);
        //    }
        //    return pathwaysDict;
        //}

        public List<PatientPreferences_CITIZENPATHWAY> GetCitizenPathways(string CitizenCPR)
        {
            return GetPatientPreferences(CitizenCPR).CITIZENPATHWAY;
        }

        public List<PatientPreferences_CITIZENPATHWAY> GetCitizenPathways(int id)
        {
            return GetPatientPreferences(id).CITIZENPATHWAY;

        }

        //public string GetCitizenPathwayLink_ByCPR(string CitizenCPR, string pathwayName)
        //{
        //    var pathways = GetCitizenPathways_ByCPR(CitizenCPR);
        //    var pathwayElement = GetElementDataFromSortedDict(pathways, pathwayName);

        //    dynamic linkObject = JObject.Parse(pathwayElement);
        //    return dataHandler.GetHref(linkObject,true);
        //}

        public string GetCitizenPathwayLink(string CitizenCPR, string pathwayName)
        {
            var pathways = GetCitizenPathways(CitizenCPR);
            return GetElementFromList(pathways, pathwayName).Links.Self.Href;
        }

        public string GetCitizenPathwayLink(int id, string pathwayName)
        {
            var pathways = GetCitizenPathways(id);
            return GetElementFromList(pathways, pathwayName).Links.Self.Href;
        }

        public PatientPreferences_CITIZENPATHWAY GetElementFromList(List<PatientPreferences_CITIZENPATHWAY> list, string elementName)
        {
            PatientPreferences_CITIZENPATHWAY result = new PatientPreferences_CITIZENPATHWAY();
            foreach (var item in list)
            {
                if (item.Name == elementName)
                {
                    result = item;
                    break;
                }
            }
            return result;
        }
        
        public SortedDictionary<string, string> GetCitizenPathwayInfo(string CitizenCPR, string pathwayName)
        {
            //SortedDictionary<string, string> dict = new SortedDictionary<string, string>();
            result.GetCitizenPathwayInfo(this, CitizenCPR, pathwayName);
            var stringObject = dataHandler.ConvertNexusResultToJsonObject(result);
            var dict = dataHandler.ConvertJsonToSortedDictionary(stringObject);
            return dict;
        }
        public SortedDictionary<string, string> GetCitizenPathwayInfo(int id, string pathwayName)
        {
            //SortedDictionary<string, string> dict = new SortedDictionary<string, string>();
            result.GetCitizenPathwayInfo(this, id, pathwayName);
            var stringObject = dataHandler.ConvertNexusResultToJsonObject(result);
            var dict = dataHandler.ConvertJsonToSortedDictionary(stringObject);
            return dict;
        }

        public string GetCitizenPathwayReferencesLink(string CitizenCPR, string pathwayName)
        {
            var pathwayInfo = GetCitizenPathwayInfo(CitizenCPR, pathwayName);

            var links = pathwayInfo["_links"];
            var linksDict = dataHandler.ConvertJsonToSortedDictionary(links);
            var pathwayReferences = linksDict["pathwayReferences"];
            var pathwayReferencesDict = dataHandler.ConvertJsonToSortedDictionary(pathwayReferences);
            return dataHandler.GetHref(pathwayReferencesDict,false);
        }
        public string GetCitizenPathwayReferencesLink(int id, string pathwayName)
        {
            var pathwayInfo = GetCitizenPathwayInfo(id, pathwayName);

            var links = pathwayInfo["_links"];
            var linksDict = dataHandler.ConvertJsonToSortedDictionary(links);
            var pathwayReferences = linksDict["pathwayReferences"];
            var pathwayReferencesDict = dataHandler.ConvertJsonToSortedDictionary(pathwayReferences);
            return dataHandler.GetHref(pathwayReferencesDict, false);
        }

        public SortedDictionary<string,string> GetCitizenPathwayReferences(string CitizenCPR, string pathwayName)
        {
            result.GetCitizenPathwayReferences(this, CitizenCPR, pathwayName);
            var pathwayReferences = dataHandler.ConvertNexusResultToJsonObject(result);
            return dataHandler.ConvertJsonToSortedDictionary(pathwayReferences);
        }

        //public SortedDictionary<string, string> GetCitizenPathwayReferences(int id, string pathwayName)
        //{
        //    result.GetCitizenPathwayReferences(this, id, pathwayName);
        //    var pathwayReferences = dataHandler.ConvertNexusResultToJsonObject(result);
        //    return dataHandler.ConvertJsonToSortedDictionary(pathwayReferences);
        //}
        public List<PatientPathwayReferences_Child> GetPathwayChildrenElements(List<PatientPathwayReferences_Child> list, PatientPathwayReferences_Child childElement)
        {
            foreach (var item in childElement.Children)
            {
                switch (item.Type)
                {
                    case "documentReference":
                        // we add it to the list of elements to return
                        list.Add(item);
                        break;
                    case "patientPathwayReference":
                        GetPathwayChildrenElements(list, childElement);
                        break;
                }
            }
            return list;
        }
        public PatientPathwayReferences_Root GetCitizenPathwayReferences(int id, string pathwayName)
        {
            result.GetCitizenPathwayReferences(this, id, pathwayName);
            string stringToConvert = result.Result.ToString();
            string trimmedStringToConvert = stringToConvert.Trim();
            string jsonString = stringToConvert.Substring(1, trimmedStringToConvert.Length - 2);
            return JsonConvert.DeserializeObject<PatientPathwayReferences_Root>(jsonString);
        }

        public SortedDictionary<string, string> GetCitizenPathwayDocuments(string CitizenCPR, string pathwayName)
        {
            // As it could be misleading to have to call the pathway references documents, this method has been made to make more sense.
            // Here you just get the pathway documents.
            return GetCitizenPathwayReferencesDocuments(CitizenCPR, pathwayName);
        }
        public SortedDictionary<string, string> GetCitizenPathwayReferencesDocuments(string CitizenCPR, string pathwayName)
        {
            result.GetCitizenPathwayReferences(this, CitizenCPR, pathwayName);
            var pathwayReferences = dataHandler.ConvertNexusResultToJsonObject(result);
            // In pathway references the children will be either child pathways (type :	patientPathwayReference)
            // or documents (type : documentReference).
            // In order to get the documents, we loop through everything, locating elements with the type we want
            // and put them in a dictionary

            SortedDictionary<string, string> Documents = new SortedDictionary<string, string>();
            var children = pathwayReferences["children"];
            // convert jarray to dictionary || var childrenDict = dataHandler.
            //var childrenDict = dataHandler.ArrayJsonStringToSortedDictionary(children);

            foreach (var item in children)
            {
                
                var itemJObject = item as JObject;
                var itemDict = dataHandler.JsonToSortedDictionary(itemJObject);
                var elementType = itemDict["type"];

                switch (elementType)
                {
                    case "patientPathwayReference":
                        //We don't add to dictionary as it is not a direct document reference
                        break;
                    case "documentReference":
                        //We add to the Documents dictionary
                        dataHandler.AddResource(Documents, item["name"].ToString(), item["id"].ToString());
                        break;
                    default:
                        break;
                }

            }

            return Documents; //dataHandler.ConvertJsonToSortedDictionary(pathwayReferences);
        }

        public string GetCitizenPathwayReferencesSelfLink(string CitizenCPR, string pathwayName)
        {
            var pathwayReferences = GetCitizenPathwayReferences(CitizenCPR,pathwayName);
            var links = pathwayReferences["_links"];
            var linksDict = dataHandler.ConvertJsonToSortedDictionary(links);
            return dataHandler.GetHref(linksDict,true);
        }

        public SortedDictionary<string,string> GetCitizenPathwayReferencesSelf(string CitizenCPR, string pathwayName)
        {
            result.GetCitizenPathwayReferencesSelf(this, CitizenCPR, pathwayName);
            var objects = dataHandler.ConvertNexusResultToJsonObject(result);
            
            return dataHandler.JsonToSortedDictionary(objects);
        }

        public SortedDictionary<string,string> GetCitizenPathwayReferencesSelf_Links(string CitizenCPR, string pathwayName)
        {
            var CitizenPathwayReferencesSelf = GetCitizenPathwayReferencesSelf(CitizenCPR,pathwayName);
            var links = CitizenPathwayReferencesSelf["_links"];
            return dataHandler.JsonStringToSortedDictionary(links);
        }

        public SortedDictionary<string,string> GetCitizenPathwayChildren(string CitizenCPR, string pathwayName)
        {
            var pathwayReferences = GetCitizenPathwayReferences(CitizenCPR, pathwayName);
            return dataHandler.ArrayJsonStringToSortedDictionary(pathwayReferences["children"]);
        }

        public SortedDictionary<string,string> GetCitizenPathwayChild(string CitizenCPR,string pathwayName, string pathwayChildName)
        {
            var pathwayChildren = GetCitizenPathwayChildren(CitizenCPR, pathwayName);
            string strResult = string.Empty;
            foreach (var item in pathwayChildren)
            {
                if (item.Key == pathwayChildName)
                {
                    strResult = item.Value;
                }
            }

            SortedDictionary<string, string> childDict = new SortedDictionary<string, string>();
            if (strResult == string.Empty)
            {

            }
            else
            {
                childDict = dataHandler.JsonStringToSortedDictionary(strResult);
            }
            
            return childDict;
        }
        public string GetCitizenPathwayChildSelfLink(string CitizenCPR, string pathwayName, string pathwayChildName)
        {
            var pathwayChild = GetCitizenPathwayChild(CitizenCPR, pathwayName, pathwayChildName);
            var pathwayChildLinks = pathwayChild["_links"];
            var links = dataHandler.JsonStringToSortedDictionary(pathwayChildLinks);

            return dataHandler.GetHref(links, true);
        }
        public SortedDictionary<string, string> GetCitizenPathwayChildSelf(string CitizenCPR, string pathwayName, string pathwayChildName)
        {
            result.GetCitizenPathwayChildSelf(this, CitizenCPR, pathwayName, pathwayChildName);
            var objects = dataHandler.ConvertNexusResultToJsonObject(result);

            return dataHandler.JsonToSortedDictionary(objects);
        }

        public SortedDictionary<string, string> GetCitizenPathwayChildSelf_Links(string CitizenCPR, string pathwayName, string pathwayChildName)
        {
            var childSelf = GetCitizenPathwayChildSelf(CitizenCPR, pathwayName, pathwayChildName);
            var links = childSelf["_links"];

            return dataHandler.JsonStringToSortedDictionary(links);
        }

        public SortedDictionary<string, string> GetCitizenPathwayChildDocuments(string CitizenCPR, string pathwayName, string pathwaysChildName)
        {
            var childDict = GetCitizenPathwayChild(CitizenCPR,pathwayName, pathwaysChildName);
            SortedDictionary<string, string> documents = new SortedDictionary<string, string>();



            if (childDict.Count == 0)
            {

            }
            else
            {
                var strDocuments = childDict["children"];
                documents = dataHandler.ArrayJsonStringToSortedDictionary(strDocuments);
            }

            return documents;
        }

        //public void CreateDocumentObject(string CitizenCPR) //This method uploads a document directly under the citizen/patient
        //{
        //    CreateDocumentObject(CitizenCPR, null);
        //}
        //public void CreateDocumentObject(string CitizenCPR, string pathwayName)//This method uploads a document directly under a specific pathway
        //{
        //    CreateDocumentObject(CitizenCPR,pathwayName ,null);
        //}

        //public CreateDocumentObject CreateDocumentObject(string CitizenCPR, string pathwayName, string pathwayChildName)//This method returns the document object in order to upload a document directly under a specific pathway/pathway child
        //{
        //    if (pathwayChildName == null)
        //    {
        //        // document NOT to be uploaded on a child pathway
        //        if (pathwayName == null)
        //        {
        //            // document NOT to be uploaded on a pathway
        //            // document to be uploaded directly on the patient/citizen

        //            var patientDetailsLinks = GetPatientDetailsLinks_ByCPR(CitizenCPR);
        //            string documentPrototypeLink = dataHandler.GetDocumentPrototypeLink(patientDetailsLinks);

        //            var returnResult = CallAPI(this, documentPrototypeLink, Method.Get);
        //            return JsonConvert.DeserializeObject<CreateDocumentObject>(returnResult.Result.ToString());
        //        }
        //        else
        //        {
        //            // document to be uploaded on a pathway

        //            var patientPathwayReferencesLinks = GetPatientPreferences_ByCPR(CitizenCPR);
        //            string documentPrototypeLink = dataHandler.GetDocumentPrototypeLink(patientPathwayReferencesLinks);

        //            var returnResult = CallAPI(this, documentPrototypeLink, Method.Get);
        //            return JsonConvert.DeserializeObject<CreateDocumentObject>(returnResult.Result.ToString());
        //        }
        //    }
        //    else
        //    {
        //        // document to be uploaded on a child pathway
        //        var CitizenPathwayChildSelf_Links = GetCitizenPathwayChildSelf_Links(CitizenCPR, pathwayName, pathwayChildName);

        //        string documentPrototypeLink = dataHandler.GetDocumentPrototypeLink(CitizenPathwayChildSelf_Links);

        //        var returnResult = CallAPI(this, documentPrototypeLink, Method.Get);
        //        return JsonConvert.DeserializeObject<CreateDocumentObject>(returnResult.Result.ToString());
        //    }


        //}

        public SortedDictionary<string, string> GetOrganizations()
        {
            result.GetOrganizations(this);
            return dataHandler.ArrayJsonStringToSortedDictionary(result.Result.ToString());
        }

        public SortedDictionary<string, string> GetSpecificOrganization(string organizationName)
        {
            result.GetOrganizations(this);
            var organizations = dataHandler.ArrayJsonStringToSortedDictionary(result.Result.ToString());
            var organization = organizations[organizationName];
            var organizationData = dataHandler.JsonStringToSortedDictionary(organization);
            var organizationDataLinks = organizationData["_links"];
            var organizationsDataLinksHref = dataHandler.GetHref(JObject.Parse(organizationDataLinks), true);

            var org = result.CallAPI(this,organizationsDataLinksHref, Method.Get);
            return dataHandler.JsonStringToSortedDictionary(org.Result.ToString());
        }

        public SortedDictionary<string, string> GetOrganizationLinks(string organizationName)
        {
            var organization = GetSpecificOrganization(organizationName);
            var links = organization["_links"];

            return dataHandler.JsonStringToSortedDictionary(links);
        }

        public string GetSpecificOrganizationLink(string organizationName, string linkName)
        {
            var organizationLinks = GetOrganizationLinks(organizationName);
            var specificLink = organizationLinks[linkName];

            return dataHandler.GetHref(JObject.Parse(specificLink),false);
        }

        public SortedDictionary<string, string> GetOrganizationProfessionals(string organizationName)
        {
            var link = GetSpecificOrganizationLink(organizationName, "professionals");
            var returnResult = result.CallAPI(this,link,Method.Get);
            return dataHandler.ArrayJsonStringToSortedDictionary(returnResult.Result.ToString(),"fullName");
        }

        public SortedDictionary<string, string> GetOrganizationPatients(string organizationName)
        {
            var link = GetSpecificOrganizationLink(organizationName, "patients");
            var returnResult = result.CallAPI(this, link, Method.Get);
            return dataHandler.ArrayJsonStringToSortedDictionary(returnResult.Result.ToString(),"fullName");
        }

        public SortedDictionary<string, string> GetAllPatients()
        {
            var link = GetHomeRessourceLink("patients");
            string queryText = "?pageSize=10000000&query=";

            string endpointURL = link + queryText;
            var returnResult = result.CallAPI(this, endpointURL, Method.Get);
            return dataHandler.JsonStringToSortedDictionary(returnResult.Result.ToString());
        }

        public List<Int32> GetAllPatientIds()
        {
            var allPatients = GetAllPatients();
            var pages = allPatients["pages"];
            var pagesDict = dataHandler.ArrayJsonStringToSortedDictionary(pages, "_links");
            var links = pagesDict["_links"];
            var linksDict = dataHandler.JsonStringToSortedDictionary(links);
            var patientData = linksDict["patientData"];
            // patientDataHrefLink contains all the ids of the patients in the get all patients search
            var patientDataHrefLink = dataHandler.GetHref(JObject.Parse(patientData), false);
            var list = patientDataHrefLink.Split('=');
            var idList = list[1].Split(',');
            
            List<Int32> patientIdList = new List<Int32>();
            foreach (var item in idList)
            {
                patientIdList.Add(Convert.ToInt32(item));
            }

            return patientIdList;
        }
        /// <summary>
        /// Passing the queryString to the function will return a Dictionary of professionals by their initials
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public SortedDictionary<string, string> GetProfessionals(string queryString)
        {
            string professionalsLink = GetHomeRessourceLink("professionals");
            var professionals = CallAPI(this, professionalsLink + "?query=" + queryString, Method.Get);

            return dataHandler.ArrayJsonStringToSortedDictionary(professionals.Result.ToString(), "initials");
        }

        //public SortedDictionary<string, string> GetProfessional(int id)
        //{
        //    string professionalsLink = GetHomeRessourceLink("professionals");
        //    var professional = CallAPI(this, professionalsLink + "/" + id, Method.Get);

        //    return dataHandler.JsonStringToSortedDictionary(professional.Result.ToString());
        //}

        public Professional_Root GetProfessional(int id)
        {
            string professionalsLink = GetHomeRessourceLink("professionals");
            var professional = CallAPI(this, professionalsLink + "/" + id, Method.Get);

            Professional_Root professionalObject = new Professional_Root();
            professionalObject = JsonConvert.DeserializeObject<Professional_Root>(professional.Result.ToString());

            return professionalObject;
        }


        /// <summary>
        /// When using this method you need to update at least originalFilename and name before calling the method.
        /// </summary>
        /// <param name="documentObject"></param>
        /// <returns>DocumentObject with Id</returns>
        //public UploadedDocumentObject UploadDocumentObject(CreateDocumentObject documentObject)
        //{
        //    //The endpoint to where we create the document object in Nexus
        //    var createLink = documentObject.Links.Create.Href;
        //    var documentObjectJsonString = JsonConvert.SerializeObject(documentObject);

        //    NexusResult nexusResult = new NexusResult();
        //    var webrequest = nexusResult.StandardWebRequest(this, createLink, Method.Post);
        //    webrequest.request.AddJsonBody(documentObjectJsonString);
        //    webrequest.Execute();

        //    // This returns the same Document object BUT with an Id, which we can use to upload the document file.
        //    return JsonConvert.DeserializeObject<UploadedDocumentObject>(webrequest.response.Content);

        //}

        //public void UploadFileToDocumentObject(UploadedDocumentObject documentObject, string fullFilePath)
        //{
        //    //string uploadEndpointURL = documentObject.Links.Upload;
        //}






        #endregion Specific methods
        #region Shared methods

        ////////////////////
        ///SHARED METHODS///
        ////////////////////

        public NexusResult CallAPI(NexusAPI api, string endpointURL,Method callMethod)
        {
            NexusResult nexusResult = new NexusResult();
            return nexusResult.CallAPI(this, endpointURL, callMethod);
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
        //private string GetElementDataFromSortedDict(SortedDictionary<string,string> dict, string elementName)
        //{
        //    string result = string.Empty;
        //    foreach (var item in dict)
        //    {
        //        if (item.Key == elementName)
        //        {
        //            result = item.Value;
        //            break;
        //        }
        //        else
        //        {
        //            result = "Element not found.";
        //        }
        //    }
        //    return result;
        //}
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
        //public SortedDictionary<string, string> GetNestedData(SortedDictionary<string, string> dict)
        //{
        //    JObject dataOutput = JObject.Parse(dict.ToString());
        //    return dataHandler.ConvertJsonToSortedDictionary(dataOutput);
        //}
            public SortedDictionary<string, string> GetNestedData(SortedDictionary<string, string> dict, string dataName)
            {
                JObject dataOutput = JObject.Parse(dict[dataName]);
                return dataHandler.ConvertJsonToSortedDictionary(dataOutput);
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
        //internal SortedDictionary<string, string> ConvertJsonToSortedDictionary(JObject json)//, string partToExtract)
        //{
        //    //We only convert to sorted dictionary because we then don't need to do .ToString() on every property.
        //    SortedDictionary<string, string> dict = new SortedDictionary<string, string>();

        //    dynamic values = (dynamic)json;

        //    foreach (var item in values)
        //    {
        //        string ressourceName = Convert.ToString(item.Name);
        //        string href = Convert.ToString(item.Value);
        //        AddResource(dict, ressourceName, href);
        //    }
        //    return dict;
        //}
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


        //internal JObject ConvertNexusResultToJsonObject(NexusResult output)
        //    /*
        //     Does this one work??
        //     Not testet yet.
        //    */
        //{
        //    dynamic jsonOutput = JObject.Parse((string)output.Result);
        //    JObject result = jsonOutput;

        //    return result;
        //}
        //internal JObject ConvertNexusResultToJsonObject(NexusResult output, string objectName)
        ///*
        // Does this one work??
        // Not testet yet.
        //*/
        //{
        //    dynamic jsonOutput = JObject.Parse((string)output.Result);
        //    dynamic objectOutput = jsonOutput[objectName];
        //    JObject result = objectOutput;

        //    return result;
        //}
        //internal SortedDictionary<string, string> GetAvailableLinks(string objectInput)
        //{
        //    //The result in the Nexus output will always be JSON, as this is what is returned by the API.
        //    dynamic jsonOutput = JObject.Parse((str$ing)output.Result);
        //    dynamic links = jsonOutput["_links"];
        //    return links;
        //}

        public int GetOrganizationId(string organizationName)
        {
            return ReturnOrg(GetOrganizationsTree().Children, organizationName).Id;    
        }
        public int ReturnOrgId(List<OrganizationsTree_Child> organizationsTreeChild, string org)
        {
            OrganizationsTree_Child childOrg = new OrganizationsTree_Child();
            childOrg = ReturnOrg(organizationsTreeChild, org);

            return childOrg.Id;
        }
        public OrganizationsTree_Child ReturnOrg(List<OrganizationsTree_Child> organizationsTreeChild, string org)
        {
            OrganizationsTree_Child childOrg = new OrganizationsTree_Child();
            foreach (var child in organizationsTreeChild)
            {
                if (child.Name == org)
                {
                    childOrg = child;
                    break;
                }
                else
                {
                    childOrg = ReturnOrg(child.Children, org);
                    if (childOrg.Name != null)
                    {
                        break;
                    }
                }
            }

            return childOrg;
        }

        /// <summary>
        /// Returning a requestbody string containing eg. organization ids to be added or removed on a professional.
        /// </summary>
        /// <param name="added">Comma separated list of eg. organization Ids to be added</param>
        /// <param name="removed">Comma separated list of eg. organization Ids to be removed</param>
        /// <returns></returns>
        public string GetAddedRemovedRequestBody(string added = null, string removed = null)
        {
            // Requestbody format: {"added":[34,3],"removed":[]}
            if (added != null)
            {
                added = $"\"added\":[" + added + "]";
            }
            else
            {
                added = $"\"added\":[]";
            }

            if (removed != null)
            {
                removed = $"\"removed\":[" + removed + "]";
            }
            else
            {
                removed = $"\"removed\":[]";
            }

            return $"{{" + added + "," + removed + "}";
        }
        #endregion Shared methods



    }
}

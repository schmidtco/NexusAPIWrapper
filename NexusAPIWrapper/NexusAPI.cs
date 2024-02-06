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
using NexusAPIWrapper.HomeRessource.Preferences.ACTIVITYLIST.ACTIVITYLIST_Content.Content.Pages.Links.Content._Root.Links.ReferencedObject._Root.Links.TransformedBody;
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

        /// <summary>
        /// This link should be used with /id of the patient pathway to be worked with
        /// </summary>
        /// <returns></returns>
        public string GetPatientPathwaysLink()
        {
            return GetHomeRessourceLink("patientPathways");
        }

        public string GetPreferencesLink()
        {
            return GetHomeRessourceLink("preferences");
        }

        #endregion HomeRessource -> Links


        public PatientDetailsSearch_Patient GetPatientDetails(string citizenCPR)
        {
            result.GetPatientDetails(this, citizenCPR);
            PatientDetailsSearch_Root patientDetailsSearch = JsonConvert.DeserializeObject<PatientDetailsSearch_Root>(result.Result.ToString());

            return patientDetailsSearch.Patient;
        }

        public PatientDetailsSearch_Patient GetPatientDetails(int id)
        {
            string patientDetailsSearchEndpoint = GetPatientDetailsSearchLink();
            string patientDetailsSearchEndpointSubstring = patientDetailsSearchEndpoint.Substring(0, patientDetailsSearchEndpoint.Length - 6);

            var result = CallAPI(this, patientDetailsSearchEndpointSubstring + id, Method.Get);
            return JsonConvert.DeserializeObject<PatientDetailsSearch_Patient> (result.Result.ToString());
        }
        public string GetTransformedBodyHTML(ReferencedObject_Base_Root baseObject)
        {
            string transformedBodyLink = GetTransformedBodyLink(baseObject);
            var webResult = GetTransformedBodyOfMedcomMessage(this, transformedBodyLink);
            return webResult.Result.ToString();
        }
        public string GetTransformedBodyLink(ReferencedObject_Base_Root baseObject)
        {
            return baseObject.Links.TransformedBody.Href;
        }
        public ReferencedObject_Base_Root GetActivityListContentBaseObject(ACTIVITYLIST_Pages_Content_Root pageContent)
        {
            string referencedObjectLink = pageContent.Links.ReferencedObject.Href;
            var webResultRefObject = CallAPI(this, referencedObjectLink, Method.Get);
            return JsonConvert.DeserializeObject<ReferencedObject_Base_Root>(webResultRefObject.Result.ToString());
        }
        public List<ACTIVITYLIST_Pages_Content_Root> GetPreferencesActivityListSelfObjectContent(string listName, int startDateDay, int startDateMonth, int startDateYear, int endDateDay, int endDateMonth, int endDateYear)
        {
            var preferencesActivityListSelfObject = GetPreferencesActivityListSelfObject(listName);

            string contentLink = preferencesActivityListSelfObject.Links.Content.Href;
            var fromDate = ConvertDateToUrlParameter(startDateDay, startDateMonth, startDateYear, true);
            var ToDate = ConvertDateToUrlParameter(endDateDay, endDateMonth, endDateYear, false);
            string endpointString = contentLink + "&from=" + fromDate + "&to=" + ToDate + "&pageSize=50"; // Page size can't be eg. 200. The URI will return "too big URI" and will not have any data


            var webResultContent = CallAPI(this, endpointString, Method.Get);
            ACTIVITYLIST_Content_Root contentObject = JsonConvert.DeserializeObject<ACTIVITYLIST_Content_Root>(webResultContent.Result.ToString());
            var pages = contentObject.Pages;

            List<ACTIVITYLIST_Pages_Content_Root> pagesContent = new List<ACTIVITYLIST_Pages_Content_Root>();
            
            foreach (var page in pages)
            {
                string endpointLink = page.Links.Content.Href;
                var webResultPage = CallAPI(this, endpointLink, Method.Get);
                List<ACTIVITYLIST_Pages_Content_Root> pageContent = JsonConvert.DeserializeObject<List<ACTIVITYLIST_Pages_Content_Root>>(webResultPage.Result.ToString());

                foreach (var item in pageContent)
                {
                    pagesContent.Add(item);
                }
            }

            return pagesContent;
        }
        public ACTIVITYLIST_Root GetPreferencesActivityListSelfObject(string listName)
        {
            var list = GetPreferencesActivityList(listName);
            string listSelfLink = list.Links.Self.Href;

            var webResult = CallAPI(this, listSelfLink, Method.Get);

            return JsonConvert.DeserializeObject<ACTIVITYLIST_Root>(webResult.Result.ToString());
        }
        public Preferences_ACTIVITYLIST GetPreferencesActivityList(string listName)
        {
            var preferencesActivityLists = GetPreferencesActivityLists();
            Preferences_ACTIVITYLIST chosenList = null;
            foreach (var list in preferencesActivityLists)
            {
                if (list.Name.ToLower() == listName.ToLower())
                {
                    chosenList = list;
                    break;
                }
            }
            return chosenList;
        }
        public List<Preferences_ACTIVITYLIST> GetPreferencesActivityLists()
        {
            var preferences = GetPreferences();
            return preferences.ACTIVITYLIST;
        }
        public Preferences_Root GetPreferences()
        {
            string referencesLink = GetPreferencesLink();
            var result = CallAPI(this, referencesLink, Method.Get);
            return JsonConvert.DeserializeObject<Preferences_Root> (result.Result.ToString());
        }

        #endregion HomeRessource methods

        #region Specific methods

        private string ReturnCorrectIntString(int input)
        {
            string inputStr = input.ToString();
            if (inputStr.Length == 1)
            {
                return "0" + inputStr;
            }
            else
            {
                return inputStr;
            }
        }
        public string ConvertDateToUrlParameter(int day, int month, int year, bool startDate)
        {
            string urlAdditionStartDate = "T00:00:00.000Z";
            string urlAdditionEndDate = "T23:59:59.999Z";

            if (startDate)
            {
                return ReturnCorrectIntString(year) + "-" + ReturnCorrectIntString(month) + "-" + ReturnCorrectIntString(day) + urlAdditionStartDate;
            }
            else
            {
                return ReturnCorrectIntString(year) + "-" + ReturnCorrectIntString(month) + "-" + ReturnCorrectIntString(day) + urlAdditionEndDate;
            }
        }
        public List<Preferences_CITIZENLIST> GetPreferencesCitizenLists()
        {
            var preferences = GetPreferences();
            return preferences.CITIZENLIST;
        }

        public Preferences_CITIZENLIST GetPreferencesCitizenList(string listName)
        {
            var lists = GetPreferencesCitizenLists();            
            Preferences_CITIZENLIST list = new Preferences_CITIZENLIST();
            foreach (var item in lists)
            {
                if (item.Name.ToLower() == listName.ToLower())
                {
                    list = item;
                    break;
                }
            }
            return list;
        }

        public string GetPreferencesCitizenListSelfLink(string listName)
        {
            Preferences_CITIZENLIST list = GetPreferencesCitizenList(listName);
            if (list.Links != null)
            {
                return list.Links.Self.Href;
            }
            throw new Exception("Listen " + listName + " findes ikke. Kør metoden \"GetPreferencesCitizenLists\" for at få alle tilgængelige lister.");
            
        }

        public CITIZEN_LIST_Root GetPreferencesCitizenListSelf(string listName)
        {
            string selfLink = GetPreferencesCitizenListSelfLink(listName);
            if (selfLink != null)
            {
                var result = CallAPI(this, selfLink, Method.Get);

                return JsonConvert.DeserializeObject<CITIZEN_LIST_Root>(result.Result.ToString());
            }
            throw new Exception("SelfLink på listen " + listName + " er null.");
            
        }

        public string GetPreferencesCitizenListSelfContentLink(string listName)
        {
            CITIZEN_LIST_Root CitizenListSelf = GetPreferencesCitizenListSelf(listName);
            if (CitizenListSelf != null)
            {
                return CitizenListSelf.Links.Content.Href;
            }
            throw new Exception("CitizenListSelf-objektet er null, og kan derfor ikke returnere link til Content.");
        }

        public Content_Root GetPreferencesCitizenListSelfContent(string listName)
        {
            /*
             * Content contains x number of pages that has 2 links
             * link 1: PatientData - calling this endpoint returns a list of the patients and their data
             * link 2: PatientGrantInformation
             */
            string contentLink = GetPreferencesCitizenListSelfContentLink(listName);
            var result = CallAPI(this, contentLink, Method.Get);
            return JsonConvert.DeserializeObject<Content_Root>(result.Result.ToString());
        }

        public PatientDetailsSearch_Links GetPatientDetailsLinks(string citizenCPR)
        {
            var patientDetails = GetPatientDetails(citizenCPR);
            var links = patientDetails.Links;
            //var links = GetNestedData(patientDetails, "_links");
            //Here we need to convert the numbered dictionary to a real key/value pair dictionary.
            return links;
            //return ConvertArrayDictionaryToKeyValueDictionary(links);
        }
        public PatientDetailsSearch_Links GetPatientDetailsLinks(int id)
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
                if (organization.Name.ToLower() == _primaryOrganization.ToLower())
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
                if (organization.Name.ToLower() == organizationName.ToLower())
                {
                    primaryOrganization = organization;
                    break;
                }
            }

            return primaryOrganization;
        }

        /// <summary>
        /// Returning the professional configuration object class, where professionals can be activated/deactivated
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

        
        public string GetProgramPathwayEnrollmentLink(string citizenCPR, string programPathwayName)
        {

            var availableProgramPathways = GetPatientAvailableProgramPathways(citizenCPR);
            AvailableProgramPathways_Root chosenProgramPathway = new AvailableProgramPathways_Root();
            string chosenPathwayEnrollmentLink = string.Empty;

            foreach (var programPathway in availableProgramPathways)
            {
                if (programPathway.Name.ToLower() == programPathwayName.ToLower())
                {
                    chosenProgramPathway = programPathway;
                    break;
                }
            }
            if (chosenProgramPathway.Id != null)
            {
                chosenPathwayEnrollmentLink = chosenProgramPathway.Links.Enroll.Href;
            }

            if (chosenPathwayEnrollmentLink == string.Empty)
            {
                return null;
            }
            else
            {
                return chosenPathwayEnrollmentLink;
            }
        }

        /// <summary>
        /// can return more than 1 pathway association, as multiple of the same type can be created/enrolled in
        /// </summary>
        /// <param name="citizenCPR"></param>
        /// <param name="pathwayAssociationName"></param>
        /// <returns></returns>
        public List<AvailablePathwayAssociations_Root> GetPatientPathwayAssociation(string citizenCPR, string pathwayAssociationName)
        {
            var availablePathwayAssociations = GetPatientAvailablePathwayAssociations(citizenCPR);
            AvailablePathwayAssociations_Root chosenPathwayAssociation = new AvailablePathwayAssociations_Root();
            List<AvailablePathwayAssociations_Root> chosenPathwayAssociationList = new List<AvailablePathwayAssociations_Root>();

            foreach (var pathwayAssociation in availablePathwayAssociations)
            {
                if (pathwayAssociation.Name.ToLower() == pathwayAssociationName.ToLower())
                {
                    chosenPathwayAssociationList.Add(pathwayAssociation);
                }
            }

            return chosenPathwayAssociationList;
        }


        /// <summary>
        /// Returns all the open pathway associations (Grundforløb) for the specified patient/citizen
        /// </summary>
        /// <param name="citizenCPR"></param>
        public List<AvailablePathwayAssociations_Root> GetPatientAvailablePathwayAssociations(string citizenCPR) //Henter alle åbne grundforløb på en borger
        {
            var patient = GetPatientDetails(citizenCPR);
            
            var links = patient.Links;
            var availablePathwayAssociationsLink = links.AvailablePathwayAssociation.Href;

            var availablePathwayAssociationsResult = CallAPI(this, availablePathwayAssociationsLink, Method.Get);
            return JsonConvert.DeserializeObject<List<AvailablePathwayAssociations_Root>>(availablePathwayAssociationsResult.Result.ToString());
        }
        /// <summary>
        /// Returns all program pathways (Grundforløb) available for that specific citizen
        /// </summary>
        /// <param name="citizenCPR"></param>
        /// <returns></returns>
        public List<AvailableProgramPathways_Root> GetPatientAvailableProgramPathways(string citizenCPR)
        {
            var patient = GetPatientDetails(citizenCPR);
            //int? patientId = patient.Id;

            var links = patient.Links;
            var availableProgramPathwaysLink = links.AvailableProgramPathways.Href;

            var availableProgramPathwaysResult = CallAPI(this, availableProgramPathwaysLink, Method.Get);
            return JsonConvert.DeserializeObject<List<AvailableProgramPathways_Root>>(availableProgramPathwaysResult.Result.ToString());
        }
        public string GetPatientPreferencesLink(string citizenCPR)
        {
            var links = GetPatientDetailsLinks(citizenCPR);

            return links.PatientPreferences.Href;
        }

        public string GetPatientPreferencesLink(int id)
        {
            var links = GetPatientDetailsLinks(id);

            return links.PatientPreferences.Href;
        }

        
        public PatientPreferences_Root GetPatientPreferences(string citizenCPR)
        {
            result.GetPatientPreferences(this, citizenCPR);
            return JsonConvert.DeserializeObject<PatientPreferences_Root>((string)result.Result); 
        }

        public PatientPreferences_Root GetPatientPreferences(int id)
        {
            result.GetPatientPreferences(this, id);
            return JsonConvert.DeserializeObject<PatientPreferences_Root>((string)result.Result);
        }

        /// <summary>
        /// This will return a list of all pathways present in the dashboard widget. Most likely only 1, but can be more.
        /// </summary>
        /// <param name="citizenCPR"></param>
        /// <param name="dashboardElementName"></param>
        /// <param name="widgetTitle"></param>
        /// <param name="pathwayName"></param>
        /// <returns></returns>
        public List<PathwayReferences_Root> GetWidgetPathwayReference(string citizenCPR, string dashboardElementName, string widgetTitle, string pathwayName)
        {
            List<PathwayReferences_Root> WidgetPathwayReferences = GetWidgetPathwayReferences(citizenCPR, dashboardElementName, widgetTitle);
            List<PathwayReferences_Root> chosenPathwayReferenceList = null;

            foreach (var pathwayReference in WidgetPathwayReferences)
            {
                if (pathwayReference.Name.ToLower() == pathwayName.ToLower())
                {
                    if (chosenPathwayReferenceList == null)
                    {
                        chosenPathwayReferenceList = new List<PathwayReferences_Root>();
                    }
                    chosenPathwayReferenceList.Add(pathwayReference);
                }
            }

            return chosenPathwayReferenceList;
        }
        public List<PathwayReferences_Root> GetWidgetPathwayReferences(string citizenCPR, string dashboardElementName,string widgetTitle)
        {
            string widgetPathwayReferencesLink = GetWidgetPathwayReferencesLink(citizenCPR, dashboardElementName, widgetTitle);
            var webResult = CallAPI(this, widgetPathwayReferencesLink, Method.Get);

            return JsonConvert.DeserializeObject<List<PathwayReferences_Root>>(webResult.Result.ToString());
        }

        public string GetWidgetPathwayReferencesLink(string citizenCPR,string dashboardElementName,string widgetTitle)
        {
            CitizenDashboardSelf_Widget CitizenDashboardElementSelfWidget = GetCitizenDashboardElementSelfWidget(citizenCPR, dashboardElementName, widgetTitle);

            return CitizenDashboardElementSelfWidget.Links.PathwayReferences.Href;
        }
        public CitizenDashboardSelf_Widget GetCitizenDashboardElementSelfWidget(string citizenCPR, string dashboardElementName, string widgetTitle)
        {
            List<CitizenDashboardSelf_Widget> widgets = GetCitizenDashboardElementSelfWidgets(citizenCPR, dashboardElementName);
            CitizenDashboardSelf_Widget chosenWidget = null;

            foreach (var widget in widgets)
            {
                if (widget.HeaderTitle.ToLower() == widgetTitle.ToLower())
                {
                    chosenWidget = widget;
                    break;
                }
            }
            return chosenWidget;
        }
        /// <summary>
        /// Returns a list of widgets. Widgets are the headers of the view - eg. Organisationer tilknyttet borgeren, pårørende and opret grundforløb & forløb
        /// </summary>
        /// <param name="citizenCPR"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public List<CitizenDashboardSelf_Widget> GetCitizenDashboardElementSelfWidgets(string citizenCPR, string dashboardElementName)
        {
            CitizenDashboardSelf_Root citizenDashboardElementSelf = GetCitizenDashboardElementSelf(citizenCPR, dashboardElementName);
            return citizenDashboardElementSelf.View.Widgets;
        }
        public CitizenDashboardSelf_Root GetCitizenDashboardElementSelf(string citizenCPR, string dashboardElementName)
        {
            PatientPreferences_CITIZENDASHBOARD dashboardElement = GetCitizenDashboardElement(citizenCPR, dashboardElementName);
            var selfWebResult = CallAPI(this, dashboardElement.Links.Self.Href, Method.Get);

            return JsonConvert.DeserializeObject<CitizenDashboardSelf_Root>(selfWebResult.Result.ToString());
        }
        public PatientPreferences_CITIZENDASHBOARD GetCitizenDashboardElement(string citizenCPR, string dashboardElementName)
        {
            List<PatientPreferences_CITIZENDASHBOARD> citizenDashboardElements = GetCitizenDashboard(citizenCPR);
            PatientPreferences_CITIZENDASHBOARD chosenElement = null;

            foreach (var elementObject in citizenDashboardElements)
            {
                if (elementObject.Name.ToLower() == dashboardElementName.ToLower())
                {
                    chosenElement = elementObject; 
                    break;
                }
            }
            return chosenElement;
        }
        public PatientPreferences_CITIZENDASHBOARD GetCitizenDashboardElement(List<PatientPreferences_CITIZENDASHBOARD> citizenDashboard, string elementName)
        {
            PatientPreferences_CITIZENDASHBOARD chosenElement = null;

            foreach (var elementObject in citizenDashboard)
            {
                if (elementObject.Name.ToLower() == elementName.ToLower())
                {
                    chosenElement = elementObject;
                    break;
                }
            }
            return chosenElement;
        }
        /// <summary>
        /// Returns everything under the tab "Overblik" on a given citizen
        /// </summary>
        /// <param name="citizenCPR"></param>
        /// <returns></returns>
        public List<PatientPreferences_CITIZENDASHBOARD> GetCitizenDashboard(string citizenCPR)
        {
            return GetPatientPreferences(citizenCPR).CITIZENDASHBOARD;
        }
        /// <summary>
        /// Returns everything under the tab "Overblik" on a given citizen
        /// </summary>
        /// <param name="citizenCPR"></param>
        /// <returns></returns>
        public List<PatientPreferences_CITIZENDASHBOARD> GetCitizenDashboard(int id)
        {
            return GetPatientPreferences(id).CITIZENDASHBOARD;
        }
        /// <summary>
        /// Returns everything under the tab "Data" on a given citizen
        /// </summary>
        /// <param name="citizenCPR"></param>
        /// <returns></returns>
        public List<PatientPreferences_CITIZENDATA> GetCitizenData(string citizenCPR)
        {
            return GetPatientPreferences(citizenCPR).CITIZENDATA;
        }
        /// <summary>
        /// Returns everything under the tab "Data" on a given citizen
        /// </summary>
        /// <param name="citizenCPR"></param>
        /// <returns></returns>
        public List<PatientPreferences_CITIZENDATA> GetCitizenData(int id)
        {
            return GetPatientPreferences(id).CITIZENDATA;
        }

        public List<PatientPreferences_CITIZENPATHWAY> GetCitizenPathways(string citizenCPR)
        {
            return GetPatientPreferences(citizenCPR).CITIZENPATHWAY;
        }

        public List<PatientPreferences_CITIZENPATHWAY> GetCitizenPathways(int id)
        {
            return GetPatientPreferences(id).CITIZENPATHWAY;

        }

     

        public string GetCitizenPathwayLink(string citizenCPR, string pathwayName)
        {
            var pathways = GetCitizenPathways(citizenCPR);
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
                if (item.Name.ToLower() == elementName.ToLower())
                {
                    result = item;
                    break;
                }
            }
            return result;
        }
        
      
        
        public CitizenPathwaySelf_Root GetCitizenPathway(string citizenCPR, string pathwayName)
        {
            var pathways = GetCitizenPathways(citizenCPR);
            PatientPreferences_CITIZENPATHWAY chosenPathway = new PatientPreferences_CITIZENPATHWAY();

            foreach (var pathway in pathways)
            {
                if (pathway.Name.ToLower() == pathwayName.ToLower())
                {
                    chosenPathway = pathway;
                    break;
                }
            }
            //What if the pathwayName doesn't exist in pathways?????????
            //Then the below will fail
            string chosenPathwaySelfLink = chosenPathway.Links.Self.Href;

            return JsonConvert.DeserializeObject<CitizenPathwaySelf_Root>(CallAPI(this, chosenPathwaySelfLink, Method.Get).Result.ToString());

        }
        public CitizenPathwaySelf_Root GetCitizenPathway(int id, string pathwayName)
        {
            var pathways = GetCitizenPathways(id);
            PatientPreferences_CITIZENPATHWAY chosenPathway = new PatientPreferences_CITIZENPATHWAY();

            foreach (var pathway in pathways)
            {
                if (pathway.Name.ToLower() == pathwayName.ToLower())
                {
                    chosenPathway = pathway;
                }
            }
            string chosenPathwaySelfLink = chosenPathway.Links.Self.Href;

            return JsonConvert.DeserializeObject<CitizenPathwaySelf_Root>(CallAPI(this, chosenPathwaySelfLink, Method.Get).Result.ToString());

        }
        

        public string GetCitizenPathwayReferencesLink(string citizenCPR, string pathwayName)
        {
            var pathway = GetCitizenPathway(citizenCPR, pathwayName);
            return pathway.Links.PathwayReferences.Href;
        }

        public string GetCitizenPathwayReferencesLink(int id, string pathwayName)
        {
            var pathway = GetCitizenPathway(id, pathwayName);
            return pathway.Links.PathwayReferences.Href;
        }
        

        /// <summary>
        /// Returns a list of references, located under the pathway
        /// </summary>
        /// <param name="citizenCPR">CPR number for the citizen</param>
        /// <param name="pathwayName"></param>
        /// <returns></returns>
        public List<PathwayReferences_Root> GetCitizenPathwayReferences(string citizenCPR, string pathwayName)
        {
            result.GetCitizenPathwayReferences(this, citizenCPR, pathwayName);
            return JsonConvert.DeserializeObject<List<PathwayReferences_Root>>(result.Result.ToString());
        }

        /// <summary>
        /// Returns a list of references, located under the pathway
        /// </summary>
        /// <param name="id">The id of the citizen</param>
        /// <param name="pathwayName"></param>
        /// <returns></returns>
        public List<PathwayReferences_Root> GetCitizenPathwayReferences(int id, string pathwayName)
        {
            result.GetCitizenPathwayReferences(this, id, pathwayName);
            return JsonConvert.DeserializeObject<List<PathwayReferences_Root>>(result.Result.ToString());
        }

        public List<PathwayReferences_Child> GetPathwayReferencesChildrenElements(List<PathwayReferences_Child> list, PathwayReferences_Child childElement)
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
                        GetPathwayReferencesChildrenElements(list, childElement);
                        break;
                }
            }
            return list;
        }

        public List<PathwayReferences_Child> GetCitizenPathwayDocuments(string citizenCPR, string pathwayName)
        {
            // As it could be misleading to have to call the pathway references documents, this method has been made to make more sense.
            // Here you just get the pathway documents.
            return GetCitizenPathwayReferencesDocuments(citizenCPR, pathwayName);
        }
        
        public List<PathwayReferences_Child> GetCitizenPathwayReferencesDocuments(string citizenCPR, string pathwayName, bool getNestedElements = false)
        {
            result.GetCitizenPathwayReferences(this, citizenCPR, pathwayName);
            var pathwayReferences = JsonConvert.DeserializeObject<List<PathwayReferences_Root>>(result.Result.ToString());
            // In pathway references the children will be either child pathways (type :	patientPathwayReference)
            // or documents (type : documentReference).
            // In order to get the documents, we loop through everything, locating elements with the type we want
            // and put them in a list of elements

            List<PathwayReferences_Child> Documents = new List<PathwayReferences_Child>();

            foreach (var pathwayReference in pathwayReferences)
            {
                var children = pathwayReference.Children;

                foreach (var item in children)
                {

                   
                    var elementType = item.Type; 

                    switch (elementType)
                    {
                        case "patientPathwayReference":
                            //We don't add to dictionary as it is not a direct document reference
                            break;
                        case "documentReference":
                            //We add to the Documents dictionary
                            Documents.Add(item);
                            break;
                        default:
                            break;
                    }
                    
                }
            }

            return Documents; 
        }

        public string GetCitizenPathwayReferencesSelfLink(string citizenCPR, string pathwayName)
        {
            var pathwayReferences = GetCitizenPathwayReferences(citizenCPR,pathwayName);
            PathwayReferences_Root chosenReference = new PathwayReferences_Root();
            foreach (var reference in pathwayReferences)
            {
                if (reference.Name == "pathwayReferenceName")
                {
                    chosenReference = reference;
                    break;
                }
            }
            if (chosenReference.Links != null)
            {
                return chosenReference.Links.Self.Href;
            }
            else
            {
                throw new Exception("Pathway reference name is not part of the pathway name '" + pathwayName + "'.");
            }

        }

        public PathwayReferencesSelf_Root GetCitizenPathwayReferencesSelf(string citizenCPR, string pathwayName)
        {
            result.GetCitizenPathwayReferencesSelf(this, citizenCPR, pathwayName);
            return JsonConvert.DeserializeObject<PathwayReferencesSelf_Root>(result.Result.ToString());
        }
       

        public PathwayReferencesSelf_Links GetCitizenPathwayReferencesSelf_Links(string citizenCPR, string pathwayName)
        {
            var CitizenPathwayReferencesSelf = GetCitizenPathwayReferencesSelf(citizenCPR, pathwayName);
            return CitizenPathwayReferencesSelf.Links;
        }
        

        
        public List<PathwayReferences_Child> GetCitizenPathwayReferencesChildren(string citizenCPR, string pathwayName, string referenceName)
        {
            List<PathwayReferences_Root> pathwayReferences = GetCitizenPathwayReferences(citizenCPR, pathwayName);
            List<PathwayReferences_Child> children = new List<PathwayReferences_Child>();
            if (pathwayReferences == null)
            {
                return null;
            }
            else
            {
                foreach (var pathwayReference in pathwayReferences)
                {
                    if (pathwayReference.Name.ToLower() == referenceName.ToLower())
                    {
                        if (pathwayReference.Children.Count != 0)
                        {
                            children = pathwayReference.Children;
                        }
                        else
                        {
                            children = null;
                        }
                    }
                }
                return children;
            }
        }



        public List<Organizations_Root> GetOrganizations()
        {
            result.GetOrganizations(this);
            return JsonConvert.DeserializeObject<List<Organizations_Root>>(result.Result.ToString());   
        }

   

        public OrganizationsSelf_Root GetSpecificOrganization(string organizationName)
        {
            var organizations = GetOrganizations();
            Organizations_Root chosenOrganization = new Organizations_Root();

            foreach (var org in organizations)
            {
                if (org.Name.ToLower() == organizationName.ToLower())
                {
                    chosenOrganization = org;
                    break;
                }
            }
            return JsonConvert.DeserializeObject<OrganizationsSelf_Root>(CallAPI(this, chosenOrganization.Links.Self.Href, Method.Get).Result.ToString());  
        }


        public OrganizationsSelf_Links GetOrganizationLinks(string organizationName)
        {
            var organization = GetSpecificOrganization(organizationName);
            return organization.Links;
        }



        public List<Professionals_Root> GetOrganizationProfessionals(string organizationName)
        {
            var org = GetSpecificOrganization(organizationName);
            var link = org.Links.Professionals.Href;

            return JsonConvert.DeserializeObject<List<Professionals_Root>>(CallAPI(this,link,Method.Get).Result.ToString());
        }

        
        /// <summary>
        /// TAKES TIME - getting all patient details in a list of patients
        /// </summary>
        /// <returns></returns>
        public List<PatientDetailsSearch_Patient> GetAllPatients()
        {
            List<PatientDetailsSearch_Patient> patients = new List<PatientDetailsSearch_Patient>();

            var patientIds = GetAllPatientIds();

            foreach (var patientId in patientIds)
            {
                patients.Add(GetPatientDetails(patientId));
            }
            return patients;
        }

        public List<Int32> GetAllPatientIds()
        {
            //var allPatients = GetAllPatients();
            var link = GetHomeRessourceLink("patients");
            string queryText = "?pageSize=10000000&query=";

            string endpointURL = link + queryText;
            var returnResult = result.CallAPI(this, endpointURL, Method.Get);
            var allPatients = dataHandler.JsonStringToSortedDictionary(returnResult.Result.ToString());

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
        public List<Professional_Root> GetProfessionals(string queryString)
        {
            return JsonConvert.DeserializeObject < List<Professional_Root>>(CallAPI(this, GetProfessionalsLink() + "?query=" + queryString, Method.Get).Result.ToString());
        }

        public Professional_Root GetProfessional(int id)
        {
            var professional = CallAPI(this, GetProfessionalsLink() + "/" + id, Method.Get);

            Professional_Root professionalObject = new Professional_Root();
            professionalObject = JsonConvert.DeserializeObject<Professional_Root>(professional.Result.ToString());

            return professionalObject;
        }
        public void LukGrundforloeb(string citizenCPR, string pathwayName)
        {
            CloseCitizenPathwayAssociation(citizenCPR, pathwayName);
        }
        public void LukGrundforloeb(AvailablePathwayAssociations_Self_Root citizenPathwayAssociation)
        {
            CloseCitizenPathwayAssociation(citizenPathwayAssociation);
        }
        /// <summary>
        /// Will close all pathway associations with the pathwayAssociationName (grundforløb) input string
        /// </summary>
        /// <param name="citizenCPR"></param>
        /// <param name="pathwayAssociationName"></param>
        public void CloseCitizenPathwayAssociation(string citizenCPR, string pathwayAssociationName)
        {
            var pathwayAssociations = GetPatientPathwayAssociation(citizenCPR, pathwayAssociationName);
            foreach (var pathwayAssociation in pathwayAssociations)
            {
                try
                {
                    CloseCitizenPathwayAssociation(pathwayAssociation);
                }
                catch (Exception e)
                {

                    throw new Exception("Error during closure of pathway associations. Exception: " + e.Message);
                }
            }
            
        }
        public void CloseCitizenPathwayAssociation(AvailablePathwayAssociations_Self_Root citizenPathwayAssociation) 
        {
            string closureLink = citizenPathwayAssociation.Links.Close.Href;
            try
            {
                var closeResult = CallAPI(this, closureLink, Method.Put);
            }
            catch (Exception e)
            {

                throw new Exception("Could not close the pathway. Exception: " + e.Message);
            }
            
        }
        public void CloseCitizenPathwayAssociation(AvailablePathwayAssociations_Root citizenPathwayAssociation)
        {
            string selfLink = citizenPathwayAssociation.Links.Self.Href;
            var webResult = CallAPI(this,selfLink, Method.Get);
            AvailablePathwayAssociations_Self_Root selfObject = JsonConvert.DeserializeObject< AvailablePathwayAssociations_Self_Root>(webResult.Result.ToString());

            CloseCitizenPathwayAssociation(selfObject);
        }



        #endregion Specific methods
        #region Shared methods

        ////////////////////
        ///SHARED METHODS///
        ////////////////////

        public NexusResult GetTransformedBodyOfMedcomMessage(NexusAPI api, string endpointURL)
        {
            NexusResult nexusResult = new NexusResult();
            return nexusResult.GetTransformedBodyOfMedcomMessage(api, endpointURL);
        }
        public NexusResult CallAPI(NexusAPI api, string endpointURL,Method callMethod, string JsonBody = null)
        {
            NexusResult nexusResult = new NexusResult();
            if (JsonBody == null)
            {
                return nexusResult.CallAPI(this, endpointURL, callMethod);
            }
            else
            {
                return nexusResult.CallAPI(this, endpointURL, JsonBody, callMethod);
            }
        }

        private string GetElementDataFromSortedDict(SortedDictionary<string, SortedDictionary<string, string>> dict, string elementName)
        {
            string result = string.Empty;
            foreach (var item in dict)
            {
                if (item.Key.ToLower() == elementName.ToLower())
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
       
            public SortedDictionary<string, string> GetNestedData(SortedDictionary<string, string> dict, string dataName)
            {
                JObject dataOutput = JObject.Parse(dict[dataName]);
                return dataHandler.ConvertJsonToSortedDictionary(dataOutput);
            }
       
        public void AddResource(SortedDictionary<string,string> dict, string linkName, string href)
        {
            if (dict.ContainsKey(linkName))
                return;
            dict.Add(linkName, href);
        }
        
        internal JObject GetJsonObjectFromRestResponse(NexusResult output, string objectName)
        {
            dynamic jsonOutput = JObject.Parse((string)output.Result);
            dynamic objectOutput = jsonOutput[objectName];
            return objectOutput;
        }


        

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
                if (child.Name.ToLower() == org.ToLower())
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
            public void AddElementToList(List<object> list, object objectToAdd)
            {
                list.Add(objectToAdd);
            }

        public object GetRefObjectElement(ReferencedObject_Base_Root baseObject, string baseObjectJSONString)
        {
            switch (baseObject.Type.ToString().ToLower())
            {
                case "medcom":
                    return JsonConvert.DeserializeObject<RefObject_medcom_Root>(baseObjectJSONString);
                case "rehabilitationplan":
                    return JsonConvert.DeserializeObject<RefObject_rehabplan_Root>(baseObjectJSONString);
                case "clinicalemail2006":
                    return JsonConvert.DeserializeObject<RefObject_clinicalemail_Root>(baseObjectJSONString);
                default:
                    throw new Exception("medcom type " + baseObject.Type.ToString() + " is not known. Please add classes"); 
            }
        }

        public void AddDataToTransformedBodyObject(TransformedBody_Root transformedBody, string htmlResult, string h5HeaderStart)
        {
            switch (h5HeaderStart.ToLower())
            {
                #region udskrivningsrapport selectors
                case "fremtidige aftaler":
                    
                    break;
                case "aftaler omkring kost første døgn efter udskrivning":
                    
                    break;
                case "medicin information relateret til udskrivning":
                    
                    break;
                case "seneste medicingivning":
                    
                    break;
                case "sygeplejefaglige problemområder":
                    
                    break;
                case "diagnoser":
                    
                    break;
                case "aktuel indlæggelse":
                    
                    break;
                case "pårørende/relationer":
                    HandleRelativesData(transformedBody, htmlResult);
                    break;
                    #endregion  udskrivningsrapport selectors 

            }
        }

        private void HandleRelativesData(TransformedBody_Root transformedBody, string htmlResult)
        {

        }
        #endregion Shared methods



    }
}

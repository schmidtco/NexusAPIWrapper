using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CsQuery;
using CsQuery.Engine.PseudoClassSelectors;
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
        private Dictionary<string,string> _MIMETypes;
        private string _primaryOrganization = "Ringsted Kommune";

        public Dictionary<string, string> MIMETypes {
            get => _MIMETypes; 
        }
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
            GetMIMETypes();
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
            return JsonConvert.DeserializeObject<PatientDetailsSearch_Patient>(result.Result.ToString());
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
        public ReferencedObject_Base_Root GetReferencedObject_Base_Root(ACTIVITYLIST_Pages_Content_Root pageContent)
        {
            return GetActivityListContentBaseObject(pageContent);
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
            return JsonConvert.DeserializeObject<Preferences_Root>(result.Result.ToString());
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
        public List<PathwayReferences_Root> GetWidgetPathwayReferences(string citizenCPR, string dashboardElementName, string widgetTitle)
        {
            string widgetPathwayReferencesLink = GetWidgetPathwayReferencesLink(citizenCPR, dashboardElementName, widgetTitle);
            var webResult = CallAPI(this, widgetPathwayReferencesLink, Method.Get);

            return JsonConvert.DeserializeObject<List<PathwayReferences_Root>>(webResult.Result.ToString());
        }

        public string GetWidgetPathwayReferencesLink(string citizenCPR, string dashboardElementName, string widgetTitle)
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
            List<PathwayReferences_Root> referencesList = new List<PathwayReferences_Root>();
            result.GetCitizenPathwayReferences(this, citizenCPR, pathwayName);
            try
            {
                return JsonConvert.DeserializeObject<List<PathwayReferences_Root>>(result.Result.ToString());
            }
            catch (Exception)
            {
                var reference = JsonConvert.DeserializeObject<PathwayReferences_Root>(result.Result.ToString());
                referencesList.Add(reference);
                return referencesList;
            }
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

        public string GetCitizenPathwayReferencesSelfLink(string citizenCPR, string pathwayName, int pathwayReferenceId)
        {
            var pathwayReferences = GetCitizenPathwayReferences(citizenCPR, pathwayName);
            return pathwayReferences.FirstOrDefault(x => x.PatientPathwayId.ToString() == pathwayReferenceId.ToString()).Links.Self.Href;
        }
        public string GetCitizenPathwayReferencesSelfLink(string citizenCPR, string pathwayName, string pathwayReferenceName)
        {
            var pathwayReferences = GetCitizenPathwayReferences(citizenCPR, pathwayName);
            List<PathwayReferences_Root> referencesList= new List<PathwayReferences_Root>();
            foreach (var reference in pathwayReferences)
            {
                if (reference.Name == pathwayReferenceName)
                {
                    referencesList.Add(reference);
                }
            }
            PathwayReferences_Root chosenReference = new PathwayReferences_Root();
            if (referencesList.Count == 1)
            {
                chosenReference = referencesList[0];
                if (chosenReference.Links != null)
                {
                    return chosenReference.Links.Self.Href;
                }
                else
                {
                    throw new Exception("Pathway reference name is not part of the pathway name '" + pathwayName + "'.");
                }
            }
            else
            {
                throw new Exception("No single pathway reference exists. Call the GetCitizenPathwayReferences method to get all references with the name \"" + pathwayReferenceName + "\" and get selflink by PatientPathwayId");
            }

        }
        public CitPathwSelfDocPrototype_Root CreateGetCitizenPathwayReferencesSelf_DocumentPrototype(string citizenCPR, string pathwayName, string pathwayReferenceName, string name, string originalFileName, int pathwayReferenceId = 0)
        {
            var prototype = GetGetCitizenPathwayReferencesSelf_DocumentPrototype(citizenCPR,pathwayName,pathwayReferenceName,pathwayReferenceId);
            prototype.Name = name;
            prototype.OriginalFileName = originalFileName;

            return prototype;
        }
        /// <summary>
        /// Creates a prototype with name and original file name set, based on the file it self
        /// </summary>
        /// <param name="docPrototype"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public CitPathwSelfDocPrototype_Root CreateGetCitizenPathwayReferencesSelf_DocumentPrototype(CitPathwSelfDocPrototype_Root docPrototype, string filePath)
        {
            string fullFileName = System.IO.Path.GetFileName(filePath);
            string fileName = System.IO.Path.GetFileNameWithoutExtension(filePath);
            docPrototype.Name = fileName;
            docPrototype.OriginalFileName = fullFileName;

            return docPrototype;
        }
        /// <summary>
        /// Creates a prototype with name and orginal file name set by input
        /// </summary>
        /// <param name="docPrototype"></param>
        /// <param name="name"></param>
        /// <param name="originalFileName"></param>
        /// <returns></returns>
        public CitPathwSelfDocPrototype_Root CreateGetCitizenPathwayReferencesSelf_DocumentPrototype(CitPathwSelfDocPrototype_Root docPrototype, string name, string originalFileName)
        {
            docPrototype.Name = name;
            docPrototype.OriginalFileName = originalFileName;

            return docPrototype;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="citizenCPR"></param>
        /// <param name="pathwayName"></param>
        /// <param name="pathwayReferenceName"></param>
        /// <param name="pathwayReferenceId">The PatientPathwayId</param>
        /// <returns></returns>
        public CitPathwSelfDocPrototype_Root GetGetCitizenPathwayReferencesSelf_DocumentPrototype(string citizenCPR, string pathwayName, string pathwayReferenceName, int pathwayReferenceId = 0)
        {
            string pathwaySelfDocumentPrototypeLink = string.Empty;
            
            if (pathwayReferenceId == 0)
            {
                pathwaySelfDocumentPrototypeLink = GetGetCitizenPathwayReferencesSelf_DocumentPrototypeLink(citizenCPR, pathwayName, pathwayReferenceName);
            }
            else
            {
                pathwaySelfDocumentPrototypeLink = GetGetCitizenPathwayReferencesSelf_DocumentPrototypeLink(citizenCPR, pathwayName, pathwayReferenceId);
            }
            var webResult = CallAPI(this, pathwaySelfDocumentPrototypeLink, Method.Get);
            return JsonConvert.DeserializeObject<CitPathwSelfDocPrototype_Root>(webResult.Result.ToString());
        }
        public CitPathwSelfDocPrototype_Root GetGetCitizenPathwayReferencesSelf_DocumentPrototype(PathwayReferencesSelf_Root citizenPathwayReferenceSelf)
        {
            var pathwaySelfDocumentPrototypeLink = GetGetCitizenPathwayReferencesSelf_DocumentPrototypeLink(citizenPathwayReferenceSelf);
            var webResult = CallAPI(this, pathwaySelfDocumentPrototypeLink, Method.Get);
            return JsonConvert.DeserializeObject<CitPathwSelfDocPrototype_Root>(webResult.Result.ToString());
        }
        public string GetGetCitizenPathwayReferencesSelf_DocumentPrototypeLink(PathwayReferencesSelf_Root citizenPathwayReferenceSelf)
        {
            return citizenPathwayReferenceSelf.Links.DocumentPrototype.Href;
        }
        public string GetGetCitizenPathwayReferencesSelf_DocumentPrototypeLink(string citizenCPR, string pathwayName, int pathwayReferenceId)
        {
            var pathwayRefSelf = GetCitizenPathwayReferencesSelf(citizenCPR, pathwayName, pathwayReferenceId);
            return pathwayRefSelf.Links.DocumentPrototype.Href;
        }
        public string GetGetCitizenPathwayReferencesSelf_DocumentPrototypeLink(string citizenCPR, string pathwayName, string pathwayReferenceName)
        {
            var pathwayRefSelf = GetCitizenPathwayReferencesSelf(citizenCPR,pathwayName,pathwayReferenceName);
            if (pathwayRefSelf.Count == 1)
            {
                return pathwayRefSelf[0].Links.DocumentPrototype.Href; ;
            }
            else
            {
                throw new Exception("No single pathway reference exist. Call the GetCitizenPathwayReferencesSelf method to se all references with the name \"" + pathwayReferenceName + "\"");
            }
        }
        public List<PathwayReferencesSelf_Root> GetCitizenPathwayReferencesSelf(string citizenCPR, string pathwayName, string pathwayReferenceName)
        {
            result.GetCitizenPathwayReferencesSelf(this, citizenCPR, pathwayName, pathwayReferenceName);
            return JsonConvert.DeserializeObject<List<PathwayReferencesSelf_Root>>(result.Result.ToString());
        }
        public PathwayReferencesSelf_Root GetCitizenPathwayReferencesSelf(string citizenCPR, string pathwayName, int pathwayReferenceId)
        {
            result.GetCitizenPathwayReferencesSelf(this, citizenCPR, pathwayName, pathwayReferenceId);
            return JsonConvert.DeserializeObject<PathwayReferencesSelf_Root>(result.Result.ToString());
        }


        public PathwayReferencesSelf_Links GetCitizenPathwayReferencesSelf_Links(string citizenCPR, string pathwayName, string pathwayReferenceName)
        {
            var CitizenPathwayReferencesSelf = GetCitizenPathwayReferencesSelf(citizenCPR, pathwayName, pathwayReferenceName);
            if (CitizenPathwayReferencesSelf.Count == 1)
            {
                return CitizenPathwayReferencesSelf[0].Links;
            }
            else
            {
                throw new Exception("No single pathway reference exist. Call the GetCitizenPathwayReferencesSelf method to se all references with the name \"" + pathwayReferenceName + "\"");
            }
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

            return JsonConvert.DeserializeObject<List<Professionals_Root>>(CallAPI(this, link, Method.Get).Result.ToString());
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
            return JsonConvert.DeserializeObject<List<Professional_Root>>(CallAPI(this, GetProfessionalsLink() + "?query=" + queryString, Method.Get).Result.ToString());
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
            var webResult = CallAPI(this, selfLink, Method.Get);
            AvailablePathwayAssociations_Self_Root selfObject = JsonConvert.DeserializeObject<AvailablePathwayAssociations_Self_Root>(webResult.Result.ToString());

            CloseCitizenPathwayAssociation(selfObject);
        }



        #endregion Specific methods
        #region Shared methods

        ////////////////////
        ///SHARED METHODS///
        ////////////////////

        internal string CreateWebKitFormBoundary()
        {
            string webKitStart = "WebKitFormBoundary";
            return webKitStart + GetRandomString(15);
        }
        internal string GetRandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        internal WebRequest UploadPatientPathwayDocumentToNexus(CitPathwSelfDocPrototypeCreate_Root createdDocumentObject, string filePath)
        {
            return UploadDocumentToNexus(createdDocumentObject.Links.Upload.Href, filePath);
        }
        internal WebRequest UploadPatientDocumentToNexus(Patient_DocumentPrototype_Create_Root createdDocumentObject, string filePath)
        {
            return UploadDocumentToNexus(createdDocumentObject.Links.Upload.Href, filePath);
        }
        internal WebRequest UploadDocumentToNexus(string uploadLink, string filePath)
        {
            string fileName = System.IO.Path.GetFileName(filePath);
            string extension = System.IO.Path.GetExtension(filePath);
            extension = extension.ToLower().Substring(1, extension.Length - 1);
            string mimeType = GetMIMEType(extension);
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            string webKitFormBoundary = CreateWebKitFormBoundary();

            NexusResult result = new NexusResult();
            WebRequest webRequest = result.StandardWebRequest(this, uploadLink, Method.Post);
            webRequest.AddHeaderContentType("multipart/form-data; boundary=----" + webKitFormBoundary);
            webRequest.request.AddFile("file", fileBytes, fileName, mimeType);

            webRequest.Execute();

            return webRequest;
        }

        internal CitPathwSelfDocPrototypeCreate_Root UploadPatientPathwayDocumentPrototype(CitPathwSelfDocPrototype_Root documentPrototype)
        {
            string createLink = documentPrototype.Links.Create.Href;
            string jsonPrototype = JsonConvert.SerializeObject(documentPrototype);
            var webResult = CallAPI(this, createLink, Method.Post, jsonPrototype);

            return JsonConvert.DeserializeObject<CitPathwSelfDocPrototypeCreate_Root>(webResult.Result.ToString());
        }
        internal Patient_DocumentPrototype_Create_Root UploadPatientDocumentPrototype(Patient_DocumentPrototype_Root documentPrototype)
        {
            string createLink = documentPrototype.Links.Create.Href;
            string jsonPrototype = JsonConvert.SerializeObject(documentPrototype);
            var webResult = CallAPI(this, createLink, Method.Post, jsonPrototype);

            return JsonConvert.DeserializeObject<Patient_DocumentPrototype_Create_Root>(webResult.Result.ToString());
        }
        internal string GetDocumentPrototypeLink(PatientDetailsSearch_Patient patient)
        {
            return patient.Links.DocumentPrototype.Href;
        }
        internal Patient_DocumentPrototype_Root CreatePatientDocumentPrototype(PatientDetailsSearch_Patient patient, string name, string originalFileName)
        {
            string documentPrototypeLink = GetDocumentPrototypeLink(patient);
            Patient_DocumentPrototype_Root documentPrototype = GetPatientDocumentPrototype(documentPrototypeLink);

            documentPrototype.Name = name;
            documentPrototype.OriginalFileName = originalFileName;

            return documentPrototype;
        }
        internal Patient_DocumentPrototype_Root CreatePatientDocumentPrototype(PatientDetailsSearch_Patient patient, string fullFilePath)
        {
            string documentPrototypeLink = GetDocumentPrototypeLink(patient);
            Patient_DocumentPrototype_Root documentPrototype = GetPatientDocumentPrototype(documentPrototypeLink);
            string fullFileName = System.IO.Path.GetFileName(fullFilePath);
            string fileName = System.IO.Path.GetFileNameWithoutExtension(fullFilePath);
            documentPrototype.Name = fileName;
            documentPrototype.OriginalFileName = fullFileName;

            return documentPrototype;
        }
        internal Patient_DocumentPrototype_Root GetPatientDocumentPrototype(string documentPrototypeLink)
        {
            var webResult = CallAPI(this, documentPrototypeLink, Method.Get);

            return JsonConvert.DeserializeObject<Patient_DocumentPrototype_Root>(webResult.Result.ToString());
        }

        private void GetMIMETypes()
        {
            string jsonMIME = "{\r\n    \"x3d\": \"application/vnd.hzn-3d-crossword\",\r\n    \"3gp\": \"video/3gpp\",\r\n    \"3g2\": \"video/3gpp2\",\r\n    \"mseq\": \"application/vnd.mseq\",\r\n    \"pwn\": \"application/vnd.3m.post-it-notes\",\r\n    \"plb\": \"application/vnd.3gpp.pic-bw-large\",\r\n    \"psb\": \"application/vnd.3gpp.pic-bw-small\",\r\n    \"pvb\": \"application/vnd.3gpp.pic-bw-var\",\r\n    \"tcap\": \"application/vnd.3gpp2.tcap\",\r\n    \"7z\": \"application/x-7z-compressed\",\r\n    \"abw\": \"application/x-abiword\",\r\n    \"ace\": \"application/x-ace-compressed\",\r\n    \"acc\": \"application/vnd.americandynamics.acc\",\r\n    \"acu\": \"application/vnd.acucobol\",\r\n    \"atc\": \"application/vnd.acucorp\",\r\n    \"adp\": \"audio/adpcm\",\r\n    \"aab\": \"application/x-authorware-bin\",\r\n    \"aam\": \"application/x-authorware-map\",\r\n    \"aas\": \"application/x-authorware-seg\",\r\n    \"air\": \"application/vnd.adobe.air-application-installer-package+zip\",\r\n    \"swf\": \"application/x-shockwave-flash\",\r\n    \"fxp\": \"application/vnd.adobe.fxp\",\r\n    \"pdf\": \"application/pdf\",\r\n    \"ppd\": \"application/vnd.cups-ppd\",\r\n    \"dir\": \"application/x-director\",\r\n    \"xdp\": \"application/vnd.adobe.xdp+xml\",\r\n    \"xfdf\": \"application/vnd.adobe.xfdf\",\r\n    \"aac\": \"audio/x-aac\",\r\n    \"ahead\": \"application/vnd.ahead.space\",\r\n    \"azf\": \"application/vnd.airzip.filesecure.azf\",\r\n    \"azs\": \"application/vnd.airzip.filesecure.azs\",\r\n    \"azw\": \"application/vnd.amazon.ebook\",\r\n    \"ami\": \"application/vnd.amiga.ami\",\r\n    \"N/A\": \"application/andrew-inset\",\r\n    \"apk\": \"application/vnd.android.package-archive\",\r\n    \"cii\": \"application/vnd.anser-web-certificate-issue-initiation\",\r\n    \"fti\": \"application/vnd.anser-web-funds-transfer-initiation\",\r\n    \"atx\": \"application/vnd.antix.game-component\",\r\n    \"dmg\": \"application/x-apple-diskimage\",\r\n    \"mpkg\": \"application/vnd.apple.installer+xml\",\r\n    \"aw\": \"application/applixware\",\r\n    \"mp3\": \"audio/mpeg\",\r\n    \"les\": \"application/vnd.hhe.lesson-player\",\r\n    \"swi\": \"application/vnd.aristanetworks.swi\",\r\n    \"s\": \"text/x-asm\",\r\n    \"atomcat\": \"application/atomcat+xml\",\r\n    \"atomsvc\": \"application/atomsvc+xml\",\r\n    \"atom\": \"application/atom+xml\",\r\n    \"ac\": \"application/pkix-attr-cert\",\r\n    \"aif\": \"audio/x-aiff\",\r\n    \"avi\": \"video/x-msvideo\",\r\n    \"aep\": \"application/vnd.audiograph\",\r\n    \"dxf\": \"image/vnd.dxf\",\r\n    \"dwf\": \"model/vnd.dwf\",\r\n    \"par\": \"text/plain-bas\",\r\n    \"bcpio\": \"application/x-bcpio\",\r\n    \"bin\": \"application/octet-stream\",\r\n    \"bmp\": \"image/bmp\",\r\n    \"torrent\": \"application/x-bittorrent\",\r\n    \"cod\": \"application/vnd.rim.cod\",\r\n    \"mpm\": \"application/vnd.blueice.multipass\",\r\n    \"bmi\": \"application/vnd.bmi\",\r\n    \"sh\": \"application/x-sh\",\r\n    \"btif\": \"image/prs.btif\",\r\n    \"rep\": \"application/vnd.businessobjects\",\r\n    \"bz\": \"application/x-bzip\",\r\n    \"bz2\": \"application/x-bzip2\",\r\n    \"csh\": \"application/x-csh\",\r\n    \"c\": \"text/x-c\",\r\n    \"cdxml\": \"application/vnd.chemdraw+xml\",\r\n    \"css\": \"text/css\",\r\n    \"cdx\": \"chemical/x-cdx\",\r\n    \"cml\": \"chemical/x-cml\",\r\n    \"csml\": \"chemical/x-csml\",\r\n    \"cdbcmsg\": \"application/vnd.contact.cmsg\",\r\n    \"cla\": \"application/vnd.claymore\",\r\n    \"c4g\": \"application/vnd.clonk.c4group\",\r\n    \"sub\": \"image/vnd.dvb.subtitle\",\r\n    \"cdmia\": \"application/cdmi-capability\",\r\n    \"cdmic\": \"application/cdmi-container\",\r\n    \"cdmid\": \"application/cdmi-domain\",\r\n    \"cdmio\": \"application/cdmi-object\",\r\n    \"cdmiq\": \"application/cdmi-queue\",\r\n    \"c11amc\": \"application/vnd.cluetrust.cartomobile-config\",\r\n    \"c11amz\": \"application/vnd.cluetrust.cartomobile-config-pkg\",\r\n    \"ras\": \"image/x-cmu-raster\",\r\n    \"dae\": \"model/vnd.collada+xml\",\r\n    \"csv\": \"text/csv\",\r\n    \"cpt\": \"application/mac-compactpro\",\r\n    \"wmlc\": \"application/vnd.wap.wmlc\",\r\n    \"cgm\": \"image/cgm\",\r\n    \"ice\": \"x-conference/x-cooltalk\",\r\n    \"cmx\": \"image/x-cmx\",\r\n    \"xar\": \"application/vnd.xara\",\r\n    \"cmc\": \"application/vnd.cosmocaller\",\r\n    \"cpio\": \"application/x-cpio\",\r\n    \"clkx\": \"application/vnd.crick.clicker\",\r\n    \"clkk\": \"application/vnd.crick.clicker.keyboard\",\r\n    \"clkp\": \"application/vnd.crick.clicker.palette\",\r\n    \"clkt\": \"application/vnd.crick.clicker.template\",\r\n    \"clkw\": \"application/vnd.crick.clicker.wordbank\",\r\n    \"wbs\": \"application/vnd.criticaltools.wbs+xml\",\r\n    \"cryptonote\": \"application/vnd.rig.cryptonote\",\r\n    \"cif\": \"chemical/x-cif\",\r\n    \"cmdf\": \"chemical/x-cmdf\",\r\n    \"cu\": \"application/cu-seeme\",\r\n    \"cww\": \"application/prs.cww\",\r\n    \"curl\": \"text/vnd.curl\",\r\n    \"dcurl\": \"text/vnd.curl.dcurl\",\r\n    \"mcurl\": \"text/vnd.curl.mcurl\",\r\n    \"scurl\": \"text/vnd.curl.scurl\",\r\n    \"car\": \"application/vnd.curl.car\",\r\n    \"pcurl\": \"application/vnd.curl.pcurl\",\r\n    \"cmp\": \"application/vnd.yellowriver-custom-menu\",\r\n    \"dssc\": \"application/dssc+der\",\r\n    \"xdssc\": \"application/dssc+xml\",\r\n    \"deb\": \"application/x-debian-package\",\r\n    \"uva\": \"audio/vnd.dece.audio\",\r\n    \"uvi\": \"image/vnd.dece.graphic\",\r\n    \"uvh\": \"video/vnd.dece.hd\",\r\n    \"uvm\": \"video/vnd.dece.mobile\",\r\n    \"uvu\": \"video/vnd.uvvu.mp4\",\r\n    \"uvp\": \"video/vnd.dece.pd\",\r\n    \"uvs\": \"video/vnd.dece.sd\",\r\n    \"uvv\": \"video/vnd.dece.video\",\r\n    \"dvi\": \"application/x-dvi\",\r\n    \"seed\": \"application/vnd.fdsn.seed\",\r\n    \"dtb\": \"application/x-dtbook+xml\",\r\n    \"res\": \"application/x-dtbresource+xml\",\r\n    \"ait\": \"application/vnd.dvb.ait\",\r\n    \"svc\": \"application/vnd.dvb.service\",\r\n    \"eol\": \"audio/vnd.digital-winds\",\r\n    \"djvu\": \"image/vnd.djvu\",\r\n    \"dtd\": \"application/xml-dtd\",\r\n    \"mlp\": \"application/vnd.dolby.mlp\",\r\n    \"wad\": \"application/x-doom\",\r\n    \"dpg\": \"application/vnd.dpgraph\",\r\n    \"dra\": \"audio/vnd.dra\",\r\n    \"dfac\": \"application/vnd.dreamfactory\",\r\n    \"dts\": \"audio/vnd.dts\",\r\n    \"dtshd\": \"audio/vnd.dts.hd\",\r\n    \"dwg\": \"image/vnd.dwg\",\r\n    \"geo\": \"application/vnd.dynageo\",\r\n    \"es\": \"application/ecmascript\",\r\n    \"mag\": \"application/vnd.ecowin.chart\",\r\n    \"mmr\": \"image/vnd.fujixerox.edmics-mmr\",\r\n    \"rlc\": \"image/vnd.fujixerox.edmics-rlc\",\r\n    \"exi\": \"application/exi\",\r\n    \"mgz\": \"application/vnd.proteus.magazine\",\r\n    \"epub\": \"application/epub+zip\",\r\n    \"eml\": \"message/rfc822\",\r\n    \"nml\": \"application/vnd.enliven\",\r\n    \"xpr\": \"application/vnd.is-xpr\",\r\n    \"xif\": \"image/vnd.xiff\",\r\n    \"xfdl\": \"application/vnd.xfdl\",\r\n    \"emma\": \"application/emma+xml\",\r\n    \"ez2\": \"application/vnd.ezpix-album\",\r\n    \"ez3\": \"application/vnd.ezpix-package\",\r\n    \"fst\": \"image/vnd.fst\",\r\n    \"fvt\": \"video/vnd.fvt\",\r\n    \"fbs\": \"image/vnd.fastbidsheet\",\r\n    \"fe_launch\": \"application/vnd.denovo.fcselayout-link\",\r\n    \"f4v\": \"video/x-f4v\",\r\n    \"flv\": \"video/x-flv\",\r\n    \"fpx\": \"image/vnd.fpx\",\r\n    \"npx\": \"image/vnd.net-fpx\",\r\n    \"flx\": \"text/vnd.fmi.flexstor\",\r\n    \"fli\": \"video/x-fli\",\r\n    \"ftc\": \"application/vnd.fluxtime.clip\",\r\n    \"fdf\": \"application/vnd.fdf\",\r\n    \"f\": \"text/x-fortran\",\r\n    \"mif\": \"application/vnd.mif\",\r\n    \"fm\": \"application/vnd.framemaker\",\r\n    \"fh\": \"image/x-freehand\",\r\n    \"fsc\": \"application/vnd.fsc.weblaunch\",\r\n    \"fnc\": \"application/vnd.frogans.fnc\",\r\n    \"ltf\": \"application/vnd.frogans.ltf\",\r\n    \"ddd\": \"application/vnd.fujixerox.ddd\",\r\n    \"xdw\": \"application/vnd.fujixerox.docuworks\",\r\n    \"xbd\": \"application/vnd.fujixerox.docuworks.binder\",\r\n    \"oas\": \"application/vnd.fujitsu.oasys\",\r\n    \"oa2\": \"application/vnd.fujitsu.oasys2\",\r\n    \"oa3\": \"application/vnd.fujitsu.oasys3\",\r\n    \"fg5\": \"application/vnd.fujitsu.oasysgp\",\r\n    \"bh2\": \"application/vnd.fujitsu.oasysprs\",\r\n    \"spl\": \"application/x-futuresplash\",\r\n    \"fzs\": \"application/vnd.fuzzysheet\",\r\n    \"g3\": \"image/g3fax\",\r\n    \"gmx\": \"application/vnd.gmx\",\r\n    \"gtw\": \"model/vnd.gtw\",\r\n    \"txd\": \"application/vnd.genomatix.tuxedo\",\r\n    \"ggb\": \"application/vnd.geogebra.file\",\r\n    \"ggt\": \"application/vnd.geogebra.tool\",\r\n    \"gdl\": \"model/vnd.gdl\",\r\n    \"gex\": \"application/vnd.geometry-explorer\",\r\n    \"gxt\": \"application/vnd.geonext\",\r\n    \"g2w\": \"application/vnd.geoplan\",\r\n    \"g3w\": \"application/vnd.geospace\",\r\n    \"gsf\": \"application/x-font-ghostscript\",\r\n    \"bdf\": \"application/x-font-bdf\",\r\n    \"gtar\": \"application/x-gtar\",\r\n    \"texinfo\": \"application/x-texinfo\",\r\n    \"gnumeric\": \"application/x-gnumeric\",\r\n    \"kml\": \"application/vnd.google-earth.kml+xml\",\r\n    \"kmz\": \"application/vnd.google-earth.kmz\",\r\n    \"gqf\": \"application/vnd.grafeq\",\r\n    \"gif\": \"image/gif\",\r\n    \"gv\": \"text/vnd.graphviz\",\r\n    \"gac\": \"application/vnd.groove-account\",\r\n    \"ghf\": \"application/vnd.groove-help\",\r\n    \"gim\": \"application/vnd.groove-identity-message\",\r\n    \"grv\": \"application/vnd.groove-injector\",\r\n    \"gtm\": \"application/vnd.groove-tool-message\",\r\n    \"tpl\": \"application/vnd.groove-tool-template\",\r\n    \"vcg\": \"application/vnd.groove-vcard\",\r\n    \"h261\": \"video/h261\",\r\n    \"h263\": \"video/h263\",\r\n    \"h264\": \"video/h264\",\r\n    \"hpid\": \"application/vnd.hp-hpid\",\r\n    \"hps\": \"application/vnd.hp-hps\",\r\n    \"hdf\": \"application/x-hdf\",\r\n    \"rip\": \"audio/vnd.rip\",\r\n    \"hbci\": \"application/vnd.hbci\",\r\n    \"jlt\": \"application/vnd.hp-jlyt\",\r\n    \"pcl\": \"application/vnd.hp-pcl\",\r\n    \"hpgl\": \"application/vnd.hp-hpgl\",\r\n    \"hvs\": \"application/vnd.yamaha.hv-script\",\r\n    \"hvd\": \"application/vnd.yamaha.hv-dic\",\r\n    \"hvp\": \"application/vnd.yamaha.hv-voice\",\r\n    \"sfd-hdstx\": \"application/vnd.hydrostatix.sof-data\",\r\n    \"stk\": \"application/hyperstudio\",\r\n    \"hal\": \"application/vnd.hal+xml\",\r\n    \"html\": \"text/html\",\r\n    \"irm\": \"application/vnd.ibm.rights-management\",\r\n    \"sc\": \"application/vnd.ibm.secure-container\",\r\n    \"ics\": \"text/calendar\",\r\n    \"icc\": \"application/vnd.iccprofile\",\r\n    \"ico\": \"image/x-icon\",\r\n    \"igl\": \"application/vnd.igloader\",\r\n    \"ief\": \"image/ief\",\r\n    \"ivp\": \"application/vnd.immervision-ivp\",\r\n    \"ivu\": \"application/vnd.immervision-ivu\",\r\n    \"rif\": \"application/reginfo+xml\",\r\n    \"3dml\": \"text/vnd.in3d.3dml\",\r\n    \"spot\": \"text/vnd.in3d.spot\",\r\n    \"igs\": \"model/iges\",\r\n    \"i2g\": \"application/vnd.intergeo\",\r\n    \"cdy\": \"application/vnd.cinderella\",\r\n    \"xpw\": \"application/vnd.intercon.formnet\",\r\n    \"fcs\": \"application/vnd.isac.fcs\",\r\n    \"ipfix\": \"application/ipfix\",\r\n    \"cer\": \"application/pkix-cert\",\r\n    \"pki\": \"application/pkixcmp\",\r\n    \"crl\": \"application/pkix-crl\",\r\n    \"pkipath\": \"application/pkix-pkipath\",\r\n    \"igm\": \"application/vnd.insors.igm\",\r\n    \"rcprofile\": \"application/vnd.ipunplugged.rcprofile\",\r\n    \"irp\": \"application/vnd.irepository.package+xml\",\r\n    \"jad\": \"text/vnd.sun.j2me.app-descriptor\",\r\n    \"jar\": \"application/java-archive\",\r\n    \"class\": \"application/java-vm\",\r\n    \"jnlp\": \"application/x-java-jnlp-file\",\r\n    \"ser\": \"application/java-serialized-object\",\r\n    \"java\": \"text/x-java-source,java\",\r\n    \"js\": \"application/javascript\",\r\n    \"json\": \"application/json\",\r\n    \"joda\": \"application/vnd.joost.joda-archive\",\r\n    \"jpm\": \"video/jpm\",\r\n    \"jpeg\": \"image/x-citrix-jpeg\",\r\n    \"jpg\": \"image/x-citrix-jpeg\",\r\n    \"pjpeg\": \"image/pjpeg\",\r\n    \"jpgv\": \"video/jpeg\",\r\n    \"ktz\": \"application/vnd.kahootz\",\r\n    \"mmd\": \"application/vnd.chipnuts.karaoke-mmd\",\r\n    \"karbon\": \"application/vnd.kde.karbon\",\r\n    \"chrt\": \"application/vnd.kde.kchart\",\r\n    \"kfo\": \"application/vnd.kde.kformula\",\r\n    \"flw\": \"application/vnd.kde.kivio\",\r\n    \"kon\": \"application/vnd.kde.kontour\",\r\n    \"kpr\": \"application/vnd.kde.kpresenter\",\r\n    \"ksp\": \"application/vnd.kde.kspread\",\r\n    \"kwd\": \"application/vnd.kde.kword\",\r\n    \"htke\": \"application/vnd.kenameaapp\",\r\n    \"kia\": \"application/vnd.kidspiration\",\r\n    \"kne\": \"application/vnd.kinar\",\r\n    \"sse\": \"application/vnd.kodak-descriptor\",\r\n    \"lasxml\": \"application/vnd.las.las+xml\",\r\n    \"latex\": \"application/x-latex\",\r\n    \"lbd\": \"application/vnd.llamagraphics.life-balance.desktop\",\r\n    \"lbe\": \"application/vnd.llamagraphics.life-balance.exchange+xml\",\r\n    \"jam\": \"application/vnd.jam\",\r\n    \"123\": \"application/vnd.lotus-1-2-3\",\r\n    \"apr\": \"application/vnd.lotus-approach\",\r\n    \"pre\": \"application/vnd.lotus-freelance\",\r\n    \"nsf\": \"application/vnd.lotus-notes\",\r\n    \"org\": \"application/vnd.lotus-organizer\",\r\n    \"scm\": \"application/vnd.lotus-screencam\",\r\n    \"lwp\": \"application/vnd.lotus-wordpro\",\r\n    \"lvp\": \"audio/vnd.lucent.voice\",\r\n    \"m3u\": \"audio/x-mpegurl\",\r\n    \"m4v\": \"video/x-m4v\",\r\n    \"hqx\": \"application/mac-binhex40\",\r\n    \"portpkg\": \"application/vnd.macports.portpkg\",\r\n    \"mgp\": \"application/vnd.osgeo.mapguide.package\",\r\n    \"mrc\": \"application/marc\",\r\n    \"mrcx\": \"application/marcxml+xml\",\r\n    \"mxf\": \"application/mxf\",\r\n    \"nbp\": \"application/vnd.wolfram.player\",\r\n    \"ma\": \"application/mathematica\",\r\n    \"mathml\": \"application/mathml+xml\",\r\n    \"mbox\": \"application/mbox\",\r\n    \"mc1\": \"application/vnd.medcalcdata\",\r\n    \"mscml\": \"application/mediaservercontrol+xml\",\r\n    \"cdkey\": \"application/vnd.mediastation.cdkey\",\r\n    \"mwf\": \"application/vnd.mfer\",\r\n    \"mfm\": \"application/vnd.mfmp\",\r\n    \"msh\": \"model/mesh\",\r\n    \"mads\": \"application/mads+xml\",\r\n    \"mets\": \"application/mets+xml\",\r\n    \"mods\": \"application/mods+xml\",\r\n    \"meta4\": \"application/metalink4+xml\",\r\n    \"mcd\": \"application/vnd.mcd\",\r\n    \"flo\": \"application/vnd.micrografx.flo\",\r\n    \"igx\": \"application/vnd.micrografx.igx\",\r\n    \"es3\": \"application/vnd.eszigno3+xml\",\r\n    \"mdb\": \"application/x-msaccess\",\r\n    \"asf\": \"video/x-ms-asf\",\r\n    \"exe\": \"application/x-msdownload\",\r\n    \"cil\": \"application/vnd.ms-artgalry\",\r\n    \"cab\": \"application/vnd.ms-cab-compressed\",\r\n    \"ims\": \"application/vnd.ms-ims\",\r\n    \"application\": \"application/x-ms-application\",\r\n    \"clp\": \"application/x-msclip\",\r\n    \"mdi\": \"image/vnd.ms-modi\",\r\n    \"eot\": \"application/vnd.ms-fontobject\",\r\n    \"xls\": \"application/vnd.ms-excel\",\r\n    \"xlam\": \"application/vnd.ms-excel.addin.macroenabled.12\",\r\n    \"xlsb\": \"application/vnd.ms-excel.sheet.binary.macroenabled.12\",\r\n    \"xltm\": \"application/vnd.ms-excel.template.macroenabled.12\",\r\n    \"xlsm\": \"application/vnd.ms-excel.sheet.macroenabled.12\",\r\n    \"chm\": \"application/vnd.ms-htmlhelp\",\r\n    \"crd\": \"application/x-mscardfile\",\r\n    \"lrm\": \"application/vnd.ms-lrm\",\r\n    \"mvb\": \"application/x-msmediaview\",\r\n    \"mny\": \"application/x-msmoney\",\r\n    \"pptx\": \"application/vnd.openxmlformats-officedocument.presentationml.presentation\",\r\n    \"sldx\": \"application/vnd.openxmlformats-officedocument.presentationml.slide\",\r\n    \"ppsx\": \"application/vnd.openxmlformats-officedocument.presentationml.slideshow\",\r\n    \"potx\": \"application/vnd.openxmlformats-officedocument.presentationml.template\",\r\n    \"xlsx\": \"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet\",\r\n    \"xltx\": \"application/vnd.openxmlformats-officedocument.spreadsheetml.template\",\r\n    \"docx\": \"application/vnd.openxmlformats-officedocument.wordprocessingml.document\",\r\n    \"dotx\": \"application/vnd.openxmlformats-officedocument.wordprocessingml.template\",\r\n    \"obd\": \"application/x-msbinder\",\r\n    \"thmx\": \"application/vnd.ms-officetheme\",\r\n    \"onetoc\": \"application/onenote\",\r\n    \"pya\": \"audio/vnd.ms-playready.media.pya\",\r\n    \"pyv\": \"video/vnd.ms-playready.media.pyv\",\r\n    \"ppt\": \"application/vnd.ms-powerpoint\",\r\n    \"ppam\": \"application/vnd.ms-powerpoint.addin.macroenabled.12\",\r\n    \"sldm\": \"application/vnd.ms-powerpoint.slide.macroenabled.12\",\r\n    \"pptm\": \"application/vnd.ms-powerpoint.presentation.macroenabled.12\",\r\n    \"ppsm\": \"application/vnd.ms-powerpoint.slideshow.macroenabled.12\",\r\n    \"potm\": \"application/vnd.ms-powerpoint.template.macroenabled.12\",\r\n    \"mpp\": \"application/vnd.ms-project\",\r\n    \"pub\": \"application/x-mspublisher\",\r\n    \"scd\": \"application/x-msschedule\",\r\n    \"xap\": \"application/x-silverlight-app\",\r\n    \"stl\": \"application/vnd.ms-pki.stl\",\r\n    \"cat\": \"application/vnd.ms-pki.seccat\",\r\n    \"vsd\": \"application/vnd.visio\",\r\n    \"vsdx\": \"application/vnd.visio2013\",\r\n    \"wm\": \"video/x-ms-wm\",\r\n    \"wma\": \"audio/x-ms-wma\",\r\n    \"wax\": \"audio/x-ms-wax\",\r\n    \"wmx\": \"video/x-ms-wmx\",\r\n    \"wmd\": \"application/x-ms-wmd\",\r\n    \"wpl\": \"application/vnd.ms-wpl\",\r\n    \"wmz\": \"application/x-ms-wmz\",\r\n    \"wmv\": \"video/x-ms-wmv\",\r\n    \"wvx\": \"video/x-ms-wvx\",\r\n    \"wmf\": \"application/x-msmetafile\",\r\n    \"trm\": \"application/x-msterminal\",\r\n    \"doc\": \"application/msword\",\r\n    \"docm\": \"application/vnd.ms-word.document.macroenabled.12\",\r\n    \"dotm\": \"application/vnd.ms-word.template.macroenabled.12\",\r\n    \"wri\": \"application/x-mswrite\",\r\n    \"wps\": \"application/vnd.ms-works\",\r\n    \"xbap\": \"application/x-ms-xbap\",\r\n    \"xps\": \"application/vnd.ms-xpsdocument\",\r\n    \"mid\": \"audio/midi\",\r\n    \"mpy\": \"application/vnd.ibm.minipay\",\r\n    \"afp\": \"application/vnd.ibm.modcap\",\r\n    \"rms\": \"application/vnd.jcp.javame.midlet-rms\",\r\n    \"tmo\": \"application/vnd.tmobile-livetv\",\r\n    \"prc\": \"application/x-mobipocket-ebook\",\r\n    \"mbk\": \"application/vnd.mobius.mbk\",\r\n    \"dis\": \"application/vnd.mobius.dis\",\r\n    \"plc\": \"application/vnd.mobius.plc\",\r\n    \"mqy\": \"application/vnd.mobius.mqy\",\r\n    \"msl\": \"application/vnd.mobius.msl\",\r\n    \"txf\": \"application/vnd.mobius.txf\",\r\n    \"daf\": \"application/vnd.mobius.daf\",\r\n    \"fly\": \"text/vnd.fly\",\r\n    \"mpc\": \"application/vnd.mophun.certificate\",\r\n    \"mpn\": \"application/vnd.mophun.application\",\r\n    \"mj2\": \"video/mj2\",\r\n    \"mpga\": \"audio/mpeg\",\r\n    \"mxu\": \"video/vnd.mpegurl\",\r\n    \"mpeg\": \"video/mpeg\",\r\n    \"m21\": \"application/mp21\",\r\n    \"mp4a\": \"audio/mp4\",\r\n    \"mp4\": \"video/mp4\",\r\n    \"m3u8\": \"application/vnd.apple.mpegurl\",\r\n    \"mus\": \"application/vnd.musician\",\r\n    \"msty\": \"application/vnd.muvee.style\",\r\n    \"mxml\": \"application/xv+xml\",\r\n    \"ngdat\": \"application/vnd.nokia.n-gage.data\",\r\n    \"n-gage\": \"application/vnd.nokia.n-gage.symbian.install\",\r\n    \"ncx\": \"application/x-dtbncx+xml\",\r\n    \"nc\": \"application/x-netcdf\",\r\n    \"nlu\": \"application/vnd.neurolanguage.nlu\",\r\n    \"dna\": \"application/vnd.dna\",\r\n    \"nnd\": \"application/vnd.noblenet-directory\",\r\n    \"nns\": \"application/vnd.noblenet-sealer\",\r\n    \"nnw\": \"application/vnd.noblenet-web\",\r\n    \"rpst\": \"application/vnd.nokia.radio-preset\",\r\n    \"rpss\": \"application/vnd.nokia.radio-presets\",\r\n    \"n3\": \"text/n3\",\r\n    \"edm\": \"application/vnd.novadigm.edm\",\r\n    \"edx\": \"application/vnd.novadigm.edx\",\r\n    \"ext\": \"application/vnd.novadigm.ext\",\r\n    \"gph\": \"application/vnd.flographit\",\r\n    \"ecelp4800\": \"audio/vnd.nuera.ecelp4800\",\r\n    \"ecelp7470\": \"audio/vnd.nuera.ecelp7470\",\r\n    \"ecelp9600\": \"audio/vnd.nuera.ecelp9600\",\r\n    \"oda\": \"application/oda\",\r\n    \"ogx\": \"application/ogg\",\r\n    \"oga\": \"audio/ogg\",\r\n    \"ogv\": \"video/ogg\",\r\n    \"dd2\": \"application/vnd.oma.dd2+xml\",\r\n    \"oth\": \"application/vnd.oasis.opendocument.text-web\",\r\n    \"opf\": \"application/oebps-package+xml\",\r\n    \"qbo\": \"application/vnd.intu.qbo\",\r\n    \"oxt\": \"application/vnd.openofficeorg.extension\",\r\n    \"osf\": \"application/vnd.yamaha.openscoreformat\",\r\n    \"weba\": \"audio/webm\",\r\n    \"webm\": \"video/webm\",\r\n    \"odc\": \"application/vnd.oasis.opendocument.chart\",\r\n    \"otc\": \"application/vnd.oasis.opendocument.chart-template\",\r\n    \"odb\": \"application/vnd.oasis.opendocument.database\",\r\n    \"odf\": \"application/vnd.oasis.opendocument.formula\",\r\n    \"odft\": \"application/vnd.oasis.opendocument.formula-template\",\r\n    \"odg\": \"application/vnd.oasis.opendocument.graphics\",\r\n    \"otg\": \"application/vnd.oasis.opendocument.graphics-template\",\r\n    \"odi\": \"application/vnd.oasis.opendocument.image\",\r\n    \"oti\": \"application/vnd.oasis.opendocument.image-template\",\r\n    \"odp\": \"application/vnd.oasis.opendocument.presentation\",\r\n    \"otp\": \"application/vnd.oasis.opendocument.presentation-template\",\r\n    \"ods\": \"application/vnd.oasis.opendocument.spreadsheet\",\r\n    \"ots\": \"application/vnd.oasis.opendocument.spreadsheet-template\",\r\n    \"odt\": \"application/vnd.oasis.opendocument.text\",\r\n    \"odm\": \"application/vnd.oasis.opendocument.text-master\",\r\n    \"ott\": \"application/vnd.oasis.opendocument.text-template\",\r\n    \"ktx\": \"image/ktx\",\r\n    \"sxc\": \"application/vnd.sun.xml.calc\",\r\n    \"stc\": \"application/vnd.sun.xml.calc.template\",\r\n    \"sxd\": \"application/vnd.sun.xml.draw\",\r\n    \"std\": \"application/vnd.sun.xml.draw.template\",\r\n    \"sxi\": \"application/vnd.sun.xml.impress\",\r\n    \"sti\": \"application/vnd.sun.xml.impress.template\",\r\n    \"sxm\": \"application/vnd.sun.xml.math\",\r\n    \"sxw\": \"application/vnd.sun.xml.writer\",\r\n    \"sxg\": \"application/vnd.sun.xml.writer.global\",\r\n    \"stw\": \"application/vnd.sun.xml.writer.template\",\r\n    \"otf\": \"application/x-font-otf\",\r\n    \"osfpvg\": \"application/vnd.yamaha.openscoreformat.osfpvg+xml\",\r\n    \"dp\": \"application/vnd.osgi.dp\",\r\n    \"pdb\": \"application/vnd.palm\",\r\n    \"p\": \"text/x-pascal\",\r\n    \"paw\": \"application/vnd.pawaafile\",\r\n    \"pclxl\": \"application/vnd.hp-pclxl\",\r\n    \"efif\": \"application/vnd.picsel\",\r\n    \"pcx\": \"image/x-pcx\",\r\n    \"psd\": \"image/vnd.adobe.photoshop\",\r\n    \"prf\": \"application/pics-rules\",\r\n    \"pic\": \"image/x-pict\",\r\n    \"chat\": \"application/x-chat\",\r\n    \"p10\": \"application/pkcs10\",\r\n    \"p12\": \"application/x-pkcs12\",\r\n    \"p7m\": \"application/pkcs7-mime\",\r\n    \"p7s\": \"application/pkcs7-signature\",\r\n    \"p7r\": \"application/x-pkcs7-certreqresp\",\r\n    \"p7b\": \"application/x-pkcs7-certificates\",\r\n    \"p8\": \"application/pkcs8\",\r\n    \"plf\": \"application/vnd.pocketlearn\",\r\n    \"pnm\": \"image/x-portable-anymap\",\r\n    \"pbm\": \"image/x-portable-bitmap\",\r\n    \"pcf\": \"application/x-font-pcf\",\r\n    \"pfr\": \"application/font-tdpfr\",\r\n    \"pgn\": \"application/x-chess-pgn\",\r\n    \"pgm\": \"image/x-portable-graymap\",\r\n    \"png\": \"image/x-png\",\r\n    \"ppm\": \"image/x-portable-pixmap\",\r\n    \"pskcxml\": \"application/pskc+xml\",\r\n    \"pml\": \"application/vnd.ctc-posml\",\r\n    \"ai\": \"application/postscript\",\r\n    \"pfa\": \"application/x-font-type1\",\r\n    \"pbd\": \"application/vnd.powerbuilder6\",\r\n    \"pgp\": \"application/pgp-signature\",\r\n    \"box\": \"application/vnd.previewsystems.box\",\r\n    \"ptid\": \"application/vnd.pvi.ptid1\",\r\n    \"pls\": \"application/pls+xml\",\r\n    \"str\": \"application/vnd.pg.format\",\r\n    \"ei6\": \"application/vnd.pg.osasli\",\r\n    \"dsc\": \"text/prs.lines.tag\",\r\n    \"psf\": \"application/x-font-linux-psf\",\r\n    \"qps\": \"application/vnd.publishare-delta-tree\",\r\n    \"wg\": \"application/vnd.pmi.widget\",\r\n    \"qxd\": \"application/vnd.quark.quarkxpress\",\r\n    \"esf\": \"application/vnd.epson.esf\",\r\n    \"msf\": \"application/vnd.epson.msf\",\r\n    \"ssf\": \"application/vnd.epson.ssf\",\r\n    \"qam\": \"application/vnd.epson.quickanime\",\r\n    \"qfx\": \"application/vnd.intu.qfx\",\r\n    \"qt\": \"video/quicktime\",\r\n    \"rar\": \"application/x-rar-compressed\",\r\n    \"ram\": \"audio/x-pn-realaudio\",\r\n    \"rmp\": \"audio/x-pn-realaudio-plugin\",\r\n    \"rsd\": \"application/rsd+xml\",\r\n    \"rm\": \"application/vnd.rn-realmedia\",\r\n    \"bed\": \"application/vnd.realvnc.bed\",\r\n    \"mxl\": \"application/vnd.recordare.musicxml\",\r\n    \"musicxml\": \"application/vnd.recordare.musicxml+xml\",\r\n    \"rnc\": \"application/relax-ng-compact-syntax\",\r\n    \"rdz\": \"application/vnd.data-vision.rdz\",\r\n    \"rdf\": \"application/rdf+xml\",\r\n    \"rp9\": \"application/vnd.cloanto.rp9\",\r\n    \"jisp\": \"application/vnd.jisp\",\r\n    \"rtf\": \"application/rtf\",\r\n    \"rtx\": \"text/richtext\",\r\n    \"link66\": \"application/vnd.route66.link66+xml\",\r\n    \"rss\": \"application/rss+xml\",\r\n    \"shf\": \"application/shf+xml\",\r\n    \"st\": \"application/vnd.sailingtracker.track\",\r\n    \"svg\": \"image/svg+xml\",\r\n    \"sus\": \"application/vnd.sus-calendar\",\r\n    \"sru\": \"application/sru+xml\",\r\n    \"setpay\": \"application/set-payment-initiation\",\r\n    \"setreg\": \"application/set-registration-initiation\",\r\n    \"sema\": \"application/vnd.sema\",\r\n    \"semd\": \"application/vnd.semd\",\r\n    \"semf\": \"application/vnd.semf\",\r\n    \"see\": \"application/vnd.seemail\",\r\n    \"snf\": \"application/x-font-snf\",\r\n    \"spq\": \"application/scvp-vp-request\",\r\n    \"spp\": \"application/scvp-vp-response\",\r\n    \"scq\": \"application/scvp-cv-request\",\r\n    \"scs\": \"application/scvp-cv-response\",\r\n    \"sdp\": \"application/sdp\",\r\n    \"etx\": \"text/x-setext\",\r\n    \"movie\": \"video/x-sgi-movie\",\r\n    \"ifm\": \"application/vnd.shana.informed.formdata\",\r\n    \"itp\": \"application/vnd.shana.informed.formtemplate\",\r\n    \"iif\": \"application/vnd.shana.informed.interchange\",\r\n    \"ipk\": \"application/vnd.shana.informed.package\",\r\n    \"tfi\": \"application/thraud+xml\",\r\n    \"shar\": \"application/x-shar\",\r\n    \"rgb\": \"image/x-rgb\",\r\n    \"slt\": \"application/vnd.epson.salt\",\r\n    \"aso\": \"application/vnd.accpac.simply.aso\",\r\n    \"imp\": \"application/vnd.accpac.simply.imp\",\r\n    \"twd\": \"application/vnd.simtech-mindmapper\",\r\n    \"csp\": \"application/vnd.commonspace\",\r\n    \"saf\": \"application/vnd.yamaha.smaf-audio\",\r\n    \"mmf\": \"application/vnd.smaf\",\r\n    \"spf\": \"application/vnd.yamaha.smaf-phrase\",\r\n    \"teacher\": \"application/vnd.smart.teacher\",\r\n    \"svd\": \"application/vnd.svd\",\r\n    \"rq\": \"application/sparql-query\",\r\n    \"srx\": \"application/sparql-results+xml\",\r\n    \"gram\": \"application/srgs\",\r\n    \"grxml\": \"application/srgs+xml\",\r\n    \"ssml\": \"application/ssml+xml\",\r\n    \"skp\": \"application/vnd.koan\",\r\n    \"sgml\": \"text/sgml\",\r\n    \"sdc\": \"application/vnd.stardivision.calc\",\r\n    \"sda\": \"application/vnd.stardivision.draw\",\r\n    \"sdd\": \"application/vnd.stardivision.impress\",\r\n    \"smf\": \"application/vnd.stardivision.math\",\r\n    \"sdw\": \"application/vnd.stardivision.writer\",\r\n    \"sgl\": \"application/vnd.stardivision.writer-global\",\r\n    \"sm\": \"application/vnd.stepmania.stepchart\",\r\n    \"sit\": \"application/x-stuffit\",\r\n    \"sitx\": \"application/x-stuffitx\",\r\n    \"sdkm\": \"application/vnd.solent.sdkm+xml\",\r\n    \"xo\": \"application/vnd.olpc-sugar\",\r\n    \"au\": \"audio/basic\",\r\n    \"wqd\": \"application/vnd.wqd\",\r\n    \"sis\": \"application/vnd.symbian.install\",\r\n    \"smi\": \"application/smil+xml\",\r\n    \"xsm\": \"application/vnd.syncml+xml\",\r\n    \"bdm\": \"application/vnd.syncml.dm+wbxml\",\r\n    \"xdm\": \"application/vnd.syncml.dm+xml\",\r\n    \"sv4cpio\": \"application/x-sv4cpio\",\r\n    \"sv4crc\": \"application/x-sv4crc\",\r\n    \"sbml\": \"application/sbml+xml\",\r\n    \"tsv\": \"text/tab-separated-values\",\r\n    \"tiff\": \"image/tiff\",\r\n    \"tao\": \"application/vnd.tao.intent-module-archive\",\r\n    \"tar\": \"application/x-tar\",\r\n    \"tcl\": \"application/x-tcl\",\r\n    \"tex\": \"application/x-tex\",\r\n    \"tfm\": \"application/x-tex-tfm\",\r\n    \"tei\": \"application/tei+xml\",\r\n    \"txt\": \"text/plain\",\r\n    \"dxp\": \"application/vnd.spotfire.dxp\",\r\n    \"sfs\": \"application/vnd.spotfire.sfs\",\r\n    \"tsd\": \"application/timestamped-data\",\r\n    \"tpt\": \"application/vnd.trid.tpt\",\r\n    \"mxs\": \"application/vnd.triscape.mxs\",\r\n    \"t\": \"text/troff\",\r\n    \"tra\": \"application/vnd.trueapp\",\r\n    \"ttf\": \"application/x-font-ttf\",\r\n    \"ttl\": \"text/turtle\",\r\n    \"umj\": \"application/vnd.umajin\",\r\n    \"uoml\": \"application/vnd.uoml+xml\",\r\n    \"unityweb\": \"application/vnd.unity\",\r\n    \"ufd\": \"application/vnd.ufdl\",\r\n    \"uri\": \"text/uri-list\",\r\n    \"utz\": \"application/vnd.uiq.theme\",\r\n    \"ustar\": \"application/x-ustar\",\r\n    \"uu\": \"text/x-uuencode\",\r\n    \"vcs\": \"text/x-vcalendar\",\r\n    \"vcf\": \"text/x-vcard\",\r\n    \"vcd\": \"application/x-cdlink\",\r\n    \"vsf\": \"application/vnd.vsf\",\r\n    \"wrl\": \"model/vrml\",\r\n    \"vcx\": \"application/vnd.vcx\",\r\n    \"mts\": \"model/vnd.mts\",\r\n    \"vtu\": \"model/vnd.vtu\",\r\n    \"vis\": \"application/vnd.visionary\",\r\n    \"viv\": \"video/vnd.vivo\",\r\n    \"ccxml\": \"application/ccxml+xml,\",\r\n    \"vxml\": \"application/voicexml+xml\",\r\n    \"src\": \"application/x-wais-source\",\r\n    \"wbxml\": \"application/vnd.wap.wbxml\",\r\n    \"wbmp\": \"image/vnd.wap.wbmp\",\r\n    \"wav\": \"audio/x-wav\",\r\n    \"davmount\": \"application/davmount+xml\",\r\n    \"woff\": \"application/x-font-woff\",\r\n    \"wspolicy\": \"application/wspolicy+xml\",\r\n    \"webp\": \"image/webp\",\r\n    \"wtb\": \"application/vnd.webturbo\",\r\n    \"wgt\": \"application/widget\",\r\n    \"hlp\": \"application/winhlp\",\r\n    \"wml\": \"text/vnd.wap.wml\",\r\n    \"wmls\": \"text/vnd.wap.wmlscript\",\r\n    \"wmlsc\": \"application/vnd.wap.wmlscriptc\",\r\n    \"wpd\": \"application/vnd.wordperfect\",\r\n    \"stf\": \"application/vnd.wt.stf\",\r\n    \"wsdl\": \"application/wsdl+xml\",\r\n    \"xbm\": \"image/x-xbitmap\",\r\n    \"xpm\": \"image/x-xpixmap\",\r\n    \"xwd\": \"image/x-xwindowdump\",\r\n    \"der\": \"application/x-x509-ca-cert\",\r\n    \"fig\": \"application/x-xfig\",\r\n    \"xhtml\": \"application/xhtml+xml\",\r\n    \"xml\": \"application/xml\",\r\n    \"xdf\": \"application/xcap-diff+xml\",\r\n    \"xenc\": \"application/xenc+xml\",\r\n    \"xer\": \"application/patch-ops-error+xml\",\r\n    \"rl\": \"application/resource-lists+xml\",\r\n    \"rs\": \"application/rls-services+xml\",\r\n    \"rld\": \"application/resource-lists-diff+xml\",\r\n    \"xslt\": \"application/xslt+xml\",\r\n    \"xop\": \"application/xop+xml\",\r\n    \"xpi\": \"application/x-xpinstall\",\r\n    \"xspf\": \"application/xspf+xml\",\r\n    \"xul\": \"application/vnd.mozilla.xul+xml\",\r\n    \"xyz\": \"chemical/x-xyz\",\r\n    \"yaml\": \"text/yaml\",\r\n    \"yang\": \"application/yang\",\r\n    \"yin\": \"application/yin+xml\",\r\n    \"zir\": \"application/vnd.zul\",\r\n    \"zip\": \"application/zip\",\r\n    \"zmm\": \"application/vnd.handheld-entertainment+xml\",\r\n    \"zaz\": \"application/vnd.zzazz.deck+xml\"\r\n}";
            _MIMETypes = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonMIME);
        }
        public string GetMIMEType(string fileExtension)
        {
            return MIMETypes.FirstOrDefault(x => x.Key == fileExtension).Value;
        }


        public NexusResult GetTransformedBodyOfMedcomMessage(NexusAPI api, string endpointURL)
        {
            NexusResult nexusResult = new NexusResult();
            return nexusResult.GetTransformedBodyOfMedcomMessage(api, endpointURL);
        }
        public NexusResult CallAPI(NexusAPI api, string endpointURL, Method callMethod, string JsonBody = null)
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
            SortedDictionary<string, string> keyValuePairs = new SortedDictionary<string, string>();
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

            KeyValuePair<string, string> pair = new KeyValuePair<string, string>(key, value);

            return pair;
        }

        public SortedDictionary<string, string> GetNestedData(SortedDictionary<string, string> dict, string dataName)
        {
            JObject dataOutput = JObject.Parse(dict[dataName]);
            return dataHandler.ConvertJsonToSortedDictionary(dataOutput);
        }

        public void AddResource(SortedDictionary<string, string> dict, string linkName, string href)
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

        internal void GetDischargeReportData_FutureAgreements(TransformedBody_Root transformedBody, HtmlHandler handler, string transformedBodyHTML)
        {
            string htmlResult = handler.GetResultFromHtml(transformedBodyHTML, "Fremtidige aftaler");
            var domObject = handler.CreateDomObject(htmlResult).FirstElement();
            int numberOfElements = domObject.ChildElements.Count();

            transformedBody.FutureAgreements = domObject.ChildElements.ElementAt(1).ChildElements.ElementAt(0).FirstChild.ToString();
        }

        internal void GetDischargeReportData_AgreementsRegardingDietFirstDayAfterDischarge(TransformedBody_Root transformedBody, HtmlHandler handler, string transformedBodyHTML)
        {
            string htmlResult = handler.GetResultFromHtml(transformedBodyHTML, "Aftaler omkring kost første døgn efter udskrivning");
            var domObject = handler.CreateDomObject(htmlResult).FirstElement();
            int numberOfElements = domObject.ChildElements.Count();

            for (int i = 0; i < numberOfElements; i++)
            {
                var dataRowElement = domObject.ChildElements.ElementAt(i);
                switch (i)
                {
                    case 0://Madpakke gives med
                        if (dataRowElement.ChildElements.ElementAt(1).FirstChild.ToString().Contains("Ja"))
                        {
                            transformedBody.AgreementsRegardingDietTheFirstDayAfterDischarge.PackedLunchProvided = true;
                        }
                        else
                        {
                            transformedBody.AgreementsRegardingDietTheFirstDayAfterDischarge.PackedLunchProvided = false;
                        }
                        break;
                    case 1: //Aftalt indkøb på udskrivelesdagen
                        if (dataRowElement.ChildElements.ElementAt(1).FirstChild.ToString().Contains("Ja"))
                        {
                            transformedBody.AgreementsRegardingDietTheFirstDayAfterDischarge.AgreedPurchasesOnTheDayOfDischarge = true;
                        }
                        else
                        {
                            transformedBody.AgreementsRegardingDietTheFirstDayAfterDischarge.AgreedPurchasesOnTheDayOfDischarge = false;
                        }
                        break;
                    default:
                        break;
                }
            }


        }

        internal void GetDischargeReportData_MedicationInformationRelatedToDischarge(TransformedBody_Root transformedBody, HtmlHandler handler, string transformedBodyHTML)
        {
            string htmlResult = handler.GetResultFromHtml(transformedBodyHTML, "Medicin information relateret til udskrivning");
            var domObject = handler.CreateDomObject(htmlResult).FirstElement();
            int numberOfElements = domObject.ChildElements.Count();

            for (int i = 0; i < domObject.ChildElements.Count(); i++)
            {
                // Not all MedicationInformation has comments and therefore has different amounts of <tr>
                // So we need to make sure which we are working on, and adding data to the transformedBody object
                var rowElement = domObject.ChildElements.ElementAt(i);
                int countOfChildElements = rowElement.ChildElements.Count();
                string informationTitle = rowElement.ChildElements.ElementAt(0).FirstChild.ToString();
                if (countOfChildElements == 2)
                {
                    //Header will be in the first element
                    //Value will be in the second element
                    switch (informationTitle)
                    {
                        case "Medsendt medicin":
                            transformedBody.MedicationInformationRelatedToDischarge.EnclosedMedicationDate = rowElement.ChildElements.ElementAt(1).FirstChild.ToString();
                            break;
                        case "Recept til apotek":
                            if (rowElement.ChildElements.ElementAt(1).FirstChild.ToString().Contains("Ja"))
                            {
                                transformedBody.MedicationInformationRelatedToDischarge.PrescriptionForPharmacy = true;
                            }
                            else
                            {
                                transformedBody.MedicationInformationRelatedToDischarge.PrescriptionForPharmacy = false;
                            }
                            break;
                        case "Afhentning/Udbringning aftalt":
                            if (rowElement.ChildElements.ElementAt(1).FirstChild.ToString().Contains("Ja"))
                            {
                                transformedBody.MedicationInformationRelatedToDischarge.PickupDeliveryAgreed = true;
                            }
                            else
                            {
                                transformedBody.MedicationInformationRelatedToDischarge.PickupDeliveryAgreed = false;
                            }
                            break;
                        case "Dosisdispensering genbestilt":
                            if (rowElement.ChildElements.ElementAt(1).FirstChild.ToString().Contains("Ja"))
                            {
                                transformedBody.MedicationInformationRelatedToDischarge.DoseDispensingReordered = true;
                            }
                            else
                            {
                                transformedBody.MedicationInformationRelatedToDischarge.DoseDispensingReordered = false;
                            }
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    // If only 1 element is present, it will be the header, and the value will be in the next <tr> element
                    switch (informationTitle)
                    {
                        case "Kommentar til medsent medicin":
                            transformedBody.MedicationInformationRelatedToDischarge.CommentsForEnclosedMedication = domObject.ChildElements.ElementAt(i + 1).ChildElements.ElementAt(0).FirstChild.ToString();
                            i++; // We add 1 to i as the next element in the iteration will be the value <tr> we just added to the transformedBody.MedicationInformationRelatedToDischarge.CommentsForEnclosedMedication property
                            break;
                        default:
                            throw new Exception("Header of \"" + informationTitle + "\" can't be handled. Please add it to GetDischargeReportData_MedicationInformationRelatedToDischarge.");
                    }
                }
            }
        }

        internal void GetDischargeReportData_MostRecentMedicationAdministration(TransformedBody_Root transformedBody, HtmlHandler handler, string transformedBodyHTML)
        {
            string htmlResult = handler.GetResultFromHtml(transformedBodyHTML, "Seneste medicingivning");
            var domObject = handler.CreateDomObject(htmlResult).FirstElement();
            int numberOfElements = domObject.ChildElements.Count();

            var dataRowElement = domObject.ChildElements.ElementAt(1); // contains 2 elements - values for latest depot and pn

            for (int i = 0; i < dataRowElement.ChildElements.Count(); i++)
            {
                CQ HtmlDomObject = handler.GetDecodedHTMLWithoutUnwantedTags(dataRowElement.ChildElements.ElementAt(i).OuterHTML);
                // if element i index 0, we add new dom object.FirstChild.ToString() to the respective property of the transformedBody.MostRecentMedicationAdministration
                switch (i)
                {
                    case 0:
                        if (HtmlDomObject.ElementAt(0).FirstChild != null)
                        {
                            transformedBody.MostRecentMedicationAdministration.LatestDepotMedicationAdministration = HtmlDomObject.ElementAt(0).FirstChild.ToString();
                        }
                        break;
                    case 1:
                        if (HtmlDomObject.FirstElement().FirstChild != null)
                        {
                            transformedBody.MostRecentMedicationAdministration.LatestPnMedicationAdministration = HtmlDomObject.FirstElement().FirstChild.ToString();
                        }
                        break;
                    default:
                        break;
                }


            }


        }


        internal void GetDischargeReportData_NursingProfessionalProblemAreas(TransformedBody_Root transformedBody, HtmlHandler handler, string transformedBodyHTML)
        {
            string htmlResult = handler.GetResultFromHtml(transformedBodyHTML, "Sygeplejefaglige problemområder");
            var domObject = handler.CreateDomObject(htmlResult).FirstElement();
            int numberOfElements = domObject.ChildElements.Count();



            for (int i = 0; i < numberOfElements; i++)
            {
                TransformedBody_NursingProfessionalProblemAreas problemArea = new TransformedBody_NursingProfessionalProblemAreas();
                var rowElement = domObject.ChildElements.ElementAt(i);
                var rowElementIPlus1 = domObject.ChildElements.ElementAt(i + 1);

                problemArea.Title = rowElement.ChildElements.ElementAt(0).FirstChild.ToString();
                CQ newDomObject = handler.GetDecodedHTMLWithoutUnwantedTags(rowElementIPlus1.ChildElements.ElementAt(0).OuterHTML);

                problemArea.Value = newDomObject.ElementAt(0).FirstChild.ToString();

                transformedBody.NursingProfessionalProblemAreas.Add(problemArea);
                i++; // We add 1 to i as header is in row 1 and value is in row 2. By adding 1 to i, we skip the value <tr> at next iteration 
            }
        }

        internal void GetDischargeReportData_FunctionalAbilitiesAtDischarge(TransformedBody_Root transformedBody, HtmlHandler handler, string transformedBodyHTML)
        {
            string htmlResult = handler.GetResultFromHtml(transformedBodyHTML, "Funktionsevner ved udskrivelse");
            var domObject = handler.CreateDomObject(htmlResult).FirstElement();
            int numberOfElements = domObject.ChildElements.Count();

            for (int i = 0; i < numberOfElements; i++)
            {
                TransformedBody_FunctionalAbilitiesAtDischarge ability = new TransformedBody_FunctionalAbilitiesAtDischarge();
                var rowElement = domObject.ChildElements.ElementAt(i);

                for (int j = 0; j < rowElement.ChildElements.Count(); j++)
                {
                    if (rowElement.ChildElements.ElementAt(j).FirstChild != null) // If element value is null, we do nothing
                    {
                        string value = rowElement.ChildElements.ElementAt(j).FirstChild.ToString();
                        switch (j)
                        {
                            case 0: // Function
                                ability.Function = value;
                                break;
                            case 1: // Score
                                ability.Score = value;
                                break;
                            case 2: // Description
                                ability.Description = value;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {

                    }
                }
                transformedBody.FunctionalAbilitiesAtDischarge.Add(ability);
            }
        }

        internal void GetDischargeReportData_Diagnoses(TransformedBody_Root transformedBody, HtmlHandler handler, string transformedBodyHTML)
        {
            string htmlResult = handler.GetResultFromHtml(transformedBodyHTML, "Diagnoser");
            var domObject = handler.CreateDomObject(htmlResult).FirstElement().ChildElements.ElementAt(1);// We use the first element, as that has the tbody with values. index 0 contains the headers
            int numberOfElements = domObject.ChildElements.Count();

            for (int i = 0; i < numberOfElements; i++)
            {
                TransformedBody_Diagnoses diagnose = new TransformedBody_Diagnoses();
                var rowElement = domObject.ChildElements.ElementAt(i);

                for (int j = 0; j < rowElement.ChildElements.Count(); j++)
                {
                    string value = rowElement.ChildElements.ElementAt(j).FirstChild.ToString();
                    switch (j)
                    {
                        case 0: // Diagnose code
                            diagnose.DiagnoseCode = value;
                            break;
                        case 1: // Classification
                            diagnose.Classification = value;
                            break;
                        case 2: // Description
                            diagnose.Description = value;
                            break;
                        default:
                            break;
                    }
                }
                transformedBody.Diagnoses.Add(diagnose);
            }
        }

        internal void GetDischargeReportData_CurrentAdmission(TransformedBody_Root transformedBody, HtmlHandler handler, string transformedBodyHTML)
        {
            string htmlResult = handler.GetResultFromHtml(transformedBodyHTML, "Aktuel indlæggelse");
            var domObject = handler.CreateDomObject(htmlResult).FirstElement().ChildElements;
            int numberOfElements = domObject.Count();

            // domObject is now the tbody containing x <tr>'s
            for (int i = 0; i < numberOfElements; i++)
            {
                var rowElement = domObject.ElementAt(i);
                switch (i) // <tr> of i
                {
                    // If it is one of the first 3 rows we use the <td> element 1 that has the value we want.
                    case 0: // time of admission
                        // ChildElements are the <td>'s
                        // ------||-----.ElementAt(1) is the second <td>
                        // ------------||------------.FirstChild is the value it self from the <td> - and we return it as string
                        transformedBody.CurrentAdmission.TimeOfAdmission = rowElement.ChildElements.ElementAt(1).FirstChild.ToString();
                        break;
                    case 1: // time of treatment done
                        transformedBody.CurrentAdmission.TimeOfTreatmentDone = rowElement.ChildElements.ElementAt(1).FirstChild.ToString();
                        break;
                    case 2: // time of discharge
                        transformedBody.CurrentAdmission.TimeOfDischarge = rowElement.ChildElements.ElementAt(1).FirstChild.ToString();
                        break;
                    //case 4: // reason for admission
                    //    transformedBody.CurrentAdmission.ReasonForAdmission = rowElement.ChildElements.ElementAt(0).FirstChild.ToString();
                    //    break;
                    //case 6: // admission process
                    //    transformedBody.CurrentAdmission.AdmissionProcess = rowElement.ChildElements.ElementAt(0).FirstChild.ToString();
                    //    break;
                    //case 8: // contamination risk
                    //    transformedBody.CurrentAdmission.ContaminatioRisk = rowElement.ChildElements.ElementAt(0).FirstChild.ToString();
                    //    break;
                    default:
                        // Here we handle the last rows of data.
                        // We need to check the current row data (header) and populate the apropriate property on the object
                        string header = rowElement.ChildElements.ElementAt(0).FirstChild.ToString();
                        string propertyValue = domObject.ElementAt(i + 1).ChildElements.ElementAt(0).FirstChild.ToString();
                        switch (header)
                        {
                            case "Årsag til aktuel indlæggelse":
                                transformedBody.CurrentAdmission.ReasonForAdmission = propertyValue;
                                break;
                            case "Indlæggelseforløb":
                                transformedBody.CurrentAdmission.AdmissionProcess = propertyValue;
                                break;
                            case "Smitterisiko":
                                transformedBody.CurrentAdmission.InfectionRisk = propertyValue;
                                break;
                            default:
                                break;
                        }
                        i++; // We increase the value of i by 1 as we need to skip the propertyValue from this check. We only check the header
                        break;
                }
            }
        }

        internal void GetDischargeReportData_Relatives(TransformedBody_Root transformedBody, HtmlHandler handler, string transformedBodyHTML)
        {
            string htmlResult = handler.GetResultFromHtmlSelectNextUntil(transformedBodyHTML, "Pårørende/Relationer", "Aktuel indlæggelse");
            var domObject = handler.CreateDomObject(htmlResult);


            for (int i = 1; i < domObject.Elements.Count(); i++)
            {
                // We start at index 1, as index 0 is the header we were searching for, and don't need it
                TransformedBody_Relavites relative = new TransformedBody_Relavites();
                List<TransformedBody_ContactInformation> contactInformationList = new List<TransformedBody_ContactInformation>();
                if (i != domObject.Elements.Count() - 1)
                {
                    // We handle the element of i
                    var table = domObject.ElementAt(i);
                    var tbody = table.ChildElements.First();
                    int tbodyElementsCount = tbody.ChildElements.Count();


                    for (int j = 0; j < tbodyElementsCount; j++)
                    {
                        // Here we handle every <tr> in the tbody

                        var tr = tbody.ChildElements.ElementAt(j); // Elements of the <tr> will have x <td>'s

                        if (j == 0)
                        {
                            // First element = the type and value of the contact - eg. type = parent
                            GetDischargeReportData_Relatives_GetType(tr.ChildElements.ElementAt(1).FirstChild.ToString(), relative);
                        }
                        else
                        {
                            if (j != tbodyElementsCount - 1)
                            {
                                // We handle all contact information - eg. name, phone etc.
                                contactInformationList = GetDischargeReportData_Relatives_GetNameAndContactInfo(tr.ChildElements.ElementAt(0), relative);
                            }
                            else
                            {
                                // We handle the last element which is "has been informed" = true/false
                                GetDischargeReportData_Relatives_GetIsInformed(tr.ChildElements.ElementAt(1).FirstChild.ToString(), relative);
                            }
                        }

                    }
                    if (contactInformationList.Count != 0)
                    {
                        contactInformationList.ForEach(relative.ContactInformation.Add);
                    }
                }
                else
                {
                    // We handle the last table containing comments for the relatives
                    GetDischargeReportData_Relatives_GetCommentsForRelatives(domObject.ElementAt(i), transformedBody);
                }

                if (relative.Name != null)
                {
                    transformedBody.Relatives.Add(relative);
                }
            }


        }

        private List<TransformedBody_ContactInformation> GetDischargeReportData_Relatives_GetNameAndContactInfo(IDomElement tdHTMLString, TransformedBody_Relavites relative)
        {
            var td = tdHTMLString;
            var contactInfoTable = td.ChildElements.First();
            var contactInfoTBody = contactInfoTable.ChildElements.First();
            List<TransformedBody_ContactInformation> contactiInformationList = new List<TransformedBody_ContactInformation>();

            for (int k = 0; k < contactInfoTBody.ChildElements.Count(); k++)
            {
                // This is each <tr> of the table
                var contactInfoTBodyTr = contactInfoTBody.ChildElements.ElementAt(k);
                if (k == 0)
                {
                    // This will be the first contact info row containing name title in <td> at index 0 and name value in <td> at index 1
                    var contactInfoTBodyTrTd = contactInfoTBodyTr.ChildElements.ElementAt(1);
                    relative.Name = contactInfoTBodyTrTd.FirstChild.ToString();
                }
                else
                {
                    // If k is not 0 then we will have phone numbers etc.  with title in <td> at index 0 and value in <td> at index 1, 2, 3 etc.
                    TransformedBody_ContactInformation contactInformation = new TransformedBody_ContactInformation();
                    for (int l = 0; l < contactInfoTBodyTr.ChildElements.Count(); l++)
                    {
                        // This is each <td> in the table
                        var tdValue = contactInfoTBodyTr.ChildElements.ElementAt(l).FirstChild.ToString();
                        if (l == 0)
                        {
                            // Then it is the title - eg. Name, Phone etc.
                            contactInformation.Title = tdValue;
                        }
                        else
                        {
                            // This is the value of the title - eg. Hans, +45 12345678 etc.
                            contactInformation.Value = tdValue;
                        }
                    }
                    contactiInformationList.Add(contactInformation);
                }


            }
            return contactiInformationList;
        }

        internal void GetDischargeReportData_Relatives_GetIsInformed(string tdStringHTMLString, TransformedBody_Relavites relative)
        {
            var relativeContactInformedTd = tdStringHTMLString; // We use index 1, as index 0 is the header
            if (relativeContactInformedTd.ToLower().Contains("ja"))
            {
                relative.IsInformed = true;
            }
            else
            {
                relative.IsInformed = false;
            }
        }

        internal void GetDischargeReportData_Relatives_GetCommentsForRelatives(IDomObject domObject, TransformedBody_Root transformedBody)
        {
            var relativeCommentsTable = domObject;
            var relativeCommentsTbody = relativeCommentsTable.ChildElements.First();
            var relativeCommentsTr = relativeCommentsTbody.ChildElements.ElementAt(1); // We use the element at 1, as the element at 0 is the header
            var relativeCommentsTdString = relativeCommentsTr.ChildElements.First().FirstChild.ToString();

            transformedBody.CommentsForRelatives = relativeCommentsTdString;
        }

        internal void GetDischargeReportData_Relatives_GetType(string typeTdHTMLString, TransformedBody_Relavites relative)
        {
            var typeTd = typeTdHTMLString;
            relative.Type = typeTd;
        }

        
        internal PatientDetailsSearch_Patient UpdatePatient(string updateEndpoint, string jsonString)
        {
            var webResult = CallAPI(this, updateEndpoint, Method.Put, jsonString);
            return JsonConvert.DeserializeObject<PatientDetailsSearch_Patient>(webResult.Result.ToString());
        }

        internal List<PatientDetailsSearch_PatientState> GetAvailablePatientStates(PatientDetailsSearch_Patient patient)
        {
            var webResult = CallAPI(this, patient.Links.AvailablePatientStates.Href, Method.Get);
            return JsonConvert.DeserializeObject<List<PatientDetailsSearch_PatientState>>(webResult.Result.ToString());
        }

        internal List<PatientDetailsSearch_PatientState> GetAvailablePatientStates(string availablePatientStateLink)
        {
            var webResult = CallAPI(this, availablePatientStateLink, Method.Get);
            return JsonConvert.DeserializeObject<List<PatientDetailsSearch_PatientState>>(webResult.Result.ToString());
        }

        /// <summary>
        /// Returns the patient state, or null if statusName input is not an option
        /// </summary>
        /// <param name="availablePatientStates"></param>
        /// <param name="statusName"></param>
        /// <returns></returns>
        internal PatientDetailsSearch_PatientState ChoosePatientState(List<PatientDetailsSearch_PatientState> availablePatientStates, string statusName)
        {
            PatientDetailsSearch_PatientState chosenState = new PatientDetailsSearch_PatientState();
            foreach (var item in availablePatientStates)
            {
                if (item.Name == statusName)
                {
                    chosenState = item;
                    break;
                }
            }
            if (chosenState.Id != null)
            {
                return chosenState;
            }
            else
            {
                return null;
            }
        }

        





        #endregion Shared methods



    }
}
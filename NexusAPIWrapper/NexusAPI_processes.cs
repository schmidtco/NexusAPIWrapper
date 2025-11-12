using AngleSharp.Text;
using CsQuery.Engine.PseudoClassSelectors;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Presentation;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.SqlServer.Server;
using MimeKit;
using Newtonsoft.Json;
using NexusAPIWrapper.Custom_classes;
using NexusAPIWrapper.Custom_classes.FS3NewConditions.OldNewConditions;
using NexusAPIWrapper.HomeRessource.Preferences.ACTIVITYLIST.ACTIVITYLIST_Content.Content.Pages.Links.Content._Root.Links.ReferencedObject._Root.Links.TransformedBody;
using NexusAPIWrapper.RKSQLRPA01DataSetTableAdapters;
using Org.BouncyCastle.Asn1.X509;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static NexusAPIWrapper.MethodAttributes;

namespace NexusAPIWrapper
{
    public class NexusAPI_processes
    {
        #region Properties

        public NexusAPI api; // public so you can access all API methods from the processes
        private List<Professional_Root> _professionalsList;


        public List<Professional_Root> professionalsList
        {
            get => _professionalsList;
            //set => professionalsList = value;
        }


        #endregion Properties

        #region Constructors

        public NexusAPI_processes() : this("review")
        { }
        public NexusAPI_processes(string environment)
        {
            api = new NexusAPI(environment);
            _professionalsList = new List<Professional_Root>();
        }
        public NexusAPI_processes(bool manualSetup)
        {
            api = new NexusAPI(manualSetup);
            _professionalsList = new List<Professional_Root>();
        }

        #endregion Constructors

        #region Property methods

        /// <summary>
        /// Returns the list of professionals, set by other methods.
        /// It also empties the list, making it ready for other methods to use.
        /// </summary>
        /// <returns></returns>
        public List<Professional_Root> GetProfessionalsList()
        {
            List<Professional_Root> professionals = new List<Professional_Root>();
            professionals = professionalsList.ToList();

            professionalsList.Clear();
            return professionals;
        }


        #endregion Property methods

        #region processes

        #region Getting/Returning processes

        //public List<Content_Page> GetDeadOrInactiveCitizens()
        //{
        //    string listName = "Døde/inaktive borgere med aktive forløb i kommunen";
        //    return GetCitizenList(listName);
        //}
        public List<Content_Page_Root> GetDeadOrInactiveCitizens()
        {
            string listName = "Døde/inaktive borgere med aktive forløb i kommunen";

            List<Content_Page> citizenList = GetCitizenList(listName);
            List<Content_Page_Root> fullList = new List<Content_Page_Root>();
            foreach (var page in citizenList)
            {
                //Getting the endpoint to load the citizens on the X page
                string endpoint = page.Links.PatientData.Href;
                //Calling the API to aget a result
                var webResult = api.CallAPI(api, endpoint, Method.Get);
                //Converting the result into a class we can work on
                var result = JsonConvert.DeserializeObject<List<Content_Page_Root>>(webResult.Result.ToString());
                foreach (var item in result)
                {
                    fullList.Add(item);
                }
            }

            return fullList;
        }

        /// <summary>
        /// Get the content/citizens on the list specified. Run the api.GetPreferencesCitizenLists to get available lists
        /// </summary>
        /// <param name="listName"></param>
        /// <returns>List of content pages containing PatientData and PatientGrantInformation</returns>
        public List<Content_Page> GetCitizenList(string listName)
        {
            var citizenlistSelf = api.GetPreferencesCitizenListSelf(listName);
            var citizenlistContent = api.GetPreferencesCitizenListSelfContent(listName);

            return citizenlistContent.Pages;
        }




        /// <summary>
        /// Enrolls the patient/citizen to the specified program pathway (Grundforløb)
        /// </summary>
        /// <param name="citizenCPR"></param>
        /// <param name="programPathway"></param>
        public PatientEnrolled_Root OpretGrundforloeb(string citizenCPR, string programPathway)
        {
            return EnrollPatientToProgramPathway(citizenCPR, programPathway);
        }
        /// <summary>
        /// Enrolls the patient/citizen to the specified program pathway (Grundforløb)
        /// </summary>
        /// <param name="citizenCPR"></param>
        /// <param name="programPathway"></param>
        public PatientEnrolled_Root EnrollPatientToProgramPathway(string citizenCPR, string programPathway)
        {
            var programPathwayEnrollmentLink = api.GetProgramPathwayEnrollmentLink(citizenCPR, programPathway);

            if (programPathwayEnrollmentLink != null)
            {
                var result = api.CallAPI(api, programPathwayEnrollmentLink, Method.Put);
                return JsonConvert.DeserializeObject<PatientEnrolled_Root>(result.Result.ToString());
            }
            else
            {
                throw new Exception("Program pathway is not a possible choice");
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="citizenCPR"></param>
        /// <param name="programPathway"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public PatientEnrolled_Root EnrollPatientToProgramPathway(PatientDetailsSearch_Patient patient, string programPathway)
        {
            var programPathwayEnrollmentLink = api.GetProgramPathwayEnrollmentLink(patient, programPathway);

            if (programPathwayEnrollmentLink != null)
            {
                var result = api.CallAPI(api, programPathwayEnrollmentLink, Method.Put);
                return JsonConvert.DeserializeObject<PatientEnrolled_Root>(result.Result.ToString());
            }
            else
            {
                throw new Exception("Program pathway is not a possible choice");
            }

        }



        /// <summary>
        /// Getting a list of document objects. From the list, the self link of each object can be used to get the complete object where a reference link can be used to download the file.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pathwayName"></param>
        /// <param name="returnAllDocuments"></param>
        /// <param name="childPathwayName"></param>
        /// <returns></returns>
        public List<PathwayReferences_Child> GetCitizenDocumentObjects(int id, string pathwayName, bool returnAllDocuments, string childPathwayName = null)
        {
            // NOT FINISHED
            var pathwayReferences = api.GetCitizenPathwayReferences(id, pathwayName);


            // Children contains elements/documents directly on the pathway ("documentReference"), AND childpathways that contain elements/documents ("patientPathwayReference")
            var children = pathwayReferences[0].Children;
            List<PathwayReferences_Child> elementsList = new List<PathwayReferences_Child>();

            switch (childPathwayName)
            {
                case null:
                    // No child pathway input.
                    // Returning objects are for all children elements/sub children
                    switch (returnAllDocuments)
                    {
                        case true:
                            // We want to return all documents 
                            foreach (var child in children)
                            {
                                switch (child.Type)
                                {
                                    case "documentReference":
                                        // we add it to the list of elements to return
                                        elementsList.Add(child);
                                        break;
                                    case "patientPathwayReference":
                                        api.GetPathwayReferencesChildrenElements(elementsList, child);
                                        break;
                                }
                            }
                            break;
                        case false:
                            // We want to return only documents directly on the pathway
                            foreach (var child in children)
                            {
                                switch (child.Type)
                                {
                                    case "documentReference":
                                        // we add it to the list of elements to return
                                        elementsList.Add(child);
                                        break;
                                }
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    // child pathway input given.
                    // We only work on that child, but can still return all documents from that child -> down the tree

                    foreach (var child in children)
                    {
                        if (child.Name == childPathwayName)
                        {
                            foreach (var subChild in child.Children)
                            {
                                switch (subChild.Type)
                                {
                                    case "documentReference":
                                        // we add it to the list of elements to return
                                        elementsList.Add(subChild);
                                        break;
                                    case "patientPathwayReference":
                                        api.GetPathwayReferencesChildrenElements(elementsList, subChild);
                                        break;
                                }
                            }
                            break;
                        }
                    }

                    break;
            }



            return elementsList;
        }

        /// <summary>
        /// Returning the professional object class
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Professional_Root GetProfessional(int id)
        {
            string professionalsLink = api.GetHomeRessourceLink("professionals");
            var professional = api.CallAPI(api, professionalsLink + "/" + id, Method.Get);

            // Convert to class object
            return JsonConvert.DeserializeObject<Professional_Root>(professional.Result.ToString());
        }


        public List<Professional_Root> GetProfessionals()
        {
            List<Professional_Root> professionals = new List<Professional_Root>();
            return professionals;
        }

        /// <summary>
        /// Returning the professional object class
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Professional_Root> GetProfessionals(string name, bool includeInactiveProfessionals)
        {
            List<Professional_Root> professionals = new List<Professional_Root>();
            var professionalsList = api.GetProfessionals(name);

            foreach (var professional in professionalsList)
            {

                if (includeInactiveProfessionals == true)
                {
                    // Add object to the list
                    professionals.Add(professional);
                }
                else
                {
                    if (professional.Active == true)
                    {
                        // Add object to the list
                        professionals.Add(professional);
                    }
                }
            }
            return professionals;
        }


        //public List<PatientConditions_Root> GetPatientConditions(string citizenCPR)
        //{
        //    var patient = api.GetPatientDetails(citizenCPR);
        //    var links = patient.Links;

        //    var result = api.CallAPI(api, links.PatientConditions.Href, Method.Get);
        //    var patientConditions = JsonConvert.DeserializeObject<List<PatientConditions_Root>>(result.Result.ToString());
        //    return patientConditions;
        //}
        

        #region Patient conditions
        public List<PatientConditions_Root> GetPatientConditions(string citizenCPR)
        {
            var patient = api.GetPatientDetails(citizenCPR);

            return GetPatientConditions(patient);
        }
        public List<PatientConditions_Root> GetPatientConditions(string citizenCPR, bool ActiveConditionsOnly)
        {
            var patient = api.GetPatientDetails(citizenCPR);
            return GetPatientConditions(patient, ActiveConditionsOnly);
        }

        public List<PatientConditions_Root> GetPatientConditions(PatientDetailsSearch_Patient patient)
        {
            string patientConditionsLink = patient.Links.PatientConditions.Href;

            var result = api.CallAPI(api, patientConditionsLink, Method.Get);
            return JsonConvert.DeserializeObject<List<PatientConditions_Root>>(result.Result.ToString());
        }
        public List<PatientConditions_Root> GetPatientConditions(PatientDetailsSearch_Patient patient, bool ActiveConditionsOnly)
        {
            string patientConditionsLink = patient.Links.PatientConditions.Href;
            var result = api.CallAPI(api, patientConditionsLink, Method.Get);
            var conditions = JsonConvert.DeserializeObject<List<PatientConditions_Root>>(result.Result.ToString());

            if (ActiveConditionsOnly)
            {
                List<PatientConditions_Root> activeConditions = new List<PatientConditions_Root>();
                foreach (var condition in conditions)
                {
                    if (condition.Status.ToLower() == "active")
                    {
                        activeConditions.Add(condition);
                    }
                }
                return activeConditions;
            }
            else
            {
                return conditions;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="borgerTilstand">patientCondition</param>
        /// <returns>liste over aktive opgaver på tilstanden</returns>
        public List<PatCond_Assign_Root> HentBorgerTilstand_AktiveOpgaver(PatientConditions_Root borgerTilstand)
        {
            return GetPatientCondition_ActiveAssignments(borgerTilstand);
        }
        public List<PatCond_Assign_Root> GetPatientCondition_ActiveAssignments(PatientConditions_Root patientCondition)
        {
            string activeAssignmentLink = patientCondition.Links.ActiveAssignments.Href;
            var result = api.CallAPI(api, activeAssignmentLink, Method.Get);
            var activeAssignments = JsonConvert.DeserializeObject<List<PatCond_Assign_Root>>(result.Result.ToString());
            return activeAssignments;
        }

        public PatCond_Observ_Root HentBorgerTilstand_NuværendeObservationer(PatientConditions_Root borgerTilstand)
        {
            return GetPatientCondition_CurrentObservations(borgerTilstand);
        }
        public PatCond_Observ_Root GetPatientCondition_CurrentObservations(PatientConditions_Root patientCondition)
        {
            string currentObservationsLink = patientCondition.Links.CurrentObservations.Href;
            var result = api.CallAPI(api, currentObservationsLink, Method.Get);
            var currentObservations = JsonConvert.DeserializeObject<PatCond_Observ_Root>(result.Result.ToString());
            return currentObservations;
        }

        public List<PatCond_Observ_Root> HentBorgerTilstand_AlleObservationer(PatientConditions_Root borgerTilstand)
        {
            return GetPatientCondition_AllObservations(borgerTilstand);
        }
        public List<PatCond_Observ_Root> GetPatientCondition_AllObservations(PatientConditions_Root patientCondition)
        {
            string allObservationsLink = patientCondition.Links.AllObservations.Href;
            var result = api.CallAPI(api, allObservationsLink, Method.Get);
            var allObservations = JsonConvert.DeserializeObject<List<PatCond_Observ_Root>>(result.Result.ToString());
            return allObservations;
        }

        public List<PatCond_RelActi_Root> HentBorgerTilstand_RelateredeAktiviteter(PatientConditions_Root borgerTilstand)
        {
            return GetPatientCondition_RelatedActivities(borgerTilstand);
        }
        public List<PatCond_RelActi_Root> GetPatientCondition_RelatedActivities(PatientConditions_Root patientCondition)
        {
            string RelatedActivitiesLink = patientCondition.Links.RelatedActivities.Href;
            var result = api.CallAPI(api, RelatedActivitiesLink, Method.Get);
            var relatedActivities = JsonConvert.DeserializeObject<List<PatCond_RelActi_Root>>(result.Result.ToString());
            return relatedActivities;
        }

        public List<PatCond_RelActiWHist_Root> HentBorgerTilstand_RelateredeAktiviteterMedHistorik(PatientConditions_Root borgerTilstand)
        {
            return GetPatientCondition_RelatedActivitiesWithHistory(borgerTilstand);
        }
        public List<PatCond_RelActiWHist_Root> GetPatientCondition_RelatedActivitiesWithHistory(PatientConditions_Root patientCondition)
        {
            string RelatedActivitiesWithHistoryLink = patientCondition.Links.RelatedActivitiesWithHistory.Href;
            var result = api.CallAPI(api, RelatedActivitiesWithHistoryLink, Method.Get);
            var relatedActivitiesWithHistory = JsonConvert.DeserializeObject<List<PatCond_RelActiWHist_Root>>(result.Result.ToString());
            return relatedActivitiesWithHistory;
        }
        /// <summary>
        /// Henter opgaver på en given tilstand
        /// </summary>
        /// <param name="patientTilstand">Tilstanden du gerne vil hente opgaver på</param>
        /// <param name="aktiveOpgaverKun">Vil du kun have aktive opgaver hentet</param>
        /// <returns></returns>
        public List<PatCond_Assign_Root> Hent_Patient_Tilstandsopgaver(PatientConditions_Root patientTilstand, bool aktiveOpgaverKun)
        {
            return GetPatientConditionAssignments(patientTilstand, aktiveOpgaverKun);
        }
        /// <summary>
        /// Returns assignments on a citizens condition
        /// </summary>
        /// <param name="patientCondition"></param>
        /// <param name="activeAssignmentsOnly"></param>
        /// <returns></returns>
        public List<PatCond_Assign_Root> GetPatientConditionAssignments(PatientConditions_Root patientCondition, bool activeAssignmentsOnly)
        {

            var conditionLinks = patientCondition.Links;
            string assignmentsLink = conditionLinks.Assignments.Href;
            string activeAssignmentsLink = conditionLinks.ActiveAssignments.Href;

            var assignmentsResult = api.CallAPI(api, assignmentsLink, Method.Get);
            var activeAssignmentsResult = api.CallAPI(api, activeAssignmentsLink, Method.Get);

            List<PatCond_Assign_Root> assignments = JsonConvert.DeserializeObject<List<PatCond_Assign_Root>>(assignmentsResult.Result.ToString());
            List<PatCond_Assign_Root> activeAssignments = JsonConvert.DeserializeObject<List<PatCond_Assign_Root>>(activeAssignmentsResult.Result.ToString());

            if (activeAssignmentsOnly)
            {
                return activeAssignments;
            }
            else
            {
                return assignments;
            }
        }
        #endregion Patient conditions
        ///// <summary>
        ///// This returns the Id of the organization, which is used in the organizations update method (POST)
        ///// </summary>
        ///// <param name="organizationsTree"></param>
        ///// <param name="organizationName"></param>
        ///// <returns></returns>
        //public int GetOrganizationId(string organizationName)
        //{
        //    return api.ReturnOrgId(api.GetOrganizationsTree().Children,organizationName);
        //}

        #endregion Getting/Returning processes

        #region ExcelReader

        private List<string> GetRowData(Row row, WorkbookPart workbookPart)
        {
            List<string> rowData = new List<string>();
            SharedStringTable sharedStringTable = workbookPart.SharedStringTablePart?.SharedStringTable;

            foreach (Cell cell in row.Elements<Cell>())
            {
                string cellValue = string.Empty;

                if (cell.DataType != null && cell.DataType == CellValues.SharedString)
                {
                    int stringId = int.Parse(cell.InnerText);
                    cellValue = sharedStringTable.ElementAt(stringId).InnerText;
                }
                else if (cell.CellValue != null)
                {
                    cellValue = cell.CellValue.InnerText;
                }

                rowData.Add(cellValue);
            }

            return rowData;
        }
        private HashSet<string> GetUsernamesFromOS2Vikar(string filePathOS2Vikar)
        {
            HashSet<string> usernames = new HashSet<string>();

            using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(filePathOS2Vikar, false))
            {
                WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

                // Skip header row and process data rows
                foreach (Row row in sheetData.Elements<Row>().Skip(1))
                {
                    List<string> rowData = GetRowData(row, workbookPart);

                    // Assume username is in column index 8 (same as first sheet)
                    if (rowData.Count >= 2)
                    {
                        string username = rowData[1];
                        if (!string.IsNullOrEmpty(username))
                        {
                            usernames.Add(username);
                        }
                    }
                }
            }

            return usernames;
        }
        #endregion ExcelReader

        #region Professionals

        /// <summary>
        /// Takes a list from SOFD with employees, loops the list, checks if employee is inactive in Nexus and not in the list from OS2Vikar. If so, the CPR and KMD vagplan is removed.
        /// </summary>
        /// <param name="fullFilePathSOFD">full file path of SOFD file <param>
        /// <param name="filePathOS2Vikar">full file path of OS2 vikar file</param>
        internal void Remove_CPR_And_KMD_vagtplan_from_professionals(string fullFilePathSOFD, string filePathOS2Vikar)
        {
            {
                // Read second Excel file to get list of usernames
                HashSet<string> validUsernames = GetUsernamesFromOS2Vikar(filePathOS2Vikar);

                using (SpreadsheetDocument spreadsheetDocument = SpreadsheetDocument.Open(fullFilePathSOFD, false))
                {
                    WorkbookPart workbookPart = spreadsheetDocument.WorkbookPart;
                    WorksheetPart worksheetPart = workbookPart.WorksheetParts.First();
                    SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().First();

                    // Skip header row and process data rows
                    foreach (Row row in sheetData.Elements<Row>().Skip(1))
                    {
                        List<string> rowData = GetRowData(row, workbookPart);

                        // Check if row has enough columns and get accountType (column index 10)
                        if (rowData.Count > 10)
                        {
                            string accountType = rowData[9];
                            if (accountType == "Active Directory")
                            {
                                string professionalUsername = rowData[7]; // Column index 8

                                // Check if username exists in second sheet
                                if (!validUsernames.Contains(professionalUsername))
                                {
                                    var professionalResult = api.GetProfessionals(professionalUsername);
                                    if (professionalResult.Count == 1)
                                    {
                                        var professional = professionalResult[0];
                                        if (professional.Active == false)
                                        {
                                            Remove_CPR_And_KMD_vagtplan(professional);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

        }

        internal void Remove_CPR_And_KMD_vagtplan(ProfessionalConfiguration_Root professionalConfigurationObject)
        {
            // You can't remove CPR unless you also remove the KmdVagplanConfiguration Extra CPR
            professionalConfigurationObject.Cpr = "";
            professionalConfigurationObject.KmdVagtplanConfiguration.CprExtra = null;
            professionalConfigurationObject.Active = false;

            this.UpdateProfessional(professionalConfigurationObject.Links.Update.Href, JsonConvert.SerializeObject(professionalConfigurationObject));
        }
        internal void Remove_CPR_And_KMD_vagtplan(Professional_Root professional)
        {
            var professionalConfigurationObject = this.api.GetProfessionalConfiguration(professional.Id);
            Remove_CPR_And_KMD_vagtplan(professionalConfigurationObject);
        }
        /// <summary>
        /// Deactivates professionals in Nexus if they are not present on the list of substitutes in OS2 Vikar. Columns to have are (A): Substitute name (B): Vikxxxx
        /// </summary>
        /// <param name="fullFilePath">Full path to the file downloaded from OS2 Vikar</param>
        internal void RemoveCPRAndStsSNFromProfessional_FJERN_VIKARER_FRA_NEXUS(string fullFilePath)
        {
            //NexusAPI_processes processes = new NexusAPI_processes("live");
            //var api = processes.api;
            string queryString = "vik1";
            // Get all VIK professionals
            var professionals = this.GetProfessionals(queryString, false);
            // Load list of active professionals
            string filePath = fullFilePath;
            string connString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};Extended Properties='Excel 12.0 Xml;HDR=YES;'";

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

                    // loop VIK professionals and check if they are in the list
                    foreach (var vikUser in professionals)
                    {
                        if (vikUser.Initials.Contains("vik")) // if the initials doesn't contain vik, we don't do anything 
                        {
                            if (vikUser.Initials == "vik1465")
                            {
                                var dkj = "0";
                            }
                            bool vikUserFound = false;
                            foreach (DataRow row in dt.Rows)
                            {

                                var vikValue = row.ItemArray[1];

                                if (vikUser.Initials.ToString() == vikValue.ToString())
                                {
                                    vikUserFound = true;
                                }
                            }
                            if (!vikUserFound)
                            {
                                var vikUserConfig = api.GetProfessionalConfiguration(vikUser.Id);
                                Remove_CPR_And_KMD_vagtplan(vikUserConfig);
                                
                                // remove StsSn
                                var result = api.CallAPI(api, vikUserConfig.Links.DeleteStsSn.Href, Method.Delete);
                                Debug.WriteLine("CPR and UUID removed for " + vikUser.Initials);
                                //Debug.WriteLine(vikUser.Initials + " has CPR: " + vikUserConfig.Cpr);
                            }
                        }

                    }

                }

            }
        }
        public void ActivateInactiveSubstituteProfessionals()
        {
            var professionalsList = api.GetProfessionals("vik");

            foreach (var professional in professionalsList)
            {
                if (professional.Initials.Length > 2)// Making sure that the key length is more than 2. Otherwise we can't check the first 3 chars.
                {
                    if (professional.Initials.Substring(0, 3) == "vik")// then it's a substitute professional, and we activate if inactive
                    {
                        ActivateProfessional(professional.Id);
                        _professionalsList.Add(professional);
                    }
                }
            }
        }


        public void ActivateProfessional(int id)
        {
            ProfessionalActivation(id, true);
        }
        public void DeactivateProfessional(int id)
        {
            ProfessionalActivation(id, false);
        }

        /// <summary>
        /// Activating or deactivating the professional.
        /// "true" will activate
        /// "false" will deactivate
        /// </summary>
        /// <param name="id"></param>
        public void ProfessionalActivation(int id, bool activate)
        {
            var professionalObject = api.GetProfessionalConfiguration(id);

            switch (activate)
            {
                case true:
                    professionalObject.Active = true;
                    break;
                case false:
                    professionalObject.Active = false;
                    break;
                default:
                    throw new ArgumentException("State not valid");
            }

            // Convert back to JSON string
            string professionalJsonStringObject = JsonConvert.SerializeObject(professionalObject);

            // Get update endpoint
            string updateEndpoint = professionalObject.Links.Update.Href;

            // Call update professional api with PUT method
            api.result.UpdateProfessional(api, updateEndpoint, professionalJsonStringObject, Method.Put);
        }

        /// <summary>
        /// Sets the professionals job title - NOT role
        /// </summary>
        /// <param name="professionalId">The id number</param>
        /// <param name="professionalJobTitle">The job title as a string. Needs to be part of the available job titles.</param>
        public void SetProfessionalJobTitle(int professionalId, string professionalJobTitle)
        {
            var professionalObject = api.GetProfessionalConfiguration(professionalId);
            var professional = GetProfessional(professionalId);
            var professionalJobActive = JsonConvert.DeserializeObject<ProfessionalJobs_Root>(professionalObject.ProfessionalJob.ToString());
            //var professionalJobId = GetProfessionalJobId(professionalId,professionalJobTitle);

            // Get the job that we want to update the professional to have
            var professionalJob = GetProfessionalJob(professionalId, professionalJobTitle);

            // Set the professionalObject to have the new job object.
            professionalObject.ProfessionalJob = professionalJob;

            // Convert the professionalObject back to JSON string
            string professionalJsonStringObject = JsonConvert.SerializeObject(professionalObject);

            // Get update endpoint
            string updateEndpoint = professionalObject.Links.Update.Href;

            // Call update professional api with PUT method - POST is not allowed
            UpdateProfessional(updateEndpoint, Method.Put, professionalJsonStringObject);
        }

        /// <summary>
        /// This will set the primary organization to be the one set in the private property of the NexusAPI class.
        /// </summary>
        /// <param name="professionalId"></param>
        public void SetProfessionalPrimaryOrganization(int professionalId)
        {
            // Get professional configuration object
            var professionalObject = api.GetProfessionalConfiguration(professionalId);

            // Get primary organization
            var primaryOrganization = api.GetProfessionalPrimaryOrganization(professionalId);

            // Set professionalObject primary organization to the organization object
            professionalObject.PrimaryOrganization = primaryOrganization;

            // Convert the professionalObject back to JSON string
            string professionalJsonStringObject = JsonConvert.SerializeObject(professionalObject);

            // Get update endpoint
            string updateEndpoint = professionalObject.Links.Update.Href;

            // Call update professional api with PUT method - POST is not allowed
            UpdateProfessional(updateEndpoint, Method.Put, professionalJsonStringObject);
        }

        /// <summary>
        /// This will set the primary organization to be the one given as an input string.
        /// </summary>
        /// <param name="professionalId"></param>
        /// <param name="organizationName">Organization name has to be valid. If not, the primary organization will be set to "ikke defineret".</param>
        public void SetProfessionalPrimaryOrganization(int professionalId, string organizationName)
        {
            // Get professional configuration object
            var professionalObject = api.GetProfessionalConfiguration(professionalId);

            // Get primary organization based on input
            var primaryOrganization = api.GetProfessionalPrimaryOrganization(professionalId, organizationName);

            // Set professionalObject primary organization to the organization object
            professionalObject.PrimaryOrganization = primaryOrganization;

            // Convert the professionalObject back to JSON string
            string professionalJsonStringObject = JsonConvert.SerializeObject(professionalObject);

            // Get update endpoint
            string updateEndpoint = professionalObject.Links.Update.Href;

            // Call update professional api with PUT method - POST is not allowed
            UpdateProfessional(updateEndpoint, Method.Put, professionalJsonStringObject);
        }



        /// <summary>
        /// Returns all the available jobs the professional can be assigned
        /// </summary>
        /// <param name="professionalId">the Id of the professional</param>
        /// <returns></returns>
        public List<ProfessionalJobs_Root> GetPossibleProfessionalJobs(int professionalId)
        {
            var professional = GetProfessional(professionalId);
            //var currentRoles = api.CallAPI(api, professional.Links.Roles.Href, Method.Get);
            var possibleProfessionalJobs = api.CallAPI(api, professional.Links.AvailableProfessionalJobs.Href, Method.Get);

            return JsonConvert.DeserializeObject<List<ProfessionalJobs_Root>>(possibleProfessionalJobs.Result.ToString());
        }

        public int GetProfessionalJobId(int professionalId, string professionalJobTitle)
        {
            //var jobs = GetPossibleProfessionalJobs(professionalId);
            //ProfessionalJobs_Root job = jobs[professionalJobTitle];

            //return job.Id;
            return GetProfessionalJob(professionalId, professionalJobTitle).Id;
        }
        public ProfessionalJobs_Root GetProfessionalJob(int professionalId, string professionalJobTitle)
        {
            var jobs = GetPossibleProfessionalJobs(professionalId);
            ProfessionalJobs_Root chosenJob = new ProfessionalJobs_Root();
            foreach (var job in jobs)
            {
                if (job.Name == professionalJobTitle)
                {
                    chosenJob = job;
                    break;
                }
            }
            return chosenJob;
        }


        /// <summary>
        /// Updating a specific professional
        /// </summary>
        /// <param name="updateEndpoint">Endpoint URL for the update</param>
        /// <param name="method">PUT, POST etc.</param>
        /// <param name="added">Ids to be added</param>
        /// <param name="removed">Ids to be removed</param>
        public void UpdateProfessional(string updateEndpoint, Method method, string added = null, string removed = null)
        {
            api.result.UpdateProfessional(api, updateEndpoint, api.GetAddedRemovedRequestBody(added, removed), method);
        }

        /// <summary>
        /// Updating a professional with a requestBody string
        /// </summary>
        /// <param name="updateEndpoint"></param>
        /// <param name="requestBody"></param>
        public void UpdateProfessional(string updateEndpoint, string requestBody)
        {
            api.result.UpdateProfessional(api, updateEndpoint, requestBody, Method.Put);
        }
        /// <summary>
        /// Updating a professional with a requestBody string
        /// </summary>
        /// <param name="updateEndpoint"></param>
        /// <param name="method"></param>
        /// <param name="requestBody"></param>
        public void UpdateProfessional(string updateEndpoint, Method method, string requestBody)
        {
            api.result.UpdateProfessional(api, updateEndpoint, requestBody, method);
        }

        /// <summary>
        /// Updating a specific professional with organization ids specified.
        /// You can remove ids that are not active/set.
        /// You CAN'T add ids that are already active/set. It will result in a bad request.
        /// </summary>
        /// <param name="added">Comma separated list of organization Ids to be added</param>
        /// <param name="removed">Comma separated list of organization Ids to be removed</param>
        public void UpdateProfessionalOrganizations(int professionalId, string added = null, string removed = null)
        {
            Professional_Root professional = api.GetProfessional(professionalId);
            string updateEndpoint = professional.Links.UpdateOrganizations.Href;

            UpdateProfessional(updateEndpoint, Method.Post, added, removed);
        }



        #endregion Professionals

        #region Shared processes



        /// <summary>
        /// Sends an email through the specified SMTP relay server
        /// </summary>
        /// <param name="To">Comma separated list of receivers</param>
        /// <param name="subject">Subject to be added</param>
        /// <param name="body">Body text of the e-mail</param>
        /// <param name="From">Left null will use the default "noreply" e-mail</param>
        public void SendEmail(string To, string subject, string body, string FromEmail = null, string FromSenderName = null)
        {
            string SMTPRelayServer = "";
            var smtpClient = new SmtpClient(SMTPRelayServer);
            if (FromEmail == null)
            {
                FromEmail = "noreply@ringsted.dk";
            }

            if (FromSenderName == null)
            {
                FromSenderName = FromEmail.Substring(1, FromEmail.IndexOf('@') - 1);
            }

            string From = FromSenderName + " " + FromEmail;

            smtpClient.Send(From, To, subject, body);
        }

        public TransformedBody_Root GetDischargeReportData(ReferencedObject_Base_Root baseObject)
        {

            string transformedBodyHTML = api.GetTransformedBodyHTML(baseObject);
            TransformedBody_Root transformedBody = new TransformedBody_Root();
            HtmlHandler handler = new HtmlHandler();

            api.GetDischargeReportData_Relatives(transformedBody, handler, transformedBodyHTML);
            api.GetDischargeReportData_CurrentAdmission(transformedBody, handler, transformedBodyHTML);
            api.GetDischargeReportData_Diagnoses(transformedBody, handler, transformedBodyHTML);
            api.GetDischargeReportData_FunctionalAbilitiesAtDischarge(transformedBody, handler, transformedBodyHTML);
            api.GetDischargeReportData_NursingProfessionalProblemAreas(transformedBody, handler, transformedBodyHTML);
            api.GetDischargeReportData_MostRecentMedicationAdministration(transformedBody, handler, transformedBodyHTML);
            api.GetDischargeReportData_MedicationInformationRelatedToDischarge(transformedBody, handler, transformedBodyHTML);
            api.GetDischargeReportData_AgreementsRegardingDietFirstDayAfterDischarge(transformedBody, handler, transformedBodyHTML);
            api.GetDischargeReportData_FutureAgreements(transformedBody, handler, transformedBodyHTML);

            return transformedBody;
        }
        public PatientDetailsSearch_Patient ChangeStatusOnCitizen(int id, string statusName)
        {
            var patient = api.GetPatientDetails(id);

            return ChangeStatusOnCitizen(patient, statusName);
        }
        public PatientDetailsSearch_Patient ChangeStatusOnCitizen(string citizenCPR, string statusName)
        {
            var patient = api.GetPatientDetails(citizenCPR);

            return ChangeStatusOnCitizen(patient, statusName);
        }
        public PatientDetailsSearch_Patient ChangeStatusOnCitizen(PatientDetailsSearch_Patient patient, string statusName)
        {
            List<PatientDetailsSearch_PatientState> availablePatientStates = api.GetAvailablePatientStates(patient);

            PatientDetailsSearch_PatientState chosenState = api.ChoosePatientState(availablePatientStates, statusName);

            patient.PatientState = chosenState;
            string serializedObject = JsonConvert.SerializeObject(patient);

            return api.UpdatePatient(patient.Links.Update.Href, serializedObject);

        }

        /// <summary>
        /// Uploader en fil til Nexus på et bestemt forløb under et borgerforløb
        /// </summary>
        /// <param name="borgerCPR"></param>
        /// <param name="borgerforloeb">eks. Dokumenttilknytning fra Vitae</param>
        /// <param name="forloeb">eks. Historiske data fra Vitae</param>
        /// <param name="fuldStiTilFil">eks. C:\Skrivebord\fil.txt || \\server\mappe\fil.txt</param>
        /// <param name="navn">Udfyldes hvis navnet der fremgår i Nexus skal være andet end det filen hedder</param>
        /// <param name="originalFilNavn"></param>
        /// <returns></returns>
        public WebRequest UploadDokumentTilNexusForloeb(string borgerCPR, string borgerforloeb, string forloeb, string fuldStiTilFil, string navn = null, string originalFilNavn = null)
        {
            return UploadPatientPathwayDocumentToNexus(borgerCPR, borgerforloeb, forloeb, fuldStiTilFil, navn, originalFilNavn);
        }

        public WebRequest UploadPatientPathwayChildDocumentToNexus(string citizenCPR, string pathwayName, string pathwayReference, string fullFilePath, string pathwayReferenceChildName)
        {
            var folderPath = System.IO.Directory.GetParent(fullFilePath).ToString();
            var prototypeDocument = api.GetGetCitizenPathwayReferencesChildSelf_DocumentPrototype(citizenCPR, pathwayName, pathwayReference, pathwayReferenceChildName); // Get document prototype
            CitPathwSelfDocPrototype_Root prototypeCreatedDocument = new CitPathwSelfDocPrototype_Root();

            prototypeCreatedDocument = api.CreateGetCitizenPathwayReferencesSelf_DocumentPrototype(prototypeDocument, fullFilePath); // Changing the prototype values

            var prototypeUploadedDocument = api.UploadPatientPathwayDocumentPrototype(prototypeCreatedDocument); // Upload document prototype to get an object with an Id
            return api.UploadPatientPathwayDocumentToNexus(prototypeUploadedDocument, fullFilePath); // Upload the document to Nexus on the Id from the prototype
        }
        public WebRequest UploadPatientPathwayChildDocumentToNexus(string citizenCPR, string pathwayName, string pathwayReference, string fullFilePath, string pathwayReferenceChildName, string tagName, string documentDescription)
        {
            string[] tagNameArray = new string[1];
            tagNameArray[0] = tagName;
            return UploadPatientPathwayDocumentToNexus(citizenCPR, pathwayName, pathwayReference, fullFilePath, pathwayReferenceChildName, tagNameArray, documentDescription);
        }
        public WebRequest UploadPatientPathwayChildDocumentToNexus(string citizenCPR, string pathwayName, string pathwayReference, string fullFilePath, string pathwayReferenceChildName, string tagName = null)
        {
            string[] tagNameArray = new string[1];
            tagNameArray[0] = tagName;
            return UploadPatientPathwayDocumentToNexus(citizenCPR, pathwayName, pathwayReference, fullFilePath, pathwayReferenceChildName, tagNameArray);
        }
        public WebRequest UploadPatientPathwayDocumentToNexus(string citizenCPR, string pathwayName, string pathwayReference, string fullFilePath, string pathwayReferenceChildName, string tagName = null, string name = null, string originalFileName = null)
        {
            string[] tagNameArray = new string[1];
            tagNameArray[0] = tagName;
            return UploadPatientPathwayDocumentToNexus(citizenCPR, pathwayName, pathwayReference, pathwayReferenceChildName, fullFilePath, tagNameArray, name, originalFileName);
        }
        public WebRequest UploadPatientPathwayDocumentToNexus(string citizenCPR, string pathwayName, string pathwayReference, string fullFilePath, string pathwayReferenceChildName, string[] tagNames, string documentDescription)
        {
            var folderPath = System.IO.Directory.GetParent(fullFilePath).ToString();
            var prototypeDocument = api.GetGetCitizenPathwayReferencesChildSelf_DocumentPrototype(citizenCPR, pathwayName, pathwayReference, pathwayReferenceChildName); // Get document prototype
            CitPathwSelfDocPrototype_Root prototypeCreatedDocument = new CitPathwSelfDocPrototype_Root();

            prototypeCreatedDocument = api.CreateGetCitizenPathwayReferencesSelf_DocumentPrototype(prototypeDocument, fullFilePath); // Changing the prototype values


            if (tagNames != null)
            {
                var availableTags = GetAvailableTags(prototypeCreatedDocument.Links.AvailableTags.Href);

                foreach (var tag in tagNames)
                {
                    prototypeCreatedDocument.Tags.Add(availableTags.FirstOrDefault(x => x.Name == tag));
                }
            }

            prototypeCreatedDocument.Notes = documentDescription;

            var prototypeUploadedDocument = api.UploadPatientPathwayDocumentPrototype(prototypeCreatedDocument); // Upload document prototype to get an object with an Id
            return api.UploadPatientPathwayDocumentToNexus(prototypeUploadedDocument, fullFilePath); // Upload the document to Nexus on the Id from the prototype
        }

        public WebRequest UploadPatientPathwayDocumentToNexus(string citizenCPR, string pathwayName, string pathwayReference, string fullFilePath, string pathwayReferenceChildName, string[] tagNames = null, string name = null, string originalFileName = null)
        {
            var folderPath = System.IO.Directory.GetParent(fullFilePath).ToString();
            var prototypeDocument = api.GetGetCitizenPathwayReferencesChildSelf_DocumentPrototype(citizenCPR, pathwayName, pathwayReference, pathwayReferenceChildName); // Get document prototype
            CitPathwSelfDocPrototype_Root prototypeCreatedDocument = new CitPathwSelfDocPrototype_Root();
            if (name != null)
            {
                prototypeCreatedDocument = api.CreateGetCitizenPathwayReferencesSelf_DocumentPrototype(prototypeDocument, folderPath, name, name); // Changing the prototype values
            }
            else
            {
                prototypeCreatedDocument = api.CreateGetCitizenPathwayReferencesSelf_DocumentPrototype(prototypeDocument, fullFilePath); // Changing the prototype values
            }

            if (tagNames != null)
            {
                var availableTags = GetAvailableTags(prototypeCreatedDocument.Links.AvailableTags.Href);

                foreach (var tag in tagNames)
                {
                    prototypeCreatedDocument.Tags.Add(availableTags.FirstOrDefault(x => x.Name == tag));
                }
            }

            var prototypeUploadedDocument = api.UploadPatientPathwayDocumentPrototype(prototypeCreatedDocument); // Upload document prototype to get an object with an Id
            return api.UploadPatientPathwayDocumentToNexus(prototypeUploadedDocument, fullFilePath); // Upload the document to Nexus on the Id from the prototype
        }
        public List<CitPathwSelfDocPrototype_AvailableTags_Root> GetAvailableTags(string availableTagsLink)
        {
            var webResult = api.CallAPI(api, availableTagsLink, Method.Get);
            return JsonConvert.DeserializeObject<List<CitPathwSelfDocPrototype_AvailableTags_Root>>(webResult.Result.ToString());
        }

        public WebRequest UploadPatientPathwayDocumentToNexus(string citizenCPR, string pathwayName, string pathwayReference, string fullFilePath, string name = null, string originalFileName = null, string tagName = null)
        {
            var folderPath = System.IO.Directory.GetParent(fullFilePath).ToString();
            var prototypeDocument = api.GetGetCitizenPathwayReferencesSelf_DocumentPrototype(citizenCPR, pathwayName, pathwayReference); // Get document prototype
            CitPathwSelfDocPrototype_Root prototypeCreatedDocument = new CitPathwSelfDocPrototype_Root();
            if (name != null)
            {
                prototypeCreatedDocument = api.CreateGetCitizenPathwayReferencesSelf_DocumentPrototype(prototypeDocument, folderPath, name, name); // Changing the prototype values
            }
            else
            {
                prototypeCreatedDocument = api.CreateGetCitizenPathwayReferencesSelf_DocumentPrototype(prototypeDocument, fullFilePath); // Changing the prototype values
            }
            var availableTagsLink = prototypeCreatedDocument.Links.AvailableTags.Href;
            var webResultAvailableTags = api.CallAPI(api, availableTagsLink, Method.Get);
            List<CitPathwSelfDocPrototype_AvailableTags_Root> availableTags = JsonConvert.DeserializeObject<List<CitPathwSelfDocPrototype_AvailableTags_Root>>(webResultAvailableTags.Result.ToString());

            var chosenTag = availableTags.FirstOrDefault(x => x.Name == tagName);
            prototypeCreatedDocument.Tags.Add(chosenTag);

            var prototypeUploadedDocument = api.UploadPatientPathwayDocumentPrototype(prototypeCreatedDocument); // Upload document prototype to get an object with an Id
            return api.UploadPatientPathwayDocumentToNexus(prototypeUploadedDocument, fullFilePath); // Upload the document to Nexus on the Id from the prototype
        }

        /// <summary>
        /// Uploads a document to a pathway (forløb)
        /// </summary>
        /// <param name="citizenCPR"></param>
        /// <param name="pathwayName"></param>
        /// <param name="pathwayReference"></param>
        /// <param name="fullFilePath"></param>
        /// <param name="name"></param>
        /// <param name="originalFileName"></param>
        /// <returns>Returns a WebRequest where Status can be checked for 200 OK</returns>
        public WebRequest UploadPatientPathwayDocumentToNexus(string citizenCPR, string pathwayName, string pathwayReference, string fullFilePath, string name = null, string originalFileName = null)
        {
            var folderPath = System.IO.Directory.GetParent(fullFilePath).ToString();
            var prototypeDocument = api.GetGetCitizenPathwayReferencesSelf_DocumentPrototype(citizenCPR, pathwayName, pathwayReference); // Get document prototype
            CitPathwSelfDocPrototype_Root prototypeCreatedDocument = new CitPathwSelfDocPrototype_Root();
            if (name != null)
            {
                prototypeCreatedDocument = api.CreateGetCitizenPathwayReferencesSelf_DocumentPrototype(prototypeDocument, folderPath, name, name); // Changing the prototype values
            }
            else
            {
                prototypeCreatedDocument = api.CreateGetCitizenPathwayReferencesSelf_DocumentPrototype(prototypeDocument, fullFilePath); // Changing the prototype values
            }
            var prototypeUploadedDocument = api.UploadPatientPathwayDocumentPrototype(prototypeCreatedDocument); // Upload document prototype to get an object with an Id
            return api.UploadPatientPathwayDocumentToNexus(prototypeUploadedDocument, fullFilePath); // Upload the document to Nexus on the Id from the prototype
        }
        /// <summary>
        /// Uploads a document to a pathway (forløb) specified by a pathwayReferenceId
        /// </summary>
        /// <param name="citizenCPR"></param>
        /// <param name="pathwayName"></param>
        /// <param name="pathwayReference"></param>
        /// <param name="fullFilePath"></param>
        /// <param name="pathwayReferenceId"></param>
        /// <param name="name"></param>
        /// <param name="originalFileName"></param>
        /// <returns>Returns a WebRequest where Status can be checked for 200 OK</returns>
        public WebRequest UploadPatientPathwayDocumentToNexus(string citizenCPR, string pathwayName, string pathwayReference, string fullFilePath, int pathwayReferenceId, string name = null, string originalFileName = null)
        {

            var prototypeDocument = api.GetGetCitizenPathwayReferencesSelf_DocumentPrototype(citizenCPR, pathwayName, pathwayReference, pathwayReferenceId); // Get document prototype
            var prototypeCreatedDocument = api.CreateGetCitizenPathwayReferencesSelf_DocumentPrototype(prototypeDocument, fullFilePath); // Changing the prototype values
            var prototypeUploadedDocument = api.UploadPatientPathwayDocumentPrototype(prototypeCreatedDocument); // Upload document prototype to get an object with an Id
            return api.UploadPatientPathwayDocumentToNexus(prototypeUploadedDocument, fullFilePath); // Upload the document to Nexus on the Id from the prototype
        }

        /// <summary>
        /// Will take a full file path as input, and check the pathway for a document to match and return true if found
        /// </summary>
        /// <param name="citizenCPR"></param>
        /// <param name="pathwayName"></param>
        /// <param name="fullFilePath"></param>
        /// <param name="name"></param>
        /// <param name="originalFileName"></param>
        /// <returns></returns>
        public bool DoesDocumentExist(string citizenCPR, string pathwayName, string fullFilePath, string name = null, string originalFileName = null)
        {
            var folderSplitList = api.dataHandler.SplitStringByString(fullFilePath);
            string fileName = folderSplitList.Last();
            bool exists = false;

            var pathwayDocuments = api.GetCitizenPathwayDocuments(citizenCPR, pathwayName);

            foreach (var document in pathwayDocuments)
            {
                if (document.Name == fileName)
                {
                    exists = true;
                    break;
                }
            }
            return exists;
        }
        public bool DoesDocumentExist(List<PathwayReferences_Child> documents, string fullFilePath)
        {
            var folderSplitList = api.dataHandler.SplitStringByString(fullFilePath);
            string fileName = folderSplitList.Last();
            bool exists = false;

            foreach (var document in documents)
            {
                if (document.Name.ToLower() == fileName.ToLower())
                {
                    exists = true;
                    break;
                }
            }
            return exists;
        }

        public void Add96HoursCitizensToDb(int startDay, int startMonth, int startYear, int endDay, int endMonth, int endYear)
        {
            var activityList = api.GetPreferencesActivityListSelfObjectContent("96 timers behandlingsansvar", startDay, startMonth, startYear, endDay, endMonth, endYear);
            foreach (var item in activityList)
            {
                // for each item in the activity list we

                // get the patient
                var patient = item.Patients.FirstOrDefault();
                // get the referenced object link
                string referencedObjLink = item.Links.ReferencedObject.Href;
                // get the referenced object
                var refObj = api.GetActivityListContentBaseObject(item);
                //get the discharge report data
                var reportData = GetDischargeReportData(refObj);
                // get the time of discharge
                var currentAdmission = reportData.CurrentAdmission;
                string timeOfDischarge = currentAdmission.TimeOfDischarge;
                DateTime dateOfDischarge = api.dataHandler.GetDateAndTime(timeOfDischarge);
                // check if patient is in the db
                PatientWith96HourTreatmentGuarantee patientInDb = api.dataHandler.GetPatientWith96HoursTreatmentGuarantee(Convert.ToInt32(patient.Id));
                // if patient is in db we check if current dateOfDischarge is later than the one in db
                if (patientInDb != null)
                {
                    // if current dateOfDischarge is later than the one in db, we update the data in db
                    if (dateOfDischarge > patientInDb.TimeOfDischarge)
                    {
                        api.dataHandler.RunSQLWithoutReturnResult("UPDATE PatientsWithCurrent96HourTreatmentGuarantee SET TimeOfDischarge = '" + api.dataHandler.ConvertDateTmeToDbFormat(dateOfDischarge) + "' WHERE Id = " + patientInDb.Id.ToString());
                        PatientWith96HourTreatmentGuarantee updatedPatientInDb = api.dataHandler.GetPatientWith96HoursTreatmentGuarantee(Convert.ToInt32(patient.Id));
                        if (dateOfDischarge != updatedPatientInDb.TimeOfDischarge)
                        {
                            throw new Exception("Update of patient failed");
                        }
                    }
                    else
                    {
                        // if current dateOfDischarge is NOT later than the one in db, we do nothing
                    }

                }
                else
                {
                    // Patient does not exist in db - therefore we add data to the db
                    api.dataHandler.RunSQLWithoutReturnResult("INSERT INTO PatientsWithCurrent96HourTreatmentGuarantee(PatientId, PatientName, TimeOfDischarge) VALUES (" + patient.Id + ", '" + patient.FirstName + "', '" + api.dataHandler.ConvertDateTmeToDbFormat(dateOfDischarge) + "')");
                    PatientWith96HourTreatmentGuarantee newPatientInDb = api.dataHandler.GetPatientWith96HoursTreatmentGuarantee(Convert.ToInt32(patient.Id));
                    if (newPatientInDb == null)
                    {
                        throw new Exception("Failed to add patient to db");
                    }
                }

            }

        }
        public void ExpireOrganizationFromCitizen(PatientDetailsSearch_Root patient, string organizationName, string effectiveEndDate = "1900-01-01")
        {
            string orgsLink = patient.Links.PatientOrganizations.Href;

            var webResultpatientOrgsLink = api.CallAPI(api, orgsLink, Method.Get);
            List<PatientOrganizations_Root> patientOrganizations = JsonConvert.DeserializeObject<List<PatientOrganizations_Root>>(webResultpatientOrgsLink.Result.ToString());
            var chosenOrg = patientOrganizations.FirstOrDefault(x => x.Organization.Name == organizationName);

            string updateLink = chosenOrg.Links.Update.Href;
            chosenOrg.EffectiveEndDate = effectiveEndDate;
            string JSONOrganization = JsonConvert.SerializeObject(chosenOrg);
            var webResultEndDate = api.CallAPI(api, updateLink, Method.Put, JSONOrganization);
        }
        public void ExpireOrganizationFromCitizens(List<PatientDetailsSearch_Root> patients, string organizationName, string effectiveEndDate = "1900-01-01")
        {
            foreach (var patient in patients)
            {
                ExpireOrganizationFromCitizen(patient, organizationName, effectiveEndDate);
            }
        }
        public void ExpireOrganizationFromCitizens(string citizenList, string organizationName, string effectiveEndDate = "1900-01-01")
        {
            var PreferencesCitizenListSelfContent = api.GetPreferencesCitizenListSelfContent(citizenList);

            foreach (var page in PreferencesCitizenListSelfContent.Pages)
            {
                string pagelink = page.Links.PatientData.Href;
                var webResult = api.CallAPI(api, pagelink, Method.Get);

                List<Content_Page_Root> pageContent = JsonConvert.DeserializeObject<List<Content_Page_Root>>(webResult.Result.ToString());

                foreach (var patient in pageContent)
                {
                    string patientSelfLink = patient.Links.Self.Href;
                    var webResultpatient = api.CallAPI(api, patientSelfLink, Method.Get);
                    var patientObject = JsonConvert.DeserializeObject<PatientDetailsSearch_Root>(webResultpatient.Result.ToString());

                    ExpireOrganizationFromCitizen(patientObject, organizationName, effectiveEndDate);
                }
            }
        }
        public void UpdateProfessionalJobsFromFKOrg()
        {
            //Get data from SOFD - all job titles
            //Get data from Nexus - all professional jobs
            var availableJobsInNexus = api.GetAllProfessionalJobs();


            //Loop SOFD data
            //Compare data in db
            //if SOFD data in db is updated
            //update database
            //update Nexus
            //else (data not present in db)
            //Create in Nexus and store data
            //add to database - both SOFD and stored Nexus data
            //if - end
            //Compare data in db - end
            //Loop SOFD data - end
        }

        public List<PwayRefChildSelf_JournalNote_RefObj_Root> GetAllJournalNotes(string citizenCPR, string pathwayName, string pathwayReferenceName)
        {

            // Get citizen pathway references
            var citizenpathwayReferencesChildren = api.GetCitizenPathwayReferencesChildren(citizenCPR, pathwayName, pathwayReferenceName);

            List<PwayRefChildSelf_JournalNote_RefObj_Root> journalNotes = new List<PwayRefChildSelf_JournalNote_RefObj_Root>();
            foreach (var child in citizenpathwayReferencesChildren)
            {
                string selfLink = child.Links.Self.Href;
                var webResult = api.CallAPI(api, selfLink, Method.Get);
                PwayRefChildSelf_JournalNote_Root journalNote = JsonConvert.DeserializeObject<PwayRefChildSelf_JournalNote_Root>(webResult.Result.ToString());

                string refObjectLink = journalNote.Links.ReferenceObject.Href;
                var webResultRefObject = api.CallAPI(api, refObjectLink, Method.Get);

                PwayRefChildSelf_JournalNote_RefObj_Root journalNoteReferencedObject = JsonConvert.DeserializeObject<PwayRefChildSelf_JournalNote_RefObj_Root>(webResultRefObject.Result.ToString());

                journalNotes.Add(journalNoteReferencedObject);
            }

            return journalNotes;
        }




        /// <summary>
        /// Parsing a journalNote prototype, it is checked if the observation is identical
        /// </summary>
        /// <param name="citizenCPR"></param>
        /// <param name="pathwayName"></param>
        /// <param name="pathwayReferenceName"></param>
        /// <param name="journalNoteReferencedObject"></param>
        /// <returns>true/false</returns>
        public bool DoesJournalNoteExist(string citizenCPR, string pathwayName, string pathwayReferenceName, string pathwayReferenceChildName, FormDataPrototype_Root journalNoteReferencedObject)
        {
            bool journalNoteExists = false;
            List<PwayRefChildSelf_JournalNote_RefObj_Root> journalNotes = GetAllCitizenJournalNotes(citizenCPR, pathwayName, pathwayReferenceName, pathwayReferenceChildName);

            foreach (var note in journalNotes)
            {
                //string journalNoteSubject = note.Items.FirstOrDefault(x => x.Label == "Emne").Value;
                string journalNoteObservation = note.Items.FirstOrDefault(x => x.Label == "Observation").Value;
                //string journalNoteAssesment = note.Items.FirstOrDefault(x => x.Label == "Vurdering").Value;

                string referencedObjectObservation = journalNoteReferencedObject.Items.FirstOrDefault(x => x.Label == "Observation").Value.ToString();
                if (referencedObjectObservation == journalNoteObservation)
                {
                    journalNoteExists = true;
                    break;
                }
            }

            return journalNoteExists;
        }
        public bool DoesJournalNoteExist(FormDataPrototype_Root journalNoteReferencedObject, List<PwayRefChildSelf_JournalNote_RefObj_Root> journalNotes)
        {
            bool journalNoteExists = false;

            foreach (var note in journalNotes)
            {
                //string journalNoteSubject = note.Items.FirstOrDefault(x => x.Label == "Emne").Value;
                string journalNoteObservation = note.Items.FirstOrDefault(x => x.Label == "Observation").Value;
                //string journalNoteAssesment = note.Items.FirstOrDefault(x => x.Label == "Vurdering").Value;

                string referencedObjectObservation = journalNoteReferencedObject.Items.FirstOrDefault(x => x.Label == "Observation").Value.ToString();
                if (referencedObjectObservation == journalNoteObservation)
                {
                    journalNoteExists = true;
                    break;
                }
            }

            return journalNoteExists;
        }
        public List<PwayRefChildSelf_JournalNote_RefObj_Root> GetAllCitizenJournalNotes(string citizenCPR, string pathwayName, string pathwayReferenceName)
        {
            // Get citizen pathway references
            var citizenpathwayReferencesChildren = api.GetCitizenPathwayReferencesChildren(citizenCPR, pathwayName, pathwayReferenceName);

            List<PwayRefChildSelf_JournalNote_RefObj_Root> journalNotes = new List<PwayRefChildSelf_JournalNote_RefObj_Root>();
            foreach (var child in citizenpathwayReferencesChildren)
            {
                if (child.Type == "formDataV2Reference")
                {
                    string selfLink = child.Links.Self.Href;
                    var webResult = api.CallAPI(api, selfLink, Method.Get);
                    PwayRefChildSelf_JournalNote_Root journalNote = JsonConvert.DeserializeObject<PwayRefChildSelf_JournalNote_Root>(webResult.Result.ToString());

                    string refObjectLink = journalNote.Links.ReferenceObject.Href;
                    var webResultRefObject = api.CallAPI(api, refObjectLink, Method.Get);

                    PwayRefChildSelf_JournalNote_RefObj_Root journalNoteReferencedObject = JsonConvert.DeserializeObject<PwayRefChildSelf_JournalNote_RefObj_Root>(webResultRefObject.Result.ToString());

                    journalNotes.Add(journalNoteReferencedObject);
                }

            }

            return journalNotes;
        }

        public List<PwayRefChildSelf_JournalNote_RefObj_Root> GetAllCitizenJournalNotes(string citizenCPR, string pathwayName, string pathwayReferenceName, string pathReferenceChildName)
        {
            // Get citizen pathway references
            var citizenpathwayReferencesChildren = api.GetCitizenPathwayReferencesChildren(citizenCPR, pathwayName, pathwayReferenceName);

            List<PwayRefChildSelf_JournalNote_RefObj_Root> journalNotes = new List<PwayRefChildSelf_JournalNote_RefObj_Root>();
            if (citizenpathwayReferencesChildren != null)
            {
                foreach (var child in citizenpathwayReferencesChildren)
                {
                    if (child.Type == "patientPathwayReference")
                    {
                        foreach (var childchild in child.Children)
                        {
                            if (childchild.Type == "formDataV2Reference")
                            {
                                string selfLink = childchild.Links.Self.Href;
                                var webResult = api.CallAPI(api, selfLink, Method.Get);
                                PwayRefChildSelf_JournalNote_Root journalNote = JsonConvert.DeserializeObject<PwayRefChildSelf_JournalNote_Root>(webResult.Result.ToString());

                                string refObjectLink = journalNote.Links.ReferenceObject.Href;
                                var webResultRefObject = api.CallAPI(api, refObjectLink, Method.Get);

                                PwayRefChildSelf_JournalNote_RefObj_Root journalNoteReferencedObject = JsonConvert.DeserializeObject<PwayRefChildSelf_JournalNote_RefObj_Root>(webResultRefObject.Result.ToString());

                                journalNotes.Add(journalNoteReferencedObject);
                            }
                        }

                    }

                }
            }


            return journalNotes;
        }
        /// <summary>
        /// Deletes all journal notes 
        /// </summary>
        /// <param name="citizenCPR"></param>
        /// <param name="pathwayName"></param>
        /// <param name="pathwayReferenceName"></param>
        /// <param name="pathReferenceChildName"></param>
        public void DeleteAllCitizenJournalNotesDirectly(string citizenCPR, string pathwayName, string pathwayReferenceName, string pathReferenceChildName, string tagName)
        {
            // Get citizen pathway references
            var citizenpathwayReferencesChildren = api.GetCitizenPathwayReferencesChildren(citizenCPR, pathwayName, pathwayReferenceName);

            List<PwayRefChildSelf_JournalNote_RefObj_Root> journalNotes = new List<PwayRefChildSelf_JournalNote_RefObj_Root>();
            foreach (var child in citizenpathwayReferencesChildren)
            {
                if (child.Type == "patientPathwayReference")
                {
                    foreach (var childchild in child.Children)
                    {
                        if (childchild.Type == "formDataV2Reference")
                        {
                            string selfLink = childchild.Links.Self.Href;
                            var webResult = api.CallAPI(api, selfLink, Method.Get);
                            PwayRefChildSelf_JournalNote_Root journalNote = JsonConvert.DeserializeObject<PwayRefChildSelf_JournalNote_Root>(webResult.Result.ToString());

                            string refObjectLink = journalNote.Links.ReferenceObject.Href;
                            var webResultRefObject = api.CallAPI(api, refObjectLink, Method.Get);

                            PwayRefChildSelf_JournalNote_RefObj_Root journalNoteReferencedObject = JsonConvert.DeserializeObject<PwayRefChildSelf_JournalNote_RefObj_Root>(webResultRefObject.Result.ToString());

                            if (journalNoteReferencedObject.Tags[0].Name == tagName)
                            {
                                string deleteURL = journalNoteReferencedObject.Links.Delete.Href;
                                api.CallAPI(api, deleteURL, Method.Delete);
                            }
                        }
                    }

                }

            }


        }

        /// <summary>
        /// Closing pathways, associations etc.
        /// </summary>
        public void HandleDeadOrInactiveCitizens()
        {
            throw new Exception("Not implemented yet.");
            /*
             * Er der nogle punkter der ikke kan udføres fordi der er andre ting der skal håndteres først!?
             * Sundhed & Træning

            For os i S&T tager det ca. 1 min at lukke en borger.

            Sådan lukker vi ned når borger er mors:
            Obs. vi afslutter kun noget i relation til Sundhed & Træning
            1.	Tjekker listen over døde borgere (borgere, som har status Mors i Nexus)
            2.	Tjek og aflyse kørsler i Movia-system - Vi skal informeres om at borger er død, da vi også manuelt skal aflyse borgers kørsler i Movia-system
            3.	Uploader evt. fysisk træningsprogrammer – dette er en manual arbejdsgang som scannes ind
            4.	*Slet tilmelding af fremtidige aftaler med ydelse/besøg – obs det er vigtigt at det ikke er hele aftalelommen, da der kan være andre borgere på aftalen 
            5.	Afslut indsatser
            6.	Inaktiver indsatsmål
            7.	Inaktiver handlingsanvisning
            8.	Inaktiver tilstand
            9.	Afslut opgaver
            10.	Fjern kontaktpersoner på forløb – obs det er vigtigt at kontaktperson på forløbet bliver fjernet før forløbet lukkes, da borger ellers vil fremgå på terapeuternes borgerliste.
            11.	*Luk forløb
            12.	**Luk grundforløb
            13.	*Fjern organisationsenheder


            Processer der ikke må automatiseres, men som er vigtigt vi gør manuelt er:
            Pkt. 4: sletter tilmelding til aftale (ellers ved vi ikke hvornår borger har kørsel)
            Pkt. 11: Lukker forløb (ellers kan vi ikke uploade den sidste dokumentation)
            ** vi kan ikke lukke et grundforløb uden at lukke forløbet
            Pkt. 13: Da vi skal vide hvilke borgere vi skal lukke det sidste ned på


             */


        }
        /// <summary>
        /// Creates 1 or more conditions on the patient, and sets the state. No additional information can be added to the conditions.
        /// </summary>
        /// <param name="patient">patient/citizen object</param>
        /// <param name="conditionIds">1 or more ids representing the different conditions you want to attach a patient</param>
        /// <param name="conditionState">"Aktiv"(default), "Inaktiv" or "Ikke relevant"</param>
        /// <returns>The created condition(s) as an object</returns>
        public List<CondBulkProtoCreate_Root> CreatePatientCondition(PatientDetailsSearch_Patient patient, int[] conditionIds, string conditionState = "Aktiv")
        {

            var prototype = api.CreatePatientConditionsBulkPrototype(patient, conditionIds);
            string createLink = prototype.Links.Create.Href;

            var stateValueChosen = prototype.State.PossibleValues.FirstOrDefault(x => x.Name == conditionState);
            prototype.UpdateStateValue(stateValueChosen);


            return CreatePatientCondition(prototype);
        }
        /// <summary>
        /// Takes a conditionPrototype that has all the information updated, and creates the condition(s) on the patient
        /// </summary>
        /// <param name="conditionPrototype"></param>
        /// <returns>The created condition(s) as an object</returns>
        public List<CondBulkProtoCreate_Root> CreatePatientCondition(ConditionsBulkPrototype_Root conditionPrototype)
        {
            var result = api.CallAPI(api, conditionPrototype.Links.Create.Href, Method.Post, JsonConvert.SerializeObject(conditionPrototype));
            List<CondBulkProtoCreate_Root> prototypeCreated = JsonConvert.DeserializeObject<List<CondBulkProtoCreate_Root>>(result.Result.ToString());
            return prototypeCreated;
        }


        public List<Patient_InboxMessages_Self_Root> GetPatientInboxMessages(string citizenCPR)
        {
            
            var patientDetails = api.GetPatientDetails(citizenCPR);
            string citizenInboxMessagesLink = patientDetails.Links.InboxMessages.Href;
            var inboxResult = api.CallAPI(api, citizenInboxMessagesLink, Method.Get);
            var inboxResultObj = JsonConvert.DeserializeObject<Patient_InboxMessages_Root>(inboxResult.Result.ToString());

            List<Patient_InboxMessages_Self_Root> inboxMessages = new List<Patient_InboxMessages_Self_Root>();
            foreach (var page in inboxResultObj.Pages)
            {
                string pageLink = page.Links.Self.Href;
                var pageSelfLinkResult = api.CallAPI(api, pageLink, Method.Get);
                var pageSelfResult = JsonConvert.DeserializeObject<List<Patient_InboxMessages_Self_Root>>(pageSelfLinkResult.Result.ToString());

                foreach (var pageObj in pageSelfResult)
                {
                    inboxMessages.Add(pageObj);
                }
            }
            return inboxMessages;
        }

        /// <summary>
        /// This method updates the field "Beskrivelse af tilstandsområde"
        /// </summary>
        /// <param name="citizenCPR"></param>
        /// <param name="dashboardName"></param>
        /// <param name="conditionToUpdateName"></param>
        /// <param name="conditionGroupName"></param>
        /// <param name="conditionText"></param>
        /// <param name="appendTextToExistingData"></param>
        /// <param name="currentScore"></param>
        /// <param name="expectedScore"></param>
        /// <returns></returns>
        public (bool conditionUpdated, CitDashbCitCondSelfWidgVisi_Root visitationObject, string comment)
            UpdateCitizenConditionGroup(
            string citizenCPR,
            string dashboardName,
            string conditionToUpdateName,
            string conditionGroupName,
            string conditionText,
            bool appendTextToExistingData,
            int currentScore = 0,
            int expectedScore = 0)
        {

            CitDashbCitCondSelfWidgVisi_Root visitation = api.GetCitizenConditionVisitations(citizenCPR, dashboardName, conditionToUpdateName);

            // visitation lists all the different conditions.
            // so we need to choose what to update

            int? conditionGroupToUpdateElementInt = null;
            int? conditionToUpdateElementInt = null;
            CitDashbCitCondSelfWidgVisi_ConditionGroupVisitation conditionGroupToUpdate = new CitDashbCitCondSelfWidgVisi_ConditionGroupVisitation();
            CitDashbCitCondSelfWidgVisi_Condition conditionToUpdate = new CitDashbCitCondSelfWidgVisi_Condition();

            for (int i = 0; i < visitation.ConditionGroupVisitation.Count; i++)
            {
                var item = visitation.ConditionGroupVisitation[i];

                if (item.ConditionGroup.GroupClassification.Name.ToLower() == conditionGroupName.ToLower())
                {
                    conditionGroupToUpdate = item;
                    conditionGroupToUpdateElementInt = i;
                    break;
                }
            }

            

            string updateText = string.Empty;
            if (appendTextToExistingData)
            {
                if (conditionGroupToUpdate.ConditionGroup.Description == null || conditionGroupToUpdate.ConditionGroup.Description == string.Empty)
                {
                    updateText = conditionText;
                }
                else
                {
                    updateText = conditionGroupToUpdate.ConditionGroup.Description + "\n\n" + conditionText;
                }
            }
            else
            {
                updateText = conditionText;
            }
            bool visitationUpdated = false;
            //Check if new condition already contains the condition text. If so we don't update the condition
            if (conditionGroupToUpdate.ConditionGroup.Description == null || !conditionGroupToUpdate.ConditionGroup.Description.Contains(conditionText))
            {
                conditionGroupToUpdate.ConditionGroup.Description = updateText;
                //conditionToUpdate.State = "ACTIVE";
                //conditionToUpdate.FollowUpDate = "2025-09-01T00:00:00.000000";

                //conditionToUpdate.CurrentScore = currentScore;
                //conditionToUpdate.ExpectedScore = expectedScore;

                //conditionGroupToUpdate.Conditions[(int)conditionToUpdateElementInt] = conditionToUpdate;
                visitation.ConditionGroupVisitation[(int)conditionGroupToUpdateElementInt] = conditionGroupToUpdate;
                string jsonObj = JsonConvert.SerializeObject(visitation);
                //var activateResult = api.CallAPI(api, conditionToUpdate.Links.Activate.Href, Method.Post, jsonObj);
                var updateResult = api.CallAPI(api, visitation.Links.Visit.Href, Method.Post, jsonObj); // Updating visitations
                                                                                                        //Converting element after updating to check if the update has passed
                visitation = api.GetCitizenConditionVisitations(citizenCPR, dashboardName, conditionToUpdateName);
                conditionGroupToUpdate = visitation.ConditionGroupVisitation[(int)conditionGroupToUpdateElementInt];
                //conditionToUpdate = conditionGroupToUpdate.Conditions[(int)conditionToUpdateElementInt];


                if (conditionGroupToUpdate.ConditionGroup.Description == updateText)
                {
                    visitationUpdated = true;
                }
                return (visitationUpdated, visitation, string.Empty);
            }
            else
            {
                
                    return (visitationUpdated, visitation, "ConditionGroup already contains text from old potential condition");
                


            }




        }
        
        
        public void ResetCitizenConditions(string citizenCPR, string conditionToReset = "Sygepleje")
        {
            CitDashbCitCondSelfWidgVisi_Root visitation = api.GetCitizenConditionVisitations(citizenCPR, "Nye tilstandsgrupper", conditionToReset);
            
            foreach (var group in visitation.ConditionGroupVisitation)
            {
                group.ConditionGroup.Description = "";
                foreach (var condition in group.Conditions)
                {
                    condition.CurrentScore = null;
                    condition.ExpectedScore = null;
                    condition.Description = null;
                    condition.FollowUpDate = null;
                    condition.State = "INACTIVE";
                }
            }
            //CitDashbCitCondSelfWidgVisi_Root visitation = JsonConvert.DeserializeObject<CitDashbCitCondSelfWidgVisi_Root>(EmptyPlejeOgOmsorg(patientId));

            string updateString = visitation.Links.Visit.Href;
            string jsonObj = JsonConvert.SerializeObject(visitation);
            var updateResult = api.CallAPI(api, visitation.Links.Visit.Href, Method.Post, jsonObj); // Updating visitations
        }
        /// <summary>
        /// If no score is added to this update method they will be 0 - only if the condition has the option
        /// </summary>
        /// <param name="citizenCPR">Person CPR</param>
        /// <param name="dashboardName">Name of the dashboard</param>
        /// <param name="conditionToUpdateName">Which condition to update</param>
        /// <param name="conditionGroupName">Condition group name to update</param>
        /// <param name="conditionName">The condition name to update</param>
        /// <param name="conditionText">The text/description to add or update</param>
        /// <param name="appendTextToExistingData">Should the text be appended to the existing text - false will override existing text</param>
        /// <param name="currentScore">Current score if condition has the option</param>
        /// <param name="expectedScore">Expected score if condition has the option</param>
        /// <returns>Object: is condition updated, and the condition object</returns>
        public (bool conditionUpdated, CitDashbCitCondSelfWidgVisi_Root visitationObject, string comment) 
            UpdateCitizenCondition(
            string citizenCPR, 
            string dashboardName, 
            string conditionToUpdateName, 
            string conditionGroupName, 
            string conditionName, 
            string conditionText, 
            bool appendTextToExistingData,
            int currentScore = 0,
            int expectedScore = 0)
        {   

            CitDashbCitCondSelfWidgVisi_Root visitation = api.GetCitizenConditionVisitations(citizenCPR, dashboardName, conditionToUpdateName);

            // visitation lists all the different conditions.
            // so we need to choose what to update

            int? conditionGroupToUpdateElementInt = null;
            int? conditionToUpdateElementInt = null;
            CitDashbCitCondSelfWidgVisi_ConditionGroupVisitation conditionGroupToUpdate = new CitDashbCitCondSelfWidgVisi_ConditionGroupVisitation();
            CitDashbCitCondSelfWidgVisi_Condition conditionToUpdate = new CitDashbCitCondSelfWidgVisi_Condition();

            for (int i = 0; i < visitation.ConditionGroupVisitation.Count; i++)
            {
                var item = visitation.ConditionGroupVisitation[i];

                if (item.ConditionGroup.GroupClassification.Name.ToLower() == conditionGroupName.ToLower())
                {
                    conditionGroupToUpdate = item;
                    conditionGroupToUpdateElementInt = i;
                    break;
                }
            }

            for (int i = 0; i < conditionGroupToUpdate.Conditions.Count; i++)
            {
                var item = conditionGroupToUpdate.Conditions[i];

                if (item.Classification.Name.ToLower() == conditionName.ToLower())
                {
                    conditionToUpdate = item;
                    conditionToUpdateElementInt = i;
                    break;
                }
            }
            
            string updateText = string.Empty;
            if (appendTextToExistingData)
            {
                if (conditionToUpdate.Description == null || conditionToUpdate.Description == string.Empty)
                {
                    updateText = conditionText;
                }
                else
                {
                    updateText = conditionToUpdate.Description + "\n\n" + conditionText;
                }
            }
            else
            {
                updateText = conditionText;
            }
            bool visitationUpdated = false;
            //Check if new condition already contains the condition text. If so we don't update the condition
            if (conditionToUpdate.Description == null || !conditionToUpdate.Description.Contains(conditionText))
            {
                conditionToUpdate.Description = updateText;
                conditionToUpdate.State = "ACTIVE";
                conditionToUpdate.FollowUpDate = "2026-01-01T00:00:00.000000";

                conditionToUpdate.CurrentScore = currentScore;
                conditionToUpdate.ExpectedScore = expectedScore;

                conditionGroupToUpdate.Conditions[(int)conditionToUpdateElementInt] = conditionToUpdate;
                visitation.ConditionGroupVisitation[(int)conditionGroupToUpdateElementInt] = conditionGroupToUpdate;
                string jsonObj = JsonConvert.SerializeObject(visitation);
                //var activateResult = api.CallAPI(api, conditionToUpdate.Links.Activate.Href, Method.Post, jsonObj);
                var updateResult = api.CallAPI(api, visitation.Links.Visit.Href, Method.Post, jsonObj); // Updating visitations
                                                                                                        //Converting element after updating to check if the update has passed
                visitation = api.GetCitizenConditionVisitations(citizenCPR, dashboardName, conditionToUpdateName);
                conditionGroupToUpdate = visitation.ConditionGroupVisitation[(int)conditionGroupToUpdateElementInt];
                conditionToUpdate = conditionGroupToUpdate.Conditions[(int)conditionToUpdateElementInt];

                
                if (conditionToUpdate.Description == updateText)
                {
                    visitationUpdated = true;
                }
                return (visitationUpdated, visitation,string.Empty);
            }
            else
            {
                if (conditionToUpdate.State != "ACTIVE")
                {
                    conditionToUpdate.State = "ACTIVE";
                    conditionToUpdate.FollowUpDate = "2026-01-01T00:00:00.000000";

                    conditionGroupToUpdate.Conditions[(int)conditionToUpdateElementInt] = conditionToUpdate;
                    visitation.ConditionGroupVisitation[(int)conditionGroupToUpdateElementInt] = conditionGroupToUpdate;
                    string jsonObj = JsonConvert.SerializeObject(visitation);
                    var updateResult = api.CallAPI(api, visitation.Links.Visit.Href, Method.Post, jsonObj);
                    
                    return (visitationUpdated, visitation,"Condition already contains text from old condition. Condition just activated");
                }
                else
                {
                    return (visitationUpdated, visitation, "Condition already contains text from old condition");
                }
                

            }

           

            
        }


        public void MigratePotentialConditionsToNewFS3ConditionGroups(string oldAndNewConditionsPath, string activityListName, string SQLConnectionString, string dbTableName, string environment)
        {
            DataHandler datahandler = new DataHandler();


            int startDay = 1;
            int startMonth = 7;
            int startYear = 2018;
            int endDay = 1;
            int endMonth = 7;
            int endYear = 2026;
            var activityList1 = api.GetPreferencesActivityListSelfObjectContent(activityListName, startDay, startMonth, startYear, endDay, endMonth, endYear);

            OldAndNewConditions oldAndNewConditions = new OldAndNewConditions(oldAndNewConditionsPath);

            List<ACTIVITYLIST_Pages_Content_Patient> PatientList = new List<ACTIVITYLIST_Pages_Content_Patient>();
            foreach (var item in activityList1)
            {
                //if (PatientList.Count != 101)
                //{
                ACTIVITYLIST_Pages_Content_Patient patientItem = item.Patients[0];
                if (!PatientList.Exists(x => x.Id == patientItem.Id))
                {
                    PatientList.Add(patientItem);
                }
                //}
                //    else
                //{
                //    break;
                //}
            }
            //Dictionary<string, string> patients = new Dictionary<string, string>();
            //foreach (var item in PatientList)
            //{
            //    //if (item.Id != 1) // not doing Nancy
            //    //{
            //        patients.Add(item.Id.ToString(), item.PatientIdentifier.Identifier);
            //    //}
            //}

            foreach (var patientElement in PatientList)
            {
                //if (patientElement.Id != 1) // not doing nancy
                //{
                    SqlConnection sqlConnection = new SqlConnection(SQLConnectionString);
                    string queryString = "SELECT * FROM " + dbTableName + " WHERE CitizenId = " + Convert.ToInt32(patientElement.Id);
                    SqlCommand command = new SqlCommand(queryString, sqlConnection);

                    int? patientIdInDb = null;
                    using (sqlConnection)
                    {
                        command.Connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            patientIdInDb = Convert.ToInt32(reader["CitizenId"].ToString());
                        }
                        if (patientIdInDb == null)
                        {
                            MigratePotentialConditionsOnPatientToCitizenConditionGroupNewFS3Conditions(Convert.ToInt32(patientElement.Id), oldAndNewConditions, environment, true, dbTableName);
                        }
                    }
                //}
                
            } // foreach patient end loop



        }
        public void MigratePotentialConditionsOnPatientToCitizenConditionGroupNewFS3Conditions(int patientId, OldAndNewConditions oldAndNewConditions, string environment, bool insertIntoDb = false, string dbTableName = null)
        {
            //NexusAPI_processes processes = new NexusAPI_processes(environment);
            //var api = processes.api;
            //DataHandler datahandler = new DataHandler();
            var patient = api.GetPatientDetails(patientId);

            // check if CPR is a CPR or not
            // if not just continue
            string citizenCPR = patient.PatientIdentifier.Identifier;
            if (CprValidator.IsValidCpr(citizenCPR))
            {
                var links = patient.Links;

                var result = api.CallAPI(api, links.PatientConditions.Href, Method.Get);
                var patientConditions = JsonConvert.DeserializeObject<List<PatientConditions_Root>>(result.Result.ToString());

                foreach (var condition in patientConditions)
                {
                    (bool conditionUpdated, CitDashbCitCondSelfWidgVisi_Root visitationObject, string comment) citizenCondtion;
                    //try
                    //{
                    string conditionNameT = condition.ConditionClassificationItem.Name;
                    if (condition.Status == "POTENTIAL") // we only handle active and potential conditions
                    {
                        string conditionArea = condition.ConditionClassificationItem.Group.Law;
                        switch (conditionArea)
                        {
                            case "SERVICE_LAW":
                                conditionArea = "Funktionsevnetilstande";
                                break;
                            case "HEALTH_LAW":
                                conditionArea = "Helbredstilstande";
                                break;
                            case "TRAINING_LAW":
                                continue;
                            default:
                                continue;
                                //break;
                        }
                        string groupName = condition.ConditionClassificationItem.Group.Name;
                        string conditionName = condition.ConditionClassificationItem.Name;

                        var newCondition = oldAndNewConditions.GetNewMapping(conditionArea, groupName, conditionName);

                        //(string ConditionGroupName, string ConditionType) = datahandler.GetCorrectConditionToUpdateName(groupName);
                        //    string newConditionToUpdate = GetNewCondition(conditionName);
                        string conditionText = GetNewConditionText(condition); //condition.CurrentLevelDescription;
                        if (conditionText == null) { conditionText = "Ingen beskrivelse i gammel tilstand."; }

                        if (conditionArea == "Funktionsevnetilstande")
                        {
                            citizenCondtion = UpdateCitizenConditionGroup(
                                    patient.PatientIdentifier.Identifier,
                                    "Nye tilstandsgrupper",
                                    newCondition.NewArea,
                                    newCondition.NewCategory,
                                    conditionText,
                                    true,
                                    condition.CurrentLevel != null ? (int)condition.CurrentLevel.NumericRepresentation : 0,
                                    condition.ExpectedLevel != null ? (int)condition.ExpectedLevel.NumericRepresentation : 0
                                    );
                        }
                        else // Helbredstilstande
                        {
                            citizenCondtion = UpdateCitizenConditionGroup(
                                    patient.PatientIdentifier.Identifier,
                                    "Nye tilstandsgrupper",
                                    newCondition.NewArea,
                                    newCondition.NewCategory,
                                    conditionText,
                                    true
                                    );
                        }
                    }


                }
                //catch (Exception)
                //{

                //    throw new Exception("Something went wrong with " + patient.FullName + " - ID: " + patient.Id);
                //}



                //}
                if (insertIntoDb && dbTableName != null)
                {
                    // Add citizen to db for finished data transfer
                    api.dataHandler.RunSQLWithoutReturnResult("INSERT INTO " + dbTableName + " VALUES  (" + patient.Id + ",'" + patient.FullName + "')");
                }
                else if (insertIntoDb && dbTableName == null)
                {
                    throw new Exception("dbTableName is null");
                }
            }
            

        }
        public void MigrateToNewFS3Conditions(string oldAndNewConditionsPath, string activityListName, string SQLConnectionString, string dbTableName, string environment)
        {
            DataHandler datahandler = new DataHandler();


            int startDay = 1;
            int startMonth = 7;
            int startYear = 2018;
            int endDay = 1;
            int endMonth = 7;
            int endYear = 2026;
            var activityList1 = api.GetPreferencesActivityListSelfObjectContent(activityListName, startDay, startMonth, startYear, endDay, endMonth, endYear);

            OldAndNewConditions oldAndNewConditions = new OldAndNewConditions(oldAndNewConditionsPath);

            List<ACTIVITYLIST_Pages_Content_Patient> PatientList = new List<ACTIVITYLIST_Pages_Content_Patient>();
            foreach (var item in activityList1)
            {
                //if (PatientList.Count != 101)
                //{
                    ACTIVITYLIST_Pages_Content_Patient patientItem = item.Patients[0];
                    if (!PatientList.Exists(x => x.Id == patientItem.Id))
                    {
                        PatientList.Add(patientItem);
                    }
                //}
                //    else
                //{
                //    break;
                //}
            }
            //Dictionary<string, string> patients = new Dictionary<string, string>();
            //foreach (var item in PatientList)
            //{
            //    //if (item.Id != 1) // not doing Nancy
            //    //{
            //        patients.Add(item.Id.ToString(), item.PatientIdentifier.Identifier);
            //    //}
            //}

            foreach (var patientElement in PatientList)
            {
                //if (patientElement.Id !=1)
                //{
                    SqlConnection sqlConnection = new SqlConnection(SQLConnectionString);
                    string queryString = "SELECT * FROM " + dbTableName + " WHERE CitizenId = " + Convert.ToInt32(patientElement.Id);
                    SqlCommand command = new SqlCommand(queryString, sqlConnection);

                    int? patientIdInDb = null;
                    using (sqlConnection)
                    {
                        command.Connection.Open();
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            patientIdInDb = Convert.ToInt32(reader["CitizenId"].ToString());
                        }
                        if (patientIdInDb == null)
                        {
                            MigrateConditionsOnPatientToCitizenCondition(Convert.ToInt32(patientElement.Id), oldAndNewConditions, environment, true, dbTableName);
                        }
                    }
                //}
                
            } // foreach patient end loop



        }
        public void MigrateConditionsOnPatientToCitizenCondition(string citizenCPR, OldAndNewConditions oldAndNewConditions, string environment, bool insertIntoDb = false)
        {
            var patient = api.GetPatientDetails(citizenCPR);
            MigrateConditionsOnPatientToCitizenCondition((int)patient.Id,oldAndNewConditions, environment, insertIntoDb);
        }
        public void MigrateConditionsOnPatientToCitizenCondition(int patientId, OldAndNewConditions oldAndNewConditions, string environment, bool insertIntoDb = false, string dbTableName = null)
        {
            //NexusAPI_processes processes = new NexusAPI_processes(environment);
            //var api = processes.api;
            DataHandler datahandler = new DataHandler();
            var patient = api.GetPatientDetails(patientId);

            // check if CPR is a CPR or not
            // if not just continue
            string citizenCPR = patient.PatientIdentifier.Identifier;
            if (CprValidator.IsValidCpr(citizenCPR))
            {
                var links = patient.Links;

                var result = api.CallAPI(api, links.PatientConditions.Href, Method.Get);
                var patientConditions = JsonConvert.DeserializeObject<List<PatientConditions_Root>>(result.Result.ToString());

                foreach (var condition in patientConditions)
                {
                    (bool conditionUpdated, CitDashbCitCondSelfWidgVisi_Root visitationObject, string comment) citizenCondtion;
                    //try
                    //{
                    if (condition.Status == "ACTIVE")// || condition.Status == "POTENTIAL") // we only handle active and potential conditions
                    {
                        string conditionArea = condition.ConditionClassificationItem.Group.Law;
                        switch (conditionArea)
                        {
                            case "SERVICE_LAW":
                                conditionArea = "Funktionsevnetilstande";
                                break;
                            case "HEALTH_LAW":
                                conditionArea = "Helbredstilstande";
                                break;
                            case "TRAINING_LAW":
                                continue;
                            default:
                                continue;
                                //break;
                        }
                        string groupName = condition.ConditionClassificationItem.Group.Name;
                        string conditionName = condition.ConditionClassificationItem.Name;

                        var newCondition = oldAndNewConditions.GetNewMapping(conditionArea, groupName, conditionName);

                        //(string ConditionGroupName, string ConditionType) = datahandler.GetCorrectConditionToUpdateName(groupName);
                        //    string newConditionToUpdate = GetNewCondition(conditionName);
                        string conditionText = GetNewConditionText(condition); //condition.CurrentLevelDescription;
                        if (conditionText == null) { conditionText = "Ingen beskrivelse i gammel tilstand."; }

                        if (conditionArea == "Funktionsevnetilstande")
                        {
                            citizenCondtion = UpdateCitizenCondition(
                                    patient.PatientIdentifier.Identifier,
                                    "Nye tilstandsgrupper",
                                    newCondition.NewArea,
                                    newCondition.NewCategory,
                                    newCondition.NewCondition,
                                    conditionText,
                                    true,
                                    condition.CurrentLevel != null ? (int)condition.CurrentLevel.NumericRepresentation : 0,
                                    condition.ExpectedLevel != null ? (int)condition.ExpectedLevel.NumericRepresentation : 0
                                    );
                        }
                        else
                        {
                            citizenCondtion = UpdateCitizenCondition(
                                    patient.PatientIdentifier.Identifier,
                                    "Nye tilstandsgrupper",
                                    newCondition.NewArea,
                                    newCondition.NewCategory,
                                    newCondition.NewCondition,
                                    conditionText,
                                    true
                                    );
                        }
                    }


                }
                //catch (Exception)
                //{

                //    throw new Exception("Something went wrong with " + patient.FullName + " - ID: " + patient.Id);
                //}



                //}
                // Add citizen to db for finished data transfer
                if (insertIntoDb && dbTableName != null)
                {
                    datahandler.RunSQLWithoutReturnResult("INSERT INTO " + dbTableName + " VALUES  (" + patient.Id + ",'" + patient.FullName + "')");
                }
                else if( insertIntoDb && dbTableName == null)
                {
                    throw new Exception("dbTableName is missing");
                }

            } // if citizen CPR is valid END

            

        }

        public string GetNewConditionText(PatientConditions_Root patientCondition)
        {
            string currentAssesment = patientCondition.CurrentAssessment;
            string currentLevelDescription = patientCondition.CurrentLevelDescription;
            string expectedLEvelDescription = patientCondition.ExpectedLevelDescription;
            string currentLevel = patientCondition.CurrentLevel != null ? patientCondition.CurrentLevel.NumericRepresentation.ToString() : 0.ToString();
            string expectedLevel = patientCondition.ExpectedLevel != null ? patientCondition.ExpectedLevel.NumericRepresentation.ToString() : 0.ToString();

            string NewConditionText = string.Empty;

            string conditionArea = patientCondition.ConditionClassificationItem.Group.Law;
            switch (conditionArea)
            {
                case "SERVICE_LAW":
                    conditionArea = "Funktionsevnetilstande";
                    NewConditionText = patientCondition.ConditionClassificationItem.Name.ToUpper() +
                "\nFagligt notat: " + currentLevelDescription +
                "\nBeskrivelse: " + expectedLEvelDescription +
                "\nNuværende funktionsniveau: " + currentLevel +
                "\nForventet funktionsniveau: " + expectedLevel;
                    break;
                case "HEALTH_LAW":
                    conditionArea = "Helbredstilstande";
                    NewConditionText = patientCondition.ConditionClassificationItem.Name.ToUpper() +
                "\nNuværende vurdering: " + currentAssesment +
                "\nFagligt notat: " + currentLevelDescription +
                "\nBeskrivelse: " + expectedLEvelDescription;
                    break;
                case "TRAINING_LAW":
                    NewConditionText = null;
                    break;
                default:
                    break;
            }

            return NewConditionText;
        }
    }
}



        #endregion Shared processes

        #endregion processes

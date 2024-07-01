using MimeKit;
using Newtonsoft.Json;
using NexusAPIWrapper.Custom_classes;
using NexusAPIWrapper.HomeRessource.Preferences.ACTIVITYLIST.ACTIVITYLIST_Content.Content.Pages.Links.Content._Root.Links.ReferencedObject._Root.Links.TransformedBody;
using NexusAPIWrapper.RKSQLRPA01DataSetTableAdapters;
using Org.BouncyCastle.Asn1.X509;
using RestSharp;
using System;
using System.Collections.Generic;
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

        #region Professionals

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
        public WebRequest UploadPatientPathwayDocumentToNexus(string citizenCPR, string pathwayName, string pathwayReference,  string fullFilePath, string pathwayReferenceChildName, string tagName = null, string name = null, string originalFileName = null)
        {
            string[] tagNameArray = new string[1];
            tagNameArray[0] = tagName;
            return UploadPatientPathwayDocumentToNexus(citizenCPR,pathwayName,pathwayReference,pathwayReferenceChildName,fullFilePath, tagNameArray,name,originalFileName);
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
        public WebRequest UploadPatientPathwayDocumentToNexus(string citizenCPR, string pathwayName, string pathwayReference,  string fullFilePath, string name = null, string originalFileName = null)
        {
            var folderPath = System.IO.Directory.GetParent(fullFilePath).ToString();
            var prototypeDocument = api.GetGetCitizenPathwayReferencesSelf_DocumentPrototype(citizenCPR, pathwayName,pathwayReference); // Get document prototype
            CitPathwSelfDocPrototype_Root prototypeCreatedDocument = new CitPathwSelfDocPrototype_Root();
            if (name != null)
            {
                prototypeCreatedDocument = api.CreateGetCitizenPathwayReferencesSelf_DocumentPrototype(prototypeDocument,folderPath, name, name); // Changing the prototype values
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

            var prototypeDocument = api.GetGetCitizenPathwayReferencesSelf_DocumentPrototype(citizenCPR, pathwayName, pathwayReference,pathwayReferenceId); // Get document prototype
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

        public void Add72HoursCitizensToDb(int startDay, int startMonth, int startYear, int endDay, int endMonth, int endYear)
        {
            var activityList = api.GetPreferencesActivityListSelfObjectContent("72 timers behandlingsansvar", startDay, startMonth, startYear, endDay, endMonth, endYear);
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
                PatientWith72HourTreatmentGuarantee patientInDb = api.dataHandler.GetPatientWith72HoursTreatmentGuarantee(Convert.ToInt32(patient.Id));
                // if patient is in db we check if current dateOfDischarge is later than the one in db
                if (patientInDb != null)
                {
                    // if current dateOfDischarge is later than the one in db, we update the data in db
                    if (dateOfDischarge > patientInDb.TimeOfDischarge)
                    {
                        api.dataHandler.RunSQLWithoutReturnResult("UPDATE PatientsWithCurrent72HourTreatmentGuarantee SET TimeOfDischarge = '" + api.dataHandler.ConvertDateTmeToDbFormat(dateOfDischarge) + "' WHERE Id = " + patientInDb.Id.ToString());
                        PatientWith72HourTreatmentGuarantee updatedPatientInDb = api.dataHandler.GetPatientWith72HoursTreatmentGuarantee(Convert.ToInt32(patient.Id));
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
                    api.dataHandler.RunSQLWithoutReturnResult("INSERT INTO PatientsWithCurrent72HourTreatmentGuarantee(PatientId, PatientName, TimeOfDischarge) VALUES (" + patient.Id + ", '" + patient.FirstName + "', '" + api.dataHandler.ConvertDateTmeToDbFormat(dateOfDischarge) + "')");
                    PatientWith72HourTreatmentGuarantee newPatientInDb = api.dataHandler.GetPatientWith72HoursTreatmentGuarantee(Convert.ToInt32(patient.Id));
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
            List<PwayRefChildSelf_JournalNote_RefObj_Root> journalNotes = GetAllCitizenJournalNotes(citizenCPR, pathwayName, pathwayReferenceName,pathwayReferenceChildName);

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
    }



        #endregion Shared processes

        #endregion processes
    }

using MimeKit;
using Newtonsoft.Json;
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


namespace NexusAPIWrapper
{
    public class NexusAPI_processes
    {
        #region Properties
        
        public NexusAPI api; // public so you can access all API methods from the processes
        private List<Professional_Root> _professionalsList;
        

        public List<Professional_Root>  professionalsList
            {
                get => _professionalsList;
                //set => professionalsList = value;
            }


        #endregion Properties

        #region Constructors

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

        
        /// <summary>
        /// Getting a list of document objects. From the list, the self link of each object can be used to get the complete object where a reference link can be used to download the file.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="pathwayName"></param>
        /// <param name="returnAllDocuments"></param>
        /// <param name="childPathwayName"></param>
        /// <returns></returns>
        public List<PatientPathwayReferences_Child> GetCitizenDocumentObjects(int id, string pathwayName, bool returnAllDocuments, string childPathwayName = null)
        {
            // NOT FINISHED
            var pathwayReferences = api.GetCitizenPathwayReferences(id, pathwayName);


            // Children contains elements/documents directly on the pathway ("documentReference"), AND childpathways that contain elements/documents ("patientPathwayReference")
            var children = pathwayReferences.Children;
            List<PatientPathwayReferences_Child> elementsList = new List<PatientPathwayReferences_Child>();

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
                                        api.GetPathwayChildrenElements(elementsList, child);
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
                                        api.GetPathwayChildrenElements(elementsList, subChild);
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
                string professionalJsonStringObject = professional.Value;
                var professionalObject = api.dataHandler.JsonStringToSortedDictionary(professionalJsonStringObject);
                string linksString = professionalObject["_links"];
                var linksDict = api.dataHandler.JsonStringToSortedDictionary(linksString);

                // Call self link to get the full object of the professional
                string selfLink = api.dataHandler.GetHref(linksDict, true);
                var specificProfessional = api.CallAPI(api, selfLink, Method.Get);

                // Deserialize the result into a professional class object
                Professional_Root specificProfessionalObject = JsonConvert.DeserializeObject<Professional_Root>(specificProfessional.Result.ToString());

                // Check for including inactive professionals
                if (includeInactiveProfessionals == true)
                {
                    // Add object to the list
                    professionals.Add(specificProfessionalObject);
                }
                else
                {
                    if (specificProfessionalObject.Active == true)
                    {
                        // Add object to the list
                        professionals.Add(specificProfessionalObject);
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
                if (professional.Key.Length>2) // Making sure that the key length is more than 2. Otherwise we can't check the first 3 chars.
                {
                    if (professional.Key.Substring(0, 3) == "vik") // then it's a substitute professional, and we activate if inactive
                    {
                        string professionalJsonStringObject = professional.Value;
                        var professionalObject = api.dataHandler.JsonStringToSortedDictionary(professionalJsonStringObject);
                        string linksString = professionalObject["_links"];
                        var linksDict = api.dataHandler.JsonStringToSortedDictionary(linksString);

                        // Call self link to get the full object of the professional
                        string selfLink = api.dataHandler.GetHref(linksDict, true);
                        var specificProfessional = api.CallAPI(api, selfLink, Method.Get);

                        // Deserialize the result into a professional class object
                        Professional_Root specificProfessionalObject = JsonConvert.DeserializeObject<Professional_Root>(specificProfessional.Result.ToString());

                        //var links = specificProfessionalObject.Links;

                        // Check the active value
                        if (!specificProfessionalObject.Active == true)
                        {
                            ActivateProfessional(specificProfessionalObject.Id);
                            _professionalsList.Add(specificProfessionalObject);
                        }   

                    }
                }

            }
            // Send email with list of activated professionals

            // SMTP server
            


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
            api.result.UpdateProfessional(api, updateEndpoint, professionalJsonStringObject,Method.Put);
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
            var primaryOrganization = api.GetProfessionalPrimaryOrganization(professionalId,organizationName);

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
        public SortedDictionary<string,ProfessionalJobs_Root> GetPossibleProfessionalJobs(int professionalId)
        {
            var professional = GetProfessional(professionalId);var currentRoles = api.CallAPI(api, professional.Links.Roles.Href, Method.Get);
            var possibleProfessionalJobs = api.CallAPI(api, professional.Links.AvailableProfessionalJobs.Href, Method.Get);

            var jobsDict = api.dataHandler.ArrayJsonStringToSortedDictionary(possibleProfessionalJobs.Result.ToString());
            SortedDictionary<string, ProfessionalJobs_Root> jobs = new SortedDictionary<string, ProfessionalJobs_Root>();

            foreach (var job in jobsDict)
            {
                jobs.Add(job.Key, JsonConvert.DeserializeObject<ProfessionalJobs_Root>(job.Value));
            }

            return jobs;
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
            return jobs[professionalJobTitle];
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
            api.result.UpdateProfessional(api,updateEndpoint, api.GetAddedRemovedRequestBody(added,removed), method);
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
        /// Returns all documentation for that specific citizen to be used in an "aktindsigt"
        /// </summary>
        /// <param name="cpr">Citizen CPR number</param>
        /// <param name="folderPath">Path to where documentation should be saved. Can be left blank, and documentation will be saved in the default folder</param>
        public void DocumentationAccess(string cpr, string folderPath =null)
        {
            // Set the folder path
            switch (folderPath)
            {
                case null:
                    folderPath = @"\\rkfil02\koncerncenter\rpa\aktindsigter";
                    break;
                default:
                    break;
            }

            // Get citizen object
            var citizen = api.GetPatientDetails(cpr);

            // Get documents


            // Get Journal notes


            // Get 


            // Get 


            // Get 


            // Get 


            // Get 
        }

        /// <summary>
        /// Returns all documentation for that specific citizen to be used in an "aktindsigt"
        /// </summary>
        /// <param name="cpr">Citizen CPR number</param>
        /// <param name="folderPath">Path to where documentation should be saved. Can be left blank, and documentation will be saved in the default folder</param>
        public void Aktindsigt(string cpr, string folderPath = null)
        {
            DocumentationAccess(cpr, folderPath);
        }

        /// <summary>
        /// Sends an email through the specified SMTP relay server
        /// </summary>
        /// <param name="To">Comma separated list of receivers</param>
        /// <param name="subject">Subject to be added</param>
        /// <param name="body">Body text of the e-mail</param>
        /// <param name="From">Left null will use the default "noreply" e-mail</param>
        public void SendEmail(string To, string subject, string body, string FromEmail = null, string FromSenderName = null) 
        {
            string SMTPRelayServer = "rkexc05.ringsted.int";
            var smtpClient = new SmtpClient(SMTPRelayServer);
            if (FromEmail == null)
            {
                FromEmail = "noreply@ringsted.dk";
            }

            if (FromSenderName == null)
            {
                FromSenderName = FromEmail.Substring(1,FromEmail.IndexOf('@')-1);
            }

            string From = FromSenderName + " " + FromEmail  ;

            smtpClient.Send(From,To,subject,body);
        }

        

        #endregion Shared processes

        #endregion processes
    }
}

using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using CsQuery;
using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NexusAPIWrapper;
using NexusAPIWrapper.HomeRessource.Preferences.ACTIVITYLIST.ACTIVITYLIST_Content.Content.Pages.Links.Content._Root.Links.ReferencedObject._Root.Links.TransformedBody;
using NUnit.Framework;
using Org.BouncyCastle.Utilities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace NexusAPITest
{
    public class Tests
    {
        //[SetUp]
        //public void Setup()
        //{
        //}
            
        string liveEnvironment = "live";
        string reviewEnvironment = "review";
        readonly string nancyBerggrenTestCPR = "251248-9996";
        [Test]
        public void testAccess()
        {
            NexusAPI _NexusAPI = new NexusAPI(liveEnvironment);
            Assert.IsFalse(string.IsNullOrEmpty(_NexusAPI.tokenObject.AccessToken));
        }
        [Test]
        public void testHomeResource()
        {
            NexusAPI _NexusAPI = new NexusAPI(liveEnvironment);
            NexusHomeRessource _ressource = new NexusHomeRessource(_NexusAPI.clientCredentials.Host, _NexusAPI.tokenObject.AccessToken);
            NexusResult result = _ressource.GetHomeRessource();
            Assert.IsTrue(result.httpStatusCode == System.Net.HttpStatusCode.OK);
        }
        [Test]
        public void testSearchPatientByCPR()
        {
            string cpr = nancyBerggrenTestCPR;
            NexusAPI _NexusAPI = new NexusAPI(liveEnvironment);
            var details = _NexusAPI.GetPatientDetails(cpr);
            Assert.IsNotNull(details);
        }
        [Test]
        public void testGetPatientDetailsLinks()
        {
            NexusAPI _NexusAPI = new NexusAPI(liveEnvironment);
            var links = _NexusAPI.GetPatientDetailsLinks(nancyBerggrenTestCPR);
            Assert.IsNotNull(links);
        }
        [Test]
        public void testGetPatientPreferences()
        {
            NexusAPI _nexusAPI = new NexusAPI(liveEnvironment);
            var preferences = _nexusAPI.GetPatientPreferences(nancyBerggrenTestCPR);
            Assert.IsNotNull(preferences);
        }
        [Test]
        public void testGetCitizenPathways()
        {
            NexusAPI _nexusAPI = new NexusAPI(liveEnvironment);
            var pathways = _nexusAPI.GetCitizenPathways(nancyBerggrenTestCPR);
            Assert.IsNotNull(pathways);
        }
        [Test]
        public void testGetCitizenPathwayLink()
        {
            NexusAPI _nexusAPI = new NexusAPI(liveEnvironment);
            var pathwayLink = _nexusAPI.GetCitizenPathwayLink(nancyBerggrenTestCPR, "Dokumenttilknytning fra Vitae");
            Assert.IsNotNull(pathwayLink);
        }

        [Test]
        public void testGetDokumentTilknytningHref()
        {
            NexusAPI nexusAPI = new NexusAPI(liveEnvironment);
            string citizenCPR = nancyBerggrenTestCPR;
            string pathwayName = "Dokumenttilknytning fra Vitae";
            //var pathwayLink = nexusAPI.GetCitizenPathwayLink_ByCPR(citizenCPR, pathwayName);
            var hrefLink = nexusAPI.GetCitizenPathwayLink(citizenCPR, pathwayName);

            Assert.IsTrue(hrefLink == @"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patient/1/preferences/CITIZEN_PATHWAY/971");
        }


        [Test]
        public void GetPathwayReferencesNew()
        {
            NexusAPI nexusAPI = new NexusAPI("live");
            string citizenCPR = nancyBerggrenTestCPR;
            string pathwayName = "Dokumenttilknytning - alt";
            string pathwayReferenceName = "";

            var pathwayDocuments = nexusAPI.GetCitizenPathwayDocuments(citizenCPR, pathwayName);
            //Assert.IsNotNull(pathwayReferencesLink);
        }
        

        [Test]
        public void testGetCitizenPathwayDocumentPrototypelink() // This will fail, as there's no self-link present
        {
            NexusAPI nexusAPI = new NexusAPI("live");
            string citizenCPR = nancyBerggrenTestCPR;
            string pathwayName = "Dokumenttilknytning fra Vitae";
            //string pathwayChildName = "Dokumenter fra Vitae";
            string pathwayReferenceName = "";

            try
            {
                var CitizenPathwayReferencesSelf_Links = nexusAPI.GetCitizenPathwayReferencesSelf_Links(citizenCPR, pathwayName);
                string documentPrototypeLink = CitizenPathwayReferencesSelf_Links.DocumentPrototype.Href;
                Assert.IsNotNull(documentPrototypeLink);
            }
            catch (Exception e)
            {
                Assert.IsNotNull(e);
            }
            

            //string documentPrototypeLink = nexusAPI.dataHandler.GetDocumentPrototypeLink(CitizenPathwayReferencesSelf_Links);

            
        }

        

        [Test]
        public void GetCitizenPathwayReferences()
        {
            NexusAPI nexusAPI = new NexusAPI("review");
            string citizenCPR = nancyBerggrenTestCPR;
            string pathwayName = "Dokumenttilknytning - alt";

            var pathwayReferences = nexusAPI.GetCitizenPathwayReferences(citizenCPR, pathwayName);

            Assert.IsNotNull(pathwayReferences);

        }
        [Test]
        public void GetCitizenPathwayReferencesDocuments()
        {
            NexusAPI nexusAPI = new NexusAPI("review");
            string citizenCPR = nancyBerggrenTestCPR;
            string pathwayName = "Dokumenttilknytning fra Vitae";

            var pathwayReferencesDocuments = nexusAPI.GetCitizenPathwayReferencesDocuments(citizenCPR, pathwayName);

            Assert.IsNotNull(pathwayReferencesDocuments);
        }

        [Test]
        public void GetCitizenPathwayDocuments()
        {
            NexusAPI nexusAPI = new NexusAPI("review");
            string citizenCPR = nancyBerggrenTestCPR;
            string pathwayName = "Dokumenttilknytning fra Vitae";

            var pathwayReferencesDocuments = nexusAPI.GetCitizenPathwayDocuments(citizenCPR, pathwayName);

            Assert.IsNotNull(pathwayReferencesDocuments);
        }

        [Test]
        public void GetAvailableProgramPathwaysForNancyBerggren()
        {
            NexusAPI nexusAPI = new NexusAPI("review");
            string citizenCPR = nancyBerggrenTestCPR;

            var availableProgramPathways = nexusAPI.GetPatientAvailableProgramPathways(citizenCPR);
            Assert.IsNotNull(availableProgramPathways);
        }
        [Test]
        public void GetAvailableProgramPathwaysEnrollmentLinkForNancyBerggrenOnSocialtOgSundhedsfagligtGrundforløb()
        {
            NexusAPI nexusAPI = new NexusAPI("review");
            string citizenCPR = nancyBerggrenTestCPR;
            string programPathwayName = "Socialt og sundhedsfagligt grundforløb";

            var enrollmentLink = nexusAPI.GetProgramPathwayEnrollmentLink(citizenCPR,programPathwayName);
            Assert.IsNotNull(enrollmentLink);
        }
        [Test]
        public void EnrollPatientToNonExistingProgramPathwayThatFails()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;
            string citizenCPR = nancyBerggrenTestCPR;
            string programPathwayName = "Socialt og sundhedsfagligt grundforløbTEST";

            PatientEnrolled_Root enrolledObject = null;
            try
            {
                enrolledObject = processes.EnrollPatientToProgramPathway(citizenCPR,programPathwayName);
            }
            catch (Exception)
            {

            }
            
            Assert.IsNull(enrolledObject);
        }
        [Test]
        public void EnrollPatientToExistingProgramPathway()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;
            string citizenCPR = nancyBerggrenTestCPR;
            string programPathwayName = "Socialt og sundhedsfagligt grundforløb";


            var enrolledObject = processes.EnrollPatientToProgramPathway(citizenCPR, programPathwayName);
            Assert.IsNotNull(enrolledObject);
        }
        [Test]
        public void GetPatientPathwayAssociations()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;
            string citizenCPR = nancyBerggrenTestCPR;

            var associatedPathways = api.GetPatientAvailablePathwayAssociations(citizenCPR);
            
            Assert.IsNotNull(associatedPathways);
        }

        [Test]
        public void GetPatientPathwayAssociation() //Returns a specific pathwayAssociation (Grundforløb)
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;
            string pathwayAssociationName = "sundhedsfagligt grundforløb";
            string citizenCPR = nancyBerggrenTestCPR;

            var pathwayAssociation = api.GetPatientPathwayAssociation(citizenCPR,pathwayAssociationName);

            Assert.IsNotNull(pathwayAssociation);
        }
        [Test]
        public void GetPatientPathwayAssociationUnclosableReferences()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;
            string pathwayAssociationName = "socialt og sundhedsfagligt grundforløb";
            string citizenCPR = nancyBerggrenTestCPR;

            var pathwayAssociation = api.GetPatientPathwayAssociation(citizenCPR, pathwayAssociationName);

            Assert.IsNotNull(pathwayAssociation);
        }

        //[Test]
        //public void GetDocumentObject()
        //{
        //    NexusAPI nexusAPI = new NexusAPI("review");
        //    string citizenCPR = nancyBerggrenTestCPR;
        //    string pathwayName = "Dokumenttilknytning fra Vitae";
        //    string pathwayChildName = "Dokumenter fra Vitae";


        //    string documentObjectResponse = "{\"id\":null,\"version\":0,\"uid\":\"e1388e3f-3d53-4e13-8b09-2f3a8d229eee\",\"name\":null,\"notes\":null,\"originalFileName\":null,\"uploadingDate\":null,\"relevanceDate\":\"2023-06-09T06:31:30.036+00:00\",\"status\":\"CREATED\",\"pathwayAssociation\":{\"placement\":{\"id\":null,\"version\":null,\"patientPathwayId\":26859,\"programPathwayId\":4809,\"_links\":{\"patientPathway\":{\"href\":\"/api/core/mobile/ringsted/v2/patientPathways/26859\"}}},\"parentReferenceId\":null,\"referenceId\":null,\"canAssociateWithPathway\":true,\"canAssociateWithPatient\":true,\"associatedWithPatient\":false,\"_links\":{\"availableRootProgramPathways\":{\"href\":\"https://ringsted.nexus-review.kmd.dk/api/core/mobile/ringsted/v2/patients/1623/availableProgramPathways\"},\"availablePathwayAssociation\":{\"href\":\"https://ringsted.nexus-review.kmd.dk/api/core/mobile/ringsted/v2/patients/1623/pathways/26859/availablePathwayAssociations\"}}},\"patientId\":1623,\"fileType\":null,\"tags\":[],\"origin\":null,\"externalId\":null,\"fileExternalId\":null,\"_links\":{\"create\":{\"href\":\"https://ringsted.nexus-review.kmd.dk/api/document-microservice/ringsted/documents\"},\"availableTags\":{\"href\":\"https://ringsted.nexus-review.kmd.dk/api/core/mobile/ringsted/v2/tags/UI/documentTags\"}},\"patientActivityType\":\"document\"}";
        //    var dObject = JObject.Parse(documentObjectResponse);
        //    var documentObject = nexusAPI.CreateDocumentObject(citizenCPR, pathwayName, pathwayChildName);
        //    documentObject.Name = "testDokumentFraCSharp - name";
        //    documentObject.OriginalFileName = "testDokumentFraCSharp - originalFileName";

        //    //var uploadedDocumentObject = nexusAPI.UploadDocumentObject(documentObject);


        //    Assert.IsNotNull(documentObject);
        //}

        //[Test]
        //public void ActivateInactiveSubstituteProfessionals()
        //{
        //    //NexusAPI nexusAPI = new NexusAPI("review");
        //    NexusAPI_processes processes = new NexusAPI_processes("review");
        //    processes.ActivateInactiveSubstituteProfessionals();
        //    var listOfActivatedProfessionals = processes.GetProfessionalsList();

        //    Assert.IsNotNull(listOfActivatedProfessionals);
        //}

        //[Test]
        //public void ActivateSpecificProfessional()
        //{
        //    NexusAPI_processes processes = new NexusAPI_processes("review");
        //    processes.ActivateProfessional(1448);

        //    var professional = processes.GetProfessional(1448);

        //    Assert.IsTrue(professional.Active);

        //}
        //[Test]
        //public void DeactivateSpecificProfessional()
        //{
        //    NexusAPI_processes processes = new NexusAPI_processes("review");
        //    processes.DeactivateProfessional(1448);

        //    var professional = processes.GetProfessional(1448);

        //    Assert.IsFalse(professional.Active);

        //}

        [Test]
        public void GetOrganizationId()
        {
            //NexusAPI api = new NexusAPI("live");
            //OrganizationsTree_Root organizationsTree =  api.GetOrganizationsTree();
            ////var OrgChildren = organizationsTree.Children[];
            string org = "Hjpl. Ude-team § 94";
            org = "Varmeforsyningen";
            int orgId = 0;
            //OrganizationsTree_Child organizationsTree_Child = new OrganizationsTree_Child();
            //if (organizationsTree.Name != org)
            //{
            //        orgId = api.ReturnOrgId(organizationsTree.Children,org);
            //        organizationsTree_Child = api.ReturnOrg(organizationsTree.Children,org);
            //}

            //string test = "2";

            NexusAPI_processes processes = new NexusAPI_processes("review");
            orgId = processes.api.GetOrganizationId(org);

            Assert.NotZero(orgId);
        }

        [Test]
        public void GetOrganizations()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;

            var organizations = api.GetOrganizations();

            Assert.IsNotNull(organizations);
        }
        [Test]
        public void GetOrganizationProfessionalsForBiblioteket()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;

            var professionals = api.GetOrganizationProfessionals("Biblioteket");
        }

        [Test]
        public void UpdateProfessionalOrganizations()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            try
            {
                List<string> orgsToAdd = new List<string>();
                List<string> orgsToRemove = new List<string>();

                orgsToAdd.Add(processes.api.GetOrganizationId("Depot").ToString());

                //You can't remove organizations that are not active on the professional.
                //So this will result in a bad request.
                orgsToRemove.Add(processes.api.GetOrganizationId("Børnecenter").ToString());
                orgsToRemove.Add(processes.api.GetOrganizationId("BC Servicelov").ToString());

                string add = string.Join(",", orgsToAdd);
                string remove = string.Join(",", orgsToRemove);

                processes.UpdateProfessionalOrganizations(1409, add, remove);
            }
            catch (Exception e)
            {
                Assert.IsNotNull(e);                
            }
            
            
        }

        [Test]
        public void SetProfessionalJobtitle()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            //string newJob = "udviklingskonsulent";
            string newJob = "vikar";
            int profId = 1409;
            //processes.SetProfessionalJobTitle(profId, newJob);
            processes.SetProfessionalJobTitle(profId, newJob);

            var prof = processes.api.GetProfessionalConfiguration(1409);
            var job = JsonConvert.DeserializeObject<ProfessionalJobs_Root>(prof.ProfessionalJob.ToString());
            Assert.AreEqual(newJob, job.Name);
        }

        [Test]
        public void GetPatientDetailsLinks()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;
            string cpr = nancyBerggrenTestCPR;
            var details = api.GetPatientDetails(cpr);

            var detailsLink = api.GetPatientDetailsLinks(cpr);
            Assert.IsNotNull(detailsLink);
        }

        [Test]
        public void GetPatientPreferences()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;
            string cpr = nancyBerggrenTestCPR;
            var preferences = api.GetPatientPreferences(cpr);


            Assert.IsNotNull(preferences);
        }

        [Test]
        public void GetCitizenPathway()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;
            string cpr = nancyBerggrenTestCPR;
            string elementName = "Dokumenttilknytning fra Vitae";
            var pathways = api.GetCitizenPathways(cpr);

            var pathway = api.GetElementFromList(pathways, elementName);

            Assert.AreEqual(pathway.Name, elementName);
        }
        [Test]
        public void GetCitizenPathwayDeepDive()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;
            string cpr = nancyBerggrenTestCPR;
            string elementName = "Dokumenttilknytning - alt";
            var pathways = api.GetCitizenPathways(cpr);

            var pathway = api.GetElementFromList(pathways, elementName);
            string pathwaySelfLink = pathway.Links.Self.Href;

            CitizenPathwaySelf_Root pathwaySelf = JsonConvert.DeserializeObject<CitizenPathwaySelf_Root>(api.CallAPI(api, pathwaySelfLink, Method.Get).Result.ToString());

            var patient = api.GetPatientDetails(cpr);
            string patientOverviewURL = patient.Links.PatientOverview.Href;
            var patientOverview = api.CallAPI(api, patientOverviewURL, Method.Get);
            var patientPreferences = api.GetPatientPreferences(cpr);
            var pathwayreferences = api.GetCitizenPathwayReferences(cpr, elementName);
            Assert.AreEqual(pathway.Name, elementName);
        }

        [Test]
        public void GetProfessionalOrganizations()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;
            int id = 1409;

            var orgs = processes.api.GetProfessionalPrimaryOrganizations(id);

            Assert.IsNotNull(orgs);
        }
        [Test]
        public void GetProfessionalPossibleJobs()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;
            int id = 1409;

            var jobs = processes.GetPossibleProfessionalJobs(id);

            Assert.IsNotNull(jobs);
        }
        [Test]
        public void GetProfessionals()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;
            string queryString = "Hansen";

            
            var professionals = api.GetProfessionals(queryString);
            Assert.IsNotNull(professionals);
        }
        [Test]
        public void SetProfessionalPrimaryOrganization()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;
            int id = 1409;

            processes.SetProfessionalPrimaryOrganization(id);

            //var professional = processes.api.GetProfessionalConfiguration(id);
            var professionalOrganization = processes.api.GetProfessionalPrimaryOrganization(id);
            Assert.AreEqual("Ringsted Kommune", professionalOrganization.Name);
        }

        [Test]
        public void SetSpecificProfessionalPrimaryOrganizationThatFailsBecauseThePrimaryOrganizationNameDoesNotExist()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;
            int id = 1409;
            string organizationName = "TestMSCH";
            processes.SetProfessionalPrimaryOrganization(id,organizationName);

            var professional = processes.api.GetProfessionalConfiguration(id);
            Assert.AreNotEqual(organizationName, professional.PrimaryOrganization);
        }

        
        [Test]
        public void GetPatientDetailsByIdAndCPR()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;

            int id = 1;

            var patientDetailsById = api.GetPatientDetails(id);
            var patientDetailsByCPR = api.GetPatientDetails(nancyBerggrenTestCPR);
        }
        [Test]
        public void GetPatientDetails()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;

            
            //var patientDetailsByCPR = api.GetPatientDetails("");
            var patientPreferences = api.GetPatientPreferences("");
        }
        [Test]
        public void GetCitizenPathwayNew()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;

            string pathwayName = "Dokumenttilknytning fra Vitae";
            string cpr = nancyBerggrenTestCPR;

            var info = api.GetCitizenPathway(cpr, pathwayName);
        }
        
        [Test]
        public void GetALLCitizenDocumentObjectsOnSpecifiedPathwayName() 
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;
            int id = 1623;
            string pathwayName = "Dokumenttilknytning fra Vitae";

            //var patient = api.GetPatientDetails(id);
            var documentList = processes.GetCitizenDocumentObjects(id, pathwayName,true);
        }
        [Test]
        public void GetONLYCitizenDocumentObjectsOnSpecifiedPathwayName()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;
            int id = 1623;
            string pathwayName = "Dokumenttilknytning fra Vitae";

            //var patient = api.GetPatientDetails(id);
            var documentList = processes.GetCitizenDocumentObjects(id, pathwayName, false);
        }

        [Test]
        public void GetALLCitizenDocumentObjectsOnSpecifiedChildPathwayName()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;
            int id = 1623;
            string pathwayName = "Dokumenttilknytning fra Vitae";

            //var patient = api.GetPatientDetails(id);
            var documentList = processes.GetCitizenDocumentObjects(id, pathwayName, true, "Dokumenter fra Vitae");
        }

        [Test]
        public void CloseCitizenPathways()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;
            int id = 694;
            string pathwayName = "Dokumenttilknytning fra Vitae";

            var citizenPathways = api.GetCitizenPathways(id);
            PatientPreferences_CITIZENPATHWAY pathwayToClose = new PatientPreferences_CITIZENPATHWAY();

            foreach (var pathway in citizenPathways)
            {
                if (pathway.Name == pathwayName)
                {
                    pathwayToClose = pathway; break;
                }
            }

            
        }

        //[Test]
        //public void ActivateDeactivatedSusbstituteProfessionalsAndSendEmailWithListOfActivatedProfessionals()
        //{
        //    NexusAPI_processes processes = new NexusAPI_processes("review");
        //    var api = processes.api;

        //    DateTime start = DateTime.Now;
        //    processes.ActivateInactiveSubstituteProfessionals();
        //    DateTime end = DateTime.Now;

        //    var prosActivated = processes.GetProfessionalsList();

            
        //    List<string> initialsList = new List<string>(); 

        //    string ToEmails = "msch@ringsted.dk,msch@ringsted.dk";
        //    string subject = "Testmail subject";
        //    string joinedViks = string.Empty;

        //    foreach (var pro in prosActivated)
        //    {
        //        initialsList.Add(pro.Initials.ToString());
        //    } 
        //    joinedViks = string.Join("\r\n", initialsList);
        //    string body = "Hej Martin og Martin, \r\nHer er listen over de(n) " + initialsList.Count + " bruger(e) der er blevet aktiveret.\r\n" + joinedViks;

        //    processes.SendEmail(ToEmails,subject,body, "noreply@ringsted.dk", "KMD Nexus robot");
            
        //    foreach (var pro in prosActivated)
        //    {
        //        processes.DeactivateProfessional(pro.Id);
        //    }
        //}

        //[Test]
        //public void TestSendMail()
        //{
        //    NexusAPI_processes processes = new NexusAPI_processes("review");
        //    var api = processes.api;

        //    processes.SendEmail("msch@ringsted.dk", "testSubject", "bodyText", "noreply@ringsted.dk", "KMD Nexus robot");
        //}

        //[Test]
        //public void ActivateDeactivatedSusbstituteProfessionals()
        //{
        //    NexusAPI_processes processes = new NexusAPI_processes("review");
        //    var api = processes.api;

        //    processes.ActivateInactiveSubstituteProfessionals();
            
        //}

        [Test]
        public void ReturnSearchedProfessionals()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;

            var df = api.GetProfessionals("jette nielsen");

        }

        
        [Test]
        public void GetSpecifiedCitizenListFromHomeRessourcePreferences()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;

            var preferencesLink = api.GetHomeRessourceLink("preferences");
            NexusResult preferencesResult = api.CallAPI(api, preferencesLink,Method.Get);
            Preferences_Root preferences = JsonConvert.DeserializeObject<Preferences_Root>(preferencesResult.Result.ToString());

            var citizenList = preferences.CITIZENLIST;

            Preferences_CITIZENLIST list = new Preferences_CITIZENLIST();
            foreach (var item in citizenList)
            {
                if (item.Name == "Døde/inaktive borgere med aktive forløb i kommunen")
                {
                    list = item;
                    break;
                }
            }

            string listLink = list.Links.Self.Href;
            var listResult = api.CallAPI(api, listLink, Method.Get);    
            CITIZEN_LIST_Root chosenListObject = JsonConvert.DeserializeObject<CITIZEN_LIST_Root>(listResult.Result.ToString());
            
        }
        [Test]
        public void GetCitizenActivePrograms()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;

            string citizenCPR = nancyBerggrenTestCPR;

            var patient = api.GetPatientDetails(citizenCPR);
            var patientPreferences = api.GetPatientPreferences(citizenCPR);
            var citizenPathways = api.GetCitizenPathways(citizenCPR);
            var citizenData = patientPreferences.CITIZENDATA;

            var patientLinks = api.GetPatientDetailsLinks(citizenCPR);
            string activeProgramsLink = patientLinks.ActivePrograms.Href;
            string availableBasketsLink = patientLinks.AvailableBaskets.Href;

            var activeProgramsLinkresult = api.CallAPI(api, activeProgramsLink, Method.Get);
            var activePrograms = JsonConvert.DeserializeObject<List<ActivePrograms_Root>>(activeProgramsLinkresult.Result.ToString());

            var availableBasketsResult = api.CallAPI(api,availableBasketsLink, Method.Get);

            var link = activePrograms[2].Links.Self.Href;
            var linkResult = api.CallAPI(api, link, Method.Get);

            var homeressource = api.homeRessource;
            Assert.IsNotNull(activePrograms);
        }
        [Test]
        public void GetPatientGrants()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;
            var homeRessource = api.homeRessource;

            var PatientGrantsLink = api.GetHomeRessourceLink("getPatientGrants");
            //NexusResult PatientGrantsResult = api.CallAPI(api, PatientGrantsLink, Method.Get);
            
        }

        [Test]
        public void GetAllPatients()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;

            //This takes forever as there's 14.000+ patients
            //var patients = api.GetAllPatients();
        }

        [Test]
        public void GetPatientNancyBerggren()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;

            var nancy = api.GetPatientDetails(nancyBerggrenTestCPR);
            string preferencesLink = nancy.Links.PatientPreferences.Href;

            var nancyPreferences = JsonConvert.DeserializeObject<PatientPreferences_Root>(api.CallAPI(api, preferencesLink, Method.Get).Result.ToString());

            #region citizendata
            PatientPreferences_CITIZENDATA nancyCitizenDataVitae = new PatientPreferences_CITIZENDATA();
            foreach (var item in nancyPreferences.CITIZENDATA)
            {
                if (item.Name == "- Journalnotater fra Vitae")
                {
                    nancyCitizenDataVitae = item;
                    break;
                }
            }
            CitizenDataSelf_Root nancyVitaeJournalNotes = JsonConvert.DeserializeObject<CitizenDataSelf_Root>(api.CallAPI(api, nancyCitizenDataVitae.Links.Self.Href, Method.Get).Result.ToString());
            #endregion citizendata


            #region citizendashboard
            PatientPreferences_CITIZENDASHBOARD nancyCitizenDashboard = new PatientPreferences_CITIZENDASHBOARD();
            foreach (var item in nancyPreferences.CITIZENDASHBOARD)
            {
                if (item.Name == "Historiske data fra CSC Vitae")
                {
                    nancyCitizenDashboard = item;
                    break;
                }
            }

            CitizenDashboardSelf_Root nancyHistoricDataVitae = JsonConvert.DeserializeObject<CitizenDashboardSelf_Root>(api.CallAPI(api, nancyCitizenDashboard.Links.Self.Href, Method.Get).Result.ToString());
            #endregion citizendashboard

            #region citizenpathway
            PatientPreferences_CITIZENPATHWAY nancyCitizenPathway = new PatientPreferences_CITIZENPATHWAY();
            foreach (var item in nancyPreferences.CITIZENPATHWAY)
            {
                if (item.Name == "Dokumenttilknytning fra Vitae")
                {
                    nancyCitizenPathway = item;
                    break;
                }
            }
            var nancypathway = api.CallAPI(api, nancyCitizenPathway.Links.Self.Href, Method.Get);
            //CitizenDashboardSelf_Root nancyHistoricDataVitae = JsonConvert.DeserializeObject<CitizenDashboardSelf_Root>(api.CallAPI(api, nancyCitizenDashboard.Links.Self.Href, Method.Get).Result.ToString());
            #endregion citizenpathway
        }

        [Test]
        public void GetDeadOrInactiveCitizensWithActivePathways()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;

            var deadList = processes.GetDeadOrInactiveCitizens();
            Assert.IsNotNull(deadList);
        }
        [Test]
        public void CitizensListThatDoesNotExist()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;
            List<Content_Page> deadList = null;
            try
            {
                deadList = processes.GetCitizenList("døde borgere");
            }
            catch (Exception)
            {
                //deadList = null;
            }
            
            Assert.IsNull(deadList);
        }

        [Test]
        public void DisableDeadCitizens()
        {
            NexusAPI_processes processes = new NexusAPI_processes("live");
            var api = processes.api;

            var deadCitizens = processes.GetDeadOrInactiveCitizens();
            DateTime? inactiveOrDeadDate = null;
            foreach (var citizen in deadCitizens)
            {
                var citizenState = citizen.PatientState.Name; // This value is used when looping the patientStateValuePeriods under PatientStateValueSchedule on the patientDetailsObject
                var citizenStateType = citizen.PatientState.Type.Id;
                var patientDetails = api.GetPatientDetails(citizen.PatientIdentifier.Identifier);
                if (citizenStateType == "DEAD")
                {
                    List<PatientDetailsSearch_ValuePeriod> valuePeriods = patientDetails.PatientStateValueSchedule.ValuePeriods;
                    foreach (var valuePeriod in valuePeriods)
                    {
                        if (valuePeriod.Value.Name == citizenState)
                        {
                            inactiveOrDeadDate = valuePeriod.StartDate;
                        }
                    }
                    
                }

                //Do what needs to be done to be able to deactivate/close the citizen in Nexus

                var patientPreferences = api.GetPatientPreferences(citizen.PatientIdentifier.Identifier);   
            }
            Assert.IsNotNull(deadCitizens);
        }

        // Find alle forløb og grundforløb på siden relationer under overblik
        [Test]
        public void testCitizenDashboadElements()
        {
            NexusAPI_processes processes = new NexusAPI_processes(reviewEnvironment);
            var api = processes.api;

            var widget = api.GetWidgetPathwayReferencesLink(nancyBerggrenTestCPR, "Relationer", "Opret Grundforløb og Forløb");
            var pathwayReference = api.GetWidgetPathwayReference(nancyBerggrenTestCPR, "Relationer", "Opret Grundforløb og Forløb", "Socialt og sundhedsfagligt grundforløb");
            Assert.IsNotNull(widget);
        }

        [Test]
        public void testMedcom()
        {
            NexusAPI_processes processes = new NexusAPI_processes(liveEnvironment);
            var api = processes.api;

            var preferences = api.GetPreferences();
            var activityLists = preferences.ACTIVITYLIST;
            var list = activityLists[1]; // medcom list
            var webResultList = api.CallAPI(api, list.Links.Self.Href, Method.Get);

            ACTIVITYLIST_Root listObject = JsonConvert.DeserializeObject< ACTIVITYLIST_Root>(webResultList.Result.ToString());
            string contentLink = listObject.Links.Content.Href;
            var fromDate = api.ConvertDateToUrlParameter(25, 01, 2024, true);
            var ToDate = api.ConvertDateToUrlParameter(25, 01, 2024, false);
            string endpointString = contentLink + "&from=" + fromDate + "&to=" + ToDate + "&pageSize=50";
            
            
            var webResultContent = api.CallAPI(api, endpointString, Method.Get);
            ACTIVITYLIST_Content_Root contentObject = JsonConvert.DeserializeObject< ACTIVITYLIST_Content_Root> (webResultContent.Result.ToString());  
            var pages = contentObject.Pages;
            List<ACTIVITYLIST_Pages_Content_Root> pagesContent = new List<ACTIVITYLIST_Pages_Content_Root>();
            string totalList = "";
            foreach ( var page in pages )
            {
                string endpointLink = page.Links.Content.Href;
                var webResultPage = api.CallAPI(api,endpointLink, Method.Get);
                List<ACTIVITYLIST_Pages_Content_Root> pageContent = JsonConvert.DeserializeObject<List<ACTIVITYLIST_Pages_Content_Root>>(webResultPage.Result.ToString());

                foreach (var item in pageContent)
                {
                    pagesContent.Add(item);
                }
                break;
            }

            //List<string> types = new List<string>();
            foreach (var item in pagesContent)
            {
                // Getting each individual MedCom element
                string referencedObjectLink = item.Links.ReferencedObject.Href;
                var webResultRefObject = api.CallAPI(api,referencedObjectLink, Method.Get);
                ReferencedObject_Base_Root baseObject = JsonConvert.DeserializeObject<ReferencedObject_Base_Root>(webResultRefObject.Result.ToString());
                var element = api.GetRefObjectElement(baseObject, webResultRefObject.Result.ToString());
                RefObject_medcom_Root medcomElement = null;
                RefObject_clinicalemail_Root clinicalemailElement = null;
                RefObject_rehabplan_Root rehabilitaionElement = null;

                string elementTransformedBodyLink = string.Empty;
                switch (baseObject.Type.ToString())
                {
                    case "medcom":
                        medcomElement = (RefObject_medcom_Root)element;
                        elementTransformedBodyLink = medcomElement.Links.TransformedBody.Href;
                        break;
                    case "clinicalEmail2006":
                        clinicalemailElement = (RefObject_clinicalemail_Root)element;
                        elementTransformedBodyLink = clinicalemailElement.Links.TransformedBody.Href;
                        break;
                    case "rehabilitationplan":
                        rehabilitaionElement = (RefObject_rehabplan_Root)element;
                        elementTransformedBodyLink = rehabilitaionElement.Links.TransformedBody.Href;
                        break;
                    default:
                        break;
                }
                if (/*baseObject.Patient.FullName == "Jytte Gudrun Jensen" && */baseObject.Subject.ToLower() == "udskrivningsrapport")
                {
                    string stopp = "stop";
                

                    //string elementTransformedBodyLink = medcomElement.Links.TransformedBody.Href;
                    var webResultElementTransformedBody2 = api.GetTransformedBodyOfMedcomMessage(api, elementTransformedBodyLink).Result.ToString();

                    //Parse the result and find needed information to put into a class object
                    #region 
                    //var HTMLDocument = GetParsedHTML(webResultElementTransformedBody2.Result.ToString());
                    //var allElements = HTMLDocument.All;

                    HtmlHandler handler = new HtmlHandler();
                    TransformedBody_Root transformedBody = new TransformedBody_Root();
                    
                    var valueToHave = handler.GetResultFromHtmlSelectNextUntil(webResultElementTransformedBody2, "Pårørende/Relationer","Aktuel indlæggelse","table");
                    //api.AddDataToTransformedBodyObject(transformedBody, valueToHave, "Pårørende/Relationer");




                    #endregion 
                }
            }
            Assert.IsNotNull (preferences);
        }
        [Test]
        public void FF()
        {
            TransformedBody_Root transformedBody = new TransformedBody_Root();
            TransformedBody_Relavites relative = new TransformedBody_Relavites();
            TransformedBody_ContactInformation contactInformation = new TransformedBody_ContactInformation();

            string title = "Mobiltelefon";
            string value = "12345678";

            contactInformation.Title = title;
            contactInformation.Value =value;
            string value1 = "87654321"; 
            contactInformation.Value = value1;

            relative.ContactInformation.Add (contactInformation);

        }
        #region medcom messages
        
        [Test]
        public void GetDischargeReportData() // Needs input of a Medcom message of type DischargeReport
        {
            NexusAPI_processes processes = new NexusAPI_processes(liveEnvironment);
            var api = processes.api;

# region getting medcom messages for test. This should be provided when calling the method
            //var medComMessages = api.GetPreferencesActivityListSelfObjectContent("- MedCom - arkiveret sidste uge", 20, 01, 2024, 25, 01, 2024);
            //ReferencedObject_Base_Root chosenMedComMessage = new ReferencedObject_Base_Root();
            //foreach (var medComMessage in medComMessages)
            //{
            //    ReferencedObject_Base_Root baseObject = api.GetActivityListContentBaseObject(medComMessage);
            //    if (baseObject.Subject.ToLower().Contains("udskrivningsrapport"))
            //    {
            //        chosenMedComMessage = baseObject;
            //        string serializedObject = JsonConvert.SerializeObject(baseObject);

            //        //break;
            //    }
            //}

            #endregion getting medcom messages for test
            #region hardcoded baseobjects and tests
            List<string> baseObjects = new List<string>();
            baseObjects.Add("{\"type\":\"medcom\",\"id\":368887,\"version\":5,\"sender\":{\"type\":\"medcomMessageDetailsBased\",\"id\":368887,\"name\":\"Region Sjællands Sygehusvæsen - Fælledvej 11 - 4200 Slagelse - EAN: 5790001360641\",\"recipientAddress\":null,\"recipientType\":\"MESSAGE_SENDER\",\"_links\":{\"medcomRecipient\":{\"href\":\"/api/core/mobile/ringsted/v2/medcomRecipients/medcomSender/368887\"},\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"recipient\":{\"type\":\"medcomMessageDetailsBased\",\"id\":368887,\"name\":\"Ringsted Kommune - Zahlesvej 18 - 4100 Ringsted - EAN: 5790001353858\",\"recipientAddress\":null,\"recipientType\":\"MESSAGE_RECIPIENT\",\"_links\":{\"medcomRecipient\":{\"href\":\"/api/core/mobile/ringsted/v2/medcomRecipients/medcomRecipient/368887\"},\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"subject\":\"Udskrivningsrapport\",\"date\":\"2024-01-25T12:45:00+01:00\",\"state\":{\"direction\":\"INCOMING\",\"status\":\"FINAL\",\"response\":\"NOT_AWAITING\",\"handshake\":\"NO\",\"processed\":\"DECLINED\",\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"customer\":null,\"raw\":\"PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiPz48bnMxOkVtZXNzYWdlIHhtbG5zPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIHhtbG5zOm5zMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4zIj48RW52ZWxvcGU+PFNlbnQ+PERhdGU+MjAyNC0wMS0yNTwvRGF0ZT48VGltZT4xMjozOTwvVGltZT48L1NlbnQ+PElkZW50aWZpZXI+RVBJQzY0MTgyMDU2NDwvSWRlbnRpZmllcj48QWNrbm93bGVkZ2VtZW50Q29kZT5wbHVzcG9zaXRpdmt2aXR0PC9BY2tub3dsZWRnZW1lbnRDb2RlPjwvRW52ZWxvcGU+PG5zMTpSZXBvcnRPZkRpc2NoYXJnZT48bnMxOkxldHRlcj48bnMxOklkZW50aWZpZXI+RVBJQzY0MTgyMDU2NDwvbnMxOklkZW50aWZpZXI+PG5zMTpWZXJzaW9uQ29kZT5YRDE4MzRDPC9uczE6VmVyc2lvbkNvZGU+PG5zMTpTdGF0aXN0aWNhbENvZGU+WERJUzE4PC9uczE6U3RhdGlzdGljYWxDb2RlPjxuczE6QXV0aG9yaXNhdGlvbj48RGF0ZT4yMDI0LTAxLTI1PC9EYXRlPjxUaW1lPjEyOjM5PC9UaW1lPjwvbnMxOkF1dGhvcmlzYXRpb24+PG5zMTpUeXBlQ29kZT5YRElTMTg8L25zMTpUeXBlQ29kZT48bnMxOkVwaXNvZGVPZkNhcmVJZGVudGlmaWVyPjAwMDAwMDAwMDAyNTNBOTNFNzY4QjdENjdBQzc4NkI1PC9uczE6RXBpc29kZU9mQ2FyZUlkZW50aWZpZXI+PC9uczE6TGV0dGVyPjxTZW5kZXI+PEVBTklkZW50aWZpZXI+NTc5MDAwMTM2MDY0MTwvRUFOSWRlbnRpZmllcj48SWRlbnRpZmllcj4zODAwUzEwPC9JZGVudGlmaWVyPjxJZGVudGlmaWVyQ29kZT5zeWdlaHVzYWZkZWxpbmdzbnVtbWVyPC9JZGVudGlmaWVyQ29kZT48T3JnYW5pc2F0aW9uTmFtZT5SZWdpb24gU2rDpmxsYW5kcyBTeWdlaHVzdsOmc2VuPC9PcmdhbmlzYXRpb25OYW1lPjxEZXBhcnRtZW50TmFtZT5TTEEgS2lydXJnaXNrIEFmZC48L0RlcGFydG1lbnROYW1lPjxVbml0TmFtZT5LaXJ1cmdpc2sgU2VuZ2VhZnNuaXQ8L1VuaXROYW1lPjxTdHJlZXROYW1lPkbDpmxsZWR2ZWogMTE8L1N0cmVldE5hbWU+PERpc3RyaWN0TmFtZT5TbGFnZWxzZTwvRGlzdHJpY3ROYW1lPjxQb3N0Q29kZUlkZW50aWZpZXI+NDIwMDwvUG9zdENvZGVJZGVudGlmaWVyPjxUZWxlcGhvbmVTdWJzY3JpYmVySWRlbnRpZmllcj41ODU1OTg1MTwvVGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+PFNpZ25lZEJ5PjxJZGVudGlmaWVyPjM4MDBTMTA8L0lkZW50aWZpZXI+PElkZW50aWZpZXJDb2RlPnN5Z2VodXNhZmRlbGluZ3NudW1tZXI8L0lkZW50aWZpZXJDb2RlPjxQZXJzb25HaXZlbk5hbWU+TGluZTwvUGVyc29uR2l2ZW5OYW1lPjxQZXJzb25TdXJuYW1lTmFtZT5Ow7hycmVsdW5kPC9QZXJzb25TdXJuYW1lTmFtZT48UGVyc29uVGl0bGU+U3lnZXBsZWplcnNrZTwvUGVyc29uVGl0bGU+PC9TaWduZWRCeT48TWVkaWNhbFNwZWNpYWxpdHlDb2RlPmtpcnVyZ2lfc3lnZWh1czwvTWVkaWNhbFNwZWNpYWxpdHlDb2RlPjxDb250YWN0SW5mb3JtYXRpb24+PENvbnRhY3ROYW1lPk1hdmUgdGFybSBraXI8L0NvbnRhY3ROYW1lPjxDb250YWN0TmFtZT5HcnVwcGUgMjwvQ29udGFjdE5hbWU+PFRlbGVwaG9uZVN1YnNjcmliZXJJZGVudGlmaWVyPjU4NTU5ODUxPC9UZWxlcGhvbmVTdWJzY3JpYmVySWRlbnRpZmllcj48L0NvbnRhY3RJbmZvcm1hdGlvbj48L1NlbmRlcj48UmVjZWl2ZXI+PEVBTklkZW50aWZpZXI+NTc5MDAwMTM1Mzg1ODwvRUFOSWRlbnRpZmllcj48SWRlbnRpZmllcj4zMjk8L0lkZW50aWZpZXI+PElkZW50aWZpZXJDb2RlPmtvbW11bmVudW1tZXI8L0lkZW50aWZpZXJDb2RlPjxPcmdhbmlzYXRpb25OYW1lPlJpbmdzdGVkIEtvbW11bmU8L09yZ2FuaXNhdGlvbk5hbWU+PERlcGFydG1lbnROYW1lPlNvY2lhbCBvZyBTdW5kaGVkPC9EZXBhcnRtZW50TmFtZT48VW5pdE5hbWU+SGplbW1lc3lnZXBsZWplbiwgUmluZ3N0ZWQgS29tbXVuZTwvVW5pdE5hbWU+PFN0cmVldE5hbWU+WmFobGVzdmVqIDE4PC9TdHJlZXROYW1lPjxEaXN0cmljdE5hbWU+UmluZ3N0ZWQ8L0Rpc3RyaWN0TmFtZT48UG9zdENvZGVJZGVudGlmaWVyPjQxMDA8L1Bvc3RDb2RlSWRlbnRpZmllcj48L1JlY2VpdmVyPjxQcmFjdGl0aW9uZXI+PElkZW50aWZpZXI+MDA3OTM1PC9JZGVudGlmaWVyPjxJZGVudGlmaWVyQ29kZT55ZGVybnVtbWVyPC9JZGVudGlmaWVyQ29kZT48UGVyc29uTmFtZT5NYXJpZSBWaWxsdW0gTWFya2VyPC9QZXJzb25OYW1lPjxUZWxlcGhvbmVTdWJzY3JpYmVySWRlbnRpZmllcj4zNTM4NzgyODwvVGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+PC9QcmFjdGl0aW9uZXI+PFBhdGllbnQ+PENpdmlsUmVnaXN0cmF0aW9uTnVtYmVyPjA5MTI3ODEwMTE8L0NpdmlsUmVnaXN0cmF0aW9uTnVtYmVyPjxQZXJzb25TdXJuYW1lTmFtZT5DaHJpc3RpYW5zZW48L1BlcnNvblN1cm5hbWVOYW1lPjxQZXJzb25HaXZlbk5hbWU+Q2xhdXMgU29vbi1Xb288L1BlcnNvbkdpdmVuTmFtZT48U3RyZWV0TmFtZT5Lw7hnZXZlaiA2MDwvU3RyZWV0TmFtZT48RGlzdHJpY3ROYW1lPlJpbmdzdGVkPC9EaXN0cmljdE5hbWU+PFBvc3RDb2RlSWRlbnRpZmllcj40MTAwPC9Qb3N0Q29kZUlkZW50aWZpZXI+PFRlbGVwaG9uZVN1YnNjcmliZXJJZGVudGlmaWVyPjMwNDMyNjEzPC9UZWxlcGhvbmVTdWJzY3JpYmVySWRlbnRpZmllcj48L1BhdGllbnQ+PG5zMTpSZWxhdGl2ZXM+PG5zMTpSZWxhdGl2ZT48bnMxOlJlbGF0aW9uQ29kZT5mb3LDpmxkcmU8L25zMTpSZWxhdGlvbkNvZGU+PFBlcnNvblN1cm5hbWVOYW1lPkNocmlzdGlhbnNlbjwvUGVyc29uU3VybmFtZU5hbWU+PFBlcnNvbkdpdmVuTmFtZT5FbW1hPC9QZXJzb25HaXZlbk5hbWU+PENvZGVkVGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+PFRlbGVwaG9uZUNvZGU+bW9iaWw8L1RlbGVwaG9uZUNvZGU+PFRlbGVwaG9uZVN1YnNjcmliZXJJZGVudGlmaWVyPjI0Njc1MzA4PC9UZWxlcGhvbmVTdWJzY3JpYmVySWRlbnRpZmllcj48L0NvZGVkVGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+PG5zMTpJbmZvcm1lZFJlbGF0aXZlPjE8L25zMTpJbmZvcm1lZFJlbGF0aXZlPjwvbnMxOlJlbGF0aXZlPjxuczE6Q29tbWVudD5Cb3IgaG9zIG1vciBpIE7DpnN2ZWQuIERhIGRldCBrdW4gZXIgc3lnZXBsZWplIHNlbmRlciBQRlAga3VuIHRpbCBqZXIuIE1vciBhZHJlc3NlOiBWZWpnw6VyZGVuIDMzLCBIZXJsdWZtYWdsZTwvbnMxOkNvbW1lbnQ+PC9uczE6UmVsYXRpdmVzPjxBZG1pc3Npb24+PERhdGU+MjAyMy0xMi0wNjwvRGF0ZT48VGltZT4xODoyNTwvVGltZT48L0FkbWlzc2lvbj48RW5kT2ZUcmVhdG1lbnQ+PERhdGU+MjAyNC0wMS0yNTwvRGF0ZT48VGltZT4xMzowMDwvVGltZT48L0VuZE9mVHJlYXRtZW50PjxEaXNjaGFyZ2U+PERhdGU+MjAyNC0wMS0yNTwvRGF0ZT48VGltZT4xMzowMDwvVGltZT48L0Rpc2NoYXJnZT48Q2F1c2VPZkFkbWlzc2lvbj4zMC81LTIzOiBpbmRsYWd0IG1lZCBvYnN0aXBhdGlvbi4gVWR2aWtsZXQgYWt1dCBwYW5jcmVhdGl0IG9nIGVmdGVyZsO4bGdlbmRlIFdPTi4gT3BzdGFydGV0IG1lcm9uZW0uIFN2w6ZydCBzZXB0aXNrIG9nIHDDpSBpbnRlbnNpdiwgaHZvciBoYW4gYmxldiBpbnR1YmVyZXQsIHRyYWNoZW90b21lcmV0LiAgIFVkdmlrbGV0IHNlcHRpc2tlIGVtYm9saWVyIGkgaGplcm5lbi4gIDcvNy0yMzogSG90IGF4aW9zIGRyw6ZuYWdlIGFmIFdPTiBtZWQgZWZ0ZXJmw7hsZ2VuZGUgc3bDpnIgc2Vwc2lzLiBCZWhhbmRsZXQgbWVkIENlZnRhemlkaW0vTGV2b2Zsb3hhY2luL1ZhbmNvbXljaW4gb2cgQ2FzcG9mdW5naW4uIEJsw7hkbmluZyBmcmEgc3RlbnRlbiAtIGtvbnNlcnZhdGl2dCBiZWhhbmRsZXQuICBBbmxhZ3QgZG9iYmVsdCBwaWd0YWlsIGlnZW5uZW0gaG90IGF4aW9zICsgZXh0ZXJuIGRyw6ZuYWdlIHBnYSBkw6VybGlnIGluZmVrdGlvbnNrb250cm9sLiBBc2NpdGVzZHLDpm5hZ2UsIHNvbSBibGV2IGdhbGRlZmFydmV0LiBIw6VuZHRlcmV0IG1lZCBQVEMtZHLDpm4uICAgMTMvNy0yMzogTWFuaWZlc3QgYWJkb21pbmFsIGNvbXBhcnRtZW50IHN5bmRyb20uIExhcGFyb3RvbWVyZXQgbWVkIGjDpm1hdG9tZmplcm5lbHNlLCBjaG9sZWN5c3Rla3RvbWksIG5la3Jvc2VrdG9taSBvZyBsYXZhZ2UuIE1pZGxlcnRpZGlnIGx1a25pbmcgYWYgYWJkb21lbiBvdmVyIHZpY3J5bG5ldC4gT21sYWd0IGZyYSBjYXNwb2Z1bmdpbiB0aWwgZmx1Y29uYXpvbC4gRS4gZsOmY2l1bSBzZXBzaXMuIFRpbGxhZ3QgVGlnZWN5Y2xpbi4gIDMxLzctMjM6IElnZW4gbWFuZ2xlbmRlIGluZmVrdGlvbnNrb250cm9sIHNhbXQgaGdiIGZhbGQuIENUIHZpc3RlIHN0b3J0IGxldmVyaMOmbWF0b20gc2FtdCBsZXZlcm5la3Jvc2UuICAgS29uc2VydmF0aXZ0IGJlaGFuZGxldC4gb2cgYmxldiBzZXQgYW4uIERyw6ZuIGkgZ2FsZGVibMOmcmVsZWpldCBvZyBsaWxsZSBiw6Zra2VuLCBzb20gdGlsIHNpZHN0IGJsZXYgZmplcm5ldC4gIDQvOS0yMzogU3VwZXJpbmZla3Rpb24gYWYgaMOmbWF0b21ldC4gQW5sYWd0IHNreWxsZWRyw6ZuLiBLbGVic2llbGxhLCBow6Ztb3BoaWx1cyBpbmZsdWVuemEgb2cgYmFjdGVyb2lkZXMsIHNvbSBibGV2IGJlaGFuZGxldCBtZWQgY2VmdHJpYXhvbiBvZyBtZXRyb25pZGF6b2wuICBMYW5nc29tIHJlc3BpcmF0b3Jpc2sgYmVkcmluZyBvZyBrdW5uZSBkZWthbnlsZXJlcy4gIDIwLzktMjM6IE55dCBkcsOmbiBhbmxhZ3QuIEVmdGVyIGtvbmZlcmVuY2ViZXNsdXRuaW5nIGJsZXYgZGVyIHZhbGd0IGZvcnRzYXQga29uc2VydmF0aXYgYmVoYW5kbGluZyBtaHAgYXQgYWZ2ZW50ZSBtaW5kc2tuaW5nIGFmIGjDpm1hdG9tZXQgb2cga3VuIG9wZXJlcmUgZGV0IHDDpSB2aXRhbCBpbmRpa2F0aW9uLiBQdCBhdXRvc2Vwb25lcmVkZSBkZXN2w6ZycmUgZHLDpm5ldCAgZGVuIDE5LzEwLTIzLiAgMjAvMTAtMjM6IFVkc2tyZXZldCBmcmEgaW50ZW5zaXYuIE1ldHJvbmlkYXpvbCBzZXBvbmVyZXQuIE1vbm90ZXJhcGkgbWVkIGNlZnRyaWF4b24uICA5LzExLTIzOiBCZXNsdXR0ZXQgbGV2ZXJraXJ1cmdpc2sgaW50ZXJ2ZW50aW9uLiBGamVybmV0IGFic2NlcyBvZyBuZWtyb3NlIGZyYSBzdWJmcmVuaXNrIG9nIGjDuGpyZSBsZXZlcmxhcC4gQ2VmdHJpYXhvbiwgbWV0cm9uaWRhem9sIG9nIHZhbmNvbXljaW4uIERyw6ZuIGZqZXJuZXQgMjcvMTEtMjMuICAyOS8xMS0yMzogS2lyLiBsdWtuaW5nIGFmIHRyYWNoZXN0b21pLiA0LzEyLTIzOiBBbmxhZ3QgZHLDpm4gaSBow7hqcmVzaWRpZyBwYXJhaGVwYXRpc2sgYW5zYW1saW5nLiA2LzEyLTIzOiBPdmVyZmx5dHRldCBoZXIgdGlsIFNsYWdlbHNlIG1ocCB2aWRlcmUgYmVoYW5kbGluZy4gUHQgZXIgdGlkbGlnZXJlIGtlbmR0IG1lZCBhbGtvaG9sb3ZlcmZvcmJ1ZyBvZyBzdG9mbWlzYnJ1Zy4gVW5kZXIgaW5kbMOmZ2dlbHNlIGkgVHlza2xhbmQgYmVza3JldmV0IG1pc3Ryb2lzayBvZyBhZ2l0ZXJldCBlciBvcHN0YXJ0ZXQgaSBhbnRpcHN5a290aXNrIGJlaGFuZGxpbmcuPC9DYXVzZU9mQWRtaXNzaW9uPjxDb3Vyc2VPZkFkbWlzc2lvbj5IYXIgaGFmdCBsYW5ndCBmb3Jsw7hiIGhlciBtZWQgbW9uaXRvcmVyaW5nIGFmIG9yZ2FuZXIsIHNhbXRsZXIgbWVkIHBzeWssIGdlbm9wdHLDpm5pbmcsIHN0ZW5hbmzDpmdnZWxzZSBpIGdhbGRlZ2FuZ2Ugb2cgZHLDpm4gaSBhbnNhbWxpbmcgdmVkIGxldmVyLiBIYXIgZ2VubmVtIGRlIHNpZHN0ZSBwYXIgdWdlciB2w6ZyZXQgcMOlIG9ybG92IGh2ZXIgZGFnLiBLdW4gaGVyaW5kZSBmb3Igc2t5bGxlIHDDpSBkcsOmbiBvZyB0aWxzZSBzw6VyIHDDpSBvcyBzYWNydW0gc2FtdCBww6UgYWJkb21lbi4gSGFyIHVkdmlrbGV0IGRpYWJldGVzLiBNw6Vsc2VyIHNlbHYgb2cgc3R5cmUgc2VsdiBpbnN1bGluIHRhZ25pbmcuIEbDuGxnZXMgaSBLw7hnZSBtZWQgZGV0dGUuVWRza3JpdmVzIG1lZCDDpWJlbiBpbmRsw6ZnZ2Vsc2UgaGVyLjwvQ291cnNlT2ZBZG1pc3Npb24+PG5zMTpOdXJzaW5nUHJvYmxlbUFyZWFzPjxuczE6TnVyc2luZ1Byb2JsZW1BcmVhPjxuczE6VGVybWlub2xvZ3k+PG5zMTpDb2RlPkEtMTIzNDU8L25zMTpDb2RlPjxuczE6Q29kZVN5c3RlbT5tZWRjb208L25zMTpDb2RlU3lzdGVtPjxuczE6Q29kZURlc2NyaXB0aW9uPkZ1bmt0aW9uc25pdmVhdSBvZyBiZXbDpmdlYXBwYXJhdDwvbnMxOkNvZGVEZXNjcmlwdGlvbj48L25zMTpUZXJtaW5vbG9neT48bnMxOlByb2JsZW1BcmVhPlByaW3DpnIgc2VrdG9yOiBmw6VyIGhqw6ZscC9zdMO4dHRlIGFmOiBLbGFyZXIgc2lnIHVkZW4gaGrDpmxwIDxCcmVhayAvPlDDpXLDuHJlbmRlOiBmw6VyIGhqw6ZscCBhZjogS2xhcmVyIHNpZyB1ZGVuIGhqw6ZscCA8QnJlYWsgLz5IasOmbHAgdGlsIHBlcnNvbmxpZyBoeWdpZWpuZTogU2VsdmhqdWxwZW4gPEJyZWFrIC8+SGrDpmxwIHZlZHLDuHJlbmRlIHRvaWxldGJlc8O4ZzogQnJ1Z2VyIGJsZSwgR8OlciBzZWx2IHDDpSB0b2lsZXR0ZXQgPEJyZWFrIC8+SGrDpmxwIHRpbCBhdCBzcGlzZSBvZyBkcmlra2U6IFNlbHZoanVscGVuIDxCcmVhayAvPkhqw6ZscCB0aWwgdmVuZGluZy9sZWpyaW5nOiBWZW5kZXIgc2lnIHNlbHYgPEJyZWFrIC8+TGVqcmluZzogU2lkZSB2ZW5zdHJlIDxCcmVhayAvPkhqw6ZscCB0aWwgbW9iaWxpc2VyaW5nOiBTZWx2aGp1bHBlbiA8QnJlYWsgLz5Nb2JpbGlzZXJpbmdzcmVzdHJpa3Rpb25lcjogSW5nZW4gcmVzdHJpa3Rpb25lciA8QnJlYWsgLz5IasOmbHBlbWlkbGVyIHRpbCBtb2JpbGlzZXJpbmc6IEvDuHJlc3RvbCA8QnJlYWsgLz5Nb2JpbGlzZXJpbmc6IEfDpXIgcMOlIHN0dWVuIDxCcmVhayAvPk1vYmlsaXNlcmluZyAoYW50YWwgZ2FuZ2UgaSB2YWd0ZW4pIDogMSA8QnJlYWsgLz5Nb2JpbGlzZXJpbmcgKHRpZCBwci4gZ2FuZyk6ICAoaGplbW1lIGJlc8O4ZyB0aWxiYWdlIGtsIDIwOjMwKSA8QnJlYWsgLz5MZWpyaW5nIC0gcGF0aWVudHZlamxlZG5pbmcvaW52b2x2ZXJpbmcgOiBQdC5zZWx2IG9icyBww6UgbGVqcmluZy92ZW5kaW5nIDxCcmVhayAvPlVkIGZyYSBmdW5rdGlvbnNldm5ldnVyZGVyaW5nZW4gdnVyZGVyZXM6ICBGYWxkcmlzaWtvIDxCcmVhayAvPlJpc2lrb2Zha3RvcmVyIGZvciBmYWxkOiBOZWRzYXQgYmFsYW5jZWV2bmUgb2cgbW9iaWxpdGV0IDxCcmVhayAvPlNpdHVhdGlvbmVyIG1lZCDDuGdldCBmYWxkcmlzaWtvOiBBbmRldCwgR8OlZW5kZSB1ZGVuIGbDuGxnZS9zdMO4dHRlIChWZWQgdWR0csOmdHRlbHNlLik8L25zMTpQcm9ibGVtQXJlYT48L25zMTpOdXJzaW5nUHJvYmxlbUFyZWE+PG5zMTpOdXJzaW5nUHJvYmxlbUFyZWE+PG5zMTpUZXJtaW5vbG9neT48bnMxOkNvZGU+Qi0xMjM0NTwvbnMxOkNvZGU+PG5zMTpDb2RlU3lzdGVtPm1lZGNvbTwvbnMxOkNvZGVTeXN0ZW0+PG5zMTpDb2RlRGVzY3JpcHRpb24+RXJuw6ZyaW5nPC9uczE6Q29kZURlc2NyaXB0aW9uPjwvbnMxOlRlcm1pbm9sb2d5PjxuczE6UHJvYmxlbUFyZWE+SMO4amRlOiAxNjggY20gPEJyZWFrIC8+VsOmZ3Q6IDUzLDUga2cgPEJyZWFrIC8+Qk1JIChCZXJlZ25ldCk6IDE5IDxCcmVhayAvPkFwcGV0aXQ6IE5vcm1hbCBhcHBldGl0IDxCcmVhayAvPlTDuHJzdDogTm9ybWFsIHTDuHJzdCA8QnJlYWsgLz5LdmFsbWUgZ3JhZC9oeXBwaWdoZWQ6IEluZ2VuIDxCcmVhayAvPktvc3Rmb3JtOiBOb3JtYWxrb3N0L0h2ZXJkYWdza29zdCA8QnJlYWsgLz5EacOmdDogSW5nZW4gZGnDpnQgPEJyZWFrIC8+QmVob3YgZm9yIG1vZGlmaWNlcmV0IGtvbnNpc3RlbnMgYWYgbWFkL2RyaWtrZSA6IE5laiA8QnJlYWsgLz5BbmJlZmFsZXQga29uc2lzdGVucyBhZiBtYWQgKGVyZ28pIDogVW1vZGlmaWNlcmV0IGtvbnNpc3RlbnMgPEJyZWFrIC8+QW5iZWZhbGV0IGtvbnNpc3RlbnMgYWYgZHJpa2tlIChlcmdvKSA6IFVtb2RpZmljZXJldCA8QnJlYWsgLz5BbmJlZmFsaW5nZXIgdmVkci4gaW5kdGFnZWxzZSBhZiBtZWRpY2luIChlcmdvKTogVGFibGV0dGVyIG1lZCBtYWQvZHJpa2tlIDxCcmVhayAvPkVybsOmcmluZ3MtIG9nIHbDpnNrZXJlc3RyaWt0aW9uOiBJa2tlIHJlbGV2YW50IDxCcmVhayAvPlbDpnNrZXJlc3RyaWt0aW9uIHBlciBvczogSW5nZW4gdsOmc2tlcmVzdHJpa3Rpb24gcGVyIG9zIDxCcmVhayAvPkVybsOmcmluZyBvZyBrdmFsbWUvb3BrYXN0IOKAkyBpbnRlcnZlbnRpb246IElra2UgYmVob3YgZm9yIGVybsOmcmluZ3MgaW50ZXJ2ZW50aW9uZXIgPEJyZWFrIC8+RXJuw6ZyaW5nIC0gcGF0aWVudHZlamxlZG5pbmcvaW52b2x2ZXJpbmcgOiBQdC5pbmR0YWdlciAgaG92ZWRtw6VsdGlkZXIgc2FtdCBtZWxsZW3DpWx0aWRlci4gSW5kdGFnZXIgc3VmZmljaWVudCA8QnJlYWsgLz5Lb21tZW50YXIgKGVybsOmcmluZyk6IHB0LnZpbCBzcGlzZSBtaWRkYWdzbWFkIGhqZW1tZSBob3Mgc2luIG1vcixkZXJmb3IgaGFyIGlra2UgdGFnZXQgaW5zdWxpbi5IYXIgc3Bpc3QgMiBiYW5hbiBlZnRlciBFUkNQPC9uczE6UHJvYmxlbUFyZWE+PC9uczE6TnVyc2luZ1Byb2JsZW1BcmVhPjxuczE6TnVyc2luZ1Byb2JsZW1BcmVhPjxuczE6VGVybWlub2xvZ3k+PG5zMTpDb2RlPkMtMTIzNDU8L25zMTpDb2RlPjxuczE6Q29kZVN5c3RlbT5tZWRjb208L25zMTpDb2RlU3lzdGVtPjxuczE6Q29kZURlc2NyaXB0aW9uPkh1ZCBvZyBzbGltaGluZGVyPC9uczE6Q29kZURlc2NyaXB0aW9uPjwvbnMxOlRlcm1pbm9sb2d5PjxuczE6UHJvYmxlbUFyZWE+SHVkOiBOb3JtYWwgdWRzZWVuZGUgb2cgaW50YWt0IGh1ZCA8QnJlYWsgLz5IasOmbHAgdGlsIHBlcnNvbmxpZyBoeWdpZWpuZSAoZG9rLiBpIEFrdHVlbHQgZnVua3Rpb25zbml2ZWF1IC0gb2JzZXJ2YXRpb24pOiBTZWx2aGp1bHBlbiA8QnJlYWsgLz5NdW5kc3RhdHVzOiBOb3JtYWwgPEJyZWFrIC8+SGrDpmxwIHRpbCBtdW5kaHlnaWVqbmU6IFBhdGllbnRlbiB2YXJldGFnZXIgc2VsdiBtdW5kaHlnaWVqbmUgPEJyZWFrIC8+UmlzaWtvIGZvciB1ZHZpa2xpbmcgYWYgdHJ5a3NrYWRlLy1zw6VyPzogSmEgPEJyZWFrIC8+SW5zcGVrdGlvbiBmb3IgdHJ5a3PDpXI6IEluZ2VuIHRyeWtza2FkZS90cnlrc8OlciAodHJ5a3PDpXIgb3ZlciBvc3NhY3J1bSBpIGdvZCBvcGjDpmxpbmdwcm9jZXMsZXIgbWVnZXQgbWluZHJlIHNpZGVuIHNpZHN0IGplZyBoYXIgc2V0IGRldC4pIDxCcmVhayAvPlNlbnNvcmlzayBwZXJjZXB0aW9uOiBMaWR0IGJlZ3LDpm5zZXQgPEJyZWFrIC8+RnVndDogTGVqbGlnaGVkc3ZpcyBmdWd0aWcgPEJyZWFrIC8+QWt0aXZpdGV0OiBCdW5kZXQgdGlsIHN0b2wgPEJyZWFrIC8+TW9iaWxpdGV0OiBMZXQgYmVncsOmbnNldCBtb2JpbGl0ZXQgPEJyZWFrIC8+RXJuw6ZyaW5nLCBpbmR0YWdlbHNlOiBGdWxkdCB0aWxzdHLDpmtrZWxpZyA8QnJlYWsgLz5HbmlkbmluZyBvZyBmb3Jza3lkbmluZzogUHJvYmxlbSA8QnJlYWsgLz5CcmFkZW4gdG90YWwgc2NvcmU6IDE2IDxCcmVhayAvPlRyeWtzw6Vyc3Jpc2lrbzogTGF2IHJpc2lrbyBmb3IgYXQgdWR2aWtsZSB0cnlrc8OlciA8QnJlYWsgLz5Ucnlrc8OlcnNmb3JlYnlnZ2Vsc2UgLSBtYWRyYXM6IFZla3NlbHRyeWtzIGx1ZnRtYWRyYXMgKG1lZCBwdW1wZSkgPEJyZWFrIC8+VHJ5a3PDpXJzZm9yZWJ5Z2dlbHNlIC0gYWZsYXN0bmluZzogS27Dpmtuw6ZrIHDDpSBzZW5nZW4gPEJyZWFrIC8+VHJ5a3PDpXJzZm9yZWJ5Z2dlbHNlIC0gbWVkaWNpbnNrIHVkc3R5cjogQmFuZGFnZSA8QnJlYWsgLz5Tw6VyOiBIYXIgc8OlciBww6UgbWF2ZW4uIGVyIGhlbHQgdMO4cnQgb2cgbsOmc3RlbiBoZWxldCBtZW4gcGdhIG1lZ2V0IHluZHQgaHVkIG9nIHRpZGxpZ2VyIGJsb3RsYWd0IHRhcm0gc2thbCBkZXIgc2t1bWJhbmRhZyBvdmVyIG9nIHNraWZ0ZXMvdGlsc2VzIG1hbmRhZyBvZyB0b3JzZGFnLiBIYXIgc8OlciBww6Ugb3Mgc2FjcnVtLiBEZW5uZSBza2xhIHJlbnNlcyBtZWQgbmFjbCBodmVyIDIgZGFnIG9nIHNraWZ0ZSBza3VtYmFkYWdlLiBtZWRnaXZldCB0aWwgd2VlayBvZyBza2lmdGV0IGlkYWcuPC9uczE6UHJvYmxlbUFyZWE+PC9uczE6TnVyc2luZ1Byb2JsZW1BcmVhPjxuczE6TnVyc2luZ1Byb2JsZW1BcmVhPjxuczE6VGVybWlub2xvZ3k+PG5zMTpDb2RlPkQtMTIzNDU8L25zMTpDb2RlPjxuczE6Q29kZVN5c3RlbT5tZWRjb208L25zMTpDb2RlU3lzdGVtPjxuczE6Q29kZURlc2NyaXB0aW9uPktvbW11bmlrYXRpb24vbmV1cm9sb2dpPC9uczE6Q29kZURlc2NyaXB0aW9uPjwvbnMxOlRlcm1pbm9sb2d5PjxuczE6UHJvYmxlbUFyZWE+QmV2aWRzdGhlZHNuaXZlYXU6IFbDpWdlbiwgcmVhZ2VyZXIgbm9ybWFsdCBlbGxlciBub3JtYWwgc8O4dm4gPEJyZWFrIC8+Tml2ZWF1IGFmIG9yaWVudGVyaW5nOiBPcmllbnRlcmV0IGkgdGlkLCBzdGVkLCBzaXR1YXRpb24gb2cgZWduZSBkYXRhIDxCcmVhayAvPkV2bmUgdGlsIGF0IGZvcnN0w6U6IFJlbGV2YW50IGTDuG1tZWtyYWZ0IDxCcmVhayAvPkV2bmUgdGlsIGF0IGfDuHJlIHNpZyBmb3JzdMOlZWxpZzogTm9ybWFsIHRhbGUgPEJyZWFrIC8+S29tbWVudGFyIChrb21tdW5pa2F0aW9uL25ldXJvbG9naXNrKSA6IFB0IGhhciB2w6ZyZXQgdsOlZ2VuIG9nIG1lZ2V0IHRhbGVuZGUgLjwvbnMxOlByb2JsZW1BcmVhPjwvbnMxOk51cnNpbmdQcm9ibGVtQXJlYT48bnMxOk51cnNpbmdQcm9ibGVtQXJlYT48bnMxOlRlcm1pbm9sb2d5PjxuczE6Q29kZT5FLTEyMzQ1PC9uczE6Q29kZT48bnMxOkNvZGVTeXN0ZW0+bWVkY29tPC9uczE6Q29kZVN5c3RlbT48bnMxOkNvZGVEZXNjcmlwdGlvbj5Qc3lrb3NvY2lhbGUgZm9yaG9sZDwvbnMxOkNvZGVEZXNjcmlwdGlvbj48L25zMTpUZXJtaW5vbG9neT48bnMxOlByb2JsZW1BcmVhPlBzeWtpc2sgdGlsc3RhbmQ6IFJvbGlnLCBHb2R0IGh1bcO4ciA8QnJlYWsgLz5BbG1lbnQgYmVmaW5kZW5kZTogUHQuIHVkdHJ5a2tlciBhbG1lbnQgdmVsYmVmaW5kZW5kZSA8QnJlYWsgLz5TZWx2bW9yZHNzY3JlZW5pbmcgZm9yZXRhZ2VzOiBOZWogPEJyZWFrIC8+WWRlcmxpZ2VyZSBwc3lraXNrZSBvYnNlcnZhdGlvbmVyOiBPYnMgb20gYmVob3YgZm9yIG9wZsO4bGduaW5nIHZlZCBlZ2VuIGzDpmdlIG1lZCBwc3lrb2Zhcm1ha2EgPEJyZWFrIC8+Qm9saWc6IEh1cyA8QnJlYWsgLz5IamVtbWV0cyBpbmRyZXRuaW5nOiBGbGVyZSBwbGFuIDxCcmVhayAvPkJvciBzYW1tZW4gbWVkOiBCb3IgYWxlbmUgPEJyZWFrIC8+UHJpbcOmciBzZWt0b3I6IGbDpXIgaGrDpmxwL3N0w7h0dGUgYWY6IEtsYXJlciBzaWcgdWRlbiBoasOmbHAgPEJyZWFrIC8+UMOlcsO4cmVuZGU6IGbDpXIgaGrDpmxwIGFmOiBLbGFyZXIgc2lnIHVkZW4gaGrDpmxwIDxCcmVhayAvPsOYbnNrZSBvbSBww6Vyw7hyZW5kZWluZGRyYWdlbHNlOiBQYXRpZW50ZW4gw7huc2tlciBww6Vyw7hyZW5kZSBpbmRkcmFnZXQgbMO4YmVuZGU8L25zMTpQcm9ibGVtQXJlYT48L25zMTpOdXJzaW5nUHJvYmxlbUFyZWE+PG5zMTpOdXJzaW5nUHJvYmxlbUFyZWE+PG5zMTpUZXJtaW5vbG9neT48bnMxOkNvZGU+Ri0xMjM0NTwvbnMxOkNvZGU+PG5zMTpDb2RlU3lzdGVtPm1lZGNvbTwvbnMxOkNvZGVTeXN0ZW0+PG5zMTpDb2RlRGVzY3JpcHRpb24+UmVzcGlyYXRpb24gb2cgY2lya3VsYXRpb248L25zMTpDb2RlRGVzY3JpcHRpb24+PC9uczE6VGVybWlub2xvZ3k+PG5zMTpQcm9ibGVtQXJlYT5SZXNwaXJhdGlvbnNtw7huc3Rlci9keWJkZTogTm9ybWFsIDxCcmVhayAvPlJlc3BpcmF0aW9uc2x5ZGU6IE5vcm1hbCA8QnJlYWsgLz5Ib3N0ZTogSW5nZW4gPEJyZWFrIC8+RWtzcGVrdG9yYXRtw6ZuZ2RlOiBNb2RlcmF0IDxCcmVhayAvPkVrc3Bla3RvcmF0dWRzZWVuZGU6IEh2aWQgPEJyZWFrIC8+RWtzcGVrdG9yYXRrb25zaXN0ZW5zOiBTZWp0LCBMw7hzdCA8QnJlYWsgLz5UaG9yYXh1ZHNlZW5kZTogTm9ybWFsdCA8QnJlYWsgLz5MdWZ0dmVqOiBGcmkgPEJyZWFrIC8+UmVzcGlyYXRpb24gLSBpbnRlcnZlbnRpb246IEludGV0IGJlaG92IGZvciByZXNwLiBzdMO4dHRlL2lsdHRpbHNrdWQgPEJyZWFrIC8+S29tbWVudGFyIChyZXNwaXJhdGlvbikgOiBQdC5tZWdldCBnb2QgdGlsIGF0IGhvc3RlIHNsaW0gb3AgPEJyZWFrIC8+Q2lya3VsYXRvcmlza2Ugc3ltcHRvbWVyOiBJbmdlbiBzeW1wdG9tZXIgPEJyZWFrIC8+Q2lya3VsYXRvcmlzayBodWR0aWxzdGFuZC90ZW1wZXJhdHVyOiBOb3JtYWwgY2lya3VsYXRvcmlzayBodWR0aWxzdGFuZC9odWR0ZW1wZXJhdHVyIDxCcmVhayAvPkh5ZHJlcmluZ3NzdGF0dXM6IE5vcm1vaHlkcmVyZXQgPEJyZWFrIC8+Q2lya3VsYXRpb24gLSBpbnRlcnZlbnRpb246IElra2UgcmVsZXZhbnQ8L25zMTpQcm9ibGVtQXJlYT48L25zMTpOdXJzaW5nUHJvYmxlbUFyZWE+PG5zMTpOdXJzaW5nUHJvYmxlbUFyZWE+PG5zMTpUZXJtaW5vbG9neT48bnMxOkNvZGU+Ry0xMjM0NTwvbnMxOkNvZGU+PG5zMTpDb2RlU3lzdGVtPm1lZGNvbTwvbnMxOkNvZGVTeXN0ZW0+PG5zMTpDb2RlRGVzY3JpcHRpb24+U2Vrc3VhbGl0ZXQsIGvDuG4gb2cga3JvcHNvcGZhdHRlbHNlPC9uczE6Q29kZURlc2NyaXB0aW9uPjwvbnMxOlRlcm1pbm9sb2d5PjxuczE6UHJvYmxlbUFyZWE+U2Vrc3VhbGl0ZXQsIGvDuG4gb2cga3JvcHNvcGZhdHRlbHNlOiBJa2tlIHJlbGV2YW50IGZvciBkZW5uZSBrb250YWt0PC9uczE6UHJvYmxlbUFyZWE+PC9uczE6TnVyc2luZ1Byb2JsZW1BcmVhPjxuczE6TnVyc2luZ1Byb2JsZW1BcmVhPjxuczE6VGVybWlub2xvZ3k+PG5zMTpDb2RlPkgtMTIzNDU8L25zMTpDb2RlPjxuczE6Q29kZVN5c3RlbT5tZWRjb208L25zMTpDb2RlU3lzdGVtPjxuczE6Q29kZURlc2NyaXB0aW9uPlNtZXJ0ZXIgb2cgc2Fuc2VpbmR0cnlrPC9uczE6Q29kZURlc2NyaXB0aW9uPjwvbnMxOlRlcm1pbm9sb2d5PjxuczE6UHJvYmxlbUFyZWE+QmVoYW5kbGluZ3Ntw6VsIGZvciBzbWVydGViZWhhbmRsaW5nOiBMZXR0ZSBzbWVydGVyIDxCcmVhayAvPkhhYml0dWVsbGUgaGrDpmxwZW1pZGxlciB0aWwgc2Fuc2VyIDogSW5nZW4gPEJyZWFrIC8+U2Fuc2VyIDogSWtrZSByZWxldmFudCBmb3IgZGVubmUga29udGFrdDwvbnMxOlByb2JsZW1BcmVhPjwvbnMxOk51cnNpbmdQcm9ibGVtQXJlYT48bnMxOk51cnNpbmdQcm9ibGVtQXJlYT48bnMxOlRlcm1pbm9sb2d5PjxuczE6Q29kZT5JLTEyMzQ1PC9uczE6Q29kZT48bnMxOkNvZGVTeXN0ZW0+bWVkY29tPC9uczE6Q29kZVN5c3RlbT48bnMxOkNvZGVEZXNjcmlwdGlvbj5Tw7h2biBvZyBodmlsZTwvbnMxOkNvZGVEZXNjcmlwdGlvbj48L25zMTpUZXJtaW5vbG9neT48bnMxOlByb2JsZW1BcmVhPlPDuHZuIG9nIGh2aWxlIC0gb2JzZXJ2YXRpb246IEJlc3bDpnIgbWVkIGF0IHNvdmUgaWdlbm5lbSA8QnJlYWsgLz5UcsOmdGhlZC9lbmVyZ2kgLSBha3R1ZWx0IDogVHLDpnQgaSBwZXJpb2RlcjwvbnMxOlByb2JsZW1BcmVhPjwvbnMxOk51cnNpbmdQcm9ibGVtQXJlYT48bnMxOk51cnNpbmdQcm9ibGVtQXJlYT48bnMxOlRlcm1pbm9sb2d5PjxuczE6Q29kZT5KLTEyMzQ1PC9uczE6Q29kZT48bnMxOkNvZGVTeXN0ZW0+bWVkY29tPC9uczE6Q29kZVN5c3RlbT48bnMxOkNvZGVEZXNjcmlwdGlvbj5WaWRlbiBvZyB1ZHZpa2xpbmc8L25zMTpDb2RlRGVzY3JpcHRpb24+PC9uczE6VGVybWlub2xvZ3k+PG5zMTpQcm9ibGVtQXJlYT5TeWdkb21zaW5kc2lndC92aWRlbnNuaXZlYXU6IFRpbHN0csOma2tlbGlnIHN5Z2RvbXNpbmRzaWd0IDxCcmVhayAvPktvZ25pdGl2IGZvcm3DpWVuIDogTmVkc2F0IGV2bmUgdGlsIGF0IHBsYW5sw6ZnZ2Ugb2cgcHJvYmxlbWzDuHNlIDxCcmVhayAvPkh1a29tbWVsc2U6IE5vcm1hbCBodWtvbW1lbHNlPC9uczE6UHJvYmxlbUFyZWE+PC9uczE6TnVyc2luZ1Byb2JsZW1BcmVhPjxuczE6TnVyc2luZ1Byb2JsZW1BcmVhPjxuczE6VGVybWlub2xvZ3k+PG5zMTpDb2RlPkstMTIzNDU8L25zMTpDb2RlPjxuczE6Q29kZVN5c3RlbT5tZWRjb208L25zMTpDb2RlU3lzdGVtPjxuczE6Q29kZURlc2NyaXB0aW9uPlVkc2tpbGxlbHNlIDwvbnMxOkNvZGVEZXNjcmlwdGlvbj48L25zMTpUZXJtaW5vbG9neT48bnMxOlByb2JsZW1BcmVhPk5lZHJlIEdJIHN5bXB0b21lcjogSW5nZW4gPEJyZWFrIC8+T2JzZXJ2YXRpb24gYWYgYWJkb21lbjogVG9wcGV0IDxCcmVhayAvPkZsYXR1czogSmEsIGZsYXR1cyBpIHZhZ3RlbiA8QnJlYWsgLz5TZW5lc3RlIGFmZsO4cmluZyB1bmRlciBpbmRsw6ZnZ2Vsc2UgKGRhdG8pOiAyNC0wMS0yNCA8QnJlYWsgLz5BZmbDuHJpbmc6IE5laiwgaWtrZSBhZmbDuHJpbmcgaSB2YWd0ZW4gKFB0IG9wbHlzZXIgYXQgZGVyIGhhciB2w6ZyZXQgYWZmw7hyaW5nICBoamVtbWUgaSBnw6VyIGFmdGVzKSA8QnJlYWsgLz5BZmbDuHJpbmdzdWRzZWVuZGU6IEM6IEZvcm1ldCA8QnJlYWsgLz5BZmbDuHJpbmdzZmFydmU6ICAoQmzDpXQsYnJpbGxpYW50IGJsdWUpIDxCcmVhayAvPkFmZsO4cmluZ3Ntw6ZuZ2RlOiBLYW4gaWtrZSB2dXJkZXJlcyA8QnJlYWsgLz5UYXJtZnVua3Rpb24gLSBpbnRlcnZlbnRpb246IExha3NhbnRpYSAoc2UgTURBL0ZNSykgPEJyZWFrIC8+VmFuZGxhZG5pbmc6IEluZ2VuIHZhbmRsYWRuaW5nc3Byb2JsZW1lciA8QnJlYWsgLz5IasOmbHAgdmVkcsO4cmVuZGUgdG9pbGV0YmVzw7hnIChkb2suIGkgQWt0dWVsdCBmdW5rdGlvbnNuaXZlYXUgLSBvYnNlcnZhdGlvbikgOiBCcnVnZXIgYmxlO0fDpXIgc2VsdiBww6UgdG9pbGV0dGV0IDxCcmVhayAvPlVyaW4gOiBKYSwgdXJpbnVkc2tpbGxlbHNlIGkgdmFndGVuIDxCcmVhayAvPlVyaW51ZHNlZW5kZTogS2xhciA8QnJlYWsgLz5VcmlubHVndDogTm9ybWFsIDxCcmVhayAvPlVyaW5tw6ZuZ2RlOiBLYW4gaWtrZSB2dXJkZXJlcyA8QnJlYWsgLz5WYW5kbGFkbmluZyAtIGludGVydmVudGlvbiA6IElra2UgcmVsZXZhbnQgPEJyZWFrIC8+RHLDpm46IEhhciBkcsOmbiBpIGFuc2FtbGluZyB2ZWQgbGV2ZXIuIERldHRlIHNrYWwgc2t5bGxlcyB4IDEgZGFnbGlndCBtZWQgMTAgbWwgbmFjbC4gUG9zZSB0w7htbWVzIHZlZCBiZWhvdi4gTWVkZ2l2ZXIgdGlsIHdlZWsgc2FtdCBlbiB2ZWpsZWRuaW5nIGkgc2t5bDwvbnMxOlByb2JsZW1BcmVhPjwvbnMxOk51cnNpbmdQcm9ibGVtQXJlYT48L25zMTpOdXJzaW5nUHJvYmxlbUFyZWFzPjxSaXNrT2ZDb250YWdpb24+SGFyIFZSRSAtIHBvZG5pbmcgZnJhIHJlY3R1bS48L1Jpc2tPZkNvbnRhZ2lvbj48bnMxOkRpYWdub3Nlcz48bnMxOkRpYWdub3Npcz48bnMxOkNvZGU+REs4NThDPC9uczE6Q29kZT48bnMxOlR5cGVDb2RlPlNLU2RpYWdub3Nla29kZTwvbnMxOlR5cGVDb2RlPjxuczE6VGV4dD5Ba3V0IChhZGlww7hzKSBwYW5jcmVhc25la3Jvc2U8L25zMTpUZXh0PjwvbnMxOkRpYWdub3Npcz48L25zMTpEaWFnbm9zZXM+PG5zMTpBYmlsaXR5VG9GdW5jdGlvbkF0RGlzY2hhcmdlPjxuczE6TGFzdE1vZGlmaWVkRGF0ZT4yMDI0LTAxLTI1PC9uczE6TGFzdE1vZGlmaWVkRGF0ZT48bnMxOkFiaWxpdHlUb0Z1bmN0aW9uRWxlbWVudHM+PG5zMTpBYmlsaXR5VG9GdW5jdGlvbkVsZW1lbnQ+PG5zMTpUZXJtaW5vbG9neT48bnMxOkNvZGU+RkE1MTA8L25zMTpDb2RlPjxuczE6Q29kZVN5c3RlbT5pY2Y8L25zMTpDb2RlU3lzdGVtPjxuczE6Q29kZURlc2NyaXB0aW9uPlZhc2tlIHNpZzwvbnMxOkNvZGVEZXNjcmlwdGlvbj48L25zMTpUZXJtaW5vbG9neT48bnMxOlNjb3JlPnViZXR5ZGVsaWdlPC9uczE6U2NvcmU+PC9uczE6QWJpbGl0eVRvRnVuY3Rpb25FbGVtZW50PjxuczE6QWJpbGl0eVRvRnVuY3Rpb25FbGVtZW50PjxuczE6VGVybWlub2xvZ3k+PG5zMTpDb2RlPkZBNTQwPC9uczE6Q29kZT48bnMxOkNvZGVTeXN0ZW0+aWNmPC9uczE6Q29kZVN5c3RlbT48bnMxOkNvZGVEZXNjcmlwdGlvbj5BZi0gb2cgcMOla2zDpmRuaW5nPC9uczE6Q29kZURlc2NyaXB0aW9uPjwvbnMxOlRlcm1pbm9sb2d5PjxuczE6U2NvcmU+dWJldHlkZWxpZ2U8L25zMTpTY29yZT48bnMxOkNvbW1lbnQ+S2xhcmVyIHNpZyBzZWx2IG1lZCBkZXR0ZTwvbnMxOkNvbW1lbnQ+PC9uczE6QWJpbGl0eVRvRnVuY3Rpb25FbGVtZW50PjxuczE6QWJpbGl0eVRvRnVuY3Rpb25FbGVtZW50PjxuczE6VGVybWlub2xvZ3k+PG5zMTpDb2RlPkZBNTMwPC9uczE6Q29kZT48bnMxOkNvZGVTeXN0ZW0+aWNmPC9uczE6Q29kZVN5c3RlbT48bnMxOkNvZGVEZXNjcmlwdGlvbj5Hw6UgcMOlIHRvaWxldDwvbnMxOkNvZGVEZXNjcmlwdGlvbj48L25zMTpUZXJtaW5vbG9neT48bnMxOlNjb3JlPnViZXR5ZGVsaWdlPC9uczE6U2NvcmU+PG5zMTpDb21tZW50PmtsYXJlciBzZWx2IHRvaWxldCBiZXPDuGc8L25zMTpDb21tZW50PjwvbnMxOkFiaWxpdHlUb0Z1bmN0aW9uRWxlbWVudD48bnMxOkFiaWxpdHlUb0Z1bmN0aW9uRWxlbWVudD48bnMxOlRlcm1pbm9sb2d5PjxuczE6Q29kZT5GQTQyMDwvbnMxOkNvZGU+PG5zMTpDb2RlU3lzdGVtPmljZjwvbnMxOkNvZGVTeXN0ZW0+PG5zMTpDb2RlRGVzY3JpcHRpb24+Rm9yZmx5dHRlIHNpZzwvbnMxOkNvZGVEZXNjcmlwdGlvbj48L25zMTpUZXJtaW5vbG9neT48bnMxOlNjb3JlPmxldHRlPC9uczE6U2NvcmU+PC9uczE6QWJpbGl0eVRvRnVuY3Rpb25FbGVtZW50PjxuczE6QWJpbGl0eVRvRnVuY3Rpb25FbGVtZW50PjxuczE6VGVybWlub2xvZ3k+PG5zMTpDb2RlPkZBNDYwPC9uczE6Q29kZT48bnMxOkNvZGVTeXN0ZW0+aWNmPC9uczE6Q29kZVN5c3RlbT48bnMxOkNvZGVEZXNjcmlwdGlvbj5Gw6ZyZGVuIGkgZm9yc2tlbGxpZ2Ugb21naXZlbHNlcjwvbnMxOkNvZGVEZXNjcmlwdGlvbj48L25zMTpUZXJtaW5vbG9neT48bnMxOlNjb3JlPmxldHRlPC9uczE6U2NvcmU+PG5zMTpDb21tZW50PkJydWdlciBrw7hyZXN0b2wgdGlsIGF0IGtvbW1lIHJ1bmR0IHDDpSBsw6ZuZ2VyZSBkaXN0YW5jZXIuIFN0b3J0IGZ1bmt0aW9uc3RhYiBwZ2EgbGFuZ3QgaW50ZW5zaXYgZm9ybMO4Yi4gSGFyIGhlciB0csOmbmV0IG1lZCBzZW5nZWN5a2VsIG1lZCBnb2QgZWZmZWt0PC9uczE6Q29tbWVudD48L25zMTpBYmlsaXR5VG9GdW5jdGlvbkVsZW1lbnQ+PG5zMTpBYmlsaXR5VG9GdW5jdGlvbkVsZW1lbnQ+PG5zMTpUZXJtaW5vbG9neT48bnMxOkNvZGU+RkE1NjA8L25zMTpDb2RlPjxuczE6Q29kZVN5c3RlbT5pY2Y8L25zMTpDb2RlU3lzdGVtPjxuczE6Q29kZURlc2NyaXB0aW9uPkRyaWtrZTwvbnMxOkNvZGVEZXNjcmlwdGlvbj48L25zMTpUZXJtaW5vbG9neT48bnMxOlNjb3JlPnViZXR5ZGVsaWdlPC9uczE6U2NvcmU+PG5zMTpDb21tZW50PlNrYWwgdsOmcmUgZnVsZHN0w6ZuZGlnIGFsY29ob2wgYWJzdGluZW50IHJlc3RlbiBhZiBzaXQgbGl2LjwvbnMxOkNvbW1lbnQ+PC9uczE6QWJpbGl0eVRvRnVuY3Rpb25FbGVtZW50PjxuczE6QWJpbGl0eVRvRnVuY3Rpb25FbGVtZW50PjxuczE6VGVybWlub2xvZ3k+PG5zMTpDb2RlPkZBNTUwPC9uczE6Q29kZT48bnMxOkNvZGVTeXN0ZW0+aWNmPC9uczE6Q29kZVN5c3RlbT48bnMxOkNvZGVEZXNjcmlwdGlvbj5TcGlzZTwvbnMxOkNvZGVEZXNjcmlwdGlvbj48L25zMTpUZXJtaW5vbG9neT48bnMxOlNjb3JlPnViZXR5ZGVsaWdlPC9uczE6U2NvcmU+PC9uczE6QWJpbGl0eVRvRnVuY3Rpb25FbGVtZW50PjwvbnMxOkFiaWxpdHlUb0Z1bmN0aW9uRWxlbWVudHM+PC9uczE6QWJpbGl0eVRvRnVuY3Rpb25BdERpc2NoYXJnZT48bnMxOkRpc2NoYXJnZVJlbGF0ZWRNZWRpY2luZUluZm9ybWF0aW9uPjxuczE6QXR0YWNoZWQ+PG5zMTpFeHBpcmU+MjAyNC0wMS0yOTwvbnMxOkV4cGlyZT48bnMxOkNvbW1lbnQ+bWFuZGFnIG1vcmdlbjwvbnMxOkNvbW1lbnQ+PC9uczE6QXR0YWNoZWQ+PG5zMTpQaWNrVXBPckRlbGl2ZXJ5PjA8L25zMTpQaWNrVXBPckRlbGl2ZXJ5PjxuczE6RG9zYWdlRXhlbXB0aW9uUmVvcmRlcmVkPjA8L25zMTpEb3NhZ2VFeGVtcHRpb25SZW9yZGVyZWQ+PC9uczE6RGlzY2hhcmdlUmVsYXRlZE1lZGljaW5lSW5mb3JtYXRpb24+PG5zMTpEaWV0Rmlyc3QyNEhvdXJzPjxuczE6THVuY2hCb3g+MDwvbnMxOkx1bmNoQm94PjxuczE6U2hvcHBpbmdBdERpc2NoYXJnZT4wPC9uczE6U2hvcHBpbmdBdERpc2NoYXJnZT48L25zMTpEaWV0Rmlyc3QyNEhvdXJzPjxGdXR1cmVQbGFucz5Ib3NwaXRhbGV0IGhhciBiZWhhbmRsaW5nc2Fuc3ZhciBmb3IgcGF0aWVudGVuIGluZHRpbCAyOC0wMS0yMDI0IDE1OjAwLiBLb250YWt0dGVsZWZvbm51bW1lciAodGlsIGJydWcgZm9yIHN1bmRoZWRzcHJvZmVzc2lvbmVsbGUpOiA1OCA1NSA5OCA1MC4gRnJlbXRpZGlnZSBhZnRhbGVyOiBTa2FsIG3DuGRlIGkgIGtpcnVyZ2lzayBhZmQgZGVuIDIvMiBrbCAwOC4zMCB0aWwgc3ZhciBww6Uga29uZmVyZW5jZS4gSGFyIMOlYmVuIGluZGzDpmdnZWxzZSBob3Mgb3MgaW5kdGlsIGtvbmZlcmVuY2UuIEluZm8gbWVkZ2l2ZXMgdGlsIHBhdGllbnQuLiA8L0Z1dHVyZVBsYW5zPjwvbnMxOlJlcG9ydE9mRGlzY2hhcmdlPjwvbnMxOkVtZXNzYWdlPg==\",\"externalId\":null,\"ccRecipient\":null,\"copiedMessageId\":null,\"letterType\":null,\"serviceTagCode\":null,\"messageType\":\"MEDCOM_MESSAGE_REPORT_OF_DISCHARGE_1_0_3\",\"versionCode\":\"XD1834C\",\"conversationId\":\"0000000000253A93E768B7D67AC786B5\",\"activityIdentifier\":{\"identifier\":\"MESSAGE:368887\",\"type\":\"MESSAGE\",\"activityId\":368887,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"patient\":{\"id\":14296,\"version\":14,\"firstName\":\"Claus Soon-Woo\",\"lastName\":\"Christiansen\",\"middleName\":null,\"fullName\":\"Claus Soon-Woo Christiansen\",\"fullReversedName\":\"Christiansen, Claus Soon-Woo\",\"homePhoneNumber\":null,\"mobilePhoneNumber\":\"30432613\",\"workPhoneNumber\":null,\"patientIdentifier\":{\"type\":\"cpr\",\"identifier\":\"091278-1011\",\"managedExternally\":false,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"currentAddress\":{\"addressLine1\":\"Køgevej 60\",\"addressLine2\":null,\"addressLine3\":null,\"addressLine4\":null,\"addressLine5\":null,\"administrativeAreaCode\":\"329\",\"countryCode\":\"dk\",\"postalCode\":\"4100\",\"postalDistrict\":\"Ringsted\",\"restricted\":false,\"geoCoordinates\":{\"x\":null,\"y\":null,\"present\":false},\"startDate\":null,\"endDate\":null,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/address/countries\"},\"searchPostalDistrict\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/address/postalDistrict\"}}},\"currentAddressIndicator\":\"PRIMARY_ADDRESS\",\"age\":45,\"monthsAfterBirthday\":1,\"gender\":\"MALE\",\"patientStatus\":\"FINAL\",\"patientState\":{\"id\":21,\"version\":0,\"name\":\"Aktiv\",\"color\":\"000000\",\"type\":{\"id\":\"ACTIVE\",\"name\":\"Aktiv\",\"abbreviation\":\"\",\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"active\":true,\"defaultObject\":true,\"citizenRegistryConfiguration\":{\"updateEnabled\":true,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"_links\":{\"medcomRecipient\":null,\"self\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patientStates/21\"},\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"telephonesRestricted\":false,\"imageId\":null,\"_links\":{\"medcomRecipient\":null,\"self\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/14296\"},\"patientOverview\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/14296/overview\"},\"citizenOverviewForms\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/14296/citizenOverviewForms\"},\"measurementData\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/14296/measurementInstructions/data/pages\"},\"measurementInstructions\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/14296/measurementInstructions/instructions\"},\"patientConditions\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/14296/conditions\"},\"patientOrganizations\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/14296/organizations\"},\"activityLinksPrototypes\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/14296/activitylinks/prototypes\"},\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"pathwayAssociation\":{\"placement\":{\"name\":\"MedCom Myndighedsenheden\",\"programPathwayId\":4355,\"pathwayTypeId\":13,\"parentPathwayId\":null,\"patientPathwayId\":70420,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"parentReferenceId\":null,\"referenceId\":1037360,\"canAssociateWithPathway\":true,\"canAssociateWithPatient\":false,\"associatedWithPatient\":false,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/14296/pathways/70420/availablePathwayAssociations\"},\"availableRootProgramPathways\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/14296/availableProgramPathways\"},\"availablePathwayPlacements\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/14296/pathways/availablePathwayPlacements\"},\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"body\":null,\"priority\":null,\"previousMessageId\":null,\"_links\":{\"medcomRecipient\":null,\"self\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/368887\"},\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/audit/medcom/368887\"},\"relatedCommunication\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/related/368887\"},\"activeAssignments\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/assignments//mobile/ringsted/v2/assignments/patient/14296/assignments/active?activity=MESSAGE:368887\"},\"assignments\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/assignments//mobile/ringsted/v2/assignments/patient/14296/assignments?activity=MESSAGE:368887\"},\"availableAssignmentTypes\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/assignments//mobile/ringsted/v2/assignments/patient/14296/assignmentTypes/active?activity=MESSAGE:368887&activityType=MESSAGE&messageTypeType=MEDCOM&messageType=MEDCOM_MESSAGE_REPORT_OF_DISCHARGE_1_0_3&messageStateDirection=INCOMING\"},\"autoAssignmentsPrototype\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/assignments//mobile/ringsted/v2/assignments/auto/prototype?activity=MESSAGE:368887&activityType=MESSAGE&messageTypeType=MEDCOM&messageType=MEDCOM_MESSAGE_REPORT_OF_DISCHARGE_1_0_3&messageStateDirection=INCOMING&messageStatus=FINAL\"},\"update\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/368887\"},\"transformedBody\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/368887/transformedBody\"},\"print\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/368887/print\"},\"reply\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/368887/reply\"},\"replyTo\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/replyTo/368887?identifierType=SYGEHUSAFDELINGSNUMMER&identifierValue=3800S10\"},\"archive\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/archived/368887\"},\"availableCountries\":null,\"searchPostalDistrict\":null},\"level\":null}\r\n");
            baseObjects.Add("{\"type\":\"medcom\",\"id\":368852,\"version\":3,\"sender\":{\"type\":\"medcomMessageDetailsBased\",\"id\":368852,\"name\":\"Region Sjællands Sygehusvæsen - Ingemannsvej 50 - 4200 Slagelse - EAN: 5790001360719\",\"recipientAddress\":null,\"recipientType\":\"MESSAGE_SENDER\",\"_links\":{\"medcomRecipient\":{\"href\":\"/api/core/mobile/ringsted/v2/medcomRecipients/medcomSender/368852\"},\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"recipient\":{\"type\":\"medcomMessageDetailsBased\",\"id\":368852,\"name\":\"Ringsted Kommune - Zahlesvej 18 - 4100 Ringsted - EAN: 5790001353858\",\"recipientAddress\":null,\"recipientType\":\"MESSAGE_RECIPIENT\",\"_links\":{\"medcomRecipient\":{\"href\":\"/api/core/mobile/ringsted/v2/medcomRecipients/medcomRecipient/368852\"},\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"subject\":\"Udskrivningsrapport\",\"date\":\"2024-01-25T12:07:00+01:00\",\"state\":{\"direction\":\"INCOMING\",\"status\":\"FINAL\",\"response\":\"NOT_AWAITING\",\"handshake\":\"NO\",\"processed\":\"PROCESSED\",\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"customer\":null,\"raw\":\"PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiPz48RW1lc3NhZ2UgeG1sbnM9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMyI+PGVwMTpFbnZlbG9wZSB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCI+PGVwMTpTZW50PjxlcDE6RGF0ZT4yMDI0LTAxLTI1PC9lcDE6RGF0ZT48ZXAxOlRpbWU+MTI6MDM8L2VwMTpUaW1lPjwvZXAxOlNlbnQ+PGVwMTpJZGVudGlmaWVyPkVQSUM2NDE4MDkxNDU8L2VwMTpJZGVudGlmaWVyPjxlcDE6QWNrbm93bGVkZ2VtZW50Q29kZT5wbHVzcG9zaXRpdmt2aXR0PC9lcDE6QWNrbm93bGVkZ2VtZW50Q29kZT48L2VwMTpFbnZlbG9wZT48UmVwb3J0T2ZEaXNjaGFyZ2U+PExldHRlcj48SWRlbnRpZmllcj5FUElDNjQxODA5MTQ1PC9JZGVudGlmaWVyPjxWZXJzaW9uQ29kZT5YRDE4MzRDPC9WZXJzaW9uQ29kZT48U3RhdGlzdGljYWxDb2RlPlhESVMxODwvU3RhdGlzdGljYWxDb2RlPjxBdXRob3Jpc2F0aW9uPjxlcDE6RGF0ZSB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCI+MjAyNC0wMS0yNTwvZXAxOkRhdGU+PGVwMTpUaW1lIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIj4xMjowMzwvZXAxOlRpbWU+PC9BdXRob3Jpc2F0aW9uPjxUeXBlQ29kZT5YRElTMTg8L1R5cGVDb2RlPjxFcGlzb2RlT2ZDYXJlSWRlbnRpZmllcj4wMDAwMDAwMDAwMDFFNEI1MUVFNjAwNEIwQjcwNkYzMDwvRXBpc29kZU9mQ2FyZUlkZW50aWZpZXI+PC9MZXR0ZXI+PGVwMTpTZW5kZXIgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiPjxlcDE6RUFOSWRlbnRpZmllcj41NzkwMDAxMzYwNzE5PC9lcDE6RUFOSWRlbnRpZmllcj48ZXAxOklkZW50aWZpZXI+MzgwMFMzMDwvZXAxOklkZW50aWZpZXI+PGVwMTpJZGVudGlmaWVyQ29kZT5zeWdlaHVzYWZkZWxpbmdzbnVtbWVyPC9lcDE6SWRlbnRpZmllckNvZGU+PGVwMTpPcmdhbmlzYXRpb25OYW1lPlJlZ2lvbiBTasOmbGxhbmRzIFN5Z2VodXN2w6ZzZW48L2VwMTpPcmdhbmlzYXRpb25OYW1lPjxlcDE6RGVwYXJ0bWVudE5hbWU+U0xBIE9ydG9ww6Zka2lydXJnaXNrIEFmZC48L2VwMTpEZXBhcnRtZW50TmFtZT48ZXAxOlVuaXROYW1lPk9ydG9ww6Zka2lydXJnaXNrIFNlbmdlYWZzbml0PC9lcDE6VW5pdE5hbWU+PGVwMTpTdHJlZXROYW1lPkluZ2VtYW5uc3ZlaiA1MDwvZXAxOlN0cmVldE5hbWU+PGVwMTpEaXN0cmljdE5hbWU+U2xhZ2Vsc2U8L2VwMTpEaXN0cmljdE5hbWU+PGVwMTpQb3N0Q29kZUlkZW50aWZpZXI+NDIwMDwvZXAxOlBvc3RDb2RlSWRlbnRpZmllcj48ZXAxOlRlbGVwaG9uZVN1YnNjcmliZXJJZGVudGlmaWVyPjU4NTU5NzgyPC9lcDE6VGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+PGVwMTpTaWduZWRCeT48ZXAxOklkZW50aWZpZXI+MzgwMFMzMDwvZXAxOklkZW50aWZpZXI+PGVwMTpJZGVudGlmaWVyQ29kZT5zeWdlaHVzYWZkZWxpbmdzbnVtbWVyPC9lcDE6SWRlbnRpZmllckNvZGU+PGVwMTpQZXJzb25HaXZlbk5hbWU+U2hhaW1hYTwvZXAxOlBlcnNvbkdpdmVuTmFtZT48ZXAxOlBlcnNvblN1cm5hbWVOYW1lPkh1c3NlaW48L2VwMTpQZXJzb25TdXJuYW1lTmFtZT48ZXAxOlBlcnNvblRpdGxlPlN5Z2VwbGVqZXJza2U8L2VwMTpQZXJzb25UaXRsZT48L2VwMTpTaWduZWRCeT48ZXAxOk1lZGljYWxTcGVjaWFsaXR5Q29kZT5vcnRvcGFlZGlza19raXJ1cmdpX3N5Z2VodXM8L2VwMTpNZWRpY2FsU3BlY2lhbGl0eUNvZGU+PGVwMTpDb250YWN0SW5mb3JtYXRpb24+PGVwMTpDb250YWN0TmFtZT5PcnRvcMOmZGtpcnVyZ2lzayBhZmQuLCBTbGFnZWxzZSBzeSo8L2VwMTpDb250YWN0TmFtZT48ZXAxOlRlbGVwaG9uZVN1YnNjcmliZXJJZGVudGlmaWVyPjU4NTU5NzgyPC9lcDE6VGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+PC9lcDE6Q29udGFjdEluZm9ybWF0aW9uPjwvZXAxOlNlbmRlcj48ZXAxOlJlY2VpdmVyIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIj48ZXAxOkVBTklkZW50aWZpZXI+NTc5MDAwMTM1Mzg1ODwvZXAxOkVBTklkZW50aWZpZXI+PGVwMTpJZGVudGlmaWVyPjMyOTwvZXAxOklkZW50aWZpZXI+PGVwMTpJZGVudGlmaWVyQ29kZT5rb21tdW5lbnVtbWVyPC9lcDE6SWRlbnRpZmllckNvZGU+PGVwMTpPcmdhbmlzYXRpb25OYW1lPlJpbmdzdGVkIEtvbW11bmU8L2VwMTpPcmdhbmlzYXRpb25OYW1lPjxlcDE6RGVwYXJ0bWVudE5hbWU+U29jaWFsIG9nIFN1bmRoZWQ8L2VwMTpEZXBhcnRtZW50TmFtZT48ZXAxOlVuaXROYW1lPkhqZW1tZXN5Z2VwbGVqZW4sIFJpbmdzdGVkIEtvbW11bmU8L2VwMTpVbml0TmFtZT48ZXAxOlN0cmVldE5hbWU+WmFobGVzdmVqIDE4PC9lcDE6U3RyZWV0TmFtZT48ZXAxOkRpc3RyaWN0TmFtZT5SaW5nc3RlZDwvZXAxOkRpc3RyaWN0TmFtZT48ZXAxOlBvc3RDb2RlSWRlbnRpZmllcj40MTAwPC9lcDE6UG9zdENvZGVJZGVudGlmaWVyPjwvZXAxOlJlY2VpdmVyPjxlcDE6UHJhY3RpdGlvbmVyIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIj48ZXAxOklkZW50aWZpZXI+MDIzMzAyPC9lcDE6SWRlbnRpZmllcj48ZXAxOklkZW50aWZpZXJDb2RlPnlkZXJudW1tZXI8L2VwMTpJZGVudGlmaWVyQ29kZT48ZXAxOlBlcnNvbk5hbWU+TMOmZ2VodXNldCBJIEJqw6Z2ZXJza292PC9lcDE6UGVyc29uTmFtZT48ZXAxOlRlbGVwaG9uZVN1YnNjcmliZXJJZGVudGlmaWVyPjU2ODY5OTk5PC9lcDE6VGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+PC9lcDE6UHJhY3RpdGlvbmVyPjxlcDE6UGF0aWVudCB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCI+PGVwMTpDaXZpbFJlZ2lzdHJhdGlvbk51bWJlcj4yMTExNDcyMzcwPC9lcDE6Q2l2aWxSZWdpc3RyYXRpb25OdW1iZXI+PGVwMTpQZXJzb25TdXJuYW1lTmFtZT5KZW5zZW48L2VwMTpQZXJzb25TdXJuYW1lTmFtZT48ZXAxOlBlcnNvbkdpdmVuTmFtZT5KeXR0ZSBHdWRydW48L2VwMTpQZXJzb25HaXZlbk5hbWU+PGVwMTpTdHJlZXROYW1lPkJ1ZWdhbmdlbiA0NjwvZXAxOlN0cmVldE5hbWU+PGVwMTpEaXN0cmljdE5hbWU+UmluZ3N0ZWQ8L2VwMTpEaXN0cmljdE5hbWU+PGVwMTpQb3N0Q29kZUlkZW50aWZpZXI+NDEwMDwvZXAxOlBvc3RDb2RlSWRlbnRpZmllcj48ZXAxOk9jY3VwYXRpb24+TUVESErDhkxQRVI8L2VwMTpPY2N1cGF0aW9uPjxlcDE6VGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+NDA5MTU4NDY8L2VwMTpUZWxlcGhvbmVTdWJzY3JpYmVySWRlbnRpZmllcj48L2VwMTpQYXRpZW50PjxSZWxhdGl2ZXM+PFJlbGF0aXZlPjxSZWxhdGlvbkNvZGU+YmFybjwvUmVsYXRpb25Db2RlPjxlcDE6UGVyc29uU3VybmFtZU5hbWUgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiPktpbTwvZXAxOlBlcnNvblN1cm5hbWVOYW1lPjxlcDE6UGVyc29uR2l2ZW5OYW1lIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIj5KZW5zZW48L2VwMTpQZXJzb25HaXZlbk5hbWU+PGVwMTpDb2RlZFRlbGVwaG9uZVN1YnNjcmliZXJJZGVudGlmaWVyIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIj48ZXAxOlRlbGVwaG9uZUNvZGU+bW9iaWw8L2VwMTpUZWxlcGhvbmVDb2RlPjxlcDE6VGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+NjE3OTM2MDY8L2VwMTpUZWxlcGhvbmVTdWJzY3JpYmVySWRlbnRpZmllcj48L2VwMTpDb2RlZFRlbGVwaG9uZVN1YnNjcmliZXJJZGVudGlmaWVyPjxJbmZvcm1lZFJlbGF0aXZlPjE8L0luZm9ybWVkUmVsYXRpdmU+PC9SZWxhdGl2ZT48Q29tbWVudD5QdCBlciBpbmYuIHZpbCBrw7hyZSBwdCBoamVtIG9ta3Jpbmcga2wgMTUuIEVyIGluZi4gb21rcmluZyBmw7hyc3RlIGJlc8O4ZyBmcmEgaGoucGxlamUgb21rcmluZyBrbC4gMTcgZGQuPC9Db21tZW50PjwvUmVsYXRpdmVzPjxlcDE6QWRtaXNzaW9uIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIj48ZXAxOkRhdGU+MjAyNC0wMS0yNDwvZXAxOkRhdGU+PGVwMTpUaW1lPjA3OjM1PC9lcDE6VGltZT48L2VwMTpBZG1pc3Npb24+PGVwMTpFbmRPZlRyZWF0bWVudCB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCI+PGVwMTpEYXRlPjIwMjQtMDEtMjU8L2VwMTpEYXRlPjxlcDE6VGltZT4xNTowMDwvZXAxOlRpbWU+PC9lcDE6RW5kT2ZUcmVhdG1lbnQ+PGVwMTpEaXNjaGFyZ2UgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiPjxlcDE6RGF0ZT4yMDI0LTAxLTI1PC9lcDE6RGF0ZT48ZXAxOlRpbWU+MTU6MDA8L2VwMTpUaW1lPjwvZXAxOkRpc2NoYXJnZT48ZXAxOkNhdXNlT2ZBZG1pc3Npb24geG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiPlB0IGVyIGZhbGRldCBkZXJoamVtbWUgcMOlIGVuIHRyYXBwZXN0aWdlIGNhIDIgdHJpbi4gVGFnZXIgZnJhIG1lZC4gaMO4IGFsYnVlLiBIYXIgcMOlZHJhZ2V0IHNpZyBow7hqcmVzaWRpZyBkaXN0YWwgaHVtZXJ1cyBmcmt0LiBFciBvc3Rlb3N5bnRlcmV0IDI0LzE8L2VwMTpDYXVzZU9mQWRtaXNzaW9uPjxOdXJzaW5nUHJvYmxlbUFyZWFzPjxOdXJzaW5nUHJvYmxlbUFyZWE+PFRlcm1pbm9sb2d5PjxDb2RlPkEtMTIzNDU8L0NvZGU+PENvZGVTeXN0ZW0+bWVkY29tPC9Db2RlU3lzdGVtPjxDb2RlRGVzY3JpcHRpb24+RnVua3Rpb25zbml2ZWF1IG9nIGJldsOmZ2VhcHBhcmF0PC9Db2RlRGVzY3JpcHRpb24+PC9UZXJtaW5vbG9neT48UHJvYmxlbUFyZWE+VWQgZnJhIGZ1bmt0aW9uc2V2bmV2dXJkZXJpbmdlbiB2dXJkZXJlczogSW5nZW4gZmFsZHJpc2lrbzwvUHJvYmxlbUFyZWE+PC9OdXJzaW5nUHJvYmxlbUFyZWE+PE51cnNpbmdQcm9ibGVtQXJlYT48VGVybWlub2xvZ3k+PENvZGU+Qy0xMjM0NTwvQ29kZT48Q29kZVN5c3RlbT5tZWRjb208L0NvZGVTeXN0ZW0+PENvZGVEZXNjcmlwdGlvbj5IdWQgb2cgc2xpbWhpbmRlcjwvQ29kZURlc2NyaXB0aW9uPjwvVGVybWlub2xvZ3k+PFByb2JsZW1BcmVhPlPDpXI6ICsgZ2VubmVtc2l2bmluZywgZm9yYmluZGluZyBlciBmb3JzdMOmcmtldC4gQ2ljLnRpbHN5biArIHBsYXN0ZXJza2lmdCBmw7hyc3RlIGdhbmcgYmVkZXMgMjcvMS4gSGVyZWZ0ZXIgdmVkIGJlaG92IG1pbiB4IDEtMiB1Z3QuPC9Qcm9ibGVtQXJlYT48L051cnNpbmdQcm9ibGVtQXJlYT48TnVyc2luZ1Byb2JsZW1BcmVhPjxUZXJtaW5vbG9neT48Q29kZT5ELTEyMzQ1PC9Db2RlPjxDb2RlU3lzdGVtPm1lZGNvbTwvQ29kZVN5c3RlbT48Q29kZURlc2NyaXB0aW9uPktvbW11bmlrYXRpb24vbmV1cm9sb2dpPC9Db2RlRGVzY3JpcHRpb24+PC9UZXJtaW5vbG9neT48UHJvYmxlbUFyZWE+QmV2aWRzdGhlZHNuaXZlYXU6IFbDpWdlbiwgcmVhZ2VyZXIgbm9ybWFsdCBlbGxlciBub3JtYWwgc8O4dm48L1Byb2JsZW1BcmVhPjwvTnVyc2luZ1Byb2JsZW1BcmVhPjxOdXJzaW5nUHJvYmxlbUFyZWE+PFRlcm1pbm9sb2d5PjxDb2RlPkUtMTIzNDU8L0NvZGU+PENvZGVTeXN0ZW0+bWVkY29tPC9Db2RlU3lzdGVtPjxDb2RlRGVzY3JpcHRpb24+UHN5a29zb2NpYWxlIGZvcmhvbGQ8L0NvZGVEZXNjcmlwdGlvbj48L1Rlcm1pbm9sb2d5PjxQcm9ibGVtQXJlYT5Cb2xpZzogSHVzIDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+SGplbW1ldHMgaW5kcmV0bmluZzogRXQgcGxhbiA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPkJvciBzYW1tZW4gbWVkOiBCb3IgYWxlbmU8L1Byb2JsZW1BcmVhPjwvTnVyc2luZ1Byb2JsZW1BcmVhPjxOdXJzaW5nUHJvYmxlbUFyZWE+PFRlcm1pbm9sb2d5PjxDb2RlPkYtMTIzNDU8L0NvZGU+PENvZGVTeXN0ZW0+bWVkY29tPC9Db2RlU3lzdGVtPjxDb2RlRGVzY3JpcHRpb24+UmVzcGlyYXRpb24gb2cgY2lya3VsYXRpb248L0NvZGVEZXNjcmlwdGlvbj48L1Rlcm1pbm9sb2d5PjxQcm9ibGVtQXJlYT5LYXBpbGzDpnJyZXNwb25zOiBOb3JtYWwgKGtvcnRlcmUgZW5kIDIgc2VrdW5kZXIpPC9Qcm9ibGVtQXJlYT48L051cnNpbmdQcm9ibGVtQXJlYT48TnVyc2luZ1Byb2JsZW1BcmVhPjxUZXJtaW5vbG9neT48Q29kZT5JLTEyMzQ1PC9Db2RlPjxDb2RlU3lzdGVtPm1lZGNvbTwvQ29kZVN5c3RlbT48Q29kZURlc2NyaXB0aW9uPlPDuHZuIG9nIGh2aWxlPC9Db2RlRGVzY3JpcHRpb24+PC9UZXJtaW5vbG9neT48UHJvYmxlbUFyZWE+U8O4dm4gb2cgaHZpbGUgLSBvYnNlcnZhdGlvbjogU292ZXQgYWZicnVkdDwvUHJvYmxlbUFyZWE+PC9OdXJzaW5nUHJvYmxlbUFyZWE+PC9OdXJzaW5nUHJvYmxlbUFyZWFzPjxlcDE6Umlza09mQ29udGFnaW9uIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIj5JbmdlbiBrZW5kdGU8L2VwMTpSaXNrT2ZDb250YWdpb24+PERpYWdub3Nlcz48RGlhZ25vc2lzPjxDb2RlPkRTNDI0PC9Db2RlPjxUeXBlQ29kZT5TS1NkaWFnbm9zZWtvZGU8L1R5cGVDb2RlPjxUZXh0PkZyYWt0dXIgYWYgbmVkZXJzdGUgZGVsIGFmIG92ZXJhcm1za25vZ2xlPC9UZXh0PjwvRGlhZ25vc2lzPjwvRGlhZ25vc2VzPjxBYmlsaXR5VG9GdW5jdGlvbkF0RGlzY2hhcmdlPjxMYXN0TW9kaWZpZWREYXRlPjIwMjQtMDEtMjU8L0xhc3RNb2RpZmllZERhdGU+PEFiaWxpdHlUb0Z1bmN0aW9uRWxlbWVudHM+PEFiaWxpdHlUb0Z1bmN0aW9uRWxlbWVudD48VGVybWlub2xvZ3k+PENvZGU+RkE1MTA8L0NvZGU+PENvZGVTeXN0ZW0+aWNmPC9Db2RlU3lzdGVtPjxDb2RlRGVzY3JpcHRpb24+VmFza2Ugc2lnPC9Db2RlRGVzY3JpcHRpb24+PC9UZXJtaW5vbG9neT48U2NvcmU+bW9kZXJhdGU8L1Njb3JlPjwvQWJpbGl0eVRvRnVuY3Rpb25FbGVtZW50PjxBYmlsaXR5VG9GdW5jdGlvbkVsZW1lbnQ+PFRlcm1pbm9sb2d5PjxDb2RlPkZBNTQwPC9Db2RlPjxDb2RlU3lzdGVtPmljZjwvQ29kZVN5c3RlbT48Q29kZURlc2NyaXB0aW9uPkFmLSBvZyBww6VrbMOmZG5pbmc8L0NvZGVEZXNjcmlwdGlvbj48L1Rlcm1pbm9sb2d5PjxTY29yZT5tb2RlcmF0ZTwvU2NvcmU+PENvbW1lbnQ+QmVob3YgZm9yIGhqw6ZscCB0aWwgYWYgKyBww6VrbMOmZG5pbmcuICsgcmV0bmluZyBhZiBjdWJpZml4IChza2lubmUpIGRhZ2xpZ3QuPC9Db21tZW50PjwvQWJpbGl0eVRvRnVuY3Rpb25FbGVtZW50PjxBYmlsaXR5VG9GdW5jdGlvbkVsZW1lbnQ+PFRlcm1pbm9sb2d5PjxDb2RlPkZBNTMwPC9Db2RlPjxDb2RlU3lzdGVtPmljZjwvQ29kZVN5c3RlbT48Q29kZURlc2NyaXB0aW9uPkfDpSBww6UgdG9pbGV0PC9Db2RlRGVzY3JpcHRpb24+PC9UZXJtaW5vbG9neT48U2NvcmU+bGV0dGU8L1Njb3JlPjxDb21tZW50PmbDuGxnZXMgdW5kZXIgaW5kbMOmZ2dlbHNlLiBCZWhvdiBmb3IgaGrDpmxwIHRpbCBhdCB0w7hycmUgc2lnLiBIYXIgbsOmc3RlbiBpbmdlbiBrcmFmdGVyIGkgdmUgaMOlbmQuPC9Db21tZW50PjwvQWJpbGl0eVRvRnVuY3Rpb25FbGVtZW50PjxBYmlsaXR5VG9GdW5jdGlvbkVsZW1lbnQ+PFRlcm1pbm9sb2d5PjxDb2RlPkZBNDIwPC9Db2RlPjxDb2RlU3lzdGVtPmljZjwvQ29kZVN5c3RlbT48Q29kZURlc2NyaXB0aW9uPkZvcmZseXR0ZSBzaWc8L0NvZGVEZXNjcmlwdGlvbj48L1Rlcm1pbm9sb2d5PjxTY29yZT5sZXR0ZTwvU2NvcmU+PENvbW1lbnQ+U8O4biBLaW0gbWVuZXIgaWtrZSBkZXQgZXIgZ8OlZXQgZ29kdCBkZXJoamVtbWUgaWZ0IGZvcmZseXRuaW5nIG9nIG1vYi4gT2JzIGhqw6ZscCB0aWwgZm9yZmx5dG5pbmdlciBpIHN0YXJ0ZW4gbWhwIHZ1cmRlcmluZyBhZiBvbSBkZXQgZXIgbsO4ZHZlbmRpZ3QuPC9Db21tZW50PjwvQWJpbGl0eVRvRnVuY3Rpb25FbGVtZW50PjxBYmlsaXR5VG9GdW5jdGlvbkVsZW1lbnQ+PFRlcm1pbm9sb2d5PjxDb2RlPkZBNDYwPC9Db2RlPjxDb2RlU3lzdGVtPmljZjwvQ29kZVN5c3RlbT48Q29kZURlc2NyaXB0aW9uPkbDpnJkZW4gaSBmb3Jza2VsbGlnZSBvbWdpdmVsc2VyPC9Db2RlRGVzY3JpcHRpb24+PC9UZXJtaW5vbG9neT48U2NvcmU+bGV0dGU8L1Njb3JlPjwvQWJpbGl0eVRvRnVuY3Rpb25FbGVtZW50PjxBYmlsaXR5VG9GdW5jdGlvbkVsZW1lbnQ+PFRlcm1pbm9sb2d5PjxDb2RlPkZBNTYwPC9Db2RlPjxDb2RlU3lzdGVtPmljZjwvQ29kZVN5c3RlbT48Q29kZURlc2NyaXB0aW9uPkRyaWtrZTwvQ29kZURlc2NyaXB0aW9uPjwvVGVybWlub2xvZ3k+PFNjb3JlPmxldHRlPC9TY29yZT48Q29tbWVudD5TdGlsbGVzIGZyZW08L0NvbW1lbnQ+PC9BYmlsaXR5VG9GdW5jdGlvbkVsZW1lbnQ+PEFiaWxpdHlUb0Z1bmN0aW9uRWxlbWVudD48VGVybWlub2xvZ3k+PENvZGU+RkE1NTA8L0NvZGU+PENvZGVTeXN0ZW0+aWNmPC9Db2RlU3lzdGVtPjxDb2RlRGVzY3JpcHRpb24+U3Bpc2U8L0NvZGVEZXNjcmlwdGlvbj48L1Rlcm1pbm9sb2d5PjxTY29yZT5sZXR0ZTwvU2NvcmU+PENvbW1lbnQ+QmVob3YgZm9yIGhqw6ZscHQgdGlsIGF0IHNtw7hyZSBtYWQgbW9yZ2VuK21pZGRhZythZnRlbi4gU3Bpc2VyIHJ1Z2Jyw7hkc21hZGRlciB0aWwgbcOlbHRpZGVyPC9Db21tZW50PjwvQWJpbGl0eVRvRnVuY3Rpb25FbGVtZW50PjwvQWJpbGl0eVRvRnVuY3Rpb25FbGVtZW50cz48L0FiaWxpdHlUb0Z1bmN0aW9uQXREaXNjaGFyZ2U+PExhdGVzdFBuTWVkaWNhdGlvbj48TmFtZU9mRHJ1Zz5veHljb2RvbiAoT1hZQ09ET05FICZxdW90O0hBTUVMTio8L05hbWVPZkRydWc+PERvc2FnZUZvcm0+MTAgbWcvbWwgKGluamVrdGlvbnMtL2luZnVzaW9uc3bDpnNrZSwgb3Bsw7hzbmluZyk8L0Rvc2FnZUZvcm0+PERydWdTdHJlbmd0aD4yLjUgbWc8L0RydWdTdHJlbmd0aD48Um91dGVPZkFkbWluaXN0cmF0aW9uPkludHJhdmVuw7hzIGFudmVuZGVsc2U8L1JvdXRlT2ZBZG1pbmlzdHJhdGlvbj48TGF0ZXN0TWVkaWNhdGlvbj48ZXAxOkRhdGUgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiPjIwMjQtMDEtMjQ8L2VwMTpEYXRlPjxlcDE6VGltZSB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCI+MTc6MjA8L2VwMTpUaW1lPjwvTGF0ZXN0TWVkaWNhdGlvbj48L0xhdGVzdFBuTWVkaWNhdGlvbj48TGF0ZXN0UG5NZWRpY2F0aW9uPjxOYW1lT2ZEcnVnPm1vcnBoaW4gKE1PUkZJTiAmcXVvdDtEQUsmcXVvdDspPC9OYW1lT2ZEcnVnPjxEb3NhZ2VGb3JtPjEwIG1nICh0YWJsZXR0ZXIpPC9Eb3NhZ2VGb3JtPjxEcnVnU3RyZW5ndGg+NSBtZzwvRHJ1Z1N0cmVuZ3RoPjxSb3V0ZU9mQWRtaW5pc3RyYXRpb24+T3JhbCBhbnZlbmRlbHNlPC9Sb3V0ZU9mQWRtaW5pc3RyYXRpb24+PExhdGVzdE1lZGljYXRpb24+PGVwMTpEYXRlIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIj4yMDI0LTAxLTI1PC9lcDE6RGF0ZT48ZXAxOlRpbWUgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiPjA4OjI2PC9lcDE6VGltZT48L0xhdGVzdE1lZGljYXRpb24+PC9MYXRlc3RQbk1lZGljYXRpb24+PERpc2NoYXJnZVJlbGF0ZWRNZWRpY2luZUluZm9ybWF0aW9uPjxBdHRhY2hlZD48RXhwaXJlPjIwMjQtMDEtMjY8L0V4cGlyZT48L0F0dGFjaGVkPjxSZWNlaXB0PjE8L1JlY2VpcHQ+PFBpY2tVcE9yRGVsaXZlcnk+MDwvUGlja1VwT3JEZWxpdmVyeT48RG9zYWdlRXhlbXB0aW9uUmVvcmRlcmVkPjA8L0Rvc2FnZUV4ZW1wdGlvblJlb3JkZXJlZD48L0Rpc2NoYXJnZVJlbGF0ZWRNZWRpY2luZUluZm9ybWF0aW9uPjxEaWV0Rmlyc3QyNEhvdXJzPjxMdW5jaEJveD4wPC9MdW5jaEJveD48U2hvcHBpbmdBdERpc2NoYXJnZT4wPC9TaG9wcGluZ0F0RGlzY2hhcmdlPjwvRGlldEZpcnN0MjRIb3Vycz48ZXAxOkZ1dHVyZVBsYW5zIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIj5Ib3NwaXRhbGV0IGhhciBiZWhhbmRsaW5nc2Fuc3ZhciBmb3IgcGF0aWVudGVuIGluZHRpbCAyOC0wMS0yMDI0IDE3OjAwLiBLb250YWt0dGVsZWZvbm51bW1lciAodGlsIGJydWcgZm9yIHN1bmRoZWRzcHJvZmVzc2lvbmVsbGUpOiA1OCA1NSA5NyA5OS4gRnJlbXRpZGlnZSBhZnRhbGVyOiBBbWIgdGlkIDcgZmVicnVhciBrbC4gMTAuMzAuIEFtYnVsYW50IGtvbnRyb2wgcHJpbnRlcyBvZyBtZWRnaXZlcy4uIDwvZXAxOkZ1dHVyZVBsYW5zPjwvUmVwb3J0T2ZEaXNjaGFyZ2U+PC9FbWVzc2FnZT4=\",\"externalId\":null,\"ccRecipient\":null,\"copiedMessageId\":null,\"letterType\":null,\"serviceTagCode\":null,\"messageType\":\"MEDCOM_MESSAGE_REPORT_OF_DISCHARGE_1_0_3\",\"versionCode\":\"XD1834C\",\"conversationId\":\"000000000001E4B51EE6004B0B706F30\",\"activityIdentifier\":{\"identifier\":\"MESSAGE:368852\",\"type\":\"MESSAGE\",\"activityId\":368852,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"patient\":{\"id\":3782,\"version\":20,\"firstName\":\"Jytte Gudrun\",\"lastName\":\"Jensen\",\"middleName\":null,\"fullName\":\"Jytte Gudrun Jensen\",\"fullReversedName\":\"Jensen, Jytte Gudrun\",\"homePhoneNumber\":null,\"mobilePhoneNumber\":\"40915846\",\"workPhoneNumber\":null,\"patientIdentifier\":{\"type\":\"cpr\",\"identifier\":\"211147-2370\",\"managedExternally\":false,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"currentAddress\":{\"addressLine1\":\"Buegangen 46\",\"addressLine2\":null,\"addressLine3\":null,\"addressLine4\":null,\"addressLine5\":null,\"administrativeAreaCode\":\"329\",\"countryCode\":\"dk\",\"postalCode\":\"4100\",\"postalDistrict\":\"Ringsted\",\"restricted\":false,\"geoCoordinates\":{\"x\":null,\"y\":null,\"present\":false},\"startDate\":null,\"endDate\":null,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/address/countries\"},\"searchPostalDistrict\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/address/postalDistrict\"}}},\"currentAddressIndicator\":\"PRIMARY_ADDRESS\",\"age\":76,\"monthsAfterBirthday\":2,\"gender\":\"FEMALE\",\"patientStatus\":\"FINAL\",\"patientState\":{\"id\":21,\"version\":0,\"name\":\"Aktiv\",\"color\":\"000000\",\"type\":{\"id\":\"ACTIVE\",\"name\":\"Aktiv\",\"abbreviation\":\"\",\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"active\":true,\"defaultObject\":true,\"citizenRegistryConfiguration\":{\"updateEnabled\":true,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"_links\":{\"medcomRecipient\":null,\"self\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patientStates/21\"},\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"telephonesRestricted\":false,\"imageId\":null,\"_links\":{\"medcomRecipient\":null,\"self\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3782\"},\"patientOverview\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3782/overview\"},\"citizenOverviewForms\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3782/citizenOverviewForms\"},\"measurementData\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3782/measurementInstructions/data/pages\"},\"measurementInstructions\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3782/measurementInstructions/instructions\"},\"patientConditions\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3782/conditions\"},\"patientOrganizations\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3782/organizations\"},\"activityLinksPrototypes\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3782/activitylinks/prototypes\"},\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"pathwayAssociation\":{\"placement\":{\"name\":\"MedCom Myndighedsenheden\",\"programPathwayId\":4355,\"pathwayTypeId\":13,\"parentPathwayId\":null,\"patientPathwayId\":70383,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"parentReferenceId\":null,\"referenceId\":1037144,\"canAssociateWithPathway\":true,\"canAssociateWithPatient\":false,\"associatedWithPatient\":false,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3782/pathways/70383/availablePathwayAssociations\"},\"availableRootProgramPathways\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3782/availableProgramPathways\"},\"availablePathwayPlacements\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3782/pathways/availablePathwayPlacements\"},\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"body\":null,\"priority\":null,\"previousMessageId\":null,\"_links\":{\"medcomRecipient\":null,\"self\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/368852\"},\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/audit/medcom/368852\"},\"relatedCommunication\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/related/368852\"},\"activeAssignments\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/assignments//mobile/ringsted/v2/assignments/patient/3782/assignments/active?activity=MESSAGE:368852\"},\"assignments\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/assignments//mobile/ringsted/v2/assignments/patient/3782/assignments?activity=MESSAGE:368852\"},\"availableAssignmentTypes\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/assignments//mobile/ringsted/v2/assignments/patient/3782/assignmentTypes/active?activity=MESSAGE:368852&activityType=MESSAGE&messageTypeType=MEDCOM&messageType=MEDCOM_MESSAGE_REPORT_OF_DISCHARGE_1_0_3&messageStateDirection=INCOMING\"},\"autoAssignmentsPrototype\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/assignments//mobile/ringsted/v2/assignments/auto/prototype?activity=MESSAGE:368852&activityType=MESSAGE&messageTypeType=MEDCOM&messageType=MEDCOM_MESSAGE_REPORT_OF_DISCHARGE_1_0_3&messageStateDirection=INCOMING&messageStatus=FINAL\"},\"update\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/368852\"},\"transformedBody\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/368852/transformedBody\"},\"print\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/368852/print\"},\"reply\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/368852/reply\"},\"replyTo\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/replyTo/368852?identifierType=SYGEHUSAFDELINGSNUMMER&identifierValue=3800S30\"},\"archive\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/archived/368852\"},\"availableCountries\":null,\"searchPostalDistrict\":null},\"level\":null}\r\n");
            baseObjects.Add("{\"type\":\"medcom\",\"id\":368829,\"version\":3,\"sender\":{\"type\":\"medcomMessageDetailsBased\",\"id\":368829,\"name\":\"Region Sjællands Sygehusvæsen - Ingemannsvej 50 - 4200 Slagelse - EAN: 5790001370886\",\"recipientAddress\":null,\"recipientType\":\"MESSAGE_SENDER\",\"_links\":{\"medcomRecipient\":{\"href\":\"/api/core/mobile/ringsted/v2/medcomRecipients/medcomSender/368829\"},\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"recipient\":{\"type\":\"medcomMessageDetailsBased\",\"id\":368829,\"name\":\"Ringsted Kommune - Zahlesvej 18 - 4100 Ringsted - EAN: 5790001353858\",\"recipientAddress\":null,\"recipientType\":\"MESSAGE_RECIPIENT\",\"_links\":{\"medcomRecipient\":{\"href\":\"/api/core/mobile/ringsted/v2/medcomRecipients/medcomRecipient/368829\"},\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"subject\":\"Udskrivningsrapport\",\"date\":\"2024-01-25T11:31:00+01:00\",\"state\":{\"direction\":\"INCOMING\",\"status\":\"FINAL\",\"response\":\"NOT_AWAITING\",\"handshake\":\"NO\",\"processed\":\"PROCESSED\",\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"customer\":null,\"raw\":\"PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiPz48RW1lc3NhZ2UgeG1sbnM9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMyI+PGVwMTpFbnZlbG9wZSB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCI+PGVwMTpTZW50PjxlcDE6RGF0ZT4yMDI0LTAxLTI1PC9lcDE6RGF0ZT48ZXAxOlRpbWU+MTE6Mjc8L2VwMTpUaW1lPjwvZXAxOlNlbnQ+PGVwMTpJZGVudGlmaWVyPkVQSUM2NDE3OTA0NDQ8L2VwMTpJZGVudGlmaWVyPjxlcDE6QWNrbm93bGVkZ2VtZW50Q29kZT5wbHVzcG9zaXRpdmt2aXR0PC9lcDE6QWNrbm93bGVkZ2VtZW50Q29kZT48L2VwMTpFbnZlbG9wZT48UmVwb3J0T2ZEaXNjaGFyZ2U+PExldHRlcj48SWRlbnRpZmllcj5FUElDNjQxNzkwNDQ0PC9JZGVudGlmaWVyPjxWZXJzaW9uQ29kZT5YRDE4MzRDPC9WZXJzaW9uQ29kZT48U3RhdGlzdGljYWxDb2RlPlhESVMxODwvU3RhdGlzdGljYWxDb2RlPjxBdXRob3Jpc2F0aW9uPjxlcDE6RGF0ZSB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCI+MjAyNC0wMS0yNTwvZXAxOkRhdGU+PGVwMTpUaW1lIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIj4xMToyNzwvZXAxOlRpbWU+PC9BdXRob3Jpc2F0aW9uPjxUeXBlQ29kZT5YRElTMTg8L1R5cGVDb2RlPjxFcGlzb2RlT2ZDYXJlSWRlbnRpZmllcj4wMDAwMDAwMDAxQzNCMTc4QjQ1MjZCQUQwQzc5MUZENTwvRXBpc29kZU9mQ2FyZUlkZW50aWZpZXI+PC9MZXR0ZXI+PGVwMTpTZW5kZXIgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiPjxlcDE6RUFOSWRlbnRpZmllcj41NzkwMDAxMzcwODg2PC9lcDE6RUFOSWRlbnRpZmllcj48ZXAxOklkZW50aWZpZXI+MzgwMFI4MDwvZXAxOklkZW50aWZpZXI+PGVwMTpJZGVudGlmaWVyQ29kZT5zeWdlaHVzYWZkZWxpbmdzbnVtbWVyPC9lcDE6SWRlbnRpZmllckNvZGU+PGVwMTpPcmdhbmlzYXRpb25OYW1lPlJlZ2lvbiBTasOmbGxhbmRzIFN5Z2VodXN2w6ZzZW48L2VwMTpPcmdhbmlzYXRpb25OYW1lPjxlcDE6RGVwYXJ0bWVudE5hbWU+U0xBIEFrdXQgQWZkZWxpbmc8L2VwMTpEZXBhcnRtZW50TmFtZT48ZXAxOlVuaXROYW1lPkFrdXRhZmRlbGluZ2VuIDEuc2FsPC9lcDE6VW5pdE5hbWU+PGVwMTpTdHJlZXROYW1lPkluZ2VtYW5uc3ZlaiA1MDwvZXAxOlN0cmVldE5hbWU+PGVwMTpEaXN0cmljdE5hbWU+U2xhZ2Vsc2U8L2VwMTpEaXN0cmljdE5hbWU+PGVwMTpQb3N0Q29kZUlkZW50aWZpZXI+NDIwMDwvZXAxOlBvc3RDb2RlSWRlbnRpZmllcj48ZXAxOlRlbGVwaG9uZVN1YnNjcmliZXJJZGVudGlmaWVyPjU4NTU5NjMxPC9lcDE6VGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+PGVwMTpTaWduZWRCeT48ZXAxOklkZW50aWZpZXI+MzgwMFI4MDwvZXAxOklkZW50aWZpZXI+PGVwMTpJZGVudGlmaWVyQ29kZT5zeWdlaHVzYWZkZWxpbmdzbnVtbWVyPC9lcDE6SWRlbnRpZmllckNvZGU+PGVwMTpQZXJzb25HaXZlbk5hbWU+TWFyaWFuYTwvZXAxOlBlcnNvbkdpdmVuTmFtZT48ZXAxOlBlcnNvblN1cm5hbWVOYW1lPlNhdnUgUGludG9pdTwvZXAxOlBlcnNvblN1cm5hbWVOYW1lPjxlcDE6UGVyc29uVGl0bGU+U29zdS1hc3Npc3RlbnQ8L2VwMTpQZXJzb25UaXRsZT48L2VwMTpTaWduZWRCeT48ZXAxOk1lZGljYWxTcGVjaWFsaXR5Q29kZT5pa2tla2xhc3NpZmljZXJldDwvZXAxOk1lZGljYWxTcGVjaWFsaXR5Q29kZT48ZXAxOkNvbnRhY3RJbmZvcm1hdGlvbj48ZXAxOkNvbnRhY3ROYW1lPmFrdXQgYWZkLiBzbGFnZWxzZTwvZXAxOkNvbnRhY3ROYW1lPjxlcDE6VGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+OTM1Njg3MDc8L2VwMTpUZWxlcGhvbmVTdWJzY3JpYmVySWRlbnRpZmllcj48L2VwMTpDb250YWN0SW5mb3JtYXRpb24+PC9lcDE6U2VuZGVyPjxlcDE6UmVjZWl2ZXIgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiPjxlcDE6RUFOSWRlbnRpZmllcj41NzkwMDAxMzUzODU4PC9lcDE6RUFOSWRlbnRpZmllcj48ZXAxOklkZW50aWZpZXI+MzI5PC9lcDE6SWRlbnRpZmllcj48ZXAxOklkZW50aWZpZXJDb2RlPmtvbW11bmVudW1tZXI8L2VwMTpJZGVudGlmaWVyQ29kZT48ZXAxOk9yZ2FuaXNhdGlvbk5hbWU+UmluZ3N0ZWQgS29tbXVuZTwvZXAxOk9yZ2FuaXNhdGlvbk5hbWU+PGVwMTpEZXBhcnRtZW50TmFtZT5Tb2NpYWwgb2cgU3VuZGhlZDwvZXAxOkRlcGFydG1lbnROYW1lPjxlcDE6VW5pdE5hbWU+SGplbW1lc3lnZXBsZWplbiwgUmluZ3N0ZWQgS29tbXVuZTwvZXAxOlVuaXROYW1lPjxlcDE6U3RyZWV0TmFtZT5aYWhsZXN2ZWogMTg8L2VwMTpTdHJlZXROYW1lPjxlcDE6RGlzdHJpY3ROYW1lPlJpbmdzdGVkPC9lcDE6RGlzdHJpY3ROYW1lPjxlcDE6UG9zdENvZGVJZGVudGlmaWVyPjQxMDA8L2VwMTpQb3N0Q29kZUlkZW50aWZpZXI+PC9lcDE6UmVjZWl2ZXI+PGVwMTpQcmFjdGl0aW9uZXIgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiPjxlcDE6SWRlbnRpZmllcj4wMjYwODU8L2VwMTpJZGVudGlmaWVyPjxlcDE6SWRlbnRpZmllckNvZGU+eWRlcm51bW1lcjwvZXAxOklkZW50aWZpZXJDb2RlPjxlcDE6UGVyc29uTmFtZT5Mw6ZnZWh1c2V0IEkgQmVubMO4c2U8L2VwMTpQZXJzb25OYW1lPjxlcDE6VGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+NTc2NzAwNDQ8L2VwMTpUZWxlcGhvbmVTdWJzY3JpYmVySWRlbnRpZmllcj48L2VwMTpQcmFjdGl0aW9uZXI+PGVwMTpQYXRpZW50IHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIj48ZXAxOkNpdmlsUmVnaXN0cmF0aW9uTnVtYmVyPjAyMDkzNjA3NzI8L2VwMTpDaXZpbFJlZ2lzdHJhdGlvbk51bWJlcj48ZXAxOlBlcnNvblN1cm5hbWVOYW1lPlBlZGVyc2VuPC9lcDE6UGVyc29uU3VybmFtZU5hbWU+PGVwMTpQZXJzb25HaXZlbk5hbWU+QmlydGhlPC9lcDE6UGVyc29uR2l2ZW5OYW1lPjxlcDE6U3RyZWV0TmFtZT5Sb3NraWxkZXZlaiA0OTgsLTIyPC9lcDE6U3RyZWV0TmFtZT48ZXAxOlN1YnVyYk5hbWU+T3J0dmVkPC9lcDE6U3VidXJiTmFtZT48ZXAxOkRpc3RyaWN0TmFtZT5SaW5nc3RlZDwvZXAxOkRpc3RyaWN0TmFtZT48ZXAxOlBvc3RDb2RlSWRlbnRpZmllcj40MTAwPC9lcDE6UG9zdENvZGVJZGVudGlmaWVyPjxlcDE6VGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+MjAyMDkyMjI8L2VwMTpUZWxlcGhvbmVTdWJzY3JpYmVySWRlbnRpZmllcj48L2VwMTpQYXRpZW50PjxSZWxhdGl2ZXM+PENvbW1lbnQ+ZGF0dGVyIGluZm9ybWVyZXQ8L0NvbW1lbnQ+PC9SZWxhdGl2ZXM+PGVwMTpBZG1pc3Npb24geG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiPjxlcDE6RGF0ZT4yMDI0LTAxLTIzPC9lcDE6RGF0ZT48ZXAxOlRpbWU+MjM6NTg8L2VwMTpUaW1lPjwvZXAxOkFkbWlzc2lvbj48ZXAxOkVuZE9mVHJlYXRtZW50IHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIj48ZXAxOkRhdGU+MjAyNC0wMS0yNTwvZXAxOkRhdGU+PGVwMTpUaW1lPjEyOjAwPC9lcDE6VGltZT48L2VwMTpFbmRPZlRyZWF0bWVudD48ZXAxOkRpc2NoYXJnZSB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCI+PGVwMTpEYXRlPjIwMjQtMDEtMjU8L2VwMTpEYXRlPjxlcDE6VGltZT4xMjowMDwvZXAxOlRpbWU+PC9lcDE6RGlzY2hhcmdlPjxlcDE6Q2F1c2VPZkFkbWlzc2lvbiB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCI+aW5kbGFndCBha3V0LCBkYSBwbGVqZWhqZW0gaGFyIG9ic2VydmVyZXQga2FmZmVncnVtc2xpZ25lbmRlIG9wa2FzdG5pbmdlci4gRGVyIGVyIGlra2Ugb2JzZXJ2ZXJldCBub2dldCBibMO4ZG5pbmcgdW5kZXIgaW5kbMOmZ2dlbHNlLCBoZ2Igc3RhYmlsLCBwdCBlciBtYXNzaXYgb2JzdGlwZXJldCBvZyBkZXIgZXIgZmplcm5ldCBlbiBzdG9yIGtub2xkIGFmZsO4cmluZyBzb20gc2FkIGZhc3QgaSByZWN0dW0sIGbDpWV0IG9saWVrbHl4LCBza2FsIE9CUyBmb3IgZGV0dGUgbsOlciBodW4ga29tbWVyIHJldHVyIHDDpSBwbGVqZWhqZW1tZXQsIHPDpnR0ZXMgaSBsaXZzbGFuZyBiZWggbWVkIHBhbnRvcHJhem9sPC9lcDE6Q2F1c2VPZkFkbWlzc2lvbj48TnVyc2luZ1Byb2JsZW1BcmVhcz48TnVyc2luZ1Byb2JsZW1BcmVhPjxUZXJtaW5vbG9neT48Q29kZT5BLTEyMzQ1PC9Db2RlPjxDb2RlU3lzdGVtPm1lZGNvbTwvQ29kZVN5c3RlbT48Q29kZURlc2NyaXB0aW9uPkZ1bmt0aW9uc25pdmVhdSBvZyBiZXbDpmdlYXBwYXJhdDwvQ29kZURlc2NyaXB0aW9uPjwvVGVybWlub2xvZ3k+PFByb2JsZW1BcmVhPkhqw6ZscCB2ZWRyw7hyZW5kZSB0b2lsZXRiZXPDuGc6IEJydWdlciBibGUgPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5VZCBmcmEgZnVua3Rpb25zZXZuZXZ1cmRlcmluZ2VuIHZ1cmRlcmVzOiBJbmdlbiBmYWxkcmlzaWtvPC9Qcm9ibGVtQXJlYT48L051cnNpbmdQcm9ibGVtQXJlYT48TnVyc2luZ1Byb2JsZW1BcmVhPjxUZXJtaW5vbG9neT48Q29kZT5CLTEyMzQ1PC9Db2RlPjxDb2RlU3lzdGVtPm1lZGNvbTwvQ29kZVN5c3RlbT48Q29kZURlc2NyaXB0aW9uPkVybsOmcmluZzwvQ29kZURlc2NyaXB0aW9uPjwvVGVybWlub2xvZ3k+PFByb2JsZW1BcmVhPkt2YWxtZSBncmFkL2h5cHBpZ2hlZDogSW5nZW4gPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5Lb21tZW50YXIgKGVybsOmcmluZyk6IERydWtrZXQgMjAwIG1sIHggMiBmcmVzdWJpbiBlbmVyZ2lkcmlrPC9Qcm9ibGVtQXJlYT48L051cnNpbmdQcm9ibGVtQXJlYT48TnVyc2luZ1Byb2JsZW1BcmVhPjxUZXJtaW5vbG9neT48Q29kZT5DLTEyMzQ1PC9Db2RlPjxDb2RlU3lzdGVtPm1lZGNvbTwvQ29kZVN5c3RlbT48Q29kZURlc2NyaXB0aW9uPkh1ZCBvZyBzbGltaGluZGVyPC9Db2RlRGVzY3JpcHRpb24+PC9UZXJtaW5vbG9neT48UHJvYmxlbUFyZWE+UmlzaWtvIGZvciB1ZHZpa2xpbmcgYWYgdHJ5a3NrYWRlLy1zw6VyPzogTmVqIDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+VHJ5a3PDpXJzZm9yZWJ5Z2dlbHNlIC0gbWFkcmFzOiBTa3VtbWFkcmFzIDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+VHJ5a3PDpXJzZm9yZWJ5Z2dlbHNlIC0gYWZsYXN0bmluZzogSWtrZSBiZWhvdiBmb3IgdHJ5a2FmbGFzdG5pbmcgPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5Ucnlrc8OlcnNmb3JlYnlnZ2Vsc2UgLSBtZWRpY2luc2sgdWRzdHlyOiBJa2tlIGJlaG92IGZvciB0cnlrYWZsYXN0bmluZzwvUHJvYmxlbUFyZWE+PC9OdXJzaW5nUHJvYmxlbUFyZWE+PE51cnNpbmdQcm9ibGVtQXJlYT48VGVybWlub2xvZ3k+PENvZGU+RC0xMjM0NTwvQ29kZT48Q29kZVN5c3RlbT5tZWRjb208L0NvZGVTeXN0ZW0+PENvZGVEZXNjcmlwdGlvbj5Lb21tdW5pa2F0aW9uL25ldXJvbG9naTwvQ29kZURlc2NyaXB0aW9uPjwvVGVybWlub2xvZ3k+PFByb2JsZW1BcmVhPkJldmlkc3RoZWRzbml2ZWF1OiBWw6VnZW4sIHJlYWdlcmVyIG5vcm1hbHQgZWxsZXIgbm9ybWFsIHPDuHZuIDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+Tml2ZWF1IGFmIG9yaWVudGVyaW5nOiBJa2tlIG9yaWVudGVyZXQgaSBlZ25lIGRhdGEsIE9yaWVudGVyZXQgaSBzdGVkLCBJa2tlIG9yaWVudGVyZXQgaSB0aWQgKEFuZ2l2ZXIgZm9ya2VydCBDUFIgbnIuIG1lbiB2ZWQgaHVuIGVyIHDDpSBzeWdlaHVzLCBtZW4gaWtrZSBodm9yZm9yIGh1biBlciBoZXIuKSA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPkV2bmUgdGlsIGF0IGZvcnN0w6U6IEthbiBpa2tlIHZ1cmRlcmVzIDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+RXZuZSB0aWwgYXQgZ8O4cmUgc2lnIGZvcnN0w6VlbGlnOiBLYW4gaWtrZSB2dXJkZXJlczwvUHJvYmxlbUFyZWE+PC9OdXJzaW5nUHJvYmxlbUFyZWE+PE51cnNpbmdQcm9ibGVtQXJlYT48VGVybWlub2xvZ3k+PENvZGU+Ri0xMjM0NTwvQ29kZT48Q29kZVN5c3RlbT5tZWRjb208L0NvZGVTeXN0ZW0+PENvZGVEZXNjcmlwdGlvbj5SZXNwaXJhdGlvbiBvZyBjaXJrdWxhdGlvbjwvQ29kZURlc2NyaXB0aW9uPjwvVGVybWlub2xvZ3k+PFByb2JsZW1BcmVhPlJlc3BpcmF0aW9uc23DuG5zdGVyL2R5YmRlOiBOb3JtYWwgPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5SZXNwaXJhdGlvbnNseWRlOiBOb3JtYWwgPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5Ib3N0ZTogSW5nZW4gPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5MdWZ0dmVqOiBGcmkgPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5SZXNwaXJhdGlvbiAtIGludGVydmVudGlvbjogSW50ZXQgYmVob3YgZm9yIHJlc3AuIHN0w7h0dGUvaWx0dGlsc2t1ZDwvUHJvYmxlbUFyZWE+PC9OdXJzaW5nUHJvYmxlbUFyZWE+PE51cnNpbmdQcm9ibGVtQXJlYT48VGVybWlub2xvZ3k+PENvZGU+Sy0xMjM0NTwvQ29kZT48Q29kZVN5c3RlbT5tZWRjb208L0NvZGVTeXN0ZW0+PENvZGVEZXNjcmlwdGlvbj5VZHNraWxsZWxzZSA8L0NvZGVEZXNjcmlwdGlvbj48L1Rlcm1pbm9sb2d5PjxQcm9ibGVtQXJlYT5PYnNlcnZhdGlvbiBhZiBhYmRvbWVuOiBOb3JtYWwgYWJkb21lbiA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPkZsYXR1czogSmEsIGZsYXR1cyBpIHZhZ3RlbiA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPlNlbmVzdGUgYWZmw7hyaW5nIHVuZGVyIGluZGzDpmdnZWxzZSAoZGF0byk6IDI0LTAxLTI0IDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+QWZmw7hyaW5nOiBOZWosIGlra2UgYWZmw7hyaW5nIGkgdmFndGVuIDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+QWZmw7hyaW5nc3Vkc2VlbmRlOiBGb3JtZXQgPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5BZmbDuHJpbmdzZmFydmU6IEJydW4gPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5BZmbDuHJpbmdzbcOmbmdkZTogU3RvciA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPkhqw6ZscCB2ZWRyw7hyZW5kZSB0b2lsZXRiZXPDuGcgKGRvay4gaSBBa3R1ZWx0IGZ1bmt0aW9uc25pdmVhdSAtIG9ic2VydmF0aW9uKSA6IEJydWdlciBibGUgPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5VcmluIDogSmEsIHVyaW51ZHNraWxsZWxzZSBpIHZhZ3RlbiA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPlVyaW51ZHNlZW5kZTogS29uY2VudHJlcmV0IDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+VXJpbm3Dpm5nZGU6IE1lbGxlbSAoaSBibGUpPC9Qcm9ibGVtQXJlYT48L051cnNpbmdQcm9ibGVtQXJlYT48L051cnNpbmdQcm9ibGVtQXJlYXM+PGVwMTpSaXNrT2ZDb250YWdpb24geG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiPm5lajwvZXAxOlJpc2tPZkNvbnRhZ2lvbj48RGlhZ25vc2VzPjxEaWFnbm9zaXM+PENvZGU+REs5MjE8L0NvZGU+PFR5cGVDb2RlPlNLU2RpYWdub3Nla29kZTwvVHlwZUNvZGU+PFRleHQ+TWVsw6ZuYTwvVGV4dD48L0RpYWdub3Npcz48L0RpYWdub3Nlcz48QWJpbGl0eVRvRnVuY3Rpb25BdERpc2NoYXJnZT48TGFzdE1vZGlmaWVkRGF0ZT4yMDI0LTAxLTI1PC9MYXN0TW9kaWZpZWREYXRlPjxBYmlsaXR5VG9GdW5jdGlvbkVsZW1lbnRzPjxBYmlsaXR5VG9GdW5jdGlvbkVsZW1lbnQ+PFRlcm1pbm9sb2d5PjxDb2RlPkZBNTEwPC9Db2RlPjxDb2RlU3lzdGVtPmljZjwvQ29kZVN5c3RlbT48Q29kZURlc2NyaXB0aW9uPlZhc2tlIHNpZzwvQ29kZURlc2NyaXB0aW9uPjwvVGVybWlub2xvZ3k+PFNjb3JlPm1vZGVyYXRlPC9TY29yZT48Q29tbWVudD5za2FsIGd1aWRlczwvQ29tbWVudD48L0FiaWxpdHlUb0Z1bmN0aW9uRWxlbWVudD48QWJpbGl0eVRvRnVuY3Rpb25FbGVtZW50PjxUZXJtaW5vbG9neT48Q29kZT5GQTU0MDwvQ29kZT48Q29kZVN5c3RlbT5pY2Y8L0NvZGVTeXN0ZW0+PENvZGVEZXNjcmlwdGlvbj5BZi0gb2cgcMOla2zDpmRuaW5nPC9Db2RlRGVzY3JpcHRpb24+PC9UZXJtaW5vbG9neT48U2NvcmU+bW9kZXJhdGU8L1Njb3JlPjxDb21tZW50PnNrYWwgZ3VpZGVzPC9Db21tZW50PjwvQWJpbGl0eVRvRnVuY3Rpb25FbGVtZW50PjxBYmlsaXR5VG9GdW5jdGlvbkVsZW1lbnQ+PFRlcm1pbm9sb2d5PjxDb2RlPkZBNTMwPC9Db2RlPjxDb2RlU3lzdGVtPmljZjwvQ29kZVN5c3RlbT48Q29kZURlc2NyaXB0aW9uPkfDpSBww6UgdG9pbGV0PC9Db2RlRGVzY3JpcHRpb24+PC9UZXJtaW5vbG9neT48U2NvcmU+bW9kZXJhdGU8L1Njb3JlPjxDb21tZW50PmbDuGxlcyBww6UgdG9pbGV0ZXQsIHVzaWtrZXIgcMOlIGJlbmVuZSwga2FuIGRyZWplIGZyYSBzdG9sIHRpbCBiw6Zra2Vuc3RvbCBtZWQgbGlkdCBzdMO4dHRlPC9Db21tZW50PjwvQWJpbGl0eVRvRnVuY3Rpb25FbGVtZW50PjxBYmlsaXR5VG9GdW5jdGlvbkVsZW1lbnQ+PFRlcm1pbm9sb2d5PjxDb2RlPkZBNDIwPC9Db2RlPjxDb2RlU3lzdGVtPmljZjwvQ29kZVN5c3RlbT48Q29kZURlc2NyaXB0aW9uPkZvcmZseXR0ZSBzaWc8L0NvZGVEZXNjcmlwdGlvbj48L1Rlcm1pbm9sb2d5PjxTY29yZT5tb2RlcmF0ZTwvU2NvcmU+PENvbW1lbnQ+dmlya2VyIGJhbmdlIHZlZCBtb2JpbGlzZXJpbmcsIGbDpWV0IHN0w7h0dGUgYWYgMSBwZXJzIHRpbCBkZXR0ZTwvQ29tbWVudD48L0FiaWxpdHlUb0Z1bmN0aW9uRWxlbWVudD48QWJpbGl0eVRvRnVuY3Rpb25FbGVtZW50PjxUZXJtaW5vbG9neT48Q29kZT5GQTQ2MDwvQ29kZT48Q29kZVN5c3RlbT5pY2Y8L0NvZGVTeXN0ZW0+PENvZGVEZXNjcmlwdGlvbj5Gw6ZyZGVuIGkgZm9yc2tlbGxpZ2Ugb21naXZlbHNlcjwvQ29kZURlc2NyaXB0aW9uPjwvVGVybWlub2xvZ3k+PFNjb3JlPmlra2VfcmVsZXZhbnQ8L1Njb3JlPjwvQWJpbGl0eVRvRnVuY3Rpb25FbGVtZW50PjxBYmlsaXR5VG9GdW5jdGlvbkVsZW1lbnQ+PFRlcm1pbm9sb2d5PjxDb2RlPkZBNTYwPC9Db2RlPjxDb2RlU3lzdGVtPmljZjwvQ29kZVN5c3RlbT48Q29kZURlc2NyaXB0aW9uPkRyaWtrZTwvQ29kZURlc2NyaXB0aW9uPjwvVGVybWlub2xvZ3k+PFNjb3JlPm1vZGVyYXRlPC9TY29yZT48Q29tbWVudD5za2FsIG7DuGRlcywgb2JzIG9tIHB0IGludGFnZSBub2sgcG88L0NvbW1lbnQ+PC9BYmlsaXR5VG9GdW5jdGlvbkVsZW1lbnQ+PEFiaWxpdHlUb0Z1bmN0aW9uRWxlbWVudD48VGVybWlub2xvZ3k+PENvZGU+RkE1NTA8L0NvZGU+PENvZGVTeXN0ZW0+aWNmPC9Db2RlU3lzdGVtPjxDb2RlRGVzY3JpcHRpb24+U3Bpc2U8L0NvZGVEZXNjcmlwdGlvbj48L1Rlcm1pbm9sb2d5PjxTY29yZT5sZXR0ZTwvU2NvcmU+PENvbW1lbnQ+c2thbCBuw7hkZXM8L0NvbW1lbnQ+PC9BYmlsaXR5VG9GdW5jdGlvbkVsZW1lbnQ+PC9BYmlsaXR5VG9GdW5jdGlvbkVsZW1lbnRzPjwvQWJpbGl0eVRvRnVuY3Rpb25BdERpc2NoYXJnZT48TGF0ZXN0RGVwb3RNZWRpY2F0aW9uPjxOYW1lT2ZEcnVnPmFsZW5kcm9uc3lyZSAoRk9TQU1BWCBVR0VUQUJMRVQpPC9OYW1lT2ZEcnVnPjxEb3NhZ2VGb3JtPjcwIG1nICh0YWJsZXR0ZXIpPC9Eb3NhZ2VGb3JtPjxSb3V0ZU9mQWRtaW5pc3RyYXRpb24+T3JhbCBhbnZlbmRlbHNlPC9Sb3V0ZU9mQWRtaW5pc3RyYXRpb24+PC9MYXRlc3REZXBvdE1lZGljYXRpb24+PERpc2NoYXJnZVJlbGF0ZWRNZWRpY2luZUluZm9ybWF0aW9uPjxBdHRhY2hlZD48RXhwaXJlPjIwMjQtMDEtMjc8L0V4cGlyZT48Q29tbWVudD5tZWRnaXZlcyBwZW5vbWF4IHRpbCBoZWxlIGt1cmVuIG9nIHBhbnRyb3Bhem9sIHRpbDIgZGFnZTwvQ29tbWVudD48L0F0dGFjaGVkPjxSZWNlaXB0PjE8L1JlY2VpcHQ+PFBpY2tVcE9yRGVsaXZlcnk+MDwvUGlja1VwT3JEZWxpdmVyeT48RG9zYWdlRXhlbXB0aW9uUmVvcmRlcmVkPjA8L0Rvc2FnZUV4ZW1wdGlvblJlb3JkZXJlZD48L0Rpc2NoYXJnZVJlbGF0ZWRNZWRpY2luZUluZm9ybWF0aW9uPjxEaWV0Rmlyc3QyNEhvdXJzPjxMdW5jaEJveD4wPC9MdW5jaEJveD48U2hvcHBpbmdBdERpc2NoYXJnZT4wPC9TaG9wcGluZ0F0RGlzY2hhcmdlPjwvRGlldEZpcnN0MjRIb3Vycz48ZXAxOkZ1dHVyZVBsYW5zIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIj5Ib3NwaXRhbGV0IGhhciBiZWhhbmRsaW5nc2Fuc3ZhciBmb3IgcGF0aWVudGVuIGluZHRpbCAyOC0wMS0yMDI0IDE0OjAwLiBLb250YWt0dGVsZWZvbm51bW1lciAodGlsIGJydWcgZm9yIHN1bmRoZWRzcHJvZmVzc2lvbmVsbGUpOiA1OCA1NSAzMiAwMC4gRnJlbXRpZGlnZSBhZnRhbGVyOiB1ZHNrcml2ZXMgdGlsIE9ydHZlZC4gPC9lcDE6RnV0dXJlUGxhbnM+PC9SZXBvcnRPZkRpc2NoYXJnZT48L0VtZXNzYWdlPg==\",\"externalId\":null,\"ccRecipient\":null,\"copiedMessageId\":null,\"letterType\":null,\"serviceTagCode\":null,\"messageType\":\"MEDCOM_MESSAGE_REPORT_OF_DISCHARGE_1_0_3\",\"versionCode\":\"XD1834C\",\"conversationId\":\"0000000001C3B178B4526BAD0C791FD5\",\"activityIdentifier\":{\"identifier\":\"MESSAGE:368829\",\"type\":\"MESSAGE\",\"activityId\":368829,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"patient\":{\"id\":143,\"version\":51,\"firstName\":\"Birthe\",\"lastName\":\"Pedersen\",\"middleName\":null,\"fullName\":\"Birthe Pedersen\",\"fullReversedName\":\"Pedersen, Birthe\",\"homePhoneNumber\":\"20209222\",\"mobilePhoneNumber\":null,\"workPhoneNumber\":null,\"patientIdentifier\":{\"type\":\"cpr\",\"identifier\":\"020936-0772\",\"managedExternally\":false,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"currentAddress\":{\"addressLine1\":\"Roskildevej 498,-22\",\"addressLine2\":null,\"addressLine3\":\"Ortved\",\"addressLine4\":null,\"addressLine5\":null,\"administrativeAreaCode\":\"329\",\"countryCode\":\"dk\",\"postalCode\":\"4100\",\"postalDistrict\":\"Ringsted\",\"restricted\":false,\"geoCoordinates\":{\"x\":null,\"y\":null,\"present\":false},\"startDate\":null,\"endDate\":null,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/address/countries\"},\"searchPostalDistrict\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/address/postalDistrict\"}}},\"currentAddressIndicator\":\"PRIMARY_ADDRESS\",\"age\":87,\"monthsAfterBirthday\":5,\"gender\":\"FEMALE\",\"patientStatus\":\"FINAL\",\"patientState\":{\"id\":21,\"version\":0,\"name\":\"Aktiv\",\"color\":\"000000\",\"type\":{\"id\":\"ACTIVE\",\"name\":\"Aktiv\",\"abbreviation\":\"\",\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"active\":true,\"defaultObject\":true,\"citizenRegistryConfiguration\":{\"updateEnabled\":true,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"_links\":{\"medcomRecipient\":null,\"self\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patientStates/21\"},\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"telephonesRestricted\":false,\"imageId\":null,\"_links\":{\"medcomRecipient\":null,\"self\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/143\"},\"patientOverview\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/143/overview\"},\"citizenOverviewForms\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/143/citizenOverviewForms\"},\"measurementData\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/143/measurementInstructions/data/pages\"},\"measurementInstructions\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/143/measurementInstructions/instructions\"},\"patientConditions\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/143/conditions\"},\"patientOrganizations\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/143/organizations\"},\"activityLinksPrototypes\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/143/activitylinks/prototypes\"},\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"pathwayAssociation\":{\"placement\":{\"name\":\"MedCom Myndighedsenheden\",\"programPathwayId\":4355,\"pathwayTypeId\":13,\"parentPathwayId\":null,\"patientPathwayId\":53420,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"parentReferenceId\":null,\"referenceId\":1036933,\"canAssociateWithPathway\":true,\"canAssociateWithPatient\":false,\"associatedWithPatient\":false,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/143/pathways/53420/availablePathwayAssociations\"},\"availableRootProgramPathways\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/143/availableProgramPathways\"},\"availablePathwayPlacements\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/143/pathways/availablePathwayPlacements\"},\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"body\":null,\"priority\":null,\"previousMessageId\":null,\"_links\":{\"medcomRecipient\":null,\"self\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/368829\"},\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/audit/medcom/368829\"},\"relatedCommunication\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/related/368829\"},\"activeAssignments\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/assignments//mobile/ringsted/v2/assignments/patient/143/assignments/active?activity=MESSAGE:368829\"},\"assignments\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/assignments//mobile/ringsted/v2/assignments/patient/143/assignments?activity=MESSAGE:368829\"},\"availableAssignmentTypes\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/assignments//mobile/ringsted/v2/assignments/patient/143/assignmentTypes/active?activity=MESSAGE:368829&activityType=MESSAGE&messageTypeType=MEDCOM&messageType=MEDCOM_MESSAGE_REPORT_OF_DISCHARGE_1_0_3&messageStateDirection=INCOMING\"},\"autoAssignmentsPrototype\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/assignments//mobile/ringsted/v2/assignments/auto/prototype?activity=MESSAGE:368829&activityType=MESSAGE&messageTypeType=MEDCOM&messageType=MEDCOM_MESSAGE_REPORT_OF_DISCHARGE_1_0_3&messageStateDirection=INCOMING&messageStatus=FINAL\"},\"update\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/368829\"},\"transformedBody\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/368829/transformedBody\"},\"print\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/368829/print\"},\"reply\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/368829/reply\"},\"replyTo\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/replyTo/368829?identifierType=SYGEHUSAFDELINGSNUMMER&identifierValue=3800R80\"},\"archive\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/archived/368829\"},\"availableCountries\":null,\"searchPostalDistrict\":null},\"level\":null}\r\n");
            baseObjects.Add("{\"type\":\"medcom\",\"id\":368786,\"version\":3,\"sender\":{\"type\":\"medcomMessageDetailsBased\",\"id\":368786,\"name\":\"Region Sjællands Sygehusvæsen - Ingemannsvej 50 - 4200 Slagelse - EAN: 5790001360719\",\"recipientAddress\":null,\"recipientType\":\"MESSAGE_SENDER\",\"_links\":{\"medcomRecipient\":{\"href\":\"/api/core/mobile/ringsted/v2/medcomRecipients/medcomSender/368786\"},\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"recipient\":{\"type\":\"medcomMessageDetailsBased\",\"id\":368786,\"name\":\"Ringsted Kommune - Zahlesvej 18 - 4100 Ringsted - EAN: 5790001353858\",\"recipientAddress\":null,\"recipientType\":\"MESSAGE_RECIPIENT\",\"_links\":{\"medcomRecipient\":{\"href\":\"/api/core/mobile/ringsted/v2/medcomRecipients/medcomRecipient/368786\"},\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"subject\":\"Udskrivningsrapport\",\"date\":\"2024-01-25T11:15:00+01:00\",\"state\":{\"direction\":\"INCOMING\",\"status\":\"FINAL\",\"response\":\"NOT_AWAITING\",\"handshake\":\"NO\",\"processed\":\"PROCESSED\",\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"customer\":null,\"raw\":\"PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiPz48RW1lc3NhZ2UgeG1sbnM9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMyI+PGVwMTpFbnZlbG9wZSB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCI+PGVwMTpTZW50PjxlcDE6RGF0ZT4yMDI0LTAxLTI1PC9lcDE6RGF0ZT48ZXAxOlRpbWU+MTE6MTE8L2VwMTpUaW1lPjwvZXAxOlNlbnQ+PGVwMTpJZGVudGlmaWVyPkVQSUM2NDE3ODMxNzY8L2VwMTpJZGVudGlmaWVyPjxlcDE6QWNrbm93bGVkZ2VtZW50Q29kZT5wbHVzcG9zaXRpdmt2aXR0PC9lcDE6QWNrbm93bGVkZ2VtZW50Q29kZT48L2VwMTpFbnZlbG9wZT48UmVwb3J0T2ZEaXNjaGFyZ2U+PExldHRlcj48SWRlbnRpZmllcj5FUElDNjQxNzgzMTc2PC9JZGVudGlmaWVyPjxWZXJzaW9uQ29kZT5YRDE4MzRDPC9WZXJzaW9uQ29kZT48U3RhdGlzdGljYWxDb2RlPlhESVMxODwvU3RhdGlzdGljYWxDb2RlPjxBdXRob3Jpc2F0aW9uPjxlcDE6RGF0ZSB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCI+MjAyNC0wMS0yNTwvZXAxOkRhdGU+PGVwMTpUaW1lIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIj4xMToxMTwvZXAxOlRpbWU+PC9BdXRob3Jpc2F0aW9uPjxUeXBlQ29kZT5YRElTMTg8L1R5cGVDb2RlPjxFcGlzb2RlT2ZDYXJlSWRlbnRpZmllcj4wMDAwMDAwMDAxMkU3OUEwMEY2Q0MzMjI5RTA2MkQ4MjwvRXBpc29kZU9mQ2FyZUlkZW50aWZpZXI+PC9MZXR0ZXI+PGVwMTpTZW5kZXIgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiPjxlcDE6RUFOSWRlbnRpZmllcj41NzkwMDAxMzYwNzE5PC9lcDE6RUFOSWRlbnRpZmllcj48ZXAxOklkZW50aWZpZXI+MzgwMFMzMDwvZXAxOklkZW50aWZpZXI+PGVwMTpJZGVudGlmaWVyQ29kZT5zeWdlaHVzYWZkZWxpbmdzbnVtbWVyPC9lcDE6SWRlbnRpZmllckNvZGU+PGVwMTpPcmdhbmlzYXRpb25OYW1lPlJlZ2lvbiBTasOmbGxhbmRzIFN5Z2VodXN2w6ZzZW48L2VwMTpPcmdhbmlzYXRpb25OYW1lPjxlcDE6RGVwYXJ0bWVudE5hbWU+U0xBIE9ydG9ww6Zka2lydXJnaXNrIEFmZC48L2VwMTpEZXBhcnRtZW50TmFtZT48ZXAxOlVuaXROYW1lPk9ydG9ww6Zka2lydXJnaXNrIFNlbmdlYWZzbml0PC9lcDE6VW5pdE5hbWU+PGVwMTpTdHJlZXROYW1lPkluZ2VtYW5uc3ZlaiA1MDwvZXAxOlN0cmVldE5hbWU+PGVwMTpEaXN0cmljdE5hbWU+U2xhZ2Vsc2U8L2VwMTpEaXN0cmljdE5hbWU+PGVwMTpQb3N0Q29kZUlkZW50aWZpZXI+NDIwMDwvZXAxOlBvc3RDb2RlSWRlbnRpZmllcj48ZXAxOlRlbGVwaG9uZVN1YnNjcmliZXJJZGVudGlmaWVyPjU4NTU5NzgyPC9lcDE6VGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+PGVwMTpTaWduZWRCeT48ZXAxOklkZW50aWZpZXI+MzgwMFMzMDwvZXAxOklkZW50aWZpZXI+PGVwMTpJZGVudGlmaWVyQ29kZT5zeWdlaHVzYWZkZWxpbmdzbnVtbWVyPC9lcDE6SWRlbnRpZmllckNvZGU+PGVwMTpQZXJzb25HaXZlbk5hbWU+UmFuZGk8L2VwMTpQZXJzb25HaXZlbk5hbWU+PGVwMTpQZXJzb25TdXJuYW1lTmFtZT5KZW5zZW4tRXJjazwvZXAxOlBlcnNvblN1cm5hbWVOYW1lPjxlcDE6UGVyc29uVGl0bGU+U3lnZXBsZWplcnNrZTwvZXAxOlBlcnNvblRpdGxlPjwvZXAxOlNpZ25lZEJ5PjxlcDE6TWVkaWNhbFNwZWNpYWxpdHlDb2RlPm9ydG9wYWVkaXNrX2tpcnVyZ2lfc3lnZWh1czwvZXAxOk1lZGljYWxTcGVjaWFsaXR5Q29kZT48ZXAxOkNvbnRhY3RJbmZvcm1hdGlvbj48ZXAxOkNvbnRhY3ROYW1lPk9ydG9ww6Zka2lydXJnaXNrIHNlbmdlYWZkZWxpbmc8L2VwMTpDb250YWN0TmFtZT48ZXAxOkNvbnRhY3ROYW1lPlNwbC4gUmFuZGkgSmVuc2VuLUVyY2s8L2VwMTpDb250YWN0TmFtZT48ZXAxOlRlbGVwaG9uZVN1YnNjcmliZXJJZGVudGlmaWVyPjU4NTU5NzgyPC9lcDE6VGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+PC9lcDE6Q29udGFjdEluZm9ybWF0aW9uPjwvZXAxOlNlbmRlcj48ZXAxOlJlY2VpdmVyIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIj48ZXAxOkVBTklkZW50aWZpZXI+NTc5MDAwMTM1Mzg1ODwvZXAxOkVBTklkZW50aWZpZXI+PGVwMTpJZGVudGlmaWVyPjMyOTwvZXAxOklkZW50aWZpZXI+PGVwMTpJZGVudGlmaWVyQ29kZT5rb21tdW5lbnVtbWVyPC9lcDE6SWRlbnRpZmllckNvZGU+PGVwMTpPcmdhbmlzYXRpb25OYW1lPlJpbmdzdGVkIEtvbW11bmU8L2VwMTpPcmdhbmlzYXRpb25OYW1lPjxlcDE6RGVwYXJ0bWVudE5hbWU+U29jaWFsIG9nIFN1bmRoZWQ8L2VwMTpEZXBhcnRtZW50TmFtZT48ZXAxOlVuaXROYW1lPkhqZW1tZXN5Z2VwbGVqZW4sIFJpbmdzdGVkIEtvbW11bmU8L2VwMTpVbml0TmFtZT48ZXAxOlN0cmVldE5hbWU+WmFobGVzdmVqIDE4PC9lcDE6U3RyZWV0TmFtZT48ZXAxOkRpc3RyaWN0TmFtZT5SaW5nc3RlZDwvZXAxOkRpc3RyaWN0TmFtZT48ZXAxOlBvc3RDb2RlSWRlbnRpZmllcj40MTAwPC9lcDE6UG9zdENvZGVJZGVudGlmaWVyPjwvZXAxOlJlY2VpdmVyPjxlcDE6UHJhY3RpdGlvbmVyIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIj48ZXAxOklkZW50aWZpZXI+MDI4MjIzPC9lcDE6SWRlbnRpZmllcj48ZXAxOklkZW50aWZpZXJDb2RlPnlkZXJudW1tZXI8L2VwMTpJZGVudGlmaWVyQ29kZT48ZXAxOlBlcnNvbk5hbWU+SGVsZ2EgTGluZCBOaWVsc2VuICZhbXA7IFN0aW5lIFByaW50ejwvZXAxOlBlcnNvbk5hbWU+PGVwMTpUZWxlcGhvbmVTdWJzY3JpYmVySWRlbnRpZmllcj41NzYxMDcxMDwvZXAxOlRlbGVwaG9uZVN1YnNjcmliZXJJZGVudGlmaWVyPjwvZXAxOlByYWN0aXRpb25lcj48ZXAxOlBhdGllbnQgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiPjxlcDE6Q2l2aWxSZWdpc3RyYXRpb25OdW1iZXI+MTkwOTI3MDc3MDwvZXAxOkNpdmlsUmVnaXN0cmF0aW9uTnVtYmVyPjxlcDE6UGVyc29uU3VybmFtZU5hbWU+TGFyc2VuPC9lcDE6UGVyc29uU3VybmFtZU5hbWU+PGVwMTpQZXJzb25HaXZlbk5hbWU+RWRpdCBKb2hhbm5lPC9lcDE6UGVyc29uR2l2ZW5OYW1lPjxlcDE6U3RyZWV0TmFtZT5GYXJlbmRsw7hzZXZlaiA4MzwvZXAxOlN0cmVldE5hbWU+PGVwMTpTdWJ1cmJOYW1lPkZhcmVuZGzDuHNlPC9lcDE6U3VidXJiTmFtZT48ZXAxOkRpc3RyaWN0TmFtZT5SaW5nc3RlZDwvZXAxOkRpc3RyaWN0TmFtZT48ZXAxOlBvc3RDb2RlSWRlbnRpZmllcj40MTAwPC9lcDE6UG9zdENvZGVJZGVudGlmaWVyPjxlcDE6VGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+MDAwMDAwMDA8L2VwMTpUZWxlcGhvbmVTdWJzY3JpYmVySWRlbnRpZmllcj48L2VwMTpQYXRpZW50PjxSZWxhdGl2ZXM+PFJlbGF0aXZlPjxSZWxhdGlvbkNvZGU+YmFybjwvUmVsYXRpb25Db2RlPjxlcDE6UGVyc29uU3VybmFtZU5hbWUgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiPlVsbGE8L2VwMTpQZXJzb25TdXJuYW1lTmFtZT48ZXAxOlBlcnNvbkdpdmVuTmFtZSB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCI+RGF0dGVyPC9lcDE6UGVyc29uR2l2ZW5OYW1lPjxlcDE6Q29kZWRUZWxlcGhvbmVTdWJzY3JpYmVySWRlbnRpZmllciB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCI+PGVwMTpUZWxlcGhvbmVDb2RlPm1vYmlsPC9lcDE6VGVsZXBob25lQ29kZT48ZXAxOlRlbGVwaG9uZVN1YnNjcmliZXJJZGVudGlmaWVyPjI0NjE4MDM0PC9lcDE6VGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+PC9lcDE6Q29kZWRUZWxlcGhvbmVTdWJzY3JpYmVySWRlbnRpZmllcj48SW5mb3JtZWRSZWxhdGl2ZT4xPC9JbmZvcm1lZFJlbGF0aXZlPjwvUmVsYXRpdmU+PFJlbGF0aXZlPjxSZWxhdGlvbkNvZGU+YmFybjwvUmVsYXRpb25Db2RlPjxlcDE6UGVyc29uU3VybmFtZU5hbWUgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiPkxhcnNlbjwvZXAxOlBlcnNvblN1cm5hbWVOYW1lPjxlcDE6UGVyc29uR2l2ZW5OYW1lIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIj5UaW5hPC9lcDE6UGVyc29uR2l2ZW5OYW1lPjxlcDE6Q29kZWRUZWxlcGhvbmVTdWJzY3JpYmVySWRlbnRpZmllciB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCI+PGVwMTpUZWxlcGhvbmVDb2RlPnByaXZhdDwvZXAxOlRlbGVwaG9uZUNvZGU+PGVwMTpUZWxlcGhvbmVTdWJzY3JpYmVySWRlbnRpZmllcj42MTM5NTk5OTwvZXAxOlRlbGVwaG9uZVN1YnNjcmliZXJJZGVudGlmaWVyPjwvZXAxOkNvZGVkVGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+PGVwMTpDb2RlZFRlbGVwaG9uZVN1YnNjcmliZXJJZGVudGlmaWVyIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIj48ZXAxOlRlbGVwaG9uZUNvZGU+bW9iaWw8L2VwMTpUZWxlcGhvbmVDb2RlPjxlcDE6VGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+NjEzOTU5OTk8L2VwMTpUZWxlcGhvbmVTdWJzY3JpYmVySWRlbnRpZmllcj48L2VwMTpDb2RlZFRlbGVwaG9uZVN1YnNjcmliZXJJZGVudGlmaWVyPjxJbmZvcm1lZFJlbGF0aXZlPjE8L0luZm9ybWVkUmVsYXRpdmU+PC9SZWxhdGl2ZT48Q29tbWVudD5Ew7h0cmUuIERhdHRlciBUaW5hIGthbiBrb250YWt0ZXM6NjEgMzkgNTkgOTk8L0NvbW1lbnQ+PC9SZWxhdGl2ZXM+PGVwMTpBZG1pc3Npb24geG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiPjxlcDE6RGF0ZT4yMDI0LTAxLTE5PC9lcDE6RGF0ZT48ZXAxOlRpbWU+MDQ6MTI8L2VwMTpUaW1lPjwvZXAxOkFkbWlzc2lvbj48ZXAxOkVuZE9mVHJlYXRtZW50IHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIj48ZXAxOkRhdGU+MjAyNC0wMS0yNTwvZXAxOkRhdGU+PGVwMTpUaW1lPjEyOjAwPC9lcDE6VGltZT48L2VwMTpFbmRPZlRyZWF0bWVudD48ZXAxOkRpc2NoYXJnZSB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCI+PGVwMTpEYXRlPjIwMjQtMDEtMjU8L2VwMTpEYXRlPjxlcDE6VGltZT4xMjowMDwvZXAxOlRpbWU+PC9lcDE6RGlzY2hhcmdlPjxlcDE6Q2F1c2VPZkFkbWlzc2lvbiB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCI+S2VuZHQgbWVkIEMubWFtbWEgc2lkZW4gMjAxNywgaHVrb21tZWxzZXNzdsOma2tldCBmYWxkZXQgdWQgYWYgc2VuZ2VuIG9nIHDDpWRyYWdldCBzaWcgZW4gaHVtZXJ1cyBmcmFrdHVyPC9lcDE6Q2F1c2VPZkFkbWlzc2lvbj48ZXAxOkNvdXJzZU9mQWRtaXNzaW9uIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIj5PcGVyZXJldCBmb3IgaHVtZXJ1cyBmcmFrdHVyIDE5LzEuIEluZGzDpmdnZXMgdGlsIHNtZXJ0ZWJlaGFuZGxpbmcgb2cgbW9iaWxpc2VyaW5nLjwvZXAxOkNvdXJzZU9mQWRtaXNzaW9uPjxOdXJzaW5nUHJvYmxlbUFyZWFzPjxOdXJzaW5nUHJvYmxlbUFyZWE+PFRlcm1pbm9sb2d5PjxDb2RlPkEtMTIzNDU8L0NvZGU+PENvZGVTeXN0ZW0+bWVkY29tPC9Db2RlU3lzdGVtPjxDb2RlRGVzY3JpcHRpb24+RnVua3Rpb25zbml2ZWF1IG9nIGJldsOmZ2VhcHBhcmF0PC9Db2RlRGVzY3JpcHRpb24+PC9UZXJtaW5vbG9neT48UHJvYmxlbUFyZWE+SGrDpmxwIHRpbCBwZXJzb25saWcgaHlnaWVqbmU6IE1ha3NpbWFsIGhqw6ZscCA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPkhqw6ZscCB0aWwgYXQgc3Bpc2Ugb2cgZHJpa2tlOiBUb3RhbHQgYWZow6ZuZ2lnIGFmIGhqw6ZscCA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPkhqw6ZscCB0aWwgdmVuZGluZy9sZWpyaW5nOiBIasOmbHAgdGlsIHZlbmRpbmcsIEhqw6ZscCB0aWwgbGVqcmluZyA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPlVkIGZyYSBmdW5rdGlvbnNldm5ldnVyZGVyaW5nZW4gdnVyZGVyZXM6ICBGYWxkcmlzaWtvIDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+U2l0dWF0aW9uZXIgbWVkIMO4Z2V0IGZhbGRyaXNpa286IEkgc3TDpWVuZGUgc3RpbGxpbmcgdWRlbiBzdMO4dHRlPC9Qcm9ibGVtQXJlYT48L051cnNpbmdQcm9ibGVtQXJlYT48TnVyc2luZ1Byb2JsZW1BcmVhPjxUZXJtaW5vbG9neT48Q29kZT5CLTEyMzQ1PC9Db2RlPjxDb2RlU3lzdGVtPm1lZGNvbTwvQ29kZVN5c3RlbT48Q29kZURlc2NyaXB0aW9uPkVybsOmcmluZzwvQ29kZURlc2NyaXB0aW9uPjwvVGVybWlub2xvZ3k+PFByb2JsZW1BcmVhPkFwcGV0aXQ6IE5lZHNhdCBhcHBldGl0IDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+VMO4cnN0OiBOb3JtYWwgdMO4cnN0IDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+S29zdGZvcm06IEtvc3QgdGlsIHNtw6V0c3Bpc2VuZGUvTsOmcmluZ3N0w6Z0IGtvc3QgPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5CZWhvdiBmb3IgbW9kaWZpY2VyZXQga29uc2lzdGVucyBhZiBtYWQvZHJpa2tlIDogTmVqIDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+S29tbWVudGFyIChlcm7DpnJpbmcpOiBOw7hkZXMgZ2VybmUgdGlsIHByb3RlaW5yaWdlIGRyaWtrZS48L1Byb2JsZW1BcmVhPjwvTnVyc2luZ1Byb2JsZW1BcmVhPjxOdXJzaW5nUHJvYmxlbUFyZWE+PFRlcm1pbm9sb2d5PjxDb2RlPkMtMTIzNDU8L0NvZGU+PENvZGVTeXN0ZW0+bWVkY29tPC9Db2RlU3lzdGVtPjxDb2RlRGVzY3JpcHRpb24+SHVkIG9nIHNsaW1oaW5kZXI8L0NvZGVEZXNjcmlwdGlvbj48L1Rlcm1pbm9sb2d5PjxQcm9ibGVtQXJlYT5IdWQ6IFLDuGRtZSA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPlLDuGRtZSwgbG9rYWxpc2F0aW9uOiBuYXRlcyA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPkhqw6ZscCB0aWwgcGVyc29ubGlnIGh5Z2llam5lIChkb2suIGkgQWt0dWVsdCBmdW5rdGlvbnNuaXZlYXUgLSBvYnNlcnZhdGlvbik6IE1ha3NpbWFsIGhqw6ZscCA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPk92ZXJtdW5kOiBIYXIgaGVscHJvdGVzZSA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPlVuZGVybXVuZDogTWFuZ2xlciBlZ25lIHTDpm5kZXIgb2cgdGFuZGVyc3RhdG5pbmdlciA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPkhqw6ZscCB0aWwgbXVuZGh5Z2llam5lOiBQYXRpZW50ZW4gaGFyIGJlaG92IGZvciBoasOmbHAgdGlsIG11bmRoeWdpZWpuZSA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPkzDpmJlciwgc3RhdHVzOiBIZWxlLCBseXNlcsO4ZGUgb2cgZnVndGlnZSA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPlJpc2lrbyBmb3IgdWR2aWtsaW5nIGFmIHRyeWtza2FkZS8tc8Olcj86IEphIDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+SW5zcGVrdGlvbiBmb3IgdHJ5a3PDpXI6IFRyeWtzw6VyIChMREEpIDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+VHJ5a3NrYWRlLCBsb2thbGlzYXRpb246IG5hdGVzIDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+VHJ5a3NrYWRlLCBpbnRlcnZlbnRpb246IGJhcnJpZXJlY3JlbWUgPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5TZW5zb3Jpc2sgcGVyY2VwdGlvbjogTGlkdCBiZWdyw6Zuc2V0IDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+RnVndDogTGVqbGlnaGVkc3ZpcyBmdWd0aWcgPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5Ba3Rpdml0ZXQ6IEJ1bmRldCB0aWwgc2VuZ2VuIDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+TW9iaWxpdGV0OiBNZWdldCBiZWdyw6Zuc2V0IG1vYmlsaXRldCA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPkVybsOmcmluZywgaW5kdGFnZWxzZTogRm9ybW9kZW50bGlnIHV0aWxzdHLDpmtrZWxpZyA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPkduaWRuaW5nIG9nIGZvcnNreWRuaW5nOiBQcm9ibGVtIDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+QnJhZGVuIHRvdGFsIHNjb3JlOiAxMiA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPlRyeWtzw6Vyc3Jpc2lrbzogSMO4aiByaXNpa28gZm9yIGF0IHVkdmlrbGUgdHJ5a3PDpXIgPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5Ucnlrc8OlcnNmb3JlYnlnZ2Vsc2UgLSBhZmxhc3RuaW5nOiBSZXBvc2l0aW9uZXJpbmc8L1Byb2JsZW1BcmVhPjwvTnVyc2luZ1Byb2JsZW1BcmVhPjxOdXJzaW5nUHJvYmxlbUFyZWE+PFRlcm1pbm9sb2d5PjxDb2RlPkQtMTIzNDU8L0NvZGU+PENvZGVTeXN0ZW0+bWVkY29tPC9Db2RlU3lzdGVtPjxDb2RlRGVzY3JpcHRpb24+S29tbXVuaWthdGlvbi9uZXVyb2xvZ2k8L0NvZGVEZXNjcmlwdGlvbj48L1Rlcm1pbm9sb2d5PjxQcm9ibGVtQXJlYT5CZXZpZHN0aGVkc25pdmVhdTogVsOlZ2VuLCByZWFnZXJlciBub3JtYWx0IGVsbGVyIG5vcm1hbCBzw7h2biA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPk5pdmVhdSBhZiBvcmllbnRlcmluZzogT3JpZW50ZXJldCBpIHRpZCwgc3RlZCwgc2l0dWF0aW9uIG9nIGVnbmUgZGF0YSA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPkV2bmUgdGlsIGF0IGZvcnN0w6U6IFJpbmdlIGJldmlkc3RoZWQgb20gZWdlbiBzaWtrZXJoZWQ8L1Byb2JsZW1BcmVhPjwvTnVyc2luZ1Byb2JsZW1BcmVhPjxOdXJzaW5nUHJvYmxlbUFyZWE+PFRlcm1pbm9sb2d5PjxDb2RlPkUtMTIzNDU8L0NvZGU+PENvZGVTeXN0ZW0+bWVkY29tPC9Db2RlU3lzdGVtPjxDb2RlRGVzY3JpcHRpb24+UHN5a29zb2NpYWxlIGZvcmhvbGQ8L0NvZGVEZXNjcmlwdGlvbj48L1Rlcm1pbm9sb2d5PjxQcm9ibGVtQXJlYT5Qc3lraXNrIHRpbHN0YW5kOiBVZG1hdHRldCAoTWVnZXQgdHLDpnQgb2cgaGFyIGhhZnQgc3bDpnJ0IHZlZCBhdCBzYW1hcmJlamRlIHRpbCBvZyBkcmlra2UuIEdpdmV0IG5hbGF4b24gdWRlbiBkZW4gc3RvcmUgZWZmZWt0LiBNYWRzIFJpaXNoZWRlIGhhciBlZnRlcmbDuGxnZW5kZSB0aWxzZXQgRWRpdCBvZyB2dXJkZXJldCBhdCBkZXQgZm9ybWVudGxpZ3QgZXIgZGVuIHN0b3JlIG9wZXJhdGlvbiwgZGVyIGhhciBibG9wcGV0IEVpZGl0IHVkLkTDuHRyZSBoYXIgdsOmcmV0IGRlciBoZWxlIERWKSA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPkhqZW1tZXRzIGluZHJldG5pbmc6IEV0IHBsYW4gPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5Cb3Igc2FtbWVuIG1lZDogQm9yIGFsZW5lIDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+U2FtYXJiZWpkZSBtZWQgcMOlcsO4cmVuZGU6IEFmdGFsdCBhdCBkYXR0ZXIgVGluYSBza2FsIGtvbnRha3RlcyBuw6VyIHB0LiBrw7hyZXIgaGVyZnJhLjwvUHJvYmxlbUFyZWE+PC9OdXJzaW5nUHJvYmxlbUFyZWE+PE51cnNpbmdQcm9ibGVtQXJlYT48VGVybWlub2xvZ3k+PENvZGU+SS0xMjM0NTwvQ29kZT48Q29kZVN5c3RlbT5tZWRjb208L0NvZGVTeXN0ZW0+PENvZGVEZXNjcmlwdGlvbj5Tw7h2biBvZyBodmlsZTwvQ29kZURlc2NyaXB0aW9uPjwvVGVybWlub2xvZ3k+PFByb2JsZW1BcmVhPlPDuHZuIG9nIGh2aWxlIC0gb2JzZXJ2YXRpb246IFNvdmV0IHZlZCB0aWxzeW48L1Byb2JsZW1BcmVhPjwvTnVyc2luZ1Byb2JsZW1BcmVhPjxOdXJzaW5nUHJvYmxlbUFyZWE+PFRlcm1pbm9sb2d5PjxDb2RlPkotMTIzNDU8L0NvZGU+PENvZGVTeXN0ZW0+bWVkY29tPC9Db2RlU3lzdGVtPjxDb2RlRGVzY3JpcHRpb24+VmlkZW4gb2cgdWR2aWtsaW5nPC9Db2RlRGVzY3JpcHRpb24+PC9UZXJtaW5vbG9neT48UHJvYmxlbUFyZWE+S29tbWVudGFyICh2aWRlbiBvZyB1ZHZpa2xpbmcpOiBGb3J0c2F0IG1lZ2V0IHRyw6Z0LCBtZW4ga2FuIGkgdsOlZ2VuIHRpbHN0YW5kIGLDpWRlIGRyaWtrZSBvZyBzcGlzZSBtZWQgaGrDpmxwLjwvUHJvYmxlbUFyZWE+PC9OdXJzaW5nUHJvYmxlbUFyZWE+PE51cnNpbmdQcm9ibGVtQXJlYT48VGVybWlub2xvZ3k+PENvZGU+Sy0xMjM0NTwvQ29kZT48Q29kZVN5c3RlbT5tZWRjb208L0NvZGVTeXN0ZW0+PENvZGVEZXNjcmlwdGlvbj5VZHNraWxsZWxzZSA8L0NvZGVEZXNjcmlwdGlvbj48L1Rlcm1pbm9sb2d5PjxQcm9ibGVtQXJlYT5TZW5lc3RlIGFmZsO4cmluZyB1bmRlciBpbmRsw6ZnZ2Vsc2UgKGRhdG8pOiAyNS0wMS0yNCA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPkFmZsO4cmluZzogSmEsIGFmZsO4cmluZyBpIHZhZ3RlbiA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPkFmZsO4cmluZ3N1ZHNlZW5kZTogQjogQmzDuGQgPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5BZmbDuHJpbmdzZmFydmU6IEJydW4gPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5BZmbDuHJpbmdzbcOmbmdkZTogU3RvciwgQWZmw7hyaW5nIGkgYmxlIDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+VmFuZGxhZG5pbmc6IFNtZXJ0ZXIgdmVkIHZhbmRsYWRuaW5nIDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+VXJpbiA6IEphLCB1cmludWRza2lsbGVsc2UgaSB2YWd0ZW4gPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5VcmludWRzZWVuZGU6IEtvbmNlbnRyZXJldCwgT3JhbmdlIDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+VXJpbm3Dpm5nZGU6IExpbGxlLCBVcmluIGkgYmxlIDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+VmFuZGxhZG5pbmcgLSBpbnRlcnZlbnRpb24gOiBCbMOmcmVzY2FubmluZyA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPkJsw6ZyZXNjYW5uaW5nICA6IEJsw6ZyZXNjYW5uaW5nIGVmdGVyIHZhbmRsYWRuaW5nL1NJSzwvUHJvYmxlbUFyZWE+PC9OdXJzaW5nUHJvYmxlbUFyZWE+PC9OdXJzaW5nUHJvYmxlbUFyZWFzPjxlcDE6Umlza09mQ29udGFnaW9uIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIj5pbmdlbjwvZXAxOlJpc2tPZkNvbnRhZ2lvbj48RGlhZ25vc2VzPjxEaWFnbm9zaXM+PENvZGU+RFM0MjM8L0NvZGU+PFR5cGVDb2RlPlNLU2RpYWdub3Nla29kZTwvVHlwZUNvZGU+PFRleHQ+RnJha3R1ciBhZiBza2FmdGV0IHDDpSBvdmVyYXJtc2tub2dsZTwvVGV4dD48L0RpYWdub3Npcz48L0RpYWdub3Nlcz48QWJpbGl0eVRvRnVuY3Rpb25BdERpc2NoYXJnZT48TGFzdE1vZGlmaWVkRGF0ZT4yMDI0LTAxLTIzPC9MYXN0TW9kaWZpZWREYXRlPjxBYmlsaXR5VG9GdW5jdGlvbkVsZW1lbnRzPjxBYmlsaXR5VG9GdW5jdGlvbkVsZW1lbnQ+PFRlcm1pbm9sb2d5PjxDb2RlPkZBNTEwPC9Db2RlPjxDb2RlU3lzdGVtPmljZjwvQ29kZVN5c3RlbT48Q29kZURlc2NyaXB0aW9uPlZhc2tlIHNpZzwvQ29kZURlc2NyaXB0aW9uPjwvVGVybWlub2xvZ3k+PFNjb3JlPnRvdGFsZTwvU2NvcmU+PENvbW1lbnQ+TGlnZ2VyIG1lZ2V0IHR1bmd0IGkgc2VuZ2VuLCBoYXIgcHLDpmdldCBzaW4gZGVtZW5zLiBIYXIgYmVob3YgZm9yIGhqw6ZscCB0aWwgYWx0PC9Db21tZW50PjwvQWJpbGl0eVRvRnVuY3Rpb25FbGVtZW50PjxBYmlsaXR5VG9GdW5jdGlvbkVsZW1lbnQ+PFRlcm1pbm9sb2d5PjxDb2RlPkZBNTQwPC9Db2RlPjxDb2RlU3lzdGVtPmljZjwvQ29kZVN5c3RlbT48Q29kZURlc2NyaXB0aW9uPkFmLSBvZyBww6VrbMOmZG5pbmc8L0NvZGVEZXNjcmlwdGlvbj48L1Rlcm1pbm9sb2d5PjxTY29yZT5zdmFlcmU8L1Njb3JlPjxDb21tZW50Pk5ZIDIzLzE6IEhqw6ZscCB0aWwgYWYtIG9nIHDDpWtsw6ZkbmluZyBhZiBhbHQgdMO4ai4gRGVzdWRlbiB0aWxzeW4gYWYgaHVkIHVuZGVyIGN1YmlmaXggYmFuZGFnZSBkYSBwdCBoZXIgZXIgZWtzdHJhIHVkc2F0IGZvciB0cnlrPC9Db21tZW50PjwvQWJpbGl0eVRvRnVuY3Rpb25FbGVtZW50PjxBYmlsaXR5VG9GdW5jdGlvbkVsZW1lbnQ+PFRlcm1pbm9sb2d5PjxDb2RlPkZBNTMwPC9Db2RlPjxDb2RlU3lzdGVtPmljZjwvQ29kZVN5c3RlbT48Q29kZURlc2NyaXB0aW9uPkfDpSBww6UgdG9pbGV0PC9Db2RlRGVzY3JpcHRpb24+PC9UZXJtaW5vbG9neT48U2NvcmU+c3ZhZXJlPC9TY29yZT48Q29tbWVudD5OWSAyMy8xOiBLQUQgZXIgc2VwLiBkLmQuIGhqw6ZscCB0aWwgdG9pbGV0YmVzw7hnIHZoYS4gc2FyYSBzdGVhZHkuIEJydWdlciBmb3J0c2F0IGJsZS48L0NvbW1lbnQ+PC9BYmlsaXR5VG9GdW5jdGlvbkVsZW1lbnQ+PEFiaWxpdHlUb0Z1bmN0aW9uRWxlbWVudD48VGVybWlub2xvZ3k+PENvZGU+RkE0MjA8L0NvZGU+PENvZGVTeXN0ZW0+aWNmPC9Db2RlU3lzdGVtPjxDb2RlRGVzY3JpcHRpb24+Rm9yZmx5dHRlIHNpZzwvQ29kZURlc2NyaXB0aW9uPjwvVGVybWlub2xvZ3k+PFNjb3JlPnN2YWVyZTwvU2NvcmU+PENvbW1lbnQ+TlkgMjMvMTogSGrDpmxwIGZyYSAxLTIgcGVyc29uZXIgYWx0IGFmaMOmbmdpZyBhZiBodm9yIHRyw6Z0IHB0IGVyLiBNb2IuIG1lZCBzYXJhIHN0ZWFkeSAtIHNrYWwgZ3VpZGVzIGZ5c2lzayBvZyB2ZXJiYWx0PC9Db21tZW50PjwvQWJpbGl0eVRvRnVuY3Rpb25FbGVtZW50PjxBYmlsaXR5VG9GdW5jdGlvbkVsZW1lbnQ+PFRlcm1pbm9sb2d5PjxDb2RlPkZBNDYwPC9Db2RlPjxDb2RlU3lzdGVtPmljZjwvQ29kZVN5c3RlbT48Q29kZURlc2NyaXB0aW9uPkbDpnJkZW4gaSBmb3Jza2VsbGlnZSBvbWdpdmVsc2VyPC9Db2RlRGVzY3JpcHRpb24+PC9UZXJtaW5vbG9neT48U2NvcmU+c3ZhZXJlPC9TY29yZT48Q29tbWVudD5QdC4ga29tbWVyIGlra2Ugc2VsdnN0w6ZuZGlndCBvbWtyaW5nIHVkZW4gb3BzeW48L0NvbW1lbnQ+PC9BYmlsaXR5VG9GdW5jdGlvbkVsZW1lbnQ+PEFiaWxpdHlUb0Z1bmN0aW9uRWxlbWVudD48VGVybWlub2xvZ3k+PENvZGU+RkE1NjA8L0NvZGU+PENvZGVTeXN0ZW0+aWNmPC9Db2RlU3lzdGVtPjxDb2RlRGVzY3JpcHRpb24+RHJpa2tlPC9Db2RlRGVzY3JpcHRpb24+PC9UZXJtaW5vbG9neT48U2NvcmU+bW9kZXJhdGU8L1Njb3JlPjxDb21tZW50PlNrYWwgaGrDpmxwZXMgbWVkIGF0IGbDpSBkcnVra2V0PC9Db21tZW50PjwvQWJpbGl0eVRvRnVuY3Rpb25FbGVtZW50PjxBYmlsaXR5VG9GdW5jdGlvbkVsZW1lbnQ+PFRlcm1pbm9sb2d5PjxDb2RlPkZBNTUwPC9Db2RlPjxDb2RlU3lzdGVtPmljZjwvQ29kZVN5c3RlbT48Q29kZURlc2NyaXB0aW9uPlNwaXNlPC9Db2RlRGVzY3JpcHRpb24+PC9UZXJtaW5vbG9neT48U2NvcmU+bW9kZXJhdGU8L1Njb3JlPjxDb21tZW50PkRhdHRlciBib3IgaG9zIEVkaXQsIGh1biBoasOmbHBlciBpIHNwaXNlc2l0dWF0aW9uZXI8L0NvbW1lbnQ+PC9BYmlsaXR5VG9GdW5jdGlvbkVsZW1lbnQ+PC9BYmlsaXR5VG9GdW5jdGlvbkVsZW1lbnRzPjwvQWJpbGl0eVRvRnVuY3Rpb25BdERpc2NoYXJnZT48RGlzY2hhcmdlUmVsYXRlZE1lZGljaW5lSW5mb3JtYXRpb24+PFJlY2VpcHQ+MTwvUmVjZWlwdD48UGlja1VwT3JEZWxpdmVyeT4wPC9QaWNrVXBPckRlbGl2ZXJ5PjxEb3NhZ2VFeGVtcHRpb25SZW9yZGVyZWQ+MDwvRG9zYWdlRXhlbXB0aW9uUmVvcmRlcmVkPjwvRGlzY2hhcmdlUmVsYXRlZE1lZGljaW5lSW5mb3JtYXRpb24+PERpZXRGaXJzdDI0SG91cnM+PEx1bmNoQm94PjA8L0x1bmNoQm94PjxTaG9wcGluZ0F0RGlzY2hhcmdlPjA8L1Nob3BwaW5nQXREaXNjaGFyZ2U+PC9EaWV0Rmlyc3QyNEhvdXJzPjxlcDE6RnV0dXJlUGxhbnMgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiPkhvc3BpdGFsZXQgaGFyIGJlaGFuZGxpbmdzYW5zdmFyIGZvciBwYXRpZW50ZW4gaW5kdGlsIDI4LTAxLTIwMjQgMTM6MDQuIEtvbnRha3R0ZWxlZm9ubnVtbWVyICh0aWwgYnJ1ZyBmb3Igc3VuZGhlZHNwcm9mZXNzaW9uZWxsZSk6IDU4IDU1IDk3IDk5LiBGcmVtdGlkaWdlIGFmdGFsZXI6IEFtYi4ga29udHJvbCAyLzIgaHZvciBwdC4gYsOlZGUgc2thbCBoYXZlIHJndC4ga29udHJvbCwgYmxvZHByw7h2ZXIgc2FtdCBzdGlsbGluZ3RhZ2VuIHRpbCBhZ3JhZmZqZXJuZWxzZS4uICBCZWhhbmRsaW5nc25pdmVhdTogQmVncsOmbnNldCBiZWhhbmRsaW5nLCBpbmdlbiBnZW5vcGxpdm5pbmcuIE1hZHMgUmlpc2hlZGUgSGFuc2VuLCBLaXJ1cmcgZC4gMjEtMDEtMjAyNCAxMjowMi4gRnJhdmFsZyBhZiBnZW5vcGxpdm5pbmc6IEluZ2VuIGzDpmdlbGlnIGluZGlrYXRpb24gZm9yIGhqZXJ0ZS9sdW5nZXJlZG5pbmcgIC4gTMOmZ2VsaWcgw6Vyc2FnIHRpbCBiZXNsdXRuaW5nOiBWdXJkZXJpbmdlbiBmb3JldGFnZXQgdWRmcmEgcGF0aWVudGVucyBzYW1sZWRlIHNpdHVhdGlvbiAoYWV0YXMsIGNvbW9yYmlkaXRldCBldGMuKSwgUGF0aWVudGVuIGVyIHVhZnZlbmRlbGlndCBkw7hlbmRlICAuIEJlc2x1dG5pbmcgdGFnZXQgb206IEluZ2VuIGludHViYXRpb24sIEluZ2VuIGluZGzDpmdnZWxzZSBww6UgaW50ZW5zaXYgICAgICAgICAgIC4gVnVyZGVyaW5nIGFmIHBhdGllbnRlbnMgYWt0dWVsbGUgdGlsc3RhbmQgZGVyIGRhbm5lciBncnVuZGxhZyBmb3Igb3ZlbnN0w6VlbmRlIGJlc2x1dG5pbmdlcjogIDk2LcOlcmlnLCBjYW5jZXItICBWZWQgdsOmc2VudGxpZyDDpm5kcmluZyBpIGRlbiBha3R1ZWxsZSB0aWxzdGFuZCB2aWwgYmVzbHV0bmluZ2VuIGJsaXZlIHJldnVyZGVyZXQuIERlciBlciBhZmhvbGR0IHNhbXRhbGUgaGVyb20gbWVkIHBhdGllbnRlbiBvZyBww6Vyw7hyZW5kZSBvZyBnaXZldCBmw7hsZ2VuZGUgaW5mb3JtYXRpb246ICBEZXIgZXIgdmVkIHNhbXRhbGUgZW5pZ2hlZCBvbWtyaW5nIGJlc2x1dG5pbmcuIEhhciBiZXNsdXR0ZXQgZGV0dGUgdmVkICAgICAgICAgICAgICAgICA8L2VwMTpGdXR1cmVQbGFucz48L1JlcG9ydE9mRGlzY2hhcmdlPjwvRW1lc3NhZ2U+\",\"externalId\":null,\"ccRecipient\":null,\"copiedMessageId\":null,\"letterType\":null,\"serviceTagCode\":null,\"messageType\":\"MEDCOM_MESSAGE_REPORT_OF_DISCHARGE_1_0_3\",\"versionCode\":\"XD1834C\",\"conversationId\":\"00000000012E79A00F6CC3229E062D82\",\"activityIdentifier\":{\"identifier\":\"MESSAGE:368786\",\"type\":\"MESSAGE\",\"activityId\":368786,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"patient\":{\"id\":3718,\"version\":20,\"firstName\":\"Edit Johanne\",\"lastName\":\"Larsen\",\"middleName\":null,\"fullName\":\"Edit Johanne Larsen\",\"fullReversedName\":\"Larsen, Edit Johanne\",\"homePhoneNumber\":\"057640093\",\"mobilePhoneNumber\":null,\"workPhoneNumber\":null,\"patientIdentifier\":{\"type\":\"cpr\",\"identifier\":\"190927-0770\",\"managedExternally\":false,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"currentAddress\":{\"addressLine1\":\"Farendløsevej 83\",\"addressLine2\":null,\"addressLine3\":\"Farendløse\",\"addressLine4\":null,\"addressLine5\":null,\"administrativeAreaCode\":\"329\",\"countryCode\":\"dk\",\"postalCode\":\"4100\",\"postalDistrict\":\"Ringsted\",\"restricted\":false,\"geoCoordinates\":{\"x\":null,\"y\":null,\"present\":false},\"startDate\":null,\"endDate\":null,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/address/countries\"},\"searchPostalDistrict\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/address/postalDistrict\"}}},\"currentAddressIndicator\":\"PRIMARY_ADDRESS\",\"age\":96,\"monthsAfterBirthday\":4,\"gender\":\"FEMALE\",\"patientStatus\":\"FINAL\",\"patientState\":{\"id\":21,\"version\":0,\"name\":\"Aktiv\",\"color\":\"000000\",\"type\":{\"id\":\"ACTIVE\",\"name\":\"Aktiv\",\"abbreviation\":\"\",\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"active\":true,\"defaultObject\":true,\"citizenRegistryConfiguration\":{\"updateEnabled\":true,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"_links\":{\"medcomRecipient\":null,\"self\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patientStates/21\"},\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"telephonesRestricted\":false,\"imageId\":null,\"_links\":{\"medcomRecipient\":null,\"self\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3718\"},\"patientOverview\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3718/overview\"},\"citizenOverviewForms\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3718/citizenOverviewForms\"},\"measurementData\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3718/measurementInstructions/data/pages\"},\"measurementInstructions\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3718/measurementInstructions/instructions\"},\"patientConditions\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3718/conditions\"},\"patientOrganizations\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3718/organizations\"},\"activityLinksPrototypes\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3718/activitylinks/prototypes\"},\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"pathwayAssociation\":{\"placement\":{\"name\":\"MedCom Myndighedsenheden\",\"programPathwayId\":4355,\"pathwayTypeId\":13,\"parentPathwayId\":null,\"patientPathwayId\":33558,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"parentReferenceId\":null,\"referenceId\":1036805,\"canAssociateWithPathway\":true,\"canAssociateWithPatient\":false,\"associatedWithPatient\":false,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3718/pathways/33558/availablePathwayAssociations\"},\"availableRootProgramPathways\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3718/availableProgramPathways\"},\"availablePathwayPlacements\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3718/pathways/availablePathwayPlacements\"},\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"body\":null,\"priority\":null,\"previousMessageId\":null,\"_links\":{\"medcomRecipient\":null,\"self\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/368786\"},\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/audit/medcom/368786\"},\"relatedCommunication\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/related/368786\"},\"activeAssignments\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/assignments//mobile/ringsted/v2/assignments/patient/3718/assignments/active?activity=MESSAGE:368786\"},\"assignments\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/assignments//mobile/ringsted/v2/assignments/patient/3718/assignments?activity=MESSAGE:368786\"},\"availableAssignmentTypes\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/assignments//mobile/ringsted/v2/assignments/patient/3718/assignmentTypes/active?activity=MESSAGE:368786&activityType=MESSAGE&messageTypeType=MEDCOM&messageType=MEDCOM_MESSAGE_REPORT_OF_DISCHARGE_1_0_3&messageStateDirection=INCOMING\"},\"autoAssignmentsPrototype\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/assignments//mobile/ringsted/v2/assignments/auto/prototype?activity=MESSAGE:368786&activityType=MESSAGE&messageTypeType=MEDCOM&messageType=MEDCOM_MESSAGE_REPORT_OF_DISCHARGE_1_0_3&messageStateDirection=INCOMING&messageStatus=FINAL\"},\"update\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/368786\"},\"transformedBody\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/368786/transformedBody\"},\"print\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/368786/print\"},\"reply\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/368786/reply\"},\"replyTo\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/replyTo/368786?identifierType=SYGEHUSAFDELINGSNUMMER&identifierValue=3800S30\"},\"archive\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/archived/368786\"},\"availableCountries\":null,\"searchPostalDistrict\":null},\"level\":null}\r\n");
            baseObjects.Add("{\"type\":\"medcom\",\"id\":368661,\"version\":3,\"sender\":{\"type\":\"medcomMessageDetailsBased\",\"id\":368661,\"name\":\"Region Sjællands Sygehusvæsen - Fælledvej 13 - 4200 Slagelse - EAN: 5790001360528\",\"recipientAddress\":null,\"recipientType\":\"MESSAGE_SENDER\",\"_links\":{\"medcomRecipient\":{\"href\":\"/api/core/mobile/ringsted/v2/medcomRecipients/medcomSender/368661\"},\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"recipient\":{\"type\":\"medcomMessageDetailsBased\",\"id\":368661,\"name\":\"Ringsted Kommune - Zahlesvej 18 - 4100 Ringsted - EAN: 5790001353858\",\"recipientAddress\":null,\"recipientType\":\"MESSAGE_RECIPIENT\",\"_links\":{\"medcomRecipient\":{\"href\":\"/api/core/mobile/ringsted/v2/medcomRecipients/medcomRecipient/368661\"},\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"subject\":\"Udskrivningsrapport\",\"date\":\"2024-01-25T08:10:00+01:00\",\"state\":{\"direction\":\"INCOMING\",\"status\":\"FINAL\",\"response\":\"NOT_AWAITING\",\"handshake\":\"NO\",\"processed\":\"PROCESSED\",\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"customer\":null,\"raw\":\"PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiPz48RW1lc3NhZ2UgeG1sbnM9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMyI+PGVwMTpFbnZlbG9wZSB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCI+PGVwMTpTZW50PjxlcDE6RGF0ZT4yMDI0LTAxLTI1PC9lcDE6RGF0ZT48ZXAxOlRpbWU+MDg6MDY8L2VwMTpUaW1lPjwvZXAxOlNlbnQ+PGVwMTpJZGVudGlmaWVyPkVQSUM2NDE3MTE2Njg8L2VwMTpJZGVudGlmaWVyPjxlcDE6QWNrbm93bGVkZ2VtZW50Q29kZT5wbHVzcG9zaXRpdmt2aXR0PC9lcDE6QWNrbm93bGVkZ2VtZW50Q29kZT48L2VwMTpFbnZlbG9wZT48UmVwb3J0T2ZEaXNjaGFyZ2U+PExldHRlcj48SWRlbnRpZmllcj5FUElDNjQxNzExNjY4PC9JZGVudGlmaWVyPjxWZXJzaW9uQ29kZT5YRDE4MzRDPC9WZXJzaW9uQ29kZT48U3RhdGlzdGljYWxDb2RlPlhESVMxODwvU3RhdGlzdGljYWxDb2RlPjxBdXRob3Jpc2F0aW9uPjxlcDE6RGF0ZSB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCI+MjAyNC0wMS0yNTwvZXAxOkRhdGU+PGVwMTpUaW1lIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIj4wODowNjwvZXAxOlRpbWU+PC9BdXRob3Jpc2F0aW9uPjxUeXBlQ29kZT5YRElTMTg8L1R5cGVDb2RlPjxFcGlzb2RlT2ZDYXJlSWRlbnRpZmllcj4wMDAwMDAwMDAwN0VCNEQwRkNDRkVDQkJGNjQ2RTcyNTwvRXBpc29kZU9mQ2FyZUlkZW50aWZpZXI+PC9MZXR0ZXI+PGVwMTpTZW5kZXIgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiPjxlcDE6RUFOSWRlbnRpZmllcj41NzkwMDAxMzYwNTI4PC9lcDE6RUFOSWRlbnRpZmllcj48ZXAxOklkZW50aWZpZXI+MzgwMFI0MDwvZXAxOklkZW50aWZpZXI+PGVwMTpJZGVudGlmaWVyQ29kZT5zeWdlaHVzYWZkZWxpbmdzbnVtbWVyPC9lcDE6SWRlbnRpZmllckNvZGU+PGVwMTpPcmdhbmlzYXRpb25OYW1lPlJlZ2lvbiBTasOmbGxhbmRzIFN5Z2VodXN2w6ZzZW48L2VwMTpPcmdhbmlzYXRpb25OYW1lPjxlcDE6RGVwYXJ0bWVudE5hbWU+U0xBIEdlcmlhdHJpc2sgQWZkLjwvZXAxOkRlcGFydG1lbnROYW1lPjxlcDE6VW5pdE5hbWU+QWZkZWxpbmdlbiBmb3Igw4ZsZHJlc3lnZG9tbWUgRzE8L2VwMTpVbml0TmFtZT48ZXAxOlN0cmVldE5hbWU+RsOmbGxlZHZlaiAxMzwvZXAxOlN0cmVldE5hbWU+PGVwMTpEaXN0cmljdE5hbWU+U2xhZ2Vsc2U8L2VwMTpEaXN0cmljdE5hbWU+PGVwMTpQb3N0Q29kZUlkZW50aWZpZXI+NDIwMDwvZXAxOlBvc3RDb2RlSWRlbnRpZmllcj48ZXAxOlRlbGVwaG9uZVN1YnNjcmliZXJJZGVudGlmaWVyPjU4NTU5NjcwPC9lcDE6VGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+PGVwMTpTaWduZWRCeT48ZXAxOklkZW50aWZpZXI+MzgwMFI0MDwvZXAxOklkZW50aWZpZXI+PGVwMTpJZGVudGlmaWVyQ29kZT5zeWdlaHVzYWZkZWxpbmdzbnVtbWVyPC9lcDE6SWRlbnRpZmllckNvZGU+PGVwMTpQZXJzb25HaXZlbk5hbWU+QmlydGhlPC9lcDE6UGVyc29uR2l2ZW5OYW1lPjxlcDE6UGVyc29uU3VybmFtZU5hbWU+SGFuc2VuPC9lcDE6UGVyc29uU3VybmFtZU5hbWU+PGVwMTpQZXJzb25UaXRsZT5TeWdlcGxlamVyc2tlPC9lcDE6UGVyc29uVGl0bGU+PC9lcDE6U2lnbmVkQnk+PGVwMTpNZWRpY2FsU3BlY2lhbGl0eUNvZGU+Z2VyaWF0cmk8L2VwMTpNZWRpY2FsU3BlY2lhbGl0eUNvZGU+PGVwMTpDb250YWN0SW5mb3JtYXRpb24+PGVwMTpDb250YWN0TmFtZT7DhmxkcmVzeWdkb21tZSAxPC9lcDE6Q29udGFjdE5hbWU+PGVwMTpDb250YWN0TmFtZT5TcGwgTWV0dGUgUGlpbDwvZXAxOkNvbnRhY3ROYW1lPjxlcDE6VGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+NTg1NTk2NzA8L2VwMTpUZWxlcGhvbmVTdWJzY3JpYmVySWRlbnRpZmllcj48L2VwMTpDb250YWN0SW5mb3JtYXRpb24+PC9lcDE6U2VuZGVyPjxlcDE6UmVjZWl2ZXIgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiPjxlcDE6RUFOSWRlbnRpZmllcj41NzkwMDAxMzUzODU4PC9lcDE6RUFOSWRlbnRpZmllcj48ZXAxOklkZW50aWZpZXI+MzI5PC9lcDE6SWRlbnRpZmllcj48ZXAxOklkZW50aWZpZXJDb2RlPmtvbW11bmVudW1tZXI8L2VwMTpJZGVudGlmaWVyQ29kZT48ZXAxOk9yZ2FuaXNhdGlvbk5hbWU+UmluZ3N0ZWQgS29tbXVuZTwvZXAxOk9yZ2FuaXNhdGlvbk5hbWU+PGVwMTpEZXBhcnRtZW50TmFtZT5Tb2NpYWwgb2cgU3VuZGhlZDwvZXAxOkRlcGFydG1lbnROYW1lPjxlcDE6VW5pdE5hbWU+SGplbW1lc3lnZXBsZWplbiwgUmluZ3N0ZWQgS29tbXVuZTwvZXAxOlVuaXROYW1lPjxlcDE6U3RyZWV0TmFtZT5aYWhsZXN2ZWogMTg8L2VwMTpTdHJlZXROYW1lPjxlcDE6RGlzdHJpY3ROYW1lPlJpbmdzdGVkPC9lcDE6RGlzdHJpY3ROYW1lPjxlcDE6UG9zdENvZGVJZGVudGlmaWVyPjQxMDA8L2VwMTpQb3N0Q29kZUlkZW50aWZpZXI+PC9lcDE6UmVjZWl2ZXI+PGVwMTpQcmFjdGl0aW9uZXIgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiPjxlcDE6SWRlbnRpZmllcj4wMjY0MjU8L2VwMTpJZGVudGlmaWVyPjxlcDE6SWRlbnRpZmllckNvZGU+eWRlcm51bW1lcjwvZXAxOklkZW50aWZpZXJDb2RlPjxlcDE6UGVyc29uTmFtZT5Mw6ZnZXJuZSBOw7hycmV0b3J2PC9lcDE6UGVyc29uTmFtZT48ZXAxOlRlbGVwaG9uZVN1YnNjcmliZXJJZGVudGlmaWVyPjU3NjEwMTY5PC9lcDE6VGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+PC9lcDE6UHJhY3RpdGlvbmVyPjxlcDE6UGF0aWVudCB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCI+PGVwMTpDaXZpbFJlZ2lzdHJhdGlvbk51bWJlcj4xNjA1MzcyMDU0PC9lcDE6Q2l2aWxSZWdpc3RyYXRpb25OdW1iZXI+PGVwMTpQZXJzb25TdXJuYW1lTmFtZT5HYWxhdml0czwvZXAxOlBlcnNvblN1cm5hbWVOYW1lPjxlcDE6UGVyc29uR2l2ZW5OYW1lPkV2YTwvZXAxOlBlcnNvbkdpdmVuTmFtZT48ZXAxOlN0cmVldE5hbWU+SHlsZGVnw6VyZHN2ZWogMTEgRDwvZXAxOlN0cmVldE5hbWU+PGVwMTpEaXN0cmljdE5hbWU+UmluZ3N0ZWQ8L2VwMTpEaXN0cmljdE5hbWU+PGVwMTpQb3N0Q29kZUlkZW50aWZpZXI+NDEwMDwvZXAxOlBvc3RDb2RlSWRlbnRpZmllcj48ZXAxOk9jY3VwYXRpb24+TMOmcmVyPC9lcDE6T2NjdXBhdGlvbj48ZXAxOlRlbGVwaG9uZVN1YnNjcmliZXJJZGVudGlmaWVyPjU3NjEzOTM4PC9lcDE6VGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+PC9lcDE6UGF0aWVudD48UmVsYXRpdmVzPjxSZWxhdGl2ZT48UmVsYXRpb25Db2RlPmJhcm48L1JlbGF0aW9uQ29kZT48ZXAxOlBlcnNvblN1cm5hbWVOYW1lIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIj5WaWN0b3I8L2VwMTpQZXJzb25TdXJuYW1lTmFtZT48ZXAxOlBlcnNvbkdpdmVuTmFtZSB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCI+R2FsYXZhdGlzPC9lcDE6UGVyc29uR2l2ZW5OYW1lPjxlcDE6Q29kZWRUZWxlcGhvbmVTdWJzY3JpYmVySWRlbnRpZmllciB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCI+PGVwMTpUZWxlcGhvbmVDb2RlPm1vYmlsPC9lcDE6VGVsZXBob25lQ29kZT48ZXAxOlRlbGVwaG9uZVN1YnNjcmliZXJJZGVudGlmaWVyPjYwNzIxMDgzPC9lcDE6VGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+PC9lcDE6Q29kZWRUZWxlcGhvbmVTdWJzY3JpYmVySWRlbnRpZmllcj48SW5mb3JtZWRSZWxhdGl2ZT4wPC9JbmZvcm1lZFJlbGF0aXZlPjwvUmVsYXRpdmU+PFJlbGF0aXZlPjxSZWxhdGlvbkNvZGU+dXNwZWNfcGFhcm9lcmVuZGU8L1JlbGF0aW9uQ29kZT48ZXAxOlBlcnNvblN1cm5hbWVOYW1lIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIj5Nw7hsbGVyPC9lcDE6UGVyc29uU3VybmFtZU5hbWU+PGVwMTpQZXJzb25HaXZlbk5hbWUgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiPkNocmlzdGluZTwvZXAxOlBlcnNvbkdpdmVuTmFtZT48ZXAxOkNvZGVkVGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXIgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiPjxlcDE6VGVsZXBob25lQ29kZT5tb2JpbDwvZXAxOlRlbGVwaG9uZUNvZGU+PGVwMTpUZWxlcGhvbmVTdWJzY3JpYmVySWRlbnRpZmllcj4yMTc0NzAyMDwvZXAxOlRlbGVwaG9uZVN1YnNjcmliZXJJZGVudGlmaWVyPjwvZXAxOkNvZGVkVGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+PEluZm9ybWVkUmVsYXRpdmU+MDwvSW5mb3JtZWRSZWxhdGl2ZT48L1JlbGF0aXZlPjxDb21tZW50PlB0IGluZm9ybWVyZSBzZWx2LjwvQ29tbWVudD48L1JlbGF0aXZlcz48ZXAxOkFkbWlzc2lvbiB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCI+PGVwMTpEYXRlPjIwMjQtMDEtMTY8L2VwMTpEYXRlPjxlcDE6VGltZT4yMzoxNjwvZXAxOlRpbWU+PC9lcDE6QWRtaXNzaW9uPjxlcDE6RW5kT2ZUcmVhdG1lbnQgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiPjxlcDE6RGF0ZT4yMDI0LTAxLTI1PC9lcDE6RGF0ZT48ZXAxOlRpbWU+MTg6MDA8L2VwMTpUaW1lPjwvZXAxOkVuZE9mVHJlYXRtZW50PjxlcDE6RGlzY2hhcmdlIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIj48ZXAxOkRhdGU+MjAyNC0wMS0yNTwvZXAxOkRhdGU+PGVwMTpUaW1lPjE4OjAwPC9lcDE6VGltZT48L2VwMTpEaXNjaGFyZ2U+PGVwMTpDYXVzZU9mQWRtaXNzaW9uIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIj5JbmRsw6ZnZ2VzIGdydW5kZXQgZmFsZCBvZyBww6VkcmFnZXQgc2lnIGhvZnRlbsOmciBmcmFrdHVyLjwvZXAxOkNhdXNlT2ZBZG1pc3Npb24+PGVwMTpDb3Vyc2VPZkFkbWlzc2lvbiB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCI+R2Vub3B0csOmbmluZyBlZnRlciBob2Z0ZW7DpnIgZnJha3R1ciwgc21lcnRlYmVoYW5kbGluZywgbW9iaWxpc2VyaW5nIG9nIHNvY2lhbCBwbGFubMOmZ25pbmcuPC9lcDE6Q291cnNlT2ZBZG1pc3Npb24+PE51cnNpbmdQcm9ibGVtQXJlYXM+PE51cnNpbmdQcm9ibGVtQXJlYT48VGVybWlub2xvZ3k+PENvZGU+QS0xMjM0NTwvQ29kZT48Q29kZVN5c3RlbT5tZWRjb208L0NvZGVTeXN0ZW0+PENvZGVEZXNjcmlwdGlvbj5GdW5rdGlvbnNuaXZlYXUgb2cgYmV2w6ZnZWFwcGFyYXQ8L0NvZGVEZXNjcmlwdGlvbj48L1Rlcm1pbm9sb2d5PjxQcm9ibGVtQXJlYT5IasOmbHAgdGlsIHBlcnNvbmxpZyBoeWdpZWpuZTogTGlkdCBoasOmbHAgPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5IasOmbHAgdmVkcsO4cmVuZGUgdG9pbGV0YmVzw7hnOiBBbmRldCAoS0FEIGFubGFndCBoZXIsIEtlbmR0IG1lZCBzdG9taSkgPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5IasOmbHAgdGlsIGF0IHNwaXNlIG9nIGRyaWtrZTogU2VsdmhqdWxwZW4gPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5IasOmbHAgdGlsIG1vYmlsaXNlcmluZzogQmVob3YgZm9yIGxldCBoasOmbHAgPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5Nb2JpbGlzZXJpbmdzcmVzdHJpa3Rpb25lcjogSW5nZW4gcmVzdHJpa3Rpb25lciA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPkhqw6ZscGVtaWRsZXIgdGlsIG1vYmlsaXNlcmluZzogUm9sbGF0b3IgPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5Nb2JpbGlzZXJpbmc6IEfDpXIgcMOlIHN0dWVuLCBHw6VyIHDDpSBnYW5nZW4sIEfDpXIgcMOlIHRvaWxldHRldCwgU2VuZ2VsaWdnZW5kZSA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPlVkIGZyYSBmdW5rdGlvbnNldm5ldnVyZGVyaW5nZW4gdnVyZGVyZXM6IEluZ2VuIGZhbGRyaXNpa28gPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5TaXR1YXRpb25lciBtZWQgw7hnZXQgZmFsZHJpc2lrbzogR8OlZW5kZSB1ZGVuIGdhbmdoasOmbHBlbWlkZGVsLCBHw6VlbmRlIHVkZW4gZsO4bGdlL3N0w7h0dGU8L1Byb2JsZW1BcmVhPjwvTnVyc2luZ1Byb2JsZW1BcmVhPjxOdXJzaW5nUHJvYmxlbUFyZWE+PFRlcm1pbm9sb2d5PjxDb2RlPkItMTIzNDU8L0NvZGU+PENvZGVTeXN0ZW0+bWVkY29tPC9Db2RlU3lzdGVtPjxDb2RlRGVzY3JpcHRpb24+RXJuw6ZyaW5nPC9Db2RlRGVzY3JpcHRpb24+PC9UZXJtaW5vbG9neT48UHJvYmxlbUFyZWE+SMO4amRlOiAxNjAgY20gPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5Ww6ZndDogNjMsMSBrZyA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPkJNSSAoQmVyZWduZXQpOiAyNCw2IDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+QXBwZXRpdDogTmVkc2F0IGFwcGV0aXQgKDEvNCB2YXJtIHJldCDCvSBmb3JtYWdlKSA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPlTDuHJzdDogTm9ybWFsIHTDuHJzdCA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPkt2YWxtZSBncmFkL2h5cHBpZ2hlZDogSW5nZW4gPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5Lb3N0Zm9ybTogTm9ybWFsa29zdC9IdmVyZGFnc2tvc3QgPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5CZWhvdiBmb3IgbW9kaWZpY2VyZXQga29uc2lzdGVucyBhZiBtYWQvZHJpa2tlIDogTmVqIDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+R8OmbGRlbmRlIGVuZXJnaWJlaG92IGtjYWwvZMO4Z246IDE3MDQga2NhbCA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPkfDpmxkZW5kZSBwcm90ZWluYmVob3YgZy9kw7hnbjogODIgZzwvUHJvYmxlbUFyZWE+PC9OdXJzaW5nUHJvYmxlbUFyZWE+PE51cnNpbmdQcm9ibGVtQXJlYT48VGVybWlub2xvZ3k+PENvZGU+Qy0xMjM0NTwvQ29kZT48Q29kZVN5c3RlbT5tZWRjb208L0NvZGVTeXN0ZW0+PENvZGVEZXNjcmlwdGlvbj5IdWQgb2cgc2xpbWhpbmRlcjwvQ29kZURlc2NyaXB0aW9uPjwvVGVybWlub2xvZ3k+PFByb2JsZW1BcmVhPkhqw6ZscCB0aWwgcGVyc29ubGlnIGh5Z2llam5lIChkb2suIGkgQWt0dWVsdCBmdW5rdGlvbnNuaXZlYXUgLSBvYnNlcnZhdGlvbik6IExpZHQgaGrDpmxwIDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+SGrDpmxwIHRpbCBtdW5kaHlnaWVqbmU6IFBhdGllbnRlbiB2YXJldGFnZXIgc2VsdiBtdW5kaHlnaWVqbmUgPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5SaXNpa28gZm9yIHVkdmlrbGluZyBhZiB0cnlrc2thZGUvLXPDpXI/OiBKYSA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPkluc3Bla3Rpb24gZm9yIHRyeWtzw6VyOiBJbmdlbiB0cnlrc2thZGUvdHJ5a3PDpXIgPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5TZW5zb3Jpc2sgcGVyY2VwdGlvbjogSW5nZW4gYmVncsOmbnNuaW5nZXIgPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5GdWd0OiBTasOmbGRlbnQgZnVndGlnIDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+QWt0aXZpdGV0OiBHw6VyIGxlamxpZ2hlZHN2aXN0IDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+TW9iaWxpdGV0OiBMZXQgYmVncsOmbnNldCBtb2JpbGl0ZXQgPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5Fcm7DpnJpbmcsIGluZHRhZ2Vsc2U6IEZvcm1vZGVudGxpZyB1dGlsc3Ryw6Zra2VsaWcgPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5HbmlkbmluZyBvZyBmb3Jza3lkbmluZzogUG90ZW50aWVsdCBwcm9ibGVtIDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+QnJhZGVuIHRvdGFsIHNjb3JlOiAxOCA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPlRyeWtzw6Vyc3Jpc2lrbzogTGF2IHJpc2lrbyBmb3IgYXQgdWR2aWtsZSB0cnlrc8OlciA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPlRyeWtzw6Vyc2ZvcmVieWdnZWxzZSAtIG1hZHJhczogU2t1bW1hZHJhcyA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPlRyeWtzw6Vyc2ZvcmVieWdnZWxzZSAtIGFmbGFzdG5pbmc6IElra2UgYmVob3YgZm9yIHRyeWthZmxhc3RuaW5nIDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+VHJ5a3PDpXJzZm9yZWJ5Z2dlbHNlIC0gbWVkaWNpbnNrIHVkc3R5cjogSWtrZSBiZWhvdiBmb3IgdHJ5a2FmbGFzdG5pbmcgPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5Tw6VyOiB0aWxzeW4gYWYgY2ljYXRyaWNlIHggMiB1Z2VudGxpZyBhZ2dyYWZmamVybmVsc2UgZCAxLzIgYWdncmFmdGFuZyBtZWRnaXZlczwvUHJvYmxlbUFyZWE+PC9OdXJzaW5nUHJvYmxlbUFyZWE+PE51cnNpbmdQcm9ibGVtQXJlYT48VGVybWlub2xvZ3k+PENvZGU+RC0xMjM0NTwvQ29kZT48Q29kZVN5c3RlbT5tZWRjb208L0NvZGVTeXN0ZW0+PENvZGVEZXNjcmlwdGlvbj5Lb21tdW5pa2F0aW9uL25ldXJvbG9naTwvQ29kZURlc2NyaXB0aW9uPjwvVGVybWlub2xvZ3k+PFByb2JsZW1BcmVhPkJldmlkc3RoZWRzbml2ZWF1OiBWw6VnZW4sIHJlYWdlcmVyIG5vcm1hbHQgZWxsZXIgbm9ybWFsIHPDuHZuIDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+Tml2ZWF1IGFmIG9yaWVudGVyaW5nOiBPcmllbnRlcmV0IGkgdGlkLCBzdGVkLCBzaXR1YXRpb24gb2cgZWduZSBkYXRhPC9Qcm9ibGVtQXJlYT48L051cnNpbmdQcm9ibGVtQXJlYT48TnVyc2luZ1Byb2JsZW1BcmVhPjxUZXJtaW5vbG9neT48Q29kZT5FLTEyMzQ1PC9Db2RlPjxDb2RlU3lzdGVtPm1lZGNvbTwvQ29kZVN5c3RlbT48Q29kZURlc2NyaXB0aW9uPlBzeWtvc29jaWFsZSBmb3Job2xkPC9Db2RlRGVzY3JpcHRpb24+PC9UZXJtaW5vbG9neT48UHJvYmxlbUFyZWE+Qm9saWc6IExlamxpZ2hlZCA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPkhqZW1tZXRzIGluZHJldG5pbmc6IEV0IHBsYW4gPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5Cb3Igc2FtbWVuIG1lZDogQm9yIGFsZW5lPC9Qcm9ibGVtQXJlYT48L051cnNpbmdQcm9ibGVtQXJlYT48TnVyc2luZ1Byb2JsZW1BcmVhPjxUZXJtaW5vbG9neT48Q29kZT5GLTEyMzQ1PC9Db2RlPjxDb2RlU3lzdGVtPm1lZGNvbTwvQ29kZVN5c3RlbT48Q29kZURlc2NyaXB0aW9uPlJlc3BpcmF0aW9uIG9nIGNpcmt1bGF0aW9uPC9Db2RlRGVzY3JpcHRpb24+PC9UZXJtaW5vbG9neT48UHJvYmxlbUFyZWE+SGplbW1lYmVoYW5kbGluZyAocmVzcGlyYXRpb24pOiBJbmdlbiByZXNwaXJhdG9yaXNrIGhqZW1tZWJlaGFuZGxpbmcgPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5SZXNwaXJhdGlvbnNtw7huc3Rlci9keWJkZTogTm9ybWFsIDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+UmVzcGlyYXRpb25zbHlkZTogTm9ybWFsPC9Qcm9ibGVtQXJlYT48L051cnNpbmdQcm9ibGVtQXJlYT48TnVyc2luZ1Byb2JsZW1BcmVhPjxUZXJtaW5vbG9neT48Q29kZT5HLTEyMzQ1PC9Db2RlPjxDb2RlU3lzdGVtPm1lZGNvbTwvQ29kZVN5c3RlbT48Q29kZURlc2NyaXB0aW9uPlNla3N1YWxpdGV0LCBrw7huIG9nIGtyb3Bzb3BmYXR0ZWxzZTwvQ29kZURlc2NyaXB0aW9uPjwvVGVybWlub2xvZ3k+PFByb2JsZW1BcmVhPlNla3N1YWxpdGV0LCBrw7huIG9nIGtyb3Bzb3BmYXR0ZWxzZTogSWtrZSByZWxldmFudCBmb3IgZGVubmUga29udGFrdDwvUHJvYmxlbUFyZWE+PC9OdXJzaW5nUHJvYmxlbUFyZWE+PE51cnNpbmdQcm9ibGVtQXJlYT48VGVybWlub2xvZ3k+PENvZGU+Si0xMjM0NTwvQ29kZT48Q29kZVN5c3RlbT5tZWRjb208L0NvZGVTeXN0ZW0+PENvZGVEZXNjcmlwdGlvbj5WaWRlbiBvZyB1ZHZpa2xpbmc8L0NvZGVEZXNjcmlwdGlvbj48L1Rlcm1pbm9sb2d5PjxQcm9ibGVtQXJlYT5TeWdkb21zaW5kc2lndC92aWRlbnNuaXZlYXU6IFRpbHN0csOma2tlbGlnIHN5Z2RvbXNpbmRzaWd0IDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+S29nbml0aXYgZm9ybcOlZW4gOiBOb3JtYWxlIGZvcnVkc8OmdG5pbmdlcjwvUHJvYmxlbUFyZWE+PC9OdXJzaW5nUHJvYmxlbUFyZWE+PE51cnNpbmdQcm9ibGVtQXJlYT48VGVybWlub2xvZ3k+PENvZGU+Sy0xMjM0NTwvQ29kZT48Q29kZVN5c3RlbT5tZWRjb208L0NvZGVTeXN0ZW0+PENvZGVEZXNjcmlwdGlvbj5VZHNraWxsZWxzZSA8L0NvZGVEZXNjcmlwdGlvbj48L1Rlcm1pbm9sb2d5PjxQcm9ibGVtQXJlYT5IasOmbHAgdmVkcsO4cmVuZGUgdG9pbGV0YmVzw7hnIChkb2suIGkgQWt0dWVsdCBmdW5rdGlvbnNuaXZlYXUgLSBvYnNlcnZhdGlvbikgOiBBbmRldCA8ZXAxOkJyZWFrIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIiAvPlVyaW4gOiBKYSwgdXJpbnVkc2tpbGxlbHNlIGkgdmFndGVuIDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+VXJpbnVkc2VlbmRlOiBLbGFyLCBHdWwgPGVwMTpCcmVhayB4bWxuczplcDE9InVybjpvaW86bWVkY29tOm11bmljaXBhbGl0eToxLjAuMCIgLz5Iw6ZtYXR1cmlncmFkOiBIw6ZtYXR1cmkgZ3JhZCAyIDxlcDE6QnJlYWsgeG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIC8+QmzDpnJlc2Nhbm5pbmcgIDogQmzDpnJlc2Nhbm5pbmcgZWZ0ZXIgdmFuZGxhZG5pbmcvU0lLPC9Qcm9ibGVtQXJlYT48L051cnNpbmdQcm9ibGVtQXJlYT48L051cnNpbmdQcm9ibGVtQXJlYXM+PGVwMTpSaXNrT2ZDb250YWdpb24geG1sbnM6ZXAxPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiPk5lai48L2VwMTpSaXNrT2ZDb250YWdpb24+PERpYWdub3Nlcz48RGlhZ25vc2lzPjxDb2RlPkRTNzIwPC9Db2RlPjxUeXBlQ29kZT5TS1NkaWFnbm9zZWtvZGU8L1R5cGVDb2RlPjxUZXh0PkZyYWt0dXIgYWYgbMOlcmJlbnNoYWxzPC9UZXh0PjwvRGlhZ25vc2lzPjwvRGlhZ25vc2VzPjxBYmlsaXR5VG9GdW5jdGlvbkF0RGlzY2hhcmdlPjxMYXN0TW9kaWZpZWREYXRlPjIwMjQtMDEtMjM8L0xhc3RNb2RpZmllZERhdGU+PEFiaWxpdHlUb0Z1bmN0aW9uRWxlbWVudHM+PEFiaWxpdHlUb0Z1bmN0aW9uRWxlbWVudD48VGVybWlub2xvZ3k+PENvZGU+RkE1MTA8L0NvZGU+PENvZGVTeXN0ZW0+aWNmPC9Db2RlU3lzdGVtPjxDb2RlRGVzY3JpcHRpb24+VmFza2Ugc2lnPC9Db2RlRGVzY3JpcHRpb24+PC9UZXJtaW5vbG9neT48U2NvcmU+bW9kZXJhdGU8L1Njb3JlPjxDb21tZW50PkJlaG92IGZvciBoasOmbHAgdGlsIHBlcnNvbmxpZyBoeWdpZWpuZSwgcHJpbcOmcnQgZsOlIGxhZ3QgdGluZ2VuZSBmcmVtIG9nIHPDpnR0ZXMgaWdhbmcuIGhqw6ZscCB0aWwgYmFkLjwvQ29tbWVudD48L0FiaWxpdHlUb0Z1bmN0aW9uRWxlbWVudD48QWJpbGl0eVRvRnVuY3Rpb25FbGVtZW50PjxUZXJtaW5vbG9neT48Q29kZT5GQTU0MDwvQ29kZT48Q29kZVN5c3RlbT5pY2Y8L0NvZGVTeXN0ZW0+PENvZGVEZXNjcmlwdGlvbj5BZi0gb2cgcMOla2zDpmRuaW5nPC9Db2RlRGVzY3JpcHRpb24+PC9UZXJtaW5vbG9neT48U2NvcmU+bW9kZXJhdGU8L1Njb3JlPjxDb21tZW50Pkhqw6ZscCB0aWwgw7h2cmUgb2cgbmVkcmUgYWYvcMOla2zDpmRuaW5nLjwvQ29tbWVudD48L0FiaWxpdHlUb0Z1bmN0aW9uRWxlbWVudD48QWJpbGl0eVRvRnVuY3Rpb25FbGVtZW50PjxUZXJtaW5vbG9neT48Q29kZT5GQTUzMDwvQ29kZT48Q29kZVN5c3RlbT5pY2Y8L0NvZGVTeXN0ZW0+PENvZGVEZXNjcmlwdGlvbj5Hw6UgcMOlIHRvaWxldDwvQ29kZURlc2NyaXB0aW9uPjwvVGVybWlub2xvZ3k+PFNjb3JlPmxldHRlPC9TY29yZT48Q29tbWVudD5IYXIgc3RvbWkgLSBrbGFyZSBzZWx2IGRldHRlLiBCZWhvdiBmb3IgaGrDpmxwIHRpbCB0b2lsZXRiZXPDuGcgLSBpbmR0aWwgcHQgc2VsdiBrYW4ga2xhcmUgZGV0LjwvQ29tbWVudD48L0FiaWxpdHlUb0Z1bmN0aW9uRWxlbWVudD48QWJpbGl0eVRvRnVuY3Rpb25FbGVtZW50PjxUZXJtaW5vbG9neT48Q29kZT5GQTQyMDwvQ29kZT48Q29kZVN5c3RlbT5pY2Y8L0NvZGVTeXN0ZW0+PENvZGVEZXNjcmlwdGlvbj5Gb3JmbHl0dGUgc2lnPC9Db2RlRGVzY3JpcHRpb24+PC9UZXJtaW5vbG9neT48U2NvcmU+bGV0dGU8L1Njb3JlPjxDb21tZW50PkfDpXIgbWVkIHJvbGxhdG9yLiBEZXIgYWZwcsO4dmVzIHRyYXBwZWdhbmcgdW5kZXIgaW5kbMOmZ2dlbHNlLjwvQ29tbWVudD48L0FiaWxpdHlUb0Z1bmN0aW9uRWxlbWVudD48QWJpbGl0eVRvRnVuY3Rpb25FbGVtZW50PjxUZXJtaW5vbG9neT48Q29kZT5GQTQ2MDwvQ29kZT48Q29kZVN5c3RlbT5pY2Y8L0NvZGVTeXN0ZW0+PENvZGVEZXNjcmlwdGlvbj5Gw6ZyZGVuIGkgZm9yc2tlbGxpZ2Ugb21naXZlbHNlcjwvQ29kZURlc2NyaXB0aW9uPjwvVGVybWlub2xvZ3k+PFNjb3JlPmxldHRlPC9TY29yZT48Q29tbWVudD5IasOmbHAgdGlsIGF0IGtvbW1lIGkgc2VuZy48L0NvbW1lbnQ+PC9BYmlsaXR5VG9GdW5jdGlvbkVsZW1lbnQ+PEFiaWxpdHlUb0Z1bmN0aW9uRWxlbWVudD48VGVybWlub2xvZ3k+PENvZGU+RkE1NjA8L0NvZGU+PENvZGVTeXN0ZW0+aWNmPC9Db2RlU3lzdGVtPjxDb2RlRGVzY3JpcHRpb24+RHJpa2tlPC9Db2RlRGVzY3JpcHRpb24+PC9UZXJtaW5vbG9neT48U2NvcmU+dWJldHlkZWxpZ2U8L1Njb3JlPjwvQWJpbGl0eVRvRnVuY3Rpb25FbGVtZW50PjxBYmlsaXR5VG9GdW5jdGlvbkVsZW1lbnQ+PFRlcm1pbm9sb2d5PjxDb2RlPkZBNTUwPC9Db2RlPjxDb2RlU3lzdGVtPmljZjwvQ29kZVN5c3RlbT48Q29kZURlc2NyaXB0aW9uPlNwaXNlPC9Db2RlRGVzY3JpcHRpb24+PC9UZXJtaW5vbG9neT48U2NvcmU+dWJldHlkZWxpZ2U8L1Njb3JlPjxDb21tZW50PsOYbnNrZXIgc2VsdiBhdCB2YXJldGFnZSBkZXR0ZS48L0NvbW1lbnQ+PC9BYmlsaXR5VG9GdW5jdGlvbkVsZW1lbnQ+PC9BYmlsaXR5VG9GdW5jdGlvbkVsZW1lbnRzPjwvQWJpbGl0eVRvRnVuY3Rpb25BdERpc2NoYXJnZT48TGF0ZXN0RGVwb3RNZWRpY2F0aW9uPjxOYW1lT2ZEcnVnPmFsZW5kcm9uc3lyZSAoQUxFTkRST05BVCAmcXVvdDtBVVIqPC9OYW1lT2ZEcnVnPjxEb3NhZ2VGb3JtPjcwIG1nICh0YWJsZXR0ZXIpPC9Eb3NhZ2VGb3JtPjxSb3V0ZU9mQWRtaW5pc3RyYXRpb24+T3JhbCBhbnZlbmRlbHNlPC9Sb3V0ZU9mQWRtaW5pc3RyYXRpb24+PC9MYXRlc3REZXBvdE1lZGljYXRpb24+PERpc2NoYXJnZVJlbGF0ZWRNZWRpY2luZUluZm9ybWF0aW9uPjxBdHRhY2hlZD48RXhwaXJlPjIwMjQtMDEtMjY8L0V4cGlyZT48L0F0dGFjaGVkPjxSZWNlaXB0PjE8L1JlY2VpcHQ+PFBpY2tVcE9yRGVsaXZlcnk+MDwvUGlja1VwT3JEZWxpdmVyeT48RG9zYWdlRXhlbXB0aW9uUmVvcmRlcmVkPjA8L0Rvc2FnZUV4ZW1wdGlvblJlb3JkZXJlZD48L0Rpc2NoYXJnZVJlbGF0ZWRNZWRpY2luZUluZm9ybWF0aW9uPjxEaWV0Rmlyc3QyNEhvdXJzPjxMdW5jaEJveD4wPC9MdW5jaEJveD48U2hvcHBpbmdBdERpc2NoYXJnZT4wPC9TaG9wcGluZ0F0RGlzY2hhcmdlPjwvRGlldEZpcnN0MjRIb3Vycz48ZXAxOkZ1dHVyZVBsYW5zIHhtbG5zOmVwMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4wIj5Ib3NwaXRhbGV0IGhhciBiZWhhbmRsaW5nc2Fuc3ZhciBmb3IgcGF0aWVudGVuIGluZHRpbCAyOC0wMS0yMDI0IDIwOjAwLiBLb250YWt0dGVsZWZvbm51bW1lciAodGlsIGJydWcgZm9yIHN1bmRoZWRzcHJvZmVzc2lvbmVsbGUpOiA1OCA1NSA5NiA3NS4gRnJlbXRpZGlnZSBhZnRhbGVyOiAucHQgc2thbCB0aWxtZWxkZXMgb3Bmw7hsZ2VuZGUgaGplbW1lYmVzw7hnLiAgQmVoYW5kbGluZ3NuaXZlYXU6IEdlbm9wbGl2bmluZywgZnVsZCBiZWhhbmRsaW5nLiBMaWxsaWFuIFdlZGVsIFN2ZW5kc2VuLCBMw6ZnZSBkLiAyMC0wMS0yMDI0IDE1OjE0LiA8L2VwMTpGdXR1cmVQbGFucz48L1JlcG9ydE9mRGlzY2hhcmdlPjwvRW1lc3NhZ2U+\",\"externalId\":null,\"ccRecipient\":null,\"copiedMessageId\":null,\"letterType\":null,\"serviceTagCode\":null,\"messageType\":\"MEDCOM_MESSAGE_REPORT_OF_DISCHARGE_1_0_3\",\"versionCode\":\"XD1834C\",\"conversationId\":\"00000000007EB4D0FCCFECBBF646E725\",\"activityIdentifier\":{\"identifier\":\"MESSAGE:368661\",\"type\":\"MESSAGE\",\"activityId\":368661,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"patient\":{\"id\":3994,\"version\":17,\"firstName\":\"Eva\",\"lastName\":\"Galavits\",\"middleName\":null,\"fullName\":\"Eva Galavits\",\"fullReversedName\":\"Galavits, Eva\",\"homePhoneNumber\":\"57613938\",\"mobilePhoneNumber\":\"41198246\",\"workPhoneNumber\":null,\"patientIdentifier\":{\"type\":\"cpr\",\"identifier\":\"160537-2054\",\"managedExternally\":false,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"currentAddress\":{\"addressLine1\":\"Hyldegårdsvej 11 D\",\"addressLine2\":null,\"addressLine3\":null,\"addressLine4\":null,\"addressLine5\":null,\"administrativeAreaCode\":\"329\",\"countryCode\":\"dk\",\"postalCode\":\"4100\",\"postalDistrict\":\"Ringsted\",\"restricted\":false,\"geoCoordinates\":{\"x\":null,\"y\":null,\"present\":false},\"startDate\":null,\"endDate\":null,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/address/countries\"},\"searchPostalDistrict\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/address/postalDistrict\"}}},\"currentAddressIndicator\":\"PRIMARY_ADDRESS\",\"age\":86,\"monthsAfterBirthday\":8,\"gender\":\"FEMALE\",\"patientStatus\":\"FINAL\",\"patientState\":{\"id\":21,\"version\":0,\"name\":\"Aktiv\",\"color\":\"000000\",\"type\":{\"id\":\"ACTIVE\",\"name\":\"Aktiv\",\"abbreviation\":\"\",\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"active\":true,\"defaultObject\":true,\"citizenRegistryConfiguration\":{\"updateEnabled\":true,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"_links\":{\"medcomRecipient\":null,\"self\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patientStates/21\"},\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"telephonesRestricted\":false,\"imageId\":null,\"_links\":{\"medcomRecipient\":null,\"self\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3994\"},\"patientOverview\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3994/overview\"},\"citizenOverviewForms\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3994/citizenOverviewForms\"},\"measurementData\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3994/measurementInstructions/data/pages\"},\"measurementInstructions\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3994/measurementInstructions/instructions\"},\"patientConditions\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3994/conditions\"},\"patientOrganizations\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3994/organizations\"},\"activityLinksPrototypes\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3994/activitylinks/prototypes\"},\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"pathwayAssociation\":{\"placement\":{\"name\":\"MedCom Myndighedsenheden\",\"programPathwayId\":4355,\"pathwayTypeId\":13,\"parentPathwayId\":null,\"patientPathwayId\":70323,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"parentReferenceId\":null,\"referenceId\":1035757,\"canAssociateWithPathway\":true,\"canAssociateWithPatient\":false,\"associatedWithPatient\":false,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3994/pathways/70323/availablePathwayAssociations\"},\"availableRootProgramPathways\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3994/availableProgramPathways\"},\"availablePathwayPlacements\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/3994/pathways/availablePathwayPlacements\"},\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"body\":null,\"priority\":null,\"previousMessageId\":null,\"_links\":{\"medcomRecipient\":null,\"self\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/368661\"},\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/audit/medcom/368661\"},\"relatedCommunication\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/related/368661\"},\"activeAssignments\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/assignments//mobile/ringsted/v2/assignments/patient/3994/assignments/active?activity=MESSAGE:368661\"},\"assignments\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/assignments//mobile/ringsted/v2/assignments/patient/3994/assignments?activity=MESSAGE:368661\"},\"availableAssignmentTypes\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/assignments//mobile/ringsted/v2/assignments/patient/3994/assignmentTypes/active?activity=MESSAGE:368661&activityType=MESSAGE&messageTypeType=MEDCOM&messageType=MEDCOM_MESSAGE_REPORT_OF_DISCHARGE_1_0_3&messageStateDirection=INCOMING\"},\"autoAssignmentsPrototype\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/assignments//mobile/ringsted/v2/assignments/auto/prototype?activity=MESSAGE:368661&activityType=MESSAGE&messageTypeType=MEDCOM&messageType=MEDCOM_MESSAGE_REPORT_OF_DISCHARGE_1_0_3&messageStateDirection=INCOMING&messageStatus=FINAL\"},\"update\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/368661\"},\"transformedBody\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/368661/transformedBody\"},\"print\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/368661/print\"},\"reply\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/368661/reply\"},\"replyTo\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/replyTo/368661?identifierType=SYGEHUSAFDELINGSNUMMER&identifierValue=3800R40\"},\"archive\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/archived/368661\"},\"availableCountries\":null,\"searchPostalDistrict\":null},\"level\":null}\r\n");
            baseObjects.Add("{\"type\":\"medcom\",\"id\":367522,\"version\":3,\"sender\":{\"type\":\"medcomMessageDetailsBased\",\"id\":367522,\"name\":\"Region Sjællands Sygehusvæsen - Fælledvej 13 - 4200 Slagelse - EAN: 5790002411854\",\"recipientAddress\":null,\"recipientType\":\"MESSAGE_SENDER\",\"_links\":{\"medcomRecipient\":{\"href\":\"/api/core/mobile/ringsted/v2/medcomRecipients/medcomSender/367522\"},\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"recipient\":{\"type\":\"medcomMessageDetailsBased\",\"id\":367522,\"name\":\"Ringsted Kommune - Zahlesvej 18 - 4100 Ringsted - EAN: 5790001353858\",\"recipientAddress\":null,\"recipientType\":\"MESSAGE_RECIPIENT\",\"_links\":{\"medcomRecipient\":{\"href\":\"/api/core/mobile/ringsted/v2/medcomRecipients/medcomRecipient/367522\"},\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"subject\":\"Udskrivningsrapport\",\"date\":\"2024-01-24T16:17:00+01:00\",\"state\":{\"direction\":\"INCOMING\",\"status\":\"FINAL\",\"response\":\"NOT_AWAITING\",\"handshake\":\"NO\",\"processed\":\"PROCESSED\",\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"customer\":null,\"raw\":\"PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiPz48bnMxOkVtZXNzYWdlIHhtbG5zPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIHhtbG5zOm5zMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4zIj48RW52ZWxvcGU+PFNlbnQ+PERhdGU+MjAyNC0wMS0yNDwvRGF0ZT48VGltZT4xNjoxMjwvVGltZT48L1NlbnQ+PElkZW50aWZpZXI+RVBJQzY0MTU1ODE2NDwvSWRlbnRpZmllcj48QWNrbm93bGVkZ2VtZW50Q29kZT5wbHVzcG9zaXRpdmt2aXR0PC9BY2tub3dsZWRnZW1lbnRDb2RlPjwvRW52ZWxvcGU+PG5zMTpSZXBvcnRPZkRpc2NoYXJnZT48bnMxOkxldHRlcj48bnMxOklkZW50aWZpZXI+RVBJQzY0MTU1ODE2NDwvbnMxOklkZW50aWZpZXI+PG5zMTpWZXJzaW9uQ29kZT5YRDE4MzRDPC9uczE6VmVyc2lvbkNvZGU+PG5zMTpTdGF0aXN0aWNhbENvZGU+WERJUzE4PC9uczE6U3RhdGlzdGljYWxDb2RlPjxuczE6QXV0aG9yaXNhdGlvbj48RGF0ZT4yMDI0LTAxLTI0PC9EYXRlPjxUaW1lPjE2OjEyPC9UaW1lPjwvbnMxOkF1dGhvcmlzYXRpb24+PG5zMTpUeXBlQ29kZT5YRElTMTg8L25zMTpUeXBlQ29kZT48bnMxOkVwaXNvZGVPZkNhcmVJZGVudGlmaWVyPjAwMDAwMDAwMDBENDgzNTcxMDQ4Q0ZENzNDQTg5RjNCPC9uczE6RXBpc29kZU9mQ2FyZUlkZW50aWZpZXI+PC9uczE6TGV0dGVyPjxTZW5kZXI+PEVBTklkZW50aWZpZXI+NTc5MDAwMjQxMTg1NDwvRUFOSWRlbnRpZmllcj48SWRlbnRpZmllcj4zODAwUjIwPC9JZGVudGlmaWVyPjxJZGVudGlmaWVyQ29kZT5zeWdlaHVzYWZkZWxpbmdzbnVtbWVyPC9JZGVudGlmaWVyQ29kZT48T3JnYW5pc2F0aW9uTmFtZT5SZWdpb24gU2rDpmxsYW5kcyBTeWdlaHVzdsOmc2VuPC9PcmdhbmlzYXRpb25OYW1lPjxEZXBhcnRtZW50TmFtZT5TTEEgS0FSRElPTE9HSVNLIEFGRC48L0RlcGFydG1lbnROYW1lPjxVbml0TmFtZT5Ib3Jtb24tIG9nIEhqZXJ0ZWFmc25pdHRldDwvVW5pdE5hbWU+PFN0cmVldE5hbWU+RsOmbGxlZHZlaiAxMzwvU3RyZWV0TmFtZT48RGlzdHJpY3ROYW1lPlNsYWdlbHNlPC9EaXN0cmljdE5hbWU+PFBvc3RDb2RlSWRlbnRpZmllcj40MjAwPC9Qb3N0Q29kZUlkZW50aWZpZXI+PFRlbGVwaG9uZVN1YnNjcmliZXJJZGVudGlmaWVyPjU4NTU5NTEyPC9UZWxlcGhvbmVTdWJzY3JpYmVySWRlbnRpZmllcj48U2lnbmVkQnk+PElkZW50aWZpZXI+MzgwMFIyMDwvSWRlbnRpZmllcj48SWRlbnRpZmllckNvZGU+c3lnZWh1c2FmZGVsaW5nc251bW1lcjwvSWRlbnRpZmllckNvZGU+PFBlcnNvbkdpdmVuTmFtZT5NaWNoZWxsZTwvUGVyc29uR2l2ZW5OYW1lPjxQZXJzb25TdXJuYW1lTmFtZT5DaHJpc3RlbnNlbjwvUGVyc29uU3VybmFtZU5hbWU+PFBlcnNvblRpdGxlPlN5Z2VwbGVqZXJza2U8L1BlcnNvblRpdGxlPjwvU2lnbmVkQnk+PE1lZGljYWxTcGVjaWFsaXR5Q29kZT5rYXJkaW9sb2dpPC9NZWRpY2FsU3BlY2lhbGl0eUNvZGU+PENvbnRhY3RJbmZvcm1hdGlvbj48Q29udGFjdE5hbWU+SGplcnRlLWhvcm1vbmFmc25pdHRldCBTbGFnZWxzZSBzeSo8L0NvbnRhY3ROYW1lPjxUZWxlcGhvbmVTdWJzY3JpYmVySWRlbnRpZmllcj41ODU1OTUxMjwvVGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+PC9Db250YWN0SW5mb3JtYXRpb24+PC9TZW5kZXI+PFJlY2VpdmVyPjxFQU5JZGVudGlmaWVyPjU3OTAwMDEzNTM4NTg8L0VBTklkZW50aWZpZXI+PElkZW50aWZpZXI+MzI5PC9JZGVudGlmaWVyPjxJZGVudGlmaWVyQ29kZT5rb21tdW5lbnVtbWVyPC9JZGVudGlmaWVyQ29kZT48T3JnYW5pc2F0aW9uTmFtZT5SaW5nc3RlZCBLb21tdW5lPC9PcmdhbmlzYXRpb25OYW1lPjxEZXBhcnRtZW50TmFtZT5Tb2NpYWwgb2cgU3VuZGhlZDwvRGVwYXJ0bWVudE5hbWU+PFVuaXROYW1lPkhqZW1tZXN5Z2VwbGVqZW4sIFJpbmdzdGVkIEtvbW11bmU8L1VuaXROYW1lPjxTdHJlZXROYW1lPlphaGxlc3ZlaiAxODwvU3RyZWV0TmFtZT48RGlzdHJpY3ROYW1lPlJpbmdzdGVkPC9EaXN0cmljdE5hbWU+PFBvc3RDb2RlSWRlbnRpZmllcj40MTAwPC9Qb3N0Q29kZUlkZW50aWZpZXI+PC9SZWNlaXZlcj48UHJhY3RpdGlvbmVyPjxJZGVudGlmaWVyPjAyNjA4NTwvSWRlbnRpZmllcj48SWRlbnRpZmllckNvZGU+eWRlcm51bW1lcjwvSWRlbnRpZmllckNvZGU+PFBlcnNvbk5hbWU+TMOmZ2VodXNldCBJIEJlbmzDuHNlPC9QZXJzb25OYW1lPjxUZWxlcGhvbmVTdWJzY3JpYmVySWRlbnRpZmllcj41NzY3MDA0NDwvVGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+PC9QcmFjdGl0aW9uZXI+PFBhdGllbnQ+PENpdmlsUmVnaXN0cmF0aW9uTnVtYmVyPjI5MDc1OTA5Nzk8L0NpdmlsUmVnaXN0cmF0aW9uTnVtYmVyPjxQZXJzb25TdXJuYW1lTmFtZT5MaW5kZXNrb3Y8L1BlcnNvblN1cm5hbWVOYW1lPjxQZXJzb25HaXZlbk5hbWU+QWxsYW48L1BlcnNvbkdpdmVuTmFtZT48U3RyZWV0TmFtZT5BYmVsc3ZlaiAxOTg8L1N0cmVldE5hbWU+PERpc3RyaWN0TmFtZT5SaW5nc3RlZDwvRGlzdHJpY3ROYW1lPjxQb3N0Q29kZUlkZW50aWZpZXI+NDEwMDwvUG9zdENvZGVJZGVudGlmaWVyPjxUZWxlcGhvbmVTdWJzY3JpYmVySWRlbnRpZmllcj4wMDAwMDAwMDwvVGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+PC9QYXRpZW50PjxuczE6UmVsYXRpdmVzPjxuczE6UmVsYXRpdmU+PG5zMTpSZWxhdGlvbkNvZGU+dXNwZWNfcGFhcm9lcmVuZGU8L25zMTpSZWxhdGlvbkNvZGU+PFBlcnNvblN1cm5hbWVOYW1lPkxpbmRoYXJkdDwvUGVyc29uU3VybmFtZU5hbWU+PFBlcnNvbkdpdmVuTmFtZT5DbGF1czwvUGVyc29uR2l2ZW5OYW1lPjxDb2RlZFRlbGVwaG9uZVN1YnNjcmliZXJJZGVudGlmaWVyPjxUZWxlcGhvbmVDb2RlPm1vYmlsPC9UZWxlcGhvbmVDb2RlPjxUZWxlcGhvbmVTdWJzY3JpYmVySWRlbnRpZmllcj40MTE3MTAwMTwvVGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+PC9Db2RlZFRlbGVwaG9uZVN1YnNjcmliZXJJZGVudGlmaWVyPjxuczE6SW5mb3JtZWRSZWxhdGl2ZT4wPC9uczE6SW5mb3JtZWRSZWxhdGl2ZT48L25zMTpSZWxhdGl2ZT48bnMxOkNvbW1lbnQ+cGF0aWVudCBrYW4gc2VsdiBpbmZvcm1lciBvZyBrb250YWt0ZSByZWxldmFudGUgcMOlcsO4cmVuZGU8L25zMTpDb21tZW50PjwvbnMxOlJlbGF0aXZlcz48QWRtaXNzaW9uPjxEYXRlPjIwMjQtMDEtMTY8L0RhdGU+PFRpbWU+MTU6MTI8L1RpbWU+PC9BZG1pc3Npb24+PEVuZE9mVHJlYXRtZW50PjxEYXRlPjIwMjQtMDEtMjQ8L0RhdGU+PFRpbWU+MTU6MDA8L1RpbWU+PC9FbmRPZlRyZWF0bWVudD48RGlzY2hhcmdlPjxEYXRlPjIwMjQtMDEtMjQ8L0RhdGU+PFRpbWU+MTU6MDA8L1RpbWU+PC9EaXNjaGFyZ2U+PENhdXNlT2ZBZG1pc3Npb24+SW5kbMOmZ2dlcyBwZ2Egc21lcnRlciBpIGJlbmVuZSBvZyB2YXIgZmFsZGV0IHZlZCBlbiBmbGV4dGF4YSAuPC9DYXVzZU9mQWRtaXNzaW9uPjxDb3Vyc2VPZkFkbWlzc2lvbj5EZXIgYmVoYW5kbGVzIGZvciBlbiBpZmVrdGlvbiBtZWQgaXYgYW50aWJpb3Rpa2Egb2cgZGVuIGVyIGFmc2x1dHRldCBpZGFnIGQgMjMvMSAuIEZJayBhdHJpZWZsaW1tZXIgb2cgYmxldiBiZWhhbmRsZXQgbWVkIEJldGFibG9ra2VyICwgZGlnb3hpbiBvZyBibG9kZm9ydHluZGVuZGUgKCB0YmwgRWxpcXVpcyApIC4gZGVyIGVyIG51IG5vcm1hbCBzaW51c3l0bWUgb2cgcHQgaGFyIGlra2UgbMOmbmdlcmUgYmVob3YgZm9yIGhqZXJ0ZW92ZXJ2w6VnbmluZyAgICAgSGFyIGbDpWV0IHVkbGV2ZXJldCBlbiBkZWwgc2tyaWdmdGxpZyBpbmZvcm1hdGlvbiBvbSBBdHJpZWZsaW1tZXIgb2cgYmxvZGZvcnR5bmRlbmRlIGJlaGFuZGxpbmcgLCBBbGxhbiBlciBvcmRibGluZCBvZyBoYXIgYnJ1ZyBmb3IgYXQgaHVzdHJ1IGhqw6ZscGVyIG1lZCBhdCBsw6ZzZSBkZXQgaMO4anQgLiBIamVtbWVwbGVqZW4gZXIgdmVsa29tbWVuIHRpbCBhdCBsw6ZzZSBpbmZvcm1hdGlvbmVuICggZGV0IGVyIGkgZW4gZ3VsIG1hcHBlICkgSGFyIHDDpSBow7hqcmUgc2lkZSBhZiBzY3JvdHVtIGh5ZHJvY2VsZSAuIGVyIHZpbCBibGl2ZXIgZnVsZ3Qgb3AgcMOlIGRldCBhbWJ1bGFudCB2aWEgdXJvbG9nZXJuZSBpIFJvc2tpbGRlIC4gRGVyIGVyIG1lZGdpdmV0IGVuIGFmbGFzdGVuZGUgUmNobyBwdWRlIC4gRGVyIGVyIGZvcnRzYXQgcGVybWFuZW50IHVyaW4ta2F0aGV0ZXIgU21lcnRlciBpIGJlbmVuZSBrYW4gdsOmcmUgZGlhYmV0aXNrIG5ldXJvcGF0aSAuIERlciBlciBsYXZldCBkaXN0YWwgYmxvZHRyeWtzbcOlbGluZywgc29tIGthcmtpcnVyZ2VuIGFuZ2l2ZXIgc29tIG5vcm1hbCAuQmVuIHPDpXIgZXIgcG9kZXQgb2cgZGVyIHZhciBpbmdlbiB2w6Zrc3QgLiB1ZHNrcml2ZXMgdGlsIHZhbmxpZyBoasOmbHAgLjwvQ291cnNlT2ZBZG1pc3Npb24+PG5zMTpOdXJzaW5nUHJvYmxlbUFyZWFzPjxuczE6TnVyc2luZ1Byb2JsZW1BcmVhPjxuczE6VGVybWlub2xvZ3k+PG5zMTpDb2RlPkEtMTIzNDU8L25zMTpDb2RlPjxuczE6Q29kZVN5c3RlbT5tZWRjb208L25zMTpDb2RlU3lzdGVtPjxuczE6Q29kZURlc2NyaXB0aW9uPkZ1bmt0aW9uc25pdmVhdSBvZyBiZXbDpmdlYXBwYXJhdDwvbnMxOkNvZGVEZXNjcmlwdGlvbj48L25zMTpUZXJtaW5vbG9neT48bnMxOlByb2JsZW1BcmVhPlByaW3DpnIgc2VrdG9yOiBmw6VyIGhqw6ZscC9zdMO4dHRlIGFmOiBIamVtbWVwbGVqZSA8QnJlYWsgLz5IasOmbHAgdGlsIHBlcnNvbmxpZyBoeWdpZWpuZTogTGlkdCBoasOmbHAgPEJyZWFrIC8+SGrDpmxwIHZlZHLDuHJlbmRlIHRvaWxldGJlc8O4ZzogR8OlciBzZWx2IHDDpSB0b2lsZXR0ZXQgPEJyZWFrIC8+SGrDpmxwIHRpbCBhdCBzcGlzZSBvZyBkcmlra2U6IFNlbHZoanVscGVuIDxCcmVhayAvPkhqw6ZscCB0aWwgdmVuZGluZy9sZWpyaW5nOiBWZW5kZXIgc2lnIHNlbHYgPEJyZWFrIC8+SGrDpmxwIHRpbCBtb2JpbGlzZXJpbmc6IFNlbHZoanVscGVuIDxCcmVhayAvPk1vYmlsaXNlcmluZ3NyZXN0cmlrdGlvbmVyOiBJbmdlbiByZXN0cmlrdGlvbmVyIDxCcmVhayAvPkhqw6ZscGVtaWRsZXIgdGlsIG1vYmlsaXNlcmluZzogSW5nZW4gPEJyZWFrIC8+TW9iaWxpc2VyaW5nOiBGcml0IG1vYmlsaXNlcmV0LCBHw6VyIHDDpSB0b2lsZXR0ZXQ8L25zMTpQcm9ibGVtQXJlYT48L25zMTpOdXJzaW5nUHJvYmxlbUFyZWE+PG5zMTpOdXJzaW5nUHJvYmxlbUFyZWE+PG5zMTpUZXJtaW5vbG9neT48bnMxOkNvZGU+Qi0xMjM0NTwvbnMxOkNvZGU+PG5zMTpDb2RlU3lzdGVtPm1lZGNvbTwvbnMxOkNvZGVTeXN0ZW0+PG5zMTpDb2RlRGVzY3JpcHRpb24+RXJuw6ZyaW5nPC9uczE6Q29kZURlc2NyaXB0aW9uPjwvbnMxOlRlcm1pbm9sb2d5PjxuczE6UHJvYmxlbUFyZWE+SMO4amRlOiAxODYgY20gPEJyZWFrIC8+VsOmZ3Q6ICghKSAxNjUsOCBrZyA8QnJlYWsgLz5CTUkgKEJlcmVnbmV0KTogNDcsOSA8QnJlYWsgLz5BcHBldGl0OiBOb3JtYWwgYXBwZXRpdCA8QnJlYWsgLz5Uw7hyc3Q6IE5vcm1hbCB0w7hyc3Q8L25zMTpQcm9ibGVtQXJlYT48L25zMTpOdXJzaW5nUHJvYmxlbUFyZWE+PG5zMTpOdXJzaW5nUHJvYmxlbUFyZWE+PG5zMTpUZXJtaW5vbG9neT48bnMxOkNvZGU+Qy0xMjM0NTwvbnMxOkNvZGU+PG5zMTpDb2RlU3lzdGVtPm1lZGNvbTwvbnMxOkNvZGVTeXN0ZW0+PG5zMTpDb2RlRGVzY3JpcHRpb24+SHVkIG9nIHNsaW1oaW5kZXI8L25zMTpDb2RlRGVzY3JpcHRpb24+PC9uczE6VGVybWlub2xvZ3k+PG5zMTpQcm9ibGVtQXJlYT5IdWQ6IFTDuHIsIFN0YXNlZWtzZW0sIFNrw6ZsbGVuZGUsIFLDuGRtZSA8QnJlYWsgLz5Sw7hkbWUsIGxva2FsaXNhdGlvbjogbHlza2UgPEJyZWFrIC8+U2vDpmxsZW5kZSwgbG9rYWxpc2F0aW9uOiB1LmUgPEJyZWFrIC8+U3Rhc2Vla3NlbSwgbG9rYWxpc2F0aW9uOiB1LmUgPEJyZWFrIC8+VMO4ciwgbG9rYWxpc2F0aW9uOiB1LmUgPEJyZWFrIC8+SGrDpmxwIHRpbCBwZXJzb25saWcgaHlnaWVqbmUgKGRvay4gaSBBa3R1ZWx0IGZ1bmt0aW9uc25pdmVhdSAtIG9ic2VydmF0aW9uKTogTGlkdCBoasOmbHAgPEJyZWFrIC8+T3Zlcm11bmQ6IE1hbmdsZXIgZWduZSB0w6ZuZGVyIG9nIHRhbmRlcnN0YXRuaW5nZXIgPEJyZWFrIC8+VW5kZXJtdW5kOiBNYW5nbGVyIGVnbmUgdMOmbmRlciBvZyB0YW5kZXJzdGF0bmluZ2VyIDxCcmVhayAvPkhqw6ZscCB0aWwgbXVuZGh5Z2llam5lOiBQYXRpZW50ZW4gdmFyZXRhZ2VyIHNlbHYgbXVuZGh5Z2llam5lIDxCcmVhayAvPlJpc2lrbyBmb3IgdWR2aWtsaW5nIGFmIHRyeWtza2FkZS8tc8Olcj86IEphIDxCcmVhayAvPkluc3Bla3Rpb24gZm9yIHRyeWtzw6VyOiBJbmdlbiB0cnlrc2thZGUvdHJ5a3PDpXIgPEJyZWFrIC8+U2Vuc29yaXNrIHBlcmNlcHRpb246IEluZ2VuIGJlZ3LDpm5zbmluZ2VyIDxCcmVhayAvPkZ1Z3Q6IExlamxpZ2hlZHN2aXMgZnVndGlnIDxCcmVhayAvPkFrdGl2aXRldDogR8OlciBsZWpsaWdoZWRzdmlzdCA8QnJlYWsgLz5Nb2JpbGl0ZXQ6IExldCBiZWdyw6Zuc2V0IG1vYmlsaXRldCA8QnJlYWsgLz5Fcm7DpnJpbmcsIGluZHRhZ2Vsc2U6IEZ1bGR0IHRpbHN0csOma2tlbGlnIDxCcmVhayAvPkduaWRuaW5nIG9nIGZvcnNreWRuaW5nOiBJbnRldCBzeW5saWd0IHByb2JsZW0gPEJyZWFrIC8+QnJhZGVuIHRvdGFsIHNjb3JlOiAyMCA8QnJlYWsgLz5Ucnlrc8OlcnNyaXNpa286IE1lZ2V0IGxhdiByaXNpa28gZm9yIGF0IHVkdmlrbGUgdHJ5a3PDpXIgPEJyZWFrIC8+VHJ5a3PDpXJzZm9yZWJ5Z2dlbHNlIC0gbWFkcmFzOiBTa3VtbWFkcmFzPC9uczE6UHJvYmxlbUFyZWE+PC9uczE6TnVyc2luZ1Byb2JsZW1BcmVhPjxuczE6TnVyc2luZ1Byb2JsZW1BcmVhPjxuczE6VGVybWlub2xvZ3k+PG5zMTpDb2RlPkQtMTIzNDU8L25zMTpDb2RlPjxuczE6Q29kZVN5c3RlbT5tZWRjb208L25zMTpDb2RlU3lzdGVtPjxuczE6Q29kZURlc2NyaXB0aW9uPktvbW11bmlrYXRpb24vbmV1cm9sb2dpPC9uczE6Q29kZURlc2NyaXB0aW9uPjwvbnMxOlRlcm1pbm9sb2d5PjxuczE6UHJvYmxlbUFyZWE+S29tbXVuaWthdGlvbnNoasOmbHBlbWlkbGVyIDogSW5nZW4gPEJyZWFrIC8+QmV2aWRzdGhlZHNuaXZlYXU6IFbDpWdlbiwgcmVhZ2VyZXIgbm9ybWFsdCBlbGxlciBub3JtYWwgc8O4dm4gPEJyZWFrIC8+Tml2ZWF1IGFmIG9yaWVudGVyaW5nOiBPcmllbnRlcmV0IGkgdGlkLCBzdGVkLCBzaXR1YXRpb24gb2cgZWduZSBkYXRhIDxCcmVhayAvPkV2bmUgdGlsIGF0IGZvcnN0w6U6IFJlbGV2YW50IGTDuG1tZWtyYWZ0IDxCcmVhayAvPkV2bmUgdGlsIGF0IGfDuHJlIHNpZyBmb3JzdMOlZWxpZzogTm9ybWFsIHRhbGU8L25zMTpQcm9ibGVtQXJlYT48L25zMTpOdXJzaW5nUHJvYmxlbUFyZWE+PG5zMTpOdXJzaW5nUHJvYmxlbUFyZWE+PG5zMTpUZXJtaW5vbG9neT48bnMxOkNvZGU+RS0xMjM0NTwvbnMxOkNvZGU+PG5zMTpDb2RlU3lzdGVtPm1lZGNvbTwvbnMxOkNvZGVTeXN0ZW0+PG5zMTpDb2RlRGVzY3JpcHRpb24+UHN5a29zb2NpYWxlIGZvcmhvbGQ8L25zMTpDb2RlRGVzY3JpcHRpb24+PC9uczE6VGVybWlub2xvZ3k+PG5zMTpQcm9ibGVtQXJlYT5BbG1lbnQgYmVmaW5kZW5kZTogUHQuIG9wbGV2ZXMgYWxtZW50IHZlbGJlZmluZGVuZGUgPEJyZWFrIC8+Qm9saWc6IEJlc2t5dHRldCBib2xpZyA8QnJlYWsgLz5Cb3Igc2FtbWVuIG1lZDogw4ZndGVmw6ZsbGUvcGFydG5lciA8QnJlYWsgLz5Qcmltw6ZyIHNla3RvcjogZsOlciBoasOmbHAvc3TDuHR0ZSBhZjogSGplbW1lcGxlamU8L25zMTpQcm9ibGVtQXJlYT48L25zMTpOdXJzaW5nUHJvYmxlbUFyZWE+PG5zMTpOdXJzaW5nUHJvYmxlbUFyZWE+PG5zMTpUZXJtaW5vbG9neT48bnMxOkNvZGU+Ri0xMjM0NTwvbnMxOkNvZGU+PG5zMTpDb2RlU3lzdGVtPm1lZGNvbTwvbnMxOkNvZGVTeXN0ZW0+PG5zMTpDb2RlRGVzY3JpcHRpb24+UmVzcGlyYXRpb24gb2cgY2lya3VsYXRpb248L25zMTpDb2RlRGVzY3JpcHRpb24+PC9uczE6VGVybWlub2xvZ3k+PG5zMTpQcm9ibGVtQXJlYT5IamVtbWViZWhhbmRsaW5nIChyZXNwaXJhdGlvbik6IFPDuHZuYXBuw7ggbWFza2luZSAoQ1BBUCkgPEJyZWFrIC8+UmVzcGlyYXRpb25zbcO4bnN0ZXIvZHliZGU6IE5vcm1hbCA8QnJlYWsgLz5SZXNwaXJhdGlvbnNseWRlOiBOb3JtYWwgPEJyZWFrIC8+SG9zdGU6IEluZ2VuIDxCcmVhayAvPlRob3JheHVkc2VlbmRlOiBOb3JtYWx0IDxCcmVhayAvPkx1ZnR2ZWo6IEZyaSA8QnJlYWsgLz5DaXJrdWxhdG9yaXNrZSBzeW1wdG9tZXI6IEluZ2VuIHN5bXB0b21lciA8QnJlYWsgLz5DaXJrdWxhdG9yaXNrIGh1ZHRpbHN0YW5kL3RlbXBlcmF0dXI6IE5vcm1hbCBjaXJrdWxhdG9yaXNrIGh1ZHRpbHN0YW5kL2h1ZHRlbXBlcmF0dXIgPEJyZWFrIC8+SHlkcmVyaW5nc3N0YXR1czogw5hkZW0gPEJyZWFrIC8+w5hkZW0sIGxva2FsaXNhdGlvbjogVUUgYmlsbGF0IDxCcmVhayAvPsOYZGVtLCBiZXNrcml2ZWxzZTogbW9kZXJhdCA8QnJlYWsgLz5DaXJrdWxhdG9yaXNrZSBlbmhlZGVyL2ltcGxhbnRhdGVyOiBJbmdlbiA8QnJlYWsgLz5ZZGVybGlnZXJlIGNpcmt1bGF0b3Jpc2tlIG9ic2VydmF0aW9uZXI6IEhqZXJ0ZWZyZWt2ZW5zLCBpbnRlcnZhbCA8QnJlYWsgLz5IamVydGVyeXRtZTogU2ludXNyeXRtZTwvbnMxOlByb2JsZW1BcmVhPjwvbnMxOk51cnNpbmdQcm9ibGVtQXJlYT48bnMxOk51cnNpbmdQcm9ibGVtQXJlYT48bnMxOlRlcm1pbm9sb2d5PjxuczE6Q29kZT5HLTEyMzQ1PC9uczE6Q29kZT48bnMxOkNvZGVTeXN0ZW0+bWVkY29tPC9uczE6Q29kZVN5c3RlbT48bnMxOkNvZGVEZXNjcmlwdGlvbj5TZWtzdWFsaXRldCwga8O4biBvZyBrcm9wc29wZmF0dGVsc2U8L25zMTpDb2RlRGVzY3JpcHRpb24+PC9uczE6VGVybWlub2xvZ3k+PG5zMTpQcm9ibGVtQXJlYT5TZWtzdWFsaXRldCwga8O4biBvZyBrcm9wc29wZmF0dGVsc2U6IElra2UgcmVsZXZhbnQgZm9yIGRlbm5lIGtvbnRha3Q8L25zMTpQcm9ibGVtQXJlYT48L25zMTpOdXJzaW5nUHJvYmxlbUFyZWE+PG5zMTpOdXJzaW5nUHJvYmxlbUFyZWE+PG5zMTpUZXJtaW5vbG9neT48bnMxOkNvZGU+SC0xMjM0NTwvbnMxOkNvZGU+PG5zMTpDb2RlU3lzdGVtPm1lZGNvbTwvbnMxOkNvZGVTeXN0ZW0+PG5zMTpDb2RlRGVzY3JpcHRpb24+U21lcnRlciBvZyBzYW5zZWluZHRyeWs8L25zMTpDb2RlRGVzY3JpcHRpb24+PC9uczE6VGVybWlub2xvZ3k+PG5zMTpQcm9ibGVtQXJlYT5IYWJpdHVlbGxlIGhqw6ZscGVtaWRsZXIgdGlsIHNhbnNlciA6IEJyaWxsZXIsIGzDpnNlLCBIw7hyZWFwcGFyYXQsIGjDuGpyZSwgSMO4cmVhcHBhcmF0LCB2ZW5zdHJlPC9uczE6UHJvYmxlbUFyZWE+PC9uczE6TnVyc2luZ1Byb2JsZW1BcmVhPjxuczE6TnVyc2luZ1Byb2JsZW1BcmVhPjxuczE6VGVybWlub2xvZ3k+PG5zMTpDb2RlPkktMTIzNDU8L25zMTpDb2RlPjxuczE6Q29kZVN5c3RlbT5tZWRjb208L25zMTpDb2RlU3lzdGVtPjxuczE6Q29kZURlc2NyaXB0aW9uPlPDuHZuIG9nIGh2aWxlPC9uczE6Q29kZURlc2NyaXB0aW9uPjwvbnMxOlRlcm1pbm9sb2d5PjxuczE6UHJvYmxlbUFyZWE+U8O4dm4gb2cgaHZpbGUgLSBvYnNlcnZhdGlvbjogU292ZXQgdmVkIHRpbHN5bjwvbnMxOlByb2JsZW1BcmVhPjwvbnMxOk51cnNpbmdQcm9ibGVtQXJlYT48bnMxOk51cnNpbmdQcm9ibGVtQXJlYT48bnMxOlRlcm1pbm9sb2d5PjxuczE6Q29kZT5KLTEyMzQ1PC9uczE6Q29kZT48bnMxOkNvZGVTeXN0ZW0+bWVkY29tPC9uczE6Q29kZVN5c3RlbT48bnMxOkNvZGVEZXNjcmlwdGlvbj5WaWRlbiBvZyB1ZHZpa2xpbmc8L25zMTpDb2RlRGVzY3JpcHRpb24+PC9uczE6VGVybWlub2xvZ3k+PG5zMTpQcm9ibGVtQXJlYT5TeWdkb21zaW5kc2lndC92aWRlbnNuaXZlYXU6IE5vZ2VuIHN5Z2RvbXNpbmRzaWd0IDxCcmVhayAvPktvZ25pdGl2IGZvcm3DpWVuIDogTmVkc2F0IGluZGzDpnJpbmdzZXZuZTwvbnMxOlByb2JsZW1BcmVhPjwvbnMxOk51cnNpbmdQcm9ibGVtQXJlYT48bnMxOk51cnNpbmdQcm9ibGVtQXJlYT48bnMxOlRlcm1pbm9sb2d5PjxuczE6Q29kZT5LLTEyMzQ1PC9uczE6Q29kZT48bnMxOkNvZGVTeXN0ZW0+bWVkY29tPC9uczE6Q29kZVN5c3RlbT48bnMxOkNvZGVEZXNjcmlwdGlvbj5VZHNraWxsZWxzZSA8L25zMTpDb2RlRGVzY3JpcHRpb24+PC9uczE6VGVybWlub2xvZ3k+PG5zMTpQcm9ibGVtQXJlYT5TZW5lc3RlIGFmZsO4cmluZyB1bmRlciBpbmRsw6ZnZ2Vsc2UgKGRhdG8pOiAyNC0wMS0yNCA8QnJlYWsgLz5BZmbDuHJpbmc6IEphLCBhZmbDuHJpbmcgaSB2YWd0ZW4gPEJyZWFrIC8+QWZmw7hyaW5nc3Vkc2VlbmRlOiBUeW5kIDxCcmVhayAvPkFmZsO4cmluZ3NmYXJ2ZTogQnJ1biA8QnJlYWsgLz5BZmbDuHJpbmdzbcOmbmdkZTogTWVsbGVtIDxCcmVhayAvPlZhbmRsYWRuaW5nOiBJbmdlbiB2YW5kbGFkbmluZ3Nwcm9ibGVtZXIgPEJyZWFrIC8+SGrDpmxwIHZlZHLDuHJlbmRlIHRvaWxldGJlc8O4ZyAoZG9rLiBpIEFrdHVlbHQgZnVua3Rpb25zbml2ZWF1IC0gb2JzZXJ2YXRpb24pIDogR8OlciBzZWx2IHDDpSB0b2lsZXR0ZXQgPEJyZWFrIC8+VXJpbiA6IEphLCB1cmludWRza2lsbGVsc2UgaSB2YWd0ZW4gPEJyZWFrIC8+VXJpbnVkc2VlbmRlOiBLbGFyLCBHdWw8L25zMTpQcm9ibGVtQXJlYT48L25zMTpOdXJzaW5nUHJvYmxlbUFyZWE+PC9uczE6TnVyc2luZ1Byb2JsZW1BcmVhcz48Umlza09mQ29udGFnaW9uPm5lajwvUmlza09mQ29udGFnaW9uPjxuczE6RGlhZ25vc2VzPjxuczE6RGlhZ25vc2lzPjxuczE6Q29kZT5ERTY2MEc8L25zMTpDb2RlPjxuczE6VHlwZUNvZGU+U0tTZGlhZ25vc2Vrb2RlPC9uczE6VHlwZUNvZGU+PG5zMTpUZXh0PkVrc3RyZW0gZmVkbWUsIEJNSSA1MC01NC45PC9uczE6VGV4dD48L25zMTpEaWFnbm9zaXM+PG5zMTpEaWFnbm9zaXM+PG5zMTpDb2RlPkRKMTg5PC9uczE6Q29kZT48bnMxOlR5cGVDb2RlPlNLU2RpYWdub3Nla29kZTwvbnMxOlR5cGVDb2RlPjxuczE6VGV4dD5QbmV1bW9uaSBVTlM8L25zMTpUZXh0PjwvbnMxOkRpYWdub3Npcz48L25zMTpEaWFnbm9zZXM+PG5zMTpBYmlsaXR5VG9GdW5jdGlvbkF0RGlzY2hhcmdlPjxuczE6TGFzdE1vZGlmaWVkRGF0ZT4yMDI0LTAxLTI0PC9uczE6TGFzdE1vZGlmaWVkRGF0ZT48bnMxOkFiaWxpdHlUb0Z1bmN0aW9uUmVsZXZhbmNlPmlra2VfcmVsZXZhbnQ8L25zMTpBYmlsaXR5VG9GdW5jdGlvblJlbGV2YW5jZT48L25zMTpBYmlsaXR5VG9GdW5jdGlvbkF0RGlzY2hhcmdlPjxuczE6TGF0ZXN0RGVwb3RNZWRpY2F0aW9uPjxuczE6TmFtZU9mRHJ1Zz5TZW1hZ2x1dGlkIChPWkVNUElDKTwvbnMxOk5hbWVPZkRydWc+PG5zMTpEb3NhZ2VGb3JtPjAsMjUgbWcgKGluamVrdGlvbnN2w6Zza2UsIG9wbMO4c25pbmcgaSBmeWxkdCBwZW4pPC9uczE6RG9zYWdlRm9ybT48bnMxOkRydWdTdHJlbmd0aD4wLjI1IG1nPC9uczE6RHJ1Z1N0cmVuZ3RoPjxuczE6Um91dGVPZkFkbWluaXN0cmF0aW9uPlN1Ymt1dGFuIGFudmVuZGVsc2U8L25zMTpSb3V0ZU9mQWRtaW5pc3RyYXRpb24+PG5zMTpMYXRlc3RNZWRpY2F0aW9uPjxEYXRlPjIwMjQtMDEtMjM8L0RhdGU+PFRpbWU+MTU6MDM8L1RpbWU+PC9uczE6TGF0ZXN0TWVkaWNhdGlvbj48L25zMTpMYXRlc3REZXBvdE1lZGljYXRpb24+PG5zMTpEaXNjaGFyZ2VSZWxhdGVkTWVkaWNpbmVJbmZvcm1hdGlvbj48bnMxOkF0dGFjaGVkPjxuczE6RXhwaXJlPjIwMjQtMDEtMjU8L25zMTpFeHBpcmU+PC9uczE6QXR0YWNoZWQ+PG5zMTpSZWNlaXB0PjE8L25zMTpSZWNlaXB0PjxuczE6UGlja1VwT3JEZWxpdmVyeT4xPC9uczE6UGlja1VwT3JEZWxpdmVyeT48bnMxOkRvc2FnZUV4ZW1wdGlvblJlb3JkZXJlZD4xPC9uczE6RG9zYWdlRXhlbXB0aW9uUmVvcmRlcmVkPjwvbnMxOkRpc2NoYXJnZVJlbGF0ZWRNZWRpY2luZUluZm9ybWF0aW9uPjxuczE6RGlldEZpcnN0MjRIb3Vycz48bnMxOkx1bmNoQm94PjA8L25zMTpMdW5jaEJveD48bnMxOlNob3BwaW5nQXREaXNjaGFyZ2U+MDwvbnMxOlNob3BwaW5nQXREaXNjaGFyZ2U+PC9uczE6RGlldEZpcnN0MjRIb3Vycz48RnV0dXJlUGxhbnM+SG9zcGl0YWxldCBoYXIgYmVoYW5kbGluZ3NhbnN2YXIgZm9yIHBhdGllbnRlbiBpbmR0aWwgMjctMDEtMjAyNCAxODozMC4gS29udGFrdHRlbGVmb25udW1tZXIgKHRpbCBicnVnIGZvciBzdW5kaGVkc3Byb2Zlc3Npb25lbGxlKTogNTggNTUgOTYgOTkuIEZyZW10aWRpZ2UgYWZ0YWxlcjogaSBmb2hvbGQgdGlsIEFsbGFucyBzY3JvdHVtIG9nIHVyaW52ZWplIHZpbCB1cm9sb2dlbiBpIFJvc2tpbGRlIHNlIGhhbSBhbWJ1bGFudCBvZyB2aWwgZsOlIGVuIGluZGthbGRlbHNlIHRpbCBkZXR0ZSAuLiAgQmVoYW5kbGluZ3NuaXZlYXU6IEZ1bGQgYmVoYW5kbGluZy4gSnVsaWUgTmVsbGVtYW5uIEJhbmcsIEzDpmdlIGQuIDA1LTA1LTIwMjEgMTQ6NDkuIDwvRnV0dXJlUGxhbnM+PC9uczE6UmVwb3J0T2ZEaXNjaGFyZ2U+PC9uczE6RW1lc3NhZ2U+\",\"externalId\":null,\"ccRecipient\":null,\"copiedMessageId\":null,\"letterType\":null,\"serviceTagCode\":null,\"messageType\":\"MEDCOM_MESSAGE_REPORT_OF_DISCHARGE_1_0_3\",\"versionCode\":\"XD1834C\",\"conversationId\":\"0000000000D483571048CFD73CA89F3B\",\"activityIdentifier\":{\"identifier\":\"MESSAGE:367522\",\"type\":\"MESSAGE\",\"activityId\":367522,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"patient\":{\"id\":1844,\"version\":45,\"firstName\":\"Allan\",\"lastName\":\"Lindeskov\",\"middleName\":null,\"fullName\":\"Allan Lindeskov\",\"fullReversedName\":\"Lindeskov, Allan\",\"homePhoneNumber\":\"20685921\",\"mobilePhoneNumber\":\"20685921\",\"workPhoneNumber\":null,\"patientIdentifier\":{\"type\":\"cpr\",\"identifier\":\"290759-0979\",\"managedExternally\":false,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"currentAddress\":{\"addressLine1\":\"Abelsvej 198\",\"addressLine2\":null,\"addressLine3\":null,\"addressLine4\":null,\"addressLine5\":null,\"administrativeAreaCode\":\"329\",\"countryCode\":\"dk\",\"postalCode\":\"4100\",\"postalDistrict\":\"Ringsted\",\"restricted\":false,\"geoCoordinates\":{\"x\":null,\"y\":null,\"present\":false},\"startDate\":null,\"endDate\":null,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/address/countries\"},\"searchPostalDistrict\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/address/postalDistrict\"}}},\"currentAddressIndicator\":\"PRIMARY_ADDRESS\",\"age\":64,\"monthsAfterBirthday\":6,\"gender\":\"MALE\",\"patientStatus\":\"FINAL\",\"patientState\":{\"id\":21,\"version\":0,\"name\":\"Aktiv\",\"color\":\"000000\",\"type\":{\"id\":\"ACTIVE\",\"name\":\"Aktiv\",\"abbreviation\":\"\",\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"active\":true,\"defaultObject\":true,\"citizenRegistryConfiguration\":{\"updateEnabled\":true,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"_links\":{\"medcomRecipient\":null,\"self\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patientStates/21\"},\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"telephonesRestricted\":false,\"imageId\":null,\"_links\":{\"medcomRecipient\":null,\"self\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/1844\"},\"patientOverview\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/1844/overview\"},\"citizenOverviewForms\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/1844/citizenOverviewForms\"},\"measurementData\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/1844/measurementInstructions/data/pages\"},\"measurementInstructions\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/1844/measurementInstructions/instructions\"},\"patientConditions\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/1844/conditions\"},\"patientOrganizations\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/1844/organizations\"},\"activityLinksPrototypes\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/1844/activitylinks/prototypes\"},\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"pathwayAssociation\":{\"placement\":{\"name\":\"MedCom Myndighedsenheden\",\"programPathwayId\":4355,\"pathwayTypeId\":13,\"parentPathwayId\":null,\"patientPathwayId\":31620,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"parentReferenceId\":null,\"referenceId\":1035194,\"canAssociateWithPathway\":true,\"canAssociateWithPatient\":false,\"associatedWithPatient\":false,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/1844/pathways/31620/availablePathwayAssociations\"},\"availableRootProgramPathways\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/1844/availableProgramPathways\"},\"availablePathwayPlacements\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/1844/pathways/availablePathwayPlacements\"},\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"body\":null,\"priority\":null,\"previousMessageId\":null,\"_links\":{\"medcomRecipient\":null,\"self\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/367522\"},\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/audit/medcom/367522\"},\"relatedCommunication\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/related/367522\"},\"activeAssignments\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/assignments//mobile/ringsted/v2/assignments/patient/1844/assignments/active?activity=MESSAGE:367522\"},\"assignments\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/assignments//mobile/ringsted/v2/assignments/patient/1844/assignments?activity=MESSAGE:367522\"},\"availableAssignmentTypes\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/assignments//mobile/ringsted/v2/assignments/patient/1844/assignmentTypes/active?activity=MESSAGE:367522&activityType=MESSAGE&messageTypeType=MEDCOM&messageType=MEDCOM_MESSAGE_REPORT_OF_DISCHARGE_1_0_3&messageStateDirection=INCOMING\"},\"autoAssignmentsPrototype\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/assignments//mobile/ringsted/v2/assignments/auto/prototype?activity=MESSAGE:367522&activityType=MESSAGE&messageTypeType=MEDCOM&messageType=MEDCOM_MESSAGE_REPORT_OF_DISCHARGE_1_0_3&messageStateDirection=INCOMING&messageStatus=FINAL\"},\"update\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/367522\"},\"transformedBody\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/367522/transformedBody\"},\"print\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/367522/print\"},\"reply\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/367522/reply\"},\"replyTo\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/replyTo/367522?identifierType=SYGEHUSAFDELINGSNUMMER&identifierValue=3800R20\"},\"archive\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/archived/367522\"},\"availableCountries\":null,\"searchPostalDistrict\":null},\"level\":null}\r\n");
            baseObjects.Add("{\"type\":\"medcom\",\"id\":367474,\"version\":3,\"sender\":{\"type\":\"medcomMessageDetailsBased\",\"id\":367474,\"name\":\"Rigshospitalet - Inge Lehmanns Vej 5 - 2100 København Ø - EAN: 5790000188857\",\"recipientAddress\":null,\"recipientType\":\"MESSAGE_SENDER\",\"_links\":{\"medcomRecipient\":{\"href\":\"/api/core/mobile/ringsted/v2/medcomRecipients/medcomSender/367474\"},\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"recipient\":{\"type\":\"medcomMessageDetailsBased\",\"id\":367474,\"name\":\"Ringsted Kommune - Zahlesvej 18 - 4100 Ringsted - EAN: 5790001353858\",\"recipientAddress\":null,\"recipientType\":\"MESSAGE_RECIPIENT\",\"_links\":{\"medcomRecipient\":{\"href\":\"/api/core/mobile/ringsted/v2/medcomRecipients/medcomRecipient/367474\"},\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"subject\":\"Udskrivningsrapport\",\"date\":\"2024-01-24T14:12:00+01:00\",\"state\":{\"direction\":\"INCOMING\",\"status\":\"FINAL\",\"response\":\"NOT_AWAITING\",\"handshake\":\"NO\",\"processed\":\"PROCESSED\",\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"customer\":null,\"raw\":\"PD94bWwgdmVyc2lvbj0iMS4wIiBlbmNvZGluZz0iVVRGLTgiPz48bnMxOkVtZXNzYWdlIHhtbG5zPSJ1cm46b2lvOm1lZGNvbTptdW5pY2lwYWxpdHk6MS4wLjAiIHhtbG5zOm5zMT0idXJuOm9pbzptZWRjb206bXVuaWNpcGFsaXR5OjEuMC4zIj48RW52ZWxvcGU+PFNlbnQ+PERhdGU+MjAyNC0wMS0yNDwvRGF0ZT48VGltZT4xNDowNzwvVGltZT48L1NlbnQ+PElkZW50aWZpZXI+RVBJQzY0MTUyNTM2ODwvSWRlbnRpZmllcj48QWNrbm93bGVkZ2VtZW50Q29kZT5wbHVzcG9zaXRpdmt2aXR0PC9BY2tub3dsZWRnZW1lbnRDb2RlPjwvRW52ZWxvcGU+PG5zMTpSZXBvcnRPZkRpc2NoYXJnZT48bnMxOkxldHRlcj48bnMxOklkZW50aWZpZXI+RVBJQzY0MTUyNTM2ODwvbnMxOklkZW50aWZpZXI+PG5zMTpWZXJzaW9uQ29kZT5YRDE4MzRDPC9uczE6VmVyc2lvbkNvZGU+PG5zMTpTdGF0aXN0aWNhbENvZGU+WERJUzE4PC9uczE6U3RhdGlzdGljYWxDb2RlPjxuczE6QXV0aG9yaXNhdGlvbj48RGF0ZT4yMDI0LTAxLTI0PC9EYXRlPjxUaW1lPjE0OjA3PC9UaW1lPjwvbnMxOkF1dGhvcmlzYXRpb24+PG5zMTpUeXBlQ29kZT5YRElTMTg8L25zMTpUeXBlQ29kZT48bnMxOkVwaXNvZGVPZkNhcmVJZGVudGlmaWVyPjAwMDAwMDAwMDBCODkzMkFCNDUyNkJBRDBDNzkxRkQ1PC9uczE6RXBpc29kZU9mQ2FyZUlkZW50aWZpZXI+PC9uczE6TGV0dGVyPjxTZW5kZXI+PEVBTklkZW50aWZpZXI+NTc5MDAwMDE4ODg1NzwvRUFOSWRlbnRpZmllcj48SWRlbnRpZmllcj4xMzAxNjIyPC9JZGVudGlmaWVyPjxJZGVudGlmaWVyQ29kZT5zeWdlaHVzYWZkZWxpbmdzbnVtbWVyPC9JZGVudGlmaWVyQ29kZT48T3JnYW5pc2F0aW9uTmFtZT5SaWdzaG9zcGl0YWxldDwvT3JnYW5pc2F0aW9uTmFtZT48RGVwYXJ0bWVudE5hbWU+TWVkaWNpbnNrIGtsaW5payBmb3IgTWF2ZS0sIFRhcm0tICo8L0RlcGFydG1lbnROYW1lPjxVbml0TmFtZT5UYXJtc3ZpZ3Qgb2cgTGV2ZXJzeWdkb21tZSwgc2VuZ2VhKjwvVW5pdE5hbWU+PFN0cmVldE5hbWU+SW5nZSBMZWhtYW5ucyBWZWogNTwvU3RyZWV0TmFtZT48RGlzdHJpY3ROYW1lPkvDuGJlbmhhdm4gw5g8L0Rpc3RyaWN0TmFtZT48UG9zdENvZGVJZGVudGlmaWVyPjIxMDA8L1Bvc3RDb2RlSWRlbnRpZmllcj48VGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+MzU0NTMxMjM8L1RlbGVwaG9uZVN1YnNjcmliZXJJZGVudGlmaWVyPjxTaWduZWRCeT48SWRlbnRpZmllcj4xMzAxNjIyPC9JZGVudGlmaWVyPjxJZGVudGlmaWVyQ29kZT5zeWdlaHVzYWZkZWxpbmdzbnVtbWVyPC9JZGVudGlmaWVyQ29kZT48UGVyc29uR2l2ZW5OYW1lPk5pa2l0YTwvUGVyc29uR2l2ZW5OYW1lPjxQZXJzb25TdXJuYW1lTmFtZT5CaXJrZWRhbDwvUGVyc29uU3VybmFtZU5hbWU+PFBlcnNvblRpdGxlPlN5Z2VwbGVqZXJza2U8L1BlcnNvblRpdGxlPjwvU2lnbmVkQnk+PE1lZGljYWxTcGVjaWFsaXR5Q29kZT5pbnRlcm5fbWVkaWNpbl9zeWdlaHVzPC9NZWRpY2FsU3BlY2lhbGl0eUNvZGU+PENvbnRhY3RJbmZvcm1hdGlvbj48Q29udGFjdE5hbWU+YW5uZXNvZmllPC9Db250YWN0TmFtZT48VGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+MzU0NTMxMjQ8L1RlbGVwaG9uZVN1YnNjcmliZXJJZGVudGlmaWVyPjwvQ29udGFjdEluZm9ybWF0aW9uPjwvU2VuZGVyPjxSZWNlaXZlcj48RUFOSWRlbnRpZmllcj41NzkwMDAxMzUzODU4PC9FQU5JZGVudGlmaWVyPjxJZGVudGlmaWVyPjMyOTwvSWRlbnRpZmllcj48SWRlbnRpZmllckNvZGU+a29tbXVuZW51bW1lcjwvSWRlbnRpZmllckNvZGU+PE9yZ2FuaXNhdGlvbk5hbWU+UmluZ3N0ZWQgS29tbXVuZTwvT3JnYW5pc2F0aW9uTmFtZT48RGVwYXJ0bWVudE5hbWU+U29jaWFsIG9nIFN1bmRoZWQ8L0RlcGFydG1lbnROYW1lPjxVbml0TmFtZT5IamVtbWVzeWdlcGxlamVuLCBSaW5nc3RlZCBLb21tdW5lPC9Vbml0TmFtZT48U3RyZWV0TmFtZT5aYWhsZXN2ZWogMTg8L1N0cmVldE5hbWU+PERpc3RyaWN0TmFtZT5SaW5nc3RlZDwvRGlzdHJpY3ROYW1lPjxQb3N0Q29kZUlkZW50aWZpZXI+NDEwMDwvUG9zdENvZGVJZGVudGlmaWVyPjwvUmVjZWl2ZXI+PFByYWN0aXRpb25lcj48SWRlbnRpZmllcj4wMjUxOTQ8L0lkZW50aWZpZXI+PElkZW50aWZpZXJDb2RlPnlkZXJudW1tZXI8L0lkZW50aWZpZXJDb2RlPjxQZXJzb25OYW1lPkFsbGVzIEzDpmdlaHVzIFJpbmdzdGVkPC9QZXJzb25OYW1lPjwvUHJhY3RpdGlvbmVyPjxQYXRpZW50PjxDaXZpbFJlZ2lzdHJhdGlvbk51bWJlcj4xNDA0NjIwNzc3PC9DaXZpbFJlZ2lzdHJhdGlvbk51bWJlcj48UGVyc29uU3VybmFtZU5hbWU+SmVuc2VuPC9QZXJzb25TdXJuYW1lTmFtZT48UGVyc29uR2l2ZW5OYW1lPkhlbnJpayBCbzwvUGVyc29uR2l2ZW5OYW1lPjxTdHJlZXROYW1lPkVuZ2h1c2VuZSAxMTU8L1N0cmVldE5hbWU+PFN1YnVyYk5hbWU+SnlzdHJ1cDwvU3VidXJiTmFtZT48RGlzdHJpY3ROYW1lPkp5c3RydXAgTWlkdHNqPC9EaXN0cmljdE5hbWU+PFBvc3RDb2RlSWRlbnRpZmllcj40MTc0PC9Qb3N0Q29kZUlkZW50aWZpZXI+PE9jY3VwYXRpb24+R3Jvc3Npc3Q8L09jY3VwYXRpb24+PFRlbGVwaG9uZVN1YnNjcmliZXJJZGVudGlmaWVyPjIyMjE4MTAwPC9UZWxlcGhvbmVTdWJzY3JpYmVySWRlbnRpZmllcj48L1BhdGllbnQ+PG5zMTpSZWxhdGl2ZXM+PG5zMTpSZWxhdGl2ZT48bnMxOlJlbGF0aW9uQ29kZT5iYXJuPC9uczE6UmVsYXRpb25Db2RlPjxQZXJzb25TdXJuYW1lTmFtZT5LYXJpbmE8L1BlcnNvblN1cm5hbWVOYW1lPjxQZXJzb25HaXZlbk5hbWU+SmVuc2VuPC9QZXJzb25HaXZlbk5hbWU+PENvZGVkVGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+PFRlbGVwaG9uZUNvZGU+cHJpdmF0PC9UZWxlcGhvbmVDb2RlPjxUZWxlcGhvbmVTdWJzY3JpYmVySWRlbnRpZmllcj4yMDY2NTAwMDwvVGVsZXBob25lU3Vic2NyaWJlcklkZW50aWZpZXI+PC9Db2RlZFRlbGVwaG9uZVN1YnNjcmliZXJJZGVudGlmaWVyPjxuczE6SW5mb3JtZWRSZWxhdGl2ZT4wPC9uczE6SW5mb3JtZWRSZWxhdGl2ZT48L25zMTpSZWxhdGl2ZT48bnMxOkNvbW1lbnQ+UHQgaW5mb3JtZXJlciBzZWx2IHDDpXLDuHJlbmRlPC9uczE6Q29tbWVudD48L25zMTpSZWxhdGl2ZXM+PEFkbWlzc2lvbj48RGF0ZT4yMDI0LTAxLTE1PC9EYXRlPjxUaW1lPjE2OjM3PC9UaW1lPjwvQWRtaXNzaW9uPjxFbmRPZlRyZWF0bWVudD48RGF0ZT4yMDI0LTAxLTI0PC9EYXRlPjxUaW1lPjEyOjAwPC9UaW1lPjwvRW5kT2ZUcmVhdG1lbnQ+PERpc2NoYXJnZT48RGF0ZT4yMDI0LTAxLTI0PC9EYXRlPjxUaW1lPjEyOjAwPC9UaW1lPjwvRGlzY2hhcmdlPjxDYXVzZU9mQWRtaXNzaW9uPk9CUyBibG9kZm9yZ2lmdG5pbmcgKyBzdG9taXN0b3A8L0NhdXNlT2ZBZG1pc3Npb24+PG5zMTpOdXJzaW5nUHJvYmxlbUFyZWFzPjxuczE6TnVyc2luZ1Byb2JsZW1BcmVhPjxuczE6VGVybWlub2xvZ3k+PG5zMTpDb2RlPkItMTIzNDU8L25zMTpDb2RlPjxuczE6Q29kZVN5c3RlbT5tZWRjb208L25zMTpDb2RlU3lzdGVtPjxuczE6Q29kZURlc2NyaXB0aW9uPkVybsOmcmluZzwvbnMxOkNvZGVEZXNjcmlwdGlvbj48L25zMTpUZXJtaW5vbG9neT48bnMxOlByb2JsZW1BcmVhPktvc3Rmb3JtOiBOb3JtYWxrb3N0L0h2ZXJkYWdza29zdDwvbnMxOlByb2JsZW1BcmVhPjwvbnMxOk51cnNpbmdQcm9ibGVtQXJlYT48bnMxOk51cnNpbmdQcm9ibGVtQXJlYT48bnMxOlRlcm1pbm9sb2d5PjxuczE6Q29kZT5DLTEyMzQ1PC9uczE6Q29kZT48bnMxOkNvZGVTeXN0ZW0+bWVkY29tPC9uczE6Q29kZVN5c3RlbT48bnMxOkNvZGVEZXNjcmlwdGlvbj5IdWQgb2cgc2xpbWhpbmRlcjwvbnMxOkNvZGVEZXNjcmlwdGlvbj48L25zMTpUZXJtaW5vbG9neT48bnMxOlByb2JsZW1BcmVhPlJpc2lrbyBmb3IgdWR2aWtsaW5nIGFmIHRyeWtza2FkZS8tc8Olcj86IE5lajwvbnMxOlByb2JsZW1BcmVhPjwvbnMxOk51cnNpbmdQcm9ibGVtQXJlYT48bnMxOk51cnNpbmdQcm9ibGVtQXJlYT48bnMxOlRlcm1pbm9sb2d5PjxuczE6Q29kZT5ELTEyMzQ1PC9uczE6Q29kZT48bnMxOkNvZGVTeXN0ZW0+bWVkY29tPC9uczE6Q29kZVN5c3RlbT48bnMxOkNvZGVEZXNjcmlwdGlvbj5Lb21tdW5pa2F0aW9uL25ldXJvbG9naTwvbnMxOkNvZGVEZXNjcmlwdGlvbj48L25zMTpUZXJtaW5vbG9neT48bnMxOlByb2JsZW1BcmVhPkJldmlkc3RoZWRzbml2ZWF1OiBWw6VnZW4sIHJlYWdlcmVyIG5vcm1hbHQgZWxsZXIgbm9ybWFsIHPDuHZuPC9uczE6UHJvYmxlbUFyZWE+PC9uczE6TnVyc2luZ1Byb2JsZW1BcmVhPjwvbnMxOk51cnNpbmdQcm9ibGVtQXJlYXM+PFJpc2tPZkNvbnRhZ2lvbj5OZWo8L1Jpc2tPZkNvbnRhZ2lvbj48bnMxOkRpYWdub3Nlcz48bnMxOkRpYWdub3Npcz48bnMxOkNvZGU+REs5MTJCPC9uczE6Q29kZT48bnMxOlR5cGVDb2RlPlNLU2RpYWdub3Nla29kZTwvbnMxOlR5cGVDb2RlPjxuczE6VGV4dD5Lb3J0dGFybXNzeW5kcm9tPC9uczE6VGV4dD48L25zMTpEaWFnbm9zaXM+PC9uczE6RGlhZ25vc2VzPjxuczE6QWJpbGl0eVRvRnVuY3Rpb25BdERpc2NoYXJnZT48bnMxOkxhc3RNb2RpZmllZERhdGU+MjAyNC0wMS0yMjwvbnMxOkxhc3RNb2RpZmllZERhdGU+PG5zMTpBYmlsaXR5VG9GdW5jdGlvblJlbGV2YW5jZT5pa2tlX3JlbGV2YW50PC9uczE6QWJpbGl0eVRvRnVuY3Rpb25SZWxldmFuY2U+PC9uczE6QWJpbGl0eVRvRnVuY3Rpb25BdERpc2NoYXJnZT48bnMxOkNhdmU+QkVUQS1MQUNUQU0gQU5USUJBS1RFUklDQSwgUEVOSUNJTExJTkVSOyBJa2tlIGFsdm9ybGlnZTsgR2FzdHJvaW50ZXN0aW5hbGUgc3ltcHRvbWVyOyBBbmRlbiByZWFrdGlvbjsgS3ZhbG1lIG9nIG9wa2FzdDxCcmVhayAvPiBQUkVETklTT0xPTjsgSWtrZSBhbHZvcmxpZ2U7IEFuZGVuOyBBbmRlbiByZWFrdGlvbjsgaHVtw7hyc3ZpbmduaW5nZXI8QnJlYWsgLz4gSU5GTElYSU1BQjsgSWtrZSBhbHZvcmxpZ2U7IEFuZGVuOyBTeW5rb3BlOyBCbGV2IGTDpXJsaWcgb2cgYmVzdmltZWRlLjxCcmVhayAvPiBESUFaRVBBTTsgSWtrZSBhbHZvcmxpZ2U7IEFuZGVuOyBBbmRlbiByZWFrdGlvbjsgUsO4ZHNwcsOmbmd0ZSDDuGpuZTxCcmVhayAvPiA8L25zMTpDYXZlPjxuczE6TGF0ZXN0RGVwb3RNZWRpY2F0aW9uPjxuczE6TmFtZU9mRHJ1Zz52ZWRvbGl6dW1hYiAoRU5UWVZJTykgTmFDbCo8L25zMTpOYW1lT2ZEcnVnPjxuczE6RG9zYWdlRm9ybT4zMDAgbWd8OSBtZy9tbCAocHVsdmVyIHRpbCBrb25jZW50cmF0IHRpbCBpbmZ1c2lvbnN2w6Zza2UsIG9wbMO4c25pbmcgaW5mdXNpb25zdsOmc2tlLCBvcGzDuHNuaSo8L25zMTpEb3NhZ2VGb3JtPjxuczE6Um91dGVPZkFkbWluaXN0cmF0aW9uPkludHJhdmVuw7hzIGFudmVuZGVsc2U8L25zMTpSb3V0ZU9mQWRtaW5pc3RyYXRpb24+PC9uczE6TGF0ZXN0RGVwb3RNZWRpY2F0aW9uPjxuczE6TGF0ZXN0RGVwb3RNZWRpY2F0aW9uPjxuczE6TmFtZU9mRHJ1Zz5oeWRyb3hvY29iYWxhbWluIChIWURST1hPQ09CQUxBTUlOKjwvbnMxOk5hbWVPZkRydWc+PG5zMTpEb3NhZ2VGb3JtPjEgbWcvbWwgKGluamVrdGlvbnN2w6Zza2UsIG9wbMO4c25pbmcpPC9uczE6RG9zYWdlRm9ybT48bnMxOlJvdXRlT2ZBZG1pbmlzdHJhdGlvbj5JbnRyYW11c2t1bMOmciBhbnZlbmRlbHNlPC9uczE6Um91dGVPZkFkbWluaXN0cmF0aW9uPjwvbnMxOkxhdGVzdERlcG90TWVkaWNhdGlvbj48bnMxOkxhdGVzdERlcG90TWVkaWNhdGlvbj48bnMxOk5hbWVPZkRydWc+ZmVudGFueWwgKE1BVFJJRkVOKTwvbnMxOk5hbWVPZkRydWc+PG5zMTpEb3NhZ2VGb3JtPjEwMCBtaWtyb2dyYW0vdGltZSAoZGVwb3RwbGFzdHJlKTwvbnMxOkRvc2FnZUZvcm0+PG5zMTpEcnVnU3RyZW5ndGg+MSBwbGFzdGVyPC9uczE6RHJ1Z1N0cmVuZ3RoPjxuczE6Um91dGVPZkFkbWluaXN0cmF0aW9uPlRyYW5zZGVybWFsIGFudmVuZGVsc2U8L25zMTpSb3V0ZU9mQWRtaW5pc3RyYXRpb24+PG5zMTpMYXRlc3RNZWRpY2F0aW9uPjxEYXRlPjIwMjQtMDEtMjQ8L0RhdGU+PFRpbWU+MDg6MDA8L1RpbWU+PC9uczE6TGF0ZXN0TWVkaWNhdGlvbj48L25zMTpMYXRlc3REZXBvdE1lZGljYXRpb24+PG5zMTpMYXRlc3REZXBvdE1lZGljYXRpb24+PG5zMTpOYW1lT2ZEcnVnPm5hdHJpdW1rbG9yaWQtZ2x1a29zZSBpc290IDErMiAqPC9uczE6TmFtZU9mRHJ1Zz48bnMxOkRvc2FnZUZvcm0+IChpbmZ1c2lvbnN2w6Zza2UpPC9uczE6RG9zYWdlRm9ybT48bnMxOkRydWdTdHJlbmd0aD4xMDAwIG1sPC9uczE6RHJ1Z1N0cmVuZ3RoPjxuczE6Um91dGVPZkFkbWluaXN0cmF0aW9uPkludHJhdmVuw7hzIGFudmVuZGVsc2U8L25zMTpSb3V0ZU9mQWRtaW5pc3RyYXRpb24+PG5zMTpMYXRlc3RNZWRpY2F0aW9uPjxEYXRlPjIwMjQtMDEtMjM8L0RhdGU+PFRpbWU+MjI6MzU8L1RpbWU+PC9uczE6TGF0ZXN0TWVkaWNhdGlvbj48L25zMTpMYXRlc3REZXBvdE1lZGljYXRpb24+PG5zMTpMYXRlc3REZXBvdE1lZGljYXRpb24+PG5zMTpOYW1lT2ZEcnVnPm5hdHJpdW1rbG9yaWQtZ2x1a29zZSBpc290IDErMiAqPC9uczE6TmFtZU9mRHJ1Zz48bnMxOkRvc2FnZUZvcm0+IChpbmZ1c2lvbnN2w6Zza2UpPC9uczE6RG9zYWdlRm9ybT48bnMxOkRydWdTdHJlbmd0aD4yMDAwIG1sPC9uczE6RHJ1Z1N0cmVuZ3RoPjxuczE6Um91dGVPZkFkbWluaXN0cmF0aW9uPkludHJhdmVuw7hzIGFudmVuZGVsc2U8L25zMTpSb3V0ZU9mQWRtaW5pc3RyYXRpb24+PG5zMTpMYXRlc3RNZWRpY2F0aW9uPjxEYXRlPjIwMjQtMDEtMjI8L0RhdGU+PFRpbWU+MjA6NTE8L1RpbWU+PC9uczE6TGF0ZXN0TWVkaWNhdGlvbj48L25zMTpMYXRlc3REZXBvdE1lZGljYXRpb24+PG5zMTpMYXRlc3RQbk1lZGljYXRpb24+PG5zMTpOYW1lT2ZEcnVnPm1vcnBoaW4gKE1PUkZJTiBTQUQpPC9uczE6TmFtZU9mRHJ1Zz48bnMxOkRvc2FnZUZvcm0+MTAgbWcgKHRhYmxldHRlcik8L25zMTpEb3NhZ2VGb3JtPjxuczE6RHJ1Z1N0cmVuZ3RoPjMwIG1nPC9uczE6RHJ1Z1N0cmVuZ3RoPjxuczE6Um91dGVPZkFkbWluaXN0cmF0aW9uPk9yYWwgYW52ZW5kZWxzZTwvbnMxOlJvdXRlT2ZBZG1pbmlzdHJhdGlvbj48bnMxOkxhdGVzdE1lZGljYXRpb24+PERhdGU+MjAyNC0wMS0yMzwvRGF0ZT48VGltZT4yMTo1MDwvVGltZT48L25zMTpMYXRlc3RNZWRpY2F0aW9uPjwvbnMxOkxhdGVzdFBuTWVkaWNhdGlvbj48bnMxOkRpZXRGaXJzdDI0SG91cnM+PG5zMTpMdW5jaEJveD4wPC9uczE6THVuY2hCb3g+PG5zMTpTaG9wcGluZ0F0RGlzY2hhcmdlPjA8L25zMTpTaG9wcGluZ0F0RGlzY2hhcmdlPjwvbnMxOkRpZXRGaXJzdDI0SG91cnM+PC9uczE6UmVwb3J0T2ZEaXNjaGFyZ2U+PC9uczE6RW1lc3NhZ2U+\",\"externalId\":null,\"ccRecipient\":null,\"copiedMessageId\":null,\"letterType\":null,\"serviceTagCode\":null,\"messageType\":\"MEDCOM_MESSAGE_REPORT_OF_DISCHARGE_1_0_3\",\"versionCode\":\"XD1834C\",\"conversationId\":\"0000000000B8932AB4526BAD0C791FD5\",\"activityIdentifier\":{\"identifier\":\"MESSAGE:367474\",\"type\":\"MESSAGE\",\"activityId\":367474,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"patient\":{\"id\":10512,\"version\":34,\"firstName\":\"Henrik Bo\",\"lastName\":\"Jensen\",\"middleName\":null,\"fullName\":\"Henrik Bo Jensen\",\"fullReversedName\":\"Jensen, Henrik Bo\",\"homePhoneNumber\":null,\"mobilePhoneNumber\":\"22218100\",\"workPhoneNumber\":null,\"patientIdentifier\":{\"type\":\"cpr\",\"identifier\":\"140462-0777\",\"managedExternally\":false,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"currentAddress\":{\"addressLine1\":\"Enghusene 115\",\"addressLine2\":null,\"addressLine3\":\"Jystrup\",\"addressLine4\":null,\"addressLine5\":null,\"administrativeAreaCode\":\"329\",\"countryCode\":\"dk\",\"postalCode\":\"4174\",\"postalDistrict\":\"Jystrup Midtsj\",\"restricted\":false,\"geoCoordinates\":{\"x\":null,\"y\":null,\"present\":false},\"startDate\":null,\"endDate\":null,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/address/countries\"},\"searchPostalDistrict\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/address/postalDistrict\"}}},\"currentAddressIndicator\":\"PRIMARY_ADDRESS\",\"age\":61,\"monthsAfterBirthday\":9,\"gender\":\"MALE\",\"patientStatus\":\"FINAL\",\"patientState\":{\"id\":21,\"version\":0,\"name\":\"Aktiv\",\"color\":\"000000\",\"type\":{\"id\":\"ACTIVE\",\"name\":\"Aktiv\",\"abbreviation\":\"\",\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"active\":true,\"defaultObject\":true,\"citizenRegistryConfiguration\":{\"updateEnabled\":true,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"_links\":{\"medcomRecipient\":null,\"self\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patientStates/21\"},\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"telephonesRestricted\":false,\"imageId\":null,\"_links\":{\"medcomRecipient\":null,\"self\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/10512\"},\"patientOverview\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/10512/overview\"},\"citizenOverviewForms\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/10512/citizenOverviewForms\"},\"measurementData\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/10512/measurementInstructions/data/pages\"},\"measurementInstructions\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/10512/measurementInstructions/instructions\"},\"patientConditions\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/10512/conditions\"},\"patientOrganizations\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/10512/organizations\"},\"activityLinksPrototypes\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/10512/activitylinks/prototypes\"},\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"pathwayAssociation\":{\"placement\":{\"name\":\"MedCom Myndighedsenheden\",\"programPathwayId\":4355,\"pathwayTypeId\":13,\"parentPathwayId\":null,\"patientPathwayId\":37546,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"parentReferenceId\":null,\"referenceId\":1035005,\"canAssociateWithPathway\":true,\"canAssociateWithPatient\":false,\"associatedWithPatient\":false,\"_links\":{\"medcomRecipient\":null,\"self\":null,\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/10512/pathways/37546/availablePathwayAssociations\"},\"availableRootProgramPathways\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/10512/availableProgramPathways\"},\"availablePathwayPlacements\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patients/10512/pathways/availablePathwayPlacements\"},\"audit\":null,\"relatedCommunication\":null,\"activeAssignments\":null,\"assignments\":null,\"availableAssignmentTypes\":null,\"autoAssignmentsPrototype\":null,\"update\":null,\"transformedBody\":null,\"print\":null,\"reply\":null,\"replyTo\":null,\"archive\":null,\"availableCountries\":null,\"searchPostalDistrict\":null}},\"body\":null,\"priority\":null,\"previousMessageId\":null,\"_links\":{\"medcomRecipient\":null,\"self\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/367474\"},\"patientOverview\":null,\"citizenOverviewForms\":null,\"measurementData\":null,\"measurementInstructions\":null,\"patientConditions\":null,\"patientOrganizations\":null,\"activityLinksPrototypes\":null,\"availablePathwayAssociation\":null,\"availableRootProgramPathways\":null,\"availablePathwayPlacements\":null,\"audit\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/audit/medcom/367474\"},\"relatedCommunication\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/related/367474\"},\"activeAssignments\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/assignments//mobile/ringsted/v2/assignments/patient/10512/assignments/active?activity=MESSAGE:367474\"},\"assignments\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/assignments//mobile/ringsted/v2/assignments/patient/10512/assignments?activity=MESSAGE:367474\"},\"availableAssignmentTypes\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/assignments//mobile/ringsted/v2/assignments/patient/10512/assignmentTypes/active?activity=MESSAGE:367474&activityType=MESSAGE&messageTypeType=MEDCOM&messageType=MEDCOM_MESSAGE_REPORT_OF_DISCHARGE_1_0_3&messageStateDirection=INCOMING\"},\"autoAssignmentsPrototype\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/assignments//mobile/ringsted/v2/assignments/auto/prototype?activity=MESSAGE:367474&activityType=MESSAGE&messageTypeType=MEDCOM&messageType=MEDCOM_MESSAGE_REPORT_OF_DISCHARGE_1_0_3&messageStateDirection=INCOMING&messageStatus=FINAL\"},\"update\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/367474\"},\"transformedBody\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/367474/transformedBody\"},\"print\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/367474/print\"},\"reply\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/367474/reply\"},\"replyTo\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/replyTo/367474?identifierType=SYGEHUSAFDELINGSNUMMER&identifierValue=1301622\"},\"archive\":{\"href\":\"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/medcoms/archived/367474\"},\"availableCountries\":null,\"searchPostalDistrict\":null},\"level\":null}\r\n");

            for (int i = 1; i < baseObjects.Count; i++)
            {
                string stringObjectToDeserialze = baseObjects.ElementAt(i);
                ReferencedObject_Base_Root chosenMedComMessage = JsonConvert.DeserializeObject<ReferencedObject_Base_Root>(stringObjectToDeserialze);
            

                #endregion hardcoded baseobjects
                string transformedBodyHTML = api.GetTransformedBodyHTML(chosenMedComMessage);
                TransformedBody_Root transformedBody = new TransformedBody_Root();
                HtmlHandler handler = new HtmlHandler();



                GetDischargeReportData_Relatives(api, transformedBody, handler, transformedBodyHTML);
                GetDischargeReportData_CurrentAdmission(api, transformedBody, handler, transformedBodyHTML);
                GetDischargeReportData_Diagnoses(api, transformedBody, handler, transformedBodyHTML);
                GetDischargeReportData_FunctionalAbilitiesAtDischarge(api, transformedBody, handler, transformedBodyHTML);
                GetDischargeReportData_NursingProfessionalProblemAreas(api, transformedBody, handler, transformedBodyHTML);
                GetDischargeReportData_MostRecentMedicationAdministration(api, transformedBody, handler, transformedBodyHTML);
                GetDischargeReportData_MedicationInformationRelatedToDischarge(api, transformedBody, handler, transformedBodyHTML);
                GetDischargeReportData_AgreementsRegardingDietFirstDayAfterDischarge(api, transformedBody, handler, transformedBodyHTML);
                GetDischargeReportData_FutureAgreements(api, transformedBody, handler, transformedBodyHTML);
            }
        }

        private void GetDischargeReportData_FutureAgreements(NexusAPI api, TransformedBody_Root transformedBody, HtmlHandler handler, string transformedBodyHTML)
        {
            string htmlResult = handler.GetResultFromHtml(transformedBodyHTML, "Fremtidige aftaler");
            var domObject = handler.CreateDomObject(htmlResult).FirstElement();
            int numberOfElements = domObject.ChildElements.Count();

            transformedBody.FutureAgreements = domObject.ChildElements.ElementAt(1).ChildElements.ElementAt(0).FirstChild.ToString();
        }

        private void GetDischargeReportData_AgreementsRegardingDietFirstDayAfterDischarge(NexusAPI api, TransformedBody_Root transformedBody, HtmlHandler handler, string transformedBodyHTML)
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

        private void GetDischargeReportData_MedicationInformationRelatedToDischarge(NexusAPI api, TransformedBody_Root transformedBody, HtmlHandler handler, string transformedBodyHTML)
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
                            transformedBody.MedicationInformationRelatedToDischarge.CommentsForEnclosedMedication = domObject.ChildElements.ElementAt(i+1).ChildElements.ElementAt(0).FirstChild.ToString();
                            i++; // We add 1 to i as the next element in the iteration will be the value <tr> we just added to the transformedBody.MedicationInformationRelatedToDischarge.CommentsForEnclosedMedication property
                            break;
                        default:
                            throw new Exception("Header of \"" + informationTitle + "\" can't be handled. Please add it to GetDischargeReportData_MedicationInformationRelatedToDischarge.");
                    }
                }
            }
        }

        private void GetDischargeReportData_MostRecentMedicationAdministration(NexusAPI api, TransformedBody_Root transformedBody, HtmlHandler handler, string transformedBodyHTML)
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

        
        private void GetDischargeReportData_NursingProfessionalProblemAreas(NexusAPI api, TransformedBody_Root transformedBody, HtmlHandler handler, string transformedBodyHTML)
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

        private void GetDischargeReportData_FunctionalAbilitiesAtDischarge(NexusAPI api, TransformedBody_Root transformedBody, HtmlHandler handler, string transformedBodyHTML)
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

        private void GetDischargeReportData_Diagnoses(NexusAPI api, TransformedBody_Root transformedBody, HtmlHandler handler, string transformedBodyHTML)
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

        private void GetDischargeReportData_CurrentAdmission(NexusAPI api, TransformedBody_Root transformedBody, HtmlHandler handler, string transformedBodyHTML)
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
                        string propertyValue = domObject.ElementAt(i+1).ChildElements.ElementAt(0).FirstChild.ToString();
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

        private void GetDischargeReportData_Relatives(NexusAPI api, TransformedBody_Root transformedBody, HtmlHandler handler, string transformedBodyHTML)
        {
            string htmlResult = handler.GetResultFromHtmlSelectNextUntil(transformedBodyHTML, "Pårørende/Relationer","Aktuel indlæggelse");
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
                    GetDischargeReportData_Relatives_GetCommentsForRelatives(domObject.ElementAt(i),transformedBody);
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

        private void GetDischargeReportData_Relatives_GetIsInformed(string tdStringHTMLString, TransformedBody_Relavites relative)
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

        private void GetDischargeReportData_Relatives_GetCommentsForRelatives(IDomObject domObject, TransformedBody_Root transformedBody)
        {
            var relativeCommentsTable = domObject;
            var relativeCommentsTbody = relativeCommentsTable.ChildElements.First();
            var relativeCommentsTr = relativeCommentsTbody.ChildElements.ElementAt(1); // We use the element at 1, as the element at 0 is the header
            var relativeCommentsTdString = relativeCommentsTr.ChildElements.First().FirstChild.ToString();

            transformedBody.CommentsForRelatives = relativeCommentsTdString;
        }

        private void GetDischargeReportData_Relatives_GetType(string typeTdHTMLString, TransformedBody_Relavites relative)
        {
            var typeTd = typeTdHTMLString;
            relative.Type = typeTd;
        }




        #endregion medcom messages



    }
}

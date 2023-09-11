using Microsoft.SqlServer.Server;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NexusAPIWrapper;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace NexusAPITest
{
    public class Tests
    {
        //[SetUp]
        //public void Setup()
        //{
        //}

        string environment = "live";
        [Test]
        public void testAccess()
        {
            NexusAPI _NexusAPI = new NexusAPI(environment);
            Assert.IsFalse(string.IsNullOrEmpty(_NexusAPI.tokenObject.AccessToken));
        }
        [Test]
        public void testHomeResource()
        {
            NexusAPI _NexusAPI = new NexusAPI(environment);
            NexusHomeRessource _ressource = new NexusHomeRessource(_NexusAPI.clientCredentials.Host, _NexusAPI.tokenObject.AccessToken);
            NexusResult result = _ressource.GetHomeRessource();
            Assert.IsTrue(result.httpStatusCode == System.Net.HttpStatusCode.OK);
        }
        [Test]
        public void testSearchPatientByCPR()
        {
            string cpr = "xxxxxx-xxxx";
            NexusAPI _NexusAPI = new NexusAPI(environment);
            var details = _NexusAPI.GetPatientDetails(cpr);
            Assert.IsNotNull(details);
        }
        [Test]
        public void testGetPatientDetailsLinks()
        {
            NexusAPI _NexusAPI = new NexusAPI(environment);
            var links = _NexusAPI.GetPatientDetailsLinks("xxxxxx-xxxx");
            Assert.IsNotNull(links);
        }
        [Test]
        public void testGetPatientPreferences()
        {
            NexusAPI _nexusAPI = new NexusAPI(environment);
            var preferences = _nexusAPI.GetPatientPreferences("xxxxxx-xxxx");
            Assert.IsNotNull(preferences);
        }
        [Test]
        public void testGetCitizenPathways()
        {
            NexusAPI _nexusAPI = new NexusAPI(environment);
            var pathways = _nexusAPI.GetCitizenPathways("xxxxxx-xxxx");
            Assert.IsNotNull(pathways);
        }
        [Test]
        public void testGetCitizenPathwayLink()
        {
            NexusAPI _nexusAPI = new NexusAPI(environment);
            var pathwayLink = _nexusAPI.GetCitizenPathwayLink("xxxxxx-xxxx", "Dokumenttilknytning fra Vitae");
            Assert.IsNotNull(pathwayLink);
        }

        [Test]
        public void testGetDokumentTilknytningHref()
        {
            NexusAPI nexusAPI = new NexusAPI(environment);
            string CitizenCPR = "xxxxxx-xxxx";
            string pathwayName = "Dokumenttilknytning fra Vitae";
            //var pathwayLink = nexusAPI.GetCitizenPathwayLink_ByCPR(CitizenCPR, pathwayName);
            var hrefLink = nexusAPI.GetCitizenPathwayLink(CitizenCPR, pathwayName);

            Assert.IsTrue(hrefLink == @"https://ringsted.nexus.kmd.dk:443/api/core/mobile/ringsted/v2/patient/1623/preferences/CITIZEN_PATHWAY/971");
        }

        [Test]
        public void Test()
        {
            NexusAPI nexusAPI = new NexusAPI("live");
            string CitizenCPR = "xxxxxx-xxxx";
            string pathwayName = "Dokumenttilknytning fra Vitae";

            //var pathwayLink = nexusAPI.GetCitizenPathwayLink_ByCPR(CitizenCPR, pathwayName);
            var pathwayReferences = nexusAPI.GetCitizenPathwayReferences(CitizenCPR, pathwayName);
            Assert.IsNotNull(pathwayReferences);
        }

        [Test]
        public void GetPathwayReferencesLink()
        {
            NexusAPI nexusAPI = new NexusAPI("live");
            string CitizenCPR = "xxxxxx-xxxx";
            string pathwayName = "Dokumenttilknytning fra Vitae";

            var pathwayReferencesLink = nexusAPI.GetCitizenPathwayReferencesSelfLink(CitizenCPR, pathwayName);
            Assert.IsNotNull(pathwayReferencesLink);
        }

        [Test]
        public void GetCitizenPathwayObjects()
        {
            NexusAPI nexusAPI = new NexusAPI("review");
            string CitizenCPR = "xxxxxx-xxxx";
            string pathwayName = "Dokumenttilknytning fra Vitae";

            var objects = nexusAPI.GetCitizenPathwayReferencesSelf(CitizenCPR, pathwayName);
            var links = objects["_links"];
            var linksObj = nexusAPI.dataHandler.ConvertJsonToSortedDictionary(links);
            Assert.IsNotNull(objects);
        }

        [Test]
        public void GetCitizenPathway2()
        {
            NexusAPI nexusAPI = new NexusAPI("review");
            string CitizenCPR = "xxxxxx-xxxx";
            string pathwayName = "Dokumenttilknytning fra Vitae";

            var objects = nexusAPI.GetCitizenPathwayReferences(CitizenCPR, pathwayName);
            //var links = objects["_links"];
            //var linksObj = nexusAPI.dataHandler.ConvertJsonToSortedDictionary(links);
            Assert.IsNotNull(objects);
        }

        [Test]
        public void GetCitizenPathwayChildren()
        {
            NexusAPI nexusAPI = new NexusAPI("live");
            string CitizenCPR = "xxxxxx-xxxx";
            string pathwayName = "Dokumenttilknytning fra Vitae";

            var pathwayChildren = nexusAPI.GetCitizenPathwayChildren(CitizenCPR, pathwayName);
            Assert.IsNotNull(pathwayChildren);
        }

        [Test]
        public void GetCitizenPathwayChildDictionary()
        {
            NexusAPI nexusAPI = new NexusAPI("live");
            string CitizenCPR = "xxxxxx-xxxx";
            string pathwayName = "Dokumenttilknytning fra Vitae";
            string pathwayChildName = "Dokumenter fra Vitae";

            var pathwayChild = nexusAPI.GetCitizenPathwayChild(CitizenCPR, pathwayName, pathwayChildName);
            Assert.IsNotNull(pathwayChild);
        }

        [Test]
        public void GetCitizenPathwayChildDocuments()
        {
            NexusAPI nexusAPI = new NexusAPI("review");
            string CitizenCPR = "xxxxxx-xxxx";
            string pathwayName = "Dokumenttilknytning fra Vitae";
            string pathwayChildName = "Dokumenter fra Vitae";

            var pathwayChildDocuments = nexusAPI.GetCitizenPathwayChildDocuments(CitizenCPR, pathwayName, pathwayChildName);
            Assert.IsNotNull(pathwayChildDocuments);
        }

        [Test]
        public void GetCitizenPathwayChildDocumentsThatAreEmpty()
        {
            // This should return no child documents and therefore IS EMPTY
            NexusAPI nexusAPI = new NexusAPI("review");
            string CitizenCPR = "xxxxxx-xxxx";
            string pathwayName = "Dokumenttilknytning - alt";
            string pathwayChildName = "Udf�rer - Omsorg, pleje og tr�ning m.v.";

            //string pathwayName = "- Dokumenter - Tr�ning og Sundhed";
            //string pathwayChildName = "Udf�rer - Omsorg, pleje og tr�ning m.v.";

            var pathwayChildDocuments = nexusAPI.GetCitizenPathwayChildDocuments(CitizenCPR, pathwayName, pathwayChildName);
            Assert.IsEmpty(pathwayChildDocuments);
        }

        [Test]
        public void testGetCitizenPathwayDocumentPrototypelink()
        {
            NexusAPI nexusAPI = new NexusAPI("live");
            string CitizenCPR = "xxxxxx-xxxx";
            string pathwayName = "Dokumenttilknytning fra Vitae";
            //string pathwayChildName = "Dokumenter fra Vitae";

            var CitizenPathwayReferencesSelf_Links = nexusAPI.GetCitizenPathwayReferencesSelf_Links(CitizenCPR, pathwayName);

            string documentPrototypeLink = nexusAPI.dataHandler.GetDocumentPrototypeLink(CitizenPathwayReferencesSelf_Links);

            Assert.IsNotNull(documentPrototypeLink);
        }

        [Test]
        public void testGetCitizenPathwayChildDocumentPrototypelink()
        {
            NexusAPI nexusAPI = new NexusAPI("live");
            string CitizenCPR = "xxxxxx-xxxx";
            string pathwayName = "Dokumenttilknytning fra Vitae";
            string pathwayChildName = "Dokumenter fra Vitae";

            var CitizenPathwayChildSelf_Links = nexusAPI.GetCitizenPathwayChildSelf_Links(CitizenCPR, pathwayName, pathwayChildName);

            string documentPrototypeLink = nexusAPI.dataHandler.GetDocumentPrototypeLink(CitizenPathwayChildSelf_Links);

            Assert.IsNotNull(documentPrototypeLink);
        }

        [Test]
        public void GetCitizenPathwayReferencesDocuments()
        {
            NexusAPI nexusAPI = new NexusAPI("review");
            string CitizenCPR = "xxxxxx-xxxx";
            string pathwayName = "Dokumenttilknytning fra Vitae";

            var pathwayReferencesDocuments = nexusAPI.GetCitizenPathwayReferencesDocuments(CitizenCPR, pathwayName);

            Assert.IsNotNull(pathwayReferencesDocuments);
        }

        [Test]
        public void GetCitizenPathwayDocuments()
        {
            NexusAPI nexusAPI = new NexusAPI("review");
            string CitizenCPR = "xxxxxx-xxxx";
            string pathwayName = "Dokumenttilknytning fra Vitae";

            var pathwayReferencesDocuments = nexusAPI.GetCitizenPathwayDocuments(CitizenCPR, pathwayName);

            Assert.IsNotNull(pathwayReferencesDocuments);
        }

        //[Test]
        //public void GetDocumentObject()
        //{
        //    NexusAPI nexusAPI = new NexusAPI("review");
        //    string CitizenCPR = "xxxxxx-xxxx";
        //    string pathwayName = "Dokumenttilknytning fra Vitae";
        //    string pathwayChildName = "Dokumenter fra Vitae";


        //    string documentObjectResponse = "{\"id\":null,\"version\":0,\"uid\":\"e1388e3f-3d53-4e13-8b09-2f3a8d229eee\",\"name\":null,\"notes\":null,\"originalFileName\":null,\"uploadingDate\":null,\"relevanceDate\":\"2023-06-09T06:31:30.036+00:00\",\"status\":\"CREATED\",\"pathwayAssociation\":{\"placement\":{\"id\":null,\"version\":null,\"patientPathwayId\":26859,\"programPathwayId\":4809,\"_links\":{\"patientPathway\":{\"href\":\"/api/core/mobile/ringsted/v2/patientPathways/26859\"}}},\"parentReferenceId\":null,\"referenceId\":null,\"canAssociateWithPathway\":true,\"canAssociateWithPatient\":true,\"associatedWithPatient\":false,\"_links\":{\"availableRootProgramPathways\":{\"href\":\"https://ringsted.nexus-review.kmd.dk/api/core/mobile/ringsted/v2/patients/1623/availableProgramPathways\"},\"availablePathwayAssociation\":{\"href\":\"https://ringsted.nexus-review.kmd.dk/api/core/mobile/ringsted/v2/patients/1623/pathways/26859/availablePathwayAssociations\"}}},\"patientId\":1623,\"fileType\":null,\"tags\":[],\"origin\":null,\"externalId\":null,\"fileExternalId\":null,\"_links\":{\"create\":{\"href\":\"https://ringsted.nexus-review.kmd.dk/api/document-microservice/ringsted/documents\"},\"availableTags\":{\"href\":\"https://ringsted.nexus-review.kmd.dk/api/core/mobile/ringsted/v2/tags/UI/documentTags\"}},\"patientActivityType\":\"document\"}";
        //    var dObject = JObject.Parse(documentObjectResponse);
        //    var documentObject = nexusAPI.CreateDocumentObject(CitizenCPR, pathwayName, pathwayChildName);
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
            string org = "Hjpl. Ude-team � 94";
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
        public void UpdateProfessionalOrganizations()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            List<string> orgsToAdd = new List<string>();
            List<string> orgsToRemove = new List<string>();

            orgsToAdd.Add(processes.api.GetOrganizationId("Depot").ToString());

            //You can't remove organizations that are not active on the professional.
            //So this will result in a bad request.
            orgsToRemove.Add(processes.api.GetOrganizationId("B�rnecenter").ToString());
            orgsToRemove.Add(processes.api.GetOrganizationId("BC Servicelov").ToString());

            string add = string.Join(",", orgsToAdd);
            string remove = string.Join(",", orgsToRemove);

            processes.UpdateProfessionalOrganizations(1409, add, remove);
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
            string cpr = "xxxxxx-xxxx";
            var details = api.GetPatientDetails(cpr);

            var detailsLink = api.GetPatientDetailsLinks(cpr);
            Assert.IsNotNull(detailsLink);
        }

        [Test]
        public void GetPatientPreferences()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;
            string cpr = "xxxxxx-xxxx";
            var preferences = api.GetPatientPreferences(cpr);


            Assert.IsNotNull(preferences);
        }

        [Test]
        public void GetCitizenPathway()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;
            string cpr = "xxxxxx-xxxx";
            string elementName = "Dokumenttilknytning fra Vitae";
            var pathways = api.GetCitizenPathways(cpr);

            var pathway = api.GetElementFromList(pathways, elementName);

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
        public void GetCitizenJournalNotes()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;

            string pathwayName = "Journalnotater fra Vitae";
            string cpr = "";
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

        [Test]
        public void ActivateDeactivatedSusbstituteProfessionalsAndSendEmailWithListOfActivatedProfessionals()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;

            DateTime start = DateTime.Now;
            processes.ActivateInactiveSubstituteProfessionals();
            DateTime end = DateTime.Now;

            var prosActivated = processes.GetProfessionalsList();

            
            List<string> initialsList = new List<string>(); 

            string ToEmails = "msch@ringsted.dk,msch@ringsted.dk";
            string subject = "Testmail subject";
            string joinedViks = string.Empty;

            foreach (var pro in prosActivated)
            {
                initialsList.Add(pro.Initials.ToString());
            } 
            joinedViks = string.Join("\r\n", initialsList);
            string body = "Hej Martin og Martin, \r\nHer er listen over de(n) " + initialsList.Count + " bruger(e) der er blevet aktiveret.\r\n" + joinedViks;

            processes.SendEmail(ToEmails,subject,body, "noreply@ringsted.dk", "KMD Nexus robot");
            
            foreach (var pro in prosActivated)
            {
                processes.DeactivateProfessional(pro.Id);
            }
        }

        [Test]
        public void TestSendMail()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;

            processes.SendEmail("msch@ringsted.dk", "testSubject", "bodyText", "noreply@ringsted.dk", "KMD Nexus robot");
        }

        [Test]
        public void ActivateDeactivatedSusbstituteProfessionals()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;

            processes.ActivateInactiveSubstituteProfessionals();
            
        }

        [Test]
        public void ReturnSearchedProfessionals()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;

            var df = api.GetProfessionals("jette nielsen");

        }
    }
}

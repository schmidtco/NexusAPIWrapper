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

            var enrollmentLink = nexusAPI.GetProgramPathwayEnrollmentLink(citizenCPR, programPathwayName);
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
                enrolledObject = processes.EnrollPatientToProgramPathway(citizenCPR, programPathwayName);
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

            var pathwayAssociation = api.GetPatientPathwayAssociation(citizenCPR, pathwayAssociationName);

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
            processes.SetProfessionalPrimaryOrganization(id, organizationName);

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
            var documentList = processes.GetCitizenDocumentObjects(id, pathwayName, true);
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
            NexusResult preferencesResult = api.CallAPI(api, preferencesLink, Method.Get);
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

            var availableBasketsResult = api.CallAPI(api, availableBasketsLink, Method.Get);

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
        public void GetDischargeReportData() 
        {
            NexusAPI_processes processes = new NexusAPI_processes(liveEnvironment);
            var api = processes.api;

            var medComMessages = api.GetPreferencesActivityListSelfObjectContent("- MedCom - arkiveret sidste uge", 20, 01, 2024, 25, 01, 2024);
            ReferencedObject_Base_Root chosenMedComMessage = new ReferencedObject_Base_Root();
            TransformedBody_Root transformedBody = new TransformedBody_Root();
            foreach (var medComMessage in medComMessages)
            {
                ReferencedObject_Base_Root baseObject = api.GetActivityListContentBaseObject(medComMessage);
                if (baseObject.Subject.ToLower().Contains("udskrivningsrapport"))
                {
                    transformedBody = processes.GetDischargeReportData(baseObject);
                    break;
                }
            }
            Assert.IsNotNull(transformedBody);
        }

    }
} 
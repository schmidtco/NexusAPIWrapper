using AngleSharp;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using CsQuery;
using Microsoft.SqlServer.Server;
using MimeKit.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NexusAPIWrapper;
using NexusAPIWrapper.Custom_classes;
using NexusAPIWrapper.HomeRessource.Preferences.ACTIVITYLIST.ACTIVITYLIST_Content.Content.Pages.Links.Content._Root.Links.ReferencedObject._Root.Links.TransformedBody;
using NUnit.Framework;
using Org.BouncyCastle.Utilities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Xml;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

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
                var CitizenPathwayReferencesSelf_Links = nexusAPI.GetCitizenPathwayReferencesSelf_Links(citizenCPR, pathwayName, pathwayReferenceName);
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
        public void RemoveFileExtensionsFromFileNames()
        {
            NexusAPI_processes processes = new NexusAPI_processes("review");
            var api = processes.api;
            string citizenCPR = nancyBerggrenTestCPR;
            string pathwayName = "Dokumenttilknytning - alt";

            var documents = api.GetCitizenPathwayDocuments(citizenCPR, pathwayName);

            foreach (var doc in documents)
            {
                string selfLink = doc.Links.Self.Href;
                var webResult = api.CallAPI(api, selfLink, Method.Get);
                PathwayReferencesChildSelf_Root selfRoot = JsonConvert.DeserializeObject<PathwayReferencesChildSelf_Root>(webResult.Result.ToString());
                string referencesObjectLink = selfRoot.Links.AvailableActions.Href;
            }
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
                //Process to do so needs to be agreed upon with stakeholders

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
            Assert.IsNotNull(transformedBody.CurrentAdmission);
        }

        [Test]
        public void HomeRessourcePreferencesTest()
        {
            NexusAPI_processes processes = new NexusAPI_processes(liveEnvironment);
            var api = processes.api;

            var preferences = api.GetPreferences();
            Assert.IsNotNull(preferences);
        }

        [Test]
        public void ChangeStatusOnCitizenFailsBecauseTheStatusNameIsNotAnOption()
        {
            NexusAPI_processes processes = new NexusAPI_processes(reviewEnvironment);
            var api = processes.api;
            var patient = api.GetPatientDetails(nancyBerggrenTestCPR);
            string statusName = "Aktiv - 73 timers regel";

            //var patientStates = api.GetAvailablePatientStates(patient.Links.AvailablePatientStates.Href);
            //var updatedPatient1 = processes.ChangeStatusOnCitizen(nancyBerggrenTestCPR, "Aktiv");
            var updatedPatient2 = processes.ChangeStatusOnCitizen(patient, statusName);

            Assert.AreNotEqual(statusName, updatedPatient2.PatientState.Name);
        }
        [Test]
        public void ChangeStatusOnCitizenWorks()
        {
            NexusAPI_processes processes = new NexusAPI_processes(reviewEnvironment);
            var api = processes.api;
            var patient = api.GetPatientDetails(nancyBerggrenTestCPR);
            string statusName = "Aktiv - 72 timers regel";

            //var patientStates = api.GetAvailablePatientStates(patient.Links.AvailablePatientStates.Href);
            //var updatedPatient1 = processes.ChangeStatusOnCitizen(nancyBerggrenTestCPR, "Aktiv");
            var updatedPatient2 = processes.ChangeStatusOnCitizen(patient, statusName);

            Assert.AreEqual(statusName, updatedPatient2.PatientState.Name);
        }

        [Test]
        public void UploadDocumentToNexusDirectlyOnPatient()
        {
            NexusAPI_processes processes = new NexusAPI_processes(reviewEnvironment);
            var api = processes.api;
            string filePath = "C:\\users\\msch\\desktop\\Docs der sendes\\testTextFile.txt";
            var patient = api.GetPatientDetails(nancyBerggrenTestCPR);
            Patient_DocumentPrototype_Root documentPrototype = api.CreatePatientDocumentPrototype(patient, filePath);
            Patient_DocumentPrototype_Create_Root createdDocumentObject = api.UploadPatientDocumentPrototype(documentPrototype);


            WebRequest request = api.UploadPatientDocumentToNexus(createdDocumentObject, filePath);
            var response = request.response;
        }
        [Test]
        public void UploadDocumentToNexusOnPathway()
        {
            NexusAPI_processes processes = new NexusAPI_processes(reviewEnvironment);
            var api = processes.api;
            string filePath = "C:\\users\\xxx\\desktop\\testTextFile2.txt";

            var uploadedDocObject = processes.UploadPatientPathwayDocumentToNexus(
                nancyBerggrenTestCPR,
                "Dokumenttilknytning - alt", //Pathway (borgerforløb)
                "Sundhedsfagligt Grundforløb", //Pathway reference (refrerence til grundforløb)
                filePath,
                31); // Here we need to specify the pathway reference Id, as there's more than one pathway reference with the same name


            Assert.IsTrue(uploadedDocObject.response.IsSuccessStatusCode);
        }

        [Test]
        public void ChangeSupplierForInkontinens()
        {
            NexusAPI_processes processes = new NexusAPI_processes(reviewEnvironment);
            var api = processes.api;

            //var patient = api.GetPatientDetails(nancyBerggrenTestCPR);
            var widget = api.GetCitizenDashboardElementSelfWidget(nancyBerggrenTestCPR, "MYN - Inkontinens", "Indsatser Kropsbårne hjælpemidler");
            string pathwayReferencesLink = widget.Links.PathwayReferences.Href;

            var webResult = api.CallAPI(api, pathwayReferencesLink, Method.Get);
            var references = JsonConvert.DeserializeObject<List<PathwayReferences_Root>>(webResult.Result.ToString());

            List<PathwayReferences_Child> childrenToHandle = new List<PathwayReferences_Child>();

            // Get all children of "indsatser"
            foreach (var reference in references)
            {
                if (reference.Name == "Indsatser")
                {
                    foreach (var item in reference.Children)
                    {
                        childrenToHandle.Add(item);
                    }
                }
                else
                {
                    LoopChildren(childrenToHandle, reference.Children);
                }

            }
            //foreach "indsats" change the supplier if "indsats" is not closed/finished
            foreach (var child in childrenToHandle)
            {
                string selfLink = child.Links.Self.Href;
                var webResultIndsats = api.CallAPI(api, selfLink, Method.Get);
                var childSelf = JsonConvert.DeserializeObject<PathwayReferencesChildSelf_Root>(webResultIndsats.Result.ToString());

                string availableActionsLink = childSelf.Links.AvailableActions.Href;
                var webResultAvailableActions = api.CallAPI(api, availableActionsLink, Method.Get);
                // available actions return null !?!?!?!?!?!?

                //Getting activityIdentifierId to get the patientGrantById
                int activityIdentifierId = (int)child.ActivityIdentifier.ActivityId;
                var patientGrant = api.GetPatientGrantById(activityIdentifierId);
                string patientGrantEndpoint = api.GetPatientGrantByIdLink(activityIdentifierId);

                //Getting currentWorkflowTransitions (mulige ting der kan gøres med denne grant - ændr, ændr fremtidig etc.)
                var transitions = patientGrant.CurrentWorkflowTransitions;
                var chosenTransition = transitions.FirstOrDefault(x => x.Name == "Ændr");
                string changeLink = transitions.FirstOrDefault(x => x.Name == "Ændr").Links.PrepareEdit.Href;
                var webResultTransitionChange = api.CallAPI(api, changeLink, Method.Get);
                var transitionChange = JsonConvert.DeserializeObject<PatientGrantByIdCurWkFlTrPrepEdt_Root>(webResultTransitionChange.Result.ToString());
                var transitionChangeElements = transitionChange.Elements;
                var supplierElement = transitionChangeElements.FirstOrDefault(x => x.Type == "supplier");

                //Getting available suppliers
                string availableSuppliersLink = supplierElement.Links.AvailableSuppliers.Href;
                var webResultAvSuppliers = api.CallAPI(api, availableSuppliersLink, Method.Get);
                var avSuppliers = JsonConvert.DeserializeObject<List<PatientGrantByIdCurWkFlTrPrepEdtAvSuppl_Root>>(webResultAvSuppliers.Result.ToString());
                avSuppliers = avSuppliers.OrderBy(x => x.Name).ToList();

                string chosenSupplierName = "Frit valg af leverandør (kropsbårne hjm)";
                var supplierToAdd = avSuppliers.FirstOrDefault(x => x.Name == chosenSupplierName);
                transitionChange.Elements.FirstOrDefault(x => x.Type == "supplier").Supplier = JsonConvert.DeserializeObject<PatientGrantByIdCurWkFlTrPrepEdt_Supplier>(JsonConvert.SerializeObject(supplierToAdd));



                var WebResultSaveChange = api.CallAPI(api, transitionChange.Links.Save.Href, Method.Post, JsonConvert.SerializeObject(transitionChange));
                // this results in a missing field error
            }


        }
        public void LoopChildren(List<PathwayReferences_Child> childrenToHandle, List<PathwayReferences_Child> children)
        {
            foreach (var child in children)
            {
                if (child.Name == "Indsatser")
                {
                    foreach (var item in child.Children)
                    {
                        childrenToHandle.Add(item);
                    }
                }
                else
                {
                    LoopChildren(childrenToHandle, child.Children);
                }
            }
        }

        [Test]
        public void UploadDocumentsToNexus_Merudgift()
        {
            NexusAPI_processes processes = new NexusAPI_processes(reviewEnvironment);
            var api = processes.api;

            var folders = System.IO.Directory.GetDirectories("\\\\rkfil02\\koncerncenter\\Vitae-Nexus\\Historisk data\\Merudgifter");
            foreach (var folder in folders)
            {
                var folderSplitList = folder.Split('\\');
                string foldername = folderSplitList[folderSplitList.Length - 1];

                //Load citizen
                var patient = api.GetPatientDetails(foldername);
                var patientPathway = api.GetCitizenPathway(nancyBerggrenTestCPR, "Historiske data fra Vitae");

                var files = System.IO.Directory.GetFiles(folder);
                foreach (var item in files)
                {




                }
            }
        }
        [Test]
        public void tettetetds_møde_med_sudavili_omkring_72Timer_og_lukning_af_borgere()
        {
            NexusAPI_processes processes = new NexusAPI_processes(liveEnvironment);
            var api = processes.api;

            //var patient = api.GetPatientDetails(nancyBerggrenTestCPR);
            //string patientOrganizationsLink = patient.Links.PatientOrganizations.Href;

            ////Get patient organizations.
            //var webResult = api.CallAPI(api, patientOrganizationsLink, Method.Get);
            var allOrgs = api.GetAllOrganizations();
            string orgsUrlString = string.Empty;
            foreach (var org in allOrgs)
            {
                if (orgsUrlString == string.Empty)
                {
                    orgsUrlString = "&organizationId=" + org.Id.ToString();
                }
                else
                {
                    orgsUrlString += "&organizationId=" + org.Id.ToString();
                }
            }
            var preferences = api.GetPreferences();
            var list = api.GetPreferencesActivityLists();
            var chosenList = list.FirstOrDefault(x => x.Name == "72 timers behandlingsansvar");

            string listSelfLink = chosenList.Links.Self.Href;
            var webResultListSelf = api.CallAPI(api, listSelfLink, Method.Get);
            ACTIVITYLIST_Root activityList = JsonConvert.DeserializeObject<ACTIVITYLIST_Root>(webResultListSelf.Result.ToString());
            string contentLink = activityList.Links.Content.Href;
            string pagesize = "&pageSize=50"; // pageSize needs to be added to the link, otherwise it will result in "bad result"
            var webResultContent = api.CallAPI(api, contentLink + pagesize + orgsUrlString, Method.Get);
            ACTIVITYLIST_Content_Root contentRoot = JsonConvert.DeserializeObject<ACTIVITYLIST_Content_Root>(webResultContent.Result.ToString());

            //loop all available pages
            foreach (var page in contentRoot.Pages)
            {
                string pageLink = page.Links.Content.Href;
                var webResultPageContent = api.CallAPI(api, pageLink, Method.Get);
                List<ACTIVITYLIST_Pages_Content_Root> pageContents = JsonConvert.DeserializeObject<List<ACTIVITYLIST_Pages_Content_Root>>(webResultPageContent.Result.ToString());

                foreach (var pageContent in pageContents)
                {
                    var baseObj = api.GetReferencedObject_Base_Root(pageContent);
                    var dischargeReportData = processes.GetDischargeReportData(baseObj);
                    var timeOfDischarge = dischargeReportData.CurrentAdmission.TimeOfDischarge;
                    var dateOfDischargeString = timeOfDischarge.Substring(0, 10);
                    CultureInfo culture = CultureInfo.InvariantCulture;
                    DateTime dateOfDischarge = DateTime.ParseExact(dateOfDischargeString, "dd-MM-yyyy", culture);

                    DateTime dateForStatusChange = dateOfDischarge.AddDays(3);
                    if (DateTime.Now.Date == dateForStatusChange)
                    {
                        int patientId = (int)pageContent.Patients.FirstOrDefault().Id;
                        var patient = api.GetPatientDetails(patientId);
                        //processes.ChangeStatusOnCitizen(patient, "Aktiv");
                    }
                }
            }
        }
        [Test]
        public void Handle72TimersAnsvar()
        {
            NexusAPI_processes processes = new NexusAPI_processes(liveEnvironment);
            var api = processes.api;

            var medComMessages = api.GetPreferencesActivityListSelfObjectContent("72 timers behandlingsansvar", 28, 02, 2024, 30, 03, 2024);
            ReferencedObject_Base_Root chosenMedComMessage = new ReferencedObject_Base_Root();
            TransformedBody_Root transformedBody = new TransformedBody_Root();
            foreach (var medComMessage in medComMessages)
            {
            }
        }
        

        [Test]
        public void citizen72Hours()
        {
            NexusAPI_processes processes = new NexusAPI_processes();
            var api = processes.api;

            processes.ChangeStatusOnCitizen(1, "Aktiv");
        }
        [Test]
        public void Get72HoursCitizensToDb_processes()
        {
            NexusAPI_processes processes = new NexusAPI_processes(liveEnvironment);
            var api = processes.api;

            processes.Add72HoursCitizensToDb(15, 4, 2024, 16, 4, 2024);
        }
        [Test]
        public void Get72HoursCitizensToDb()
        {
            NexusAPI_processes processes = new NexusAPI_processes(liveEnvironment);
            var api = processes.api;

            var activityList = api.GetPreferencesActivityListSelfObjectContent("72 timers behandlingsansvar", 29, 04, 2024, 06, 05, 2024);
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
                var reportData = processes.GetDischargeReportData(refObj);
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

        [Test]
        public void UploadDocumentToNexusDirectlyOnPatient_TAGS_TEST()
        {
            NexusAPI_processes processes = new NexusAPI_processes(reviewEnvironment);
            var api = processes.api;
            string filePath = "C:\\users\\msch\\desktop\\Docs der sendes\\testTextFile2.txt";
            string tagToAdd = "Historiske data fra CSC  – Merudgifter";
            var patient = api.GetPatientDetails(nancyBerggrenTestCPR);
            Patient_DocumentPrototype_Root documentPrototype = api.CreatePatientDocumentPrototype(patient, filePath);
            var avTagsLink = documentPrototype.Links.AvailableTags.Href;

            var avTagsResult = api.CallAPI(api, avTagsLink, Method.Get);
            List<Patient_DocumentPrototype_AvailableTags_Root> availableTags = JsonConvert.DeserializeObject<List<Patient_DocumentPrototype_AvailableTags_Root>>(avTagsResult.Result.ToString());
            Patient_DocumentPrototype_AvailableTags_Root chosenTag = availableTags.FirstOrDefault(x => x.Name == tagToAdd);

            documentPrototype.Tags.Add(chosenTag);

            Patient_DocumentPrototype_Create_Root createdDocumentObject = api.UploadPatientDocumentPrototype(documentPrototype);

            WebRequest request = api.UploadPatientDocumentToNexus(createdDocumentObject, filePath);
            var response = request.response;
        }

        [Test]
        public void TestAvailableTagsForDocuments()
        {
            NexusAPI_processes processes = new NexusAPI_processes(reviewEnvironment);
            var api = processes.api;

            var avTagsResult = api.CallAPI(api, api.GetAvailabeTagsForDocumentsLink_Review(), Method.Get);
            List<Patient_DocumentPrototype_AvailableTags_Root> availableTags = JsonConvert.DeserializeObject<List<Patient_DocumentPrototype_AvailableTags_Root>>(avTagsResult.Result.ToString());

            Assert.IsNotEmpty(availableTags);
        }

        [Test]
        public void GetAllJobTitles()
        {
            NexusAPI_processes processes = new NexusAPI_processes(liveEnvironment);
            var api = processes.api;



            string professionalJobsLinkFromHomeRessource = api.GetHomeRessourceLink("professionalJobs");
            var webResult = api.CallAPI(api, professionalJobsLinkFromHomeRessource, Method.Get);
            List<ProfessionalJobs_Root> jobs = JsonConvert.DeserializeObject<List<ProfessionalJobs_Root>>(webResult.Result.ToString());

        }

        [Test]
        public void RemoveOrganizationFromCitizen()
        {
            NexusAPI_processes processes = new NexusAPI_processes(liveEnvironment);
            var api = processes.api;

            //var PreferencesCitizenList = api.GetPreferencesCitizenList("Borgere tilknyttet Myndighed og depot");
            //var PreferencesCitizenListSelf = api.GetPreferencesCitizenListSelf("Borgere tilknyttet Myndighed og depot");
            var PreferencesCitizenListSelfContent = api.GetPreferencesCitizenListSelfContent("Borgere tilknyttet Myndighed og depot");

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

                    string orgsLink = patientObject.Links.PatientOrganizations.Href;


                    var webResultpatientOrgsLink = api.CallAPI(api, orgsLink, Method.Get);
                    List<PatientOrganizations_Root> patientOrganizations = JsonConvert.DeserializeObject<List<PatientOrganizations_Root>>(webResultpatientOrgsLink.Result.ToString());
                    foreach (var org in patientOrganizations)
                    {
                        switch (org.Organization.Name)
                        {
                            case "Depot":
                            case "Myndighed":
                                string updateLink = org.Links.Update.Href;
                                org.EffectiveEndDate = "2023-01-01";
                                string JSONOrganization = JsonConvert.SerializeObject(org);
                                var webResultEndDate = api.CallAPI(api, updateLink, Method.Put, JSONOrganization);
                                //var webResultRemove = api.CallAPI(api, removeLink, Method.Delete);
                                break;
                            default:
                                break;
                        }
                    }


                }
            }
        }
        [Test]
        public void UploadJournalNotesToNexus_BB()
        {
            NexusAPI_processes processes = new NexusAPI_processes(liveEnvironment);
            var api = processes.api;

            // load citizen folders 
            string folderpath = "C:\\Users\\msch\\Downloads\\ringsted_doc (2)\\ringsted_doc";

            var folders = System.IO.Directory.GetDirectories(folderpath);

            // Loop folders
            foreach (var folder in folders)
            {
                // get data from xml folder
                var citizenFolderFolders = System.IO.Directory.GetDirectories(folder);
                var xmlFiles = System.IO.Directory.GetFiles(citizenFolderFolders.First());
                // get the journal notes file
                var xmlFile = xmlFiles.FirstOrDefault(x => x.Contains("dagbogsnoter"));
                // get CPR from xml file name
                var fileSplitList = xmlFile.Split(new char[] { '\\' });
                string xmlFileName = fileSplitList.Last();
                string dayDate = xmlFileName.Substring(0, 2);
                string monthDate = xmlFileName.Substring(3, 2);
                string yearDate = xmlFileName.Substring(6, 2);
                string controlNumbers = xmlFileName.Substring(9, 4);

                string citizenCPR = dayDate + monthDate + yearDate + controlNumbers;
                // load citizen in Nexus
                var patient = api.GetPatientDetails(citizenCPR);
                // load journal notes from file

                // loop all journal notes

                // add journal note to Nexus

                // loop notes END

                // loop folders END
            }


        }

        [Test]
        public void DoesJournalNoteExist()
        {
            NexusAPI_processes processes = new NexusAPI_processes();
            var api = processes.api;

            // choose what and where
            string pathwayName = "Historiske notater - BB";
            string pathwayReferenceName = "Socialt og sundhedsfagligt Grundforløb";
            string formName = "Journalnotat - Nebs";
            string formAction = "Låst";
            string tagName = "Historiske notater - BB";

            // Get citizen pathway references
            var citizenpathwayReferencesChildren = api.GetCitizenPathwayReferencesChildren(nancyBerggrenTestCPR, pathwayName, pathwayReferenceName);


        }

        [Test]
        public void GetAllJournalNotes()
        {
            NexusAPI_processes processes = new NexusAPI_processes();
            var api = processes.api;

            // choose what and where
            string pathwayName = "Historiske notater - BB";
            string pathwayReferenceName = "Socialt og sundhedsfagligt Grundforløb";
            string formName = "Journalnotat - Nebs";
            string formAction = "Låst";
            string tagName = "Historiske notater - BB";

            // Get citizen pathway references
            var citizenpathwayReferencesChildren = api.GetCitizenPathwayReferencesChildren("140910-6699", pathwayName, pathwayReferenceName);

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
        }


        [Test]
        public void EnrollPatientToNestedProgramPathway()
        {
            NexusAPI_processes processes = new NexusAPI_processes(reviewEnvironment);
            var api = processes.api;

            string citizenCPR = "";
            string pathwayName = "Historiske notater - BB";
            string pathwayReferenceName = "Socialt og sundhedsfagligt Grundforløb";
            string nestedProgramPathwayName = "Udfører: Døgnstøtte Nebs Møllegard";

            var availableNestedProgramPathways = GetAvailableNestedProgramPathways(citizenCPR, pathwayName, pathwayReferenceName);

            var chosenNestedProgramPathway = availableNestedProgramPathways.FirstOrDefault(x => x.Name == nestedProgramPathwayName);

            string enrollLink = chosenNestedProgramPathway.Links.Enroll.Href;
            var enrolledObject = api.CallAPI(api, enrollLink, Method.Put);
            //NOT WORKING
        }
        [Test]
        public void GetAvailableNestedProgramPathwaysTEST()
        {
            NexusAPI_processes processes = new NexusAPI_processes(liveEnvironment);
            var api = processes.api;

            string citizenCPR = "";
            string pathwayName = "Historiske notater - BB";
            string pathwayReferenceName = "Socialt og sundhedsfagligt Grundforløb";
            string nestedProgramPathwayName = "Udfører: Døgnstøtte Nebs Møllegard";

            var availableNestedProgramPathways = GetAvailableNestedProgramPathways(citizenCPR, pathwayName, pathwayReferenceName);

            var chosenNestedProgramPathway = availableNestedProgramPathways.FirstOrDefault(x => x.Name == nestedProgramPathwayName);
        }



        public List<AvNestPrgPways_Root> GetAvailableNestedProgramPathways(string citizenCPR, string pathwayName, string pathwayReferenceName)
        {
            NexusAPI_processes processes = new NexusAPI_processes(liveEnvironment);
            var api = processes.api;

            var grundforloeb = api.HentAabneGrundforloeb(citizenCPR);

            var chosenGrundforloeb = grundforloeb.FirstOrDefault(x => x.Name == pathwayReferenceName);
            var pathwayReferencesSelf = api.GetCitizenPathwayReferencesSelf(citizenCPR, pathwayName, pathwayReferenceName);
            var chosenPathwayReferenceSelf = pathwayReferencesSelf.FirstOrDefault(x => x.Name == pathwayReferenceName);

            var availableNestedProgramPathwaysLink = chosenPathwayReferenceSelf.Links.AvailableNestedProgramPathways.Href;
            var webResultNested = api.CallAPI(api, availableNestedProgramPathwaysLink, Method.Get);

            return JsonConvert.DeserializeObject<List<AvNestPrgPways_Root>>(webResultNested.Result.ToString());
        }
        [Test]
        public void UploadJournalNote(NexusAPI_processes processes, string citizenCPR, string pathwayName, string pathwayReferenceName, string pathwayReferenceChildName, string subject, string observation, DateTime observationTimeStamp, string environment, List<PwayRefChildSelf_JournalNote_RefObj_Root> journalNotes)
        {
            var api = processes.api;

            // choose what and where
            string formName = "Journalnotat - Nebs";
            string formAction = "Låst";
            string tagName = "Historiske notater - BB";

            // get citizen pathway children
            var citizenpathwayReferencesChildren = api.GetCitizenPathwayReferencesChildren(citizenCPR, pathwayName, pathwayReferenceName);

            var chosenPathwayChild = citizenpathwayReferencesChildren.FirstOrDefault(x => x.Name == pathwayReferenceChildName);
            if (chosenPathwayChild == null)
            {
                //the pathway child is not there, and we need to add it

            }

            string childSelfLink = chosenPathwayChild.Links.Self.Href;
            var webResultChildSelf = api.CallAPI(api, childSelfLink, Method.Get);
            PathwayReferencedChildSelf_Root_BB_Root childSelf = JsonConvert.DeserializeObject<PathwayReferencedChildSelf_Root_BB_Root>(webResultChildSelf.Result.ToString());

            // get form definitions
            string avFormDefinitionsLink = childSelf.Links.AvailableFormDefinitions.Href;

            var webResult = api.CallAPI(api, avFormDefinitionsLink, Method.Get);
            var avFormDef = JsonConvert.DeserializeObject<List<FrmDef_Root>>(webResult.Result.ToString());

            // choose a form
            var chosenForm = avFormDef.FirstOrDefault(x => x.Title == formName);

            // get the form prototype
            string formdataPrototypeLink = chosenForm.Links.FormDataPrototype.Href;

            var webResultPrototype = api.CallAPI(api, formdataPrototypeLink, Method.Get);
            var prototype = JsonConvert.DeserializeObject<FormDataPrototype_Root>(webResultPrototype.Result.ToString());

            // Update the prototype with data under the items property
            var items = prototype.Items;
            items.FirstOrDefault(x => x.Label == "Emne").Value = subject;
            items.FirstOrDefault(x => x.Label == "Observation").Value = observation;
            //items.FirstOrDefault(x => x.Label == "Vurdering").Value = "Vurderingstekst";

            // updating the observation time stamp to match the journal note
            prototype.ObservationTimestamp = observationTimeStamp;

            //Add tag
            // get available tags
            string avTagsLink = prototype.Links.AvailableTags.Href;
            var webResultAvTags = api.CallAPI(api, avTagsLink, Method.Get);
            List<FormDataPrototype_AvailableTags_Root> availableTags = JsonConvert.DeserializeObject<List<FormDataPrototype_AvailableTags_Root>>(webResultAvTags.Result.ToString());
            // select tag object
            var chosenTag = availableTags.FirstOrDefault(x => x.Name == tagName);
            if (chosenTag == null)
            {
                throw new Exception("Tag not available");
            }
            // add tag to prototype
            prototype.Tags.Add(chosenTag);

            // does the journal note exist??
            bool journalNoteAlreadyExists = processes.DoesJournalNoteExist(prototype, journalNotes);

            if (!journalNoteAlreadyExists)
            {
                // serialize prototype
                string jsonPrototype = JsonConvert.SerializeObject(prototype);

                // Get available actions for the form (kladde, låst, udfyldt)
                string prototypeAvActionsLink = prototype.Links.AvailableActions.Href;
                var webResultAvActions = api.CallAPI(api, prototypeAvActionsLink, Method.Get);
                var availableActions = JsonConvert.DeserializeObject<List<FormDataPrototype_AvailableActions_Root>>(webResultAvActions.Result.ToString());

                // perfom chosen action
                var chosenAction = availableActions.FirstOrDefault(x => x.Name == formAction);

                api.CallAPI(api, chosenAction.Links.CreateFormData.Href, Method.Post, jsonPrototype);
            }


        }
        [Test]
        public void GetAllMissingDocuments()
        {
            Dictionary<string, string> MissingDocuments = new Dictionary<string, string>();
            string dataFolderPath = "C:\\Users\\msch\\Downloads\\BB data til Nexus";

            #region Load directories
            var citizenDirectories = System.IO.Directory.GetDirectories(dataFolderPath);
            #endregion Load directories

            #region Loop directories
            foreach (var directory in citizenDirectories)
            {
                // get folder name
                var folderNameSplitList = directory.Split('\\');
                string folderName = folderNameSplitList.Last();

                if (folderName == "130_Maria_El-Tahhan")
                {
                    //#region Get documents
                    var documents = System.IO.Directory.GetFiles(directory + "\\dagbog_dok");
                    //#endregion Get documents

                    //#region Get data from xml folder
                    var xmlFiles = System.IO.Directory.GetFiles(directory + "\\xml");
                    //#region Loop/Handle xml-files
                    var dagbogsnotatFilePath = xmlFiles.FirstOrDefault(x => x.Contains("dagbogsnoter.xml"));
                    var dagbogsnotatDocFilePath = xmlFiles.FirstOrDefault(x => x.Contains("dagbogsnoter_doc.xml"));
                    var documentFilePath = xmlFiles.FirstOrDefault(x => x.Contains("dokumenter"));
                    #region GetCitizenCPRFromFileName

                    var filePathSplitList = dagbogsnotatFilePath.Split(new char[] { '\\' });
                    string fileName = filePathSplitList.Last();

                    string cprDay = fileName.Substring(0, 2);
                    string cprMonth = fileName.Substring(3, 2);
                    string cprYear = fileName.Substring(6, 2);
                    string cprControl = fileName.Substring(9, 4);
                    string citizenCPR = cprDay + cprMonth + cprYear + cprControl;
                    #endregion GetCitizenCPRFromFileName


                    // Get dagbogsnoter
                    var notes = GetDagbogsNoterDoc(dagbogsnotatDocFilePath);
                    // Loop noter
                    foreach (var note in notes.Dokumenter.Dokument)
                    {

                        // Get documentNumber
                        string documentNumber = note.DokDagbogsnoteId;
                        // Does document exist?
                        // If not put data in dictionary
                        bool documentStartsWithDocNumber = false;
                        foreach (var document in documents)
                        {
                            var docSplitList = document.Split(new char[] { '\\' });
                            var doc = docSplitList.Last();
                            if (doc.StartsWith(documentNumber))
                            {
                                documentStartsWithDocNumber = true;
                                break;
                            }
                        }
                        if (documentStartsWithDocNumber == false)
                        {
                            MissingDocuments.Add(documentNumber, citizenCPR);
                        }

                    }
                }
            }

            #endregion Loop directories

        }
        public BB_Root GetDagbogsNoter(string dagbogsnotatFilePath)
        {
            DataHandler dataHandler = new DataHandler();

            string xmlString = System.IO.File.ReadAllText(dagbogsnotatFilePath);
            var xmlStringBytes = System.IO.File.ReadAllBytes(dagbogsnotatFilePath);

            var inputEncoding = Encoding.GetEncoding("iso-8859-1");
            var text = inputEncoding.GetString(xmlStringBytes);
            var output = Encoding.UTF8.GetBytes(text);
            var UTF8Output = Encoding.UTF8.GetString(output);


            XmlDocument notes = new XmlDocument();
            notes.LoadXml(UTF8Output);

            string json = JsonConvert.SerializeXmlNode(notes);
            BB_Root xmlNotesDocumentObject = JsonConvert.DeserializeObject<BB_Root>(json);
            return xmlNotesDocumentObject;
        }
        public BB_Note_Doc_Root GetDagbogsNoterDoc(string dagbogsnotatFilePath)
        {
            DataHandler dataHandler = new DataHandler();

            string xmlString = System.IO.File.ReadAllText(dagbogsnotatFilePath);
            var xmlStringBytes = System.IO.File.ReadAllBytes(dagbogsnotatFilePath);

            var inputEncoding = Encoding.GetEncoding("iso-8859-1");
            var text = inputEncoding.GetString(xmlStringBytes);
            var output = Encoding.UTF8.GetBytes(text);
            var UTF8Output = Encoding.UTF8.GetString(output);


            XmlDocument notes = new XmlDocument();
            notes.LoadXml(UTF8Output);

            string json = JsonConvert.SerializeXmlNode(notes);
            BB_Note_Doc_Root xmlNotesDocumentObject = JsonConvert.DeserializeObject<BB_Note_Doc_Root>(json);
            return xmlNotesDocumentObject;
        }
        [Test]
        public void procesBBData()
        {
            string environment = liveEnvironment;

            NexusAPI_processes processes = new NexusAPI_processes(environment);
            var api = processes.api;

            string dataFolderPath = "C:\\Users\\msch\\Downloads\\BB data til Nexus";

            string pathwayName = "Historiske notater - BB";
            string pathwayReferenceName = "Socialt og sundhedsfagligt Grundforløb";
            string pathwayReferenceChildName = "Udfører: Døgnstøtte Nebs Møllegard";

            #region Load directories
            var citizenDirectories = System.IO.Directory.GetDirectories(dataFolderPath);
            #endregion Load directories

            List<string> failedCitizens = new List<string>();

            #region Loop directories
            foreach (var directory in citizenDirectories)
            {
                // get folder name
                var folderNameSplitList = directory.Split('\\');
                string folderName = folderNameSplitList.Last();

                if (folderName != "130_Maria_El-Tahhan" && folderName != "100_Andreas_Jakobsen" && folderName != "106_Kian_Steffensen" && folderName != "75_test_")
                {
                    #region Get documents
                    var documents = System.IO.Directory.GetFiles(directory);
                    #endregion Get documents

                    #region Get data from xml folder
                    var xmlFiles = System.IO.Directory.GetFiles(directory + "\\xml");
                    #region Loop/Handle xml-files
                    var dagbogsnotatFilePath = xmlFiles.FirstOrDefault(x => x.Contains("dagbogsnoter.xml"));
                    var dagbogsnotatDocFilePath = xmlFiles.FirstOrDefault(x => x.Contains("dagbogsnoter_doc.xml"));
                    var documentFilePath = xmlFiles.FirstOrDefault(x => x.Contains("dokumenter"));
                    #region GetCitizenCPRFromFileName

                    var filePathSplitList = dagbogsnotatFilePath.Split(new char[] { '\\' });
                    string fileName = filePathSplitList.Last();

                    string cprDay = fileName.Substring(0, 2);
                    string cprMonth = fileName.Substring(3, 2);
                    string cprYear = fileName.Substring(6, 2);
                    string cprControl = fileName.Substring(9, 4);
                    string citizenCPR = cprDay + cprMonth + cprYear + cprControl;
                    #endregion GetCitizenCPRFromFileName



                    // does citizen exist?
                    var patient = api.GetPatientDetails(citizenCPR);
                    if (patient.PatientFound != false)
                    {
                        #region Handle dagbogsnoter
                        List<PwayRefChildSelf_JournalNote_RefObj_Root> journalNotes = processes.GetAllCitizenJournalNotes(citizenCPR, pathwayName, pathwayReferenceName, pathwayReferenceChildName);
                        HandleDagbogsNoter(processes, dagbogsnotatFilePath, dagbogsnotatDocFilePath, citizenCPR, environment, journalNotes);
                        #endregion Handle dagbogsnoter

                        #region Handle document
                        HandleDocuments(processes, documentFilePath, directory, citizenCPR, environment);
                        HandleNotesDocuments(processes, dagbogsnotatDocFilePath, directory + "\\dagbog_dok", citizenCPR, environment);
                        #endregion Handle documents

                        System.IO.File.AppendAllText("C:\\users\\msch\\desktop\\FoldersDone.txt", "\n" + folderName + " - Done.");
                    }
                    else
                    {
                        // patient not found - added to a string list
                        System.IO.File.AppendAllText("C:\\users\\msch\\desktop\\FailedCitizens.txt", "\n" + folderName);
                    }
                    #endregion Loop/Handle xml-files
                    #endregion Get data from xml folder
                }


            }


            System.IO.File.AppendAllLines("FailedCitizens.txt", failedCitizens);


            #endregion Loop directories



        }
        [Test]
        public void TestSaveListToFile()
        {
            List<string> failedCitizens = new List<string>();
            failedCitizens.Add("folder10");
            failedCitizens.Add("folder11");
            failedCitizens.Add("folder12");
            failedCitizens.Add("folder13");
            failedCitizens.Add("folder14");

            //System.IO.File.WriteAllLines("C:\\Users\\msch\\Desktop\\SavedList.txt", failedCitizens);
            System.IO.File.AppendAllLines("C:\\Users\\msch\\Desktop\\SavedList.txt", failedCitizens);
        }
        [Test]
        public void TestSaveStringToFile()
        {
            System.IO.File.AppendAllText("C:\\Users\\msch\\Desktop\\SavedList.txt", "test");
        }
        [Test]
        public void HandleDocsTest()
        {
            NexusAPI_processes processes = new NexusAPI_processes(reviewEnvironment);

            string documentFolderPath = "C:\\Users\\msch\\Desktop\\1237_Samtykkeerklæring vedr. indhente alle tilgængelige oplysninger fra psykologiske undersøgelser - TestPersonFornavn TestPersonEfternavn.txt";
            var xmlFiles = System.IO.Directory.GetFiles(documentFolderPath);
            string documentFilePath = xmlFiles.FirstOrDefault(x => x.Contains("dokumenter"));
            string citizenCPR = "";
            HandleDocuments(processes, documentFilePath, documentFilePath, citizenCPR, reviewEnvironment);
        }
        public void HandleDocuments(NexusAPI_processes processes, string xmlDocumentFilePath, string documentFolderPath, string citizenCPR, string enviroment)
        {
            var api = processes.api;

            string xmlString = System.IO.File.ReadAllText(xmlDocumentFilePath);
            var xmlStringBytes = System.IO.File.ReadAllBytes(xmlDocumentFilePath);

            var inputEncoding = Encoding.GetEncoding("iso-8859-1");
            var text = inputEncoding.GetString(xmlStringBytes);
            var output = Encoding.UTF8.GetBytes(text);
            var UTF8Output = Encoding.UTF8.GetString(output);


            XmlDocument doc = new XmlDocument();
            doc.LoadXml(UTF8Output);

            string json = JsonConvert.SerializeXmlNode(doc);
            BB_Doc_Root xmlDocumentObject = JsonConvert.DeserializeObject<BB_Doc_Root>(json);

            // get document for upload
            var files = System.IO.Directory.GetFiles(documentFolderPath);
            string pathwayName = "Historiske dokumenter - BB";
            string pathwayReferenceName = "Socialt og sundhedsfagligt Grundforløb";
            string pathwayReferenceChildName = "Udfører: Døgnstøtte Nebs Møllegard";
            string documentTag = "Historiske dokumenter - BB";
            var existingDocuments = api.GetCitizenPathwayReferencesChildDocuments(citizenCPR, pathwayName, pathwayReferenceName, pathwayReferenceChildName);

            foreach (var document in xmlDocumentObject.Dokumenter.Document)
            {
                //do stuff
                if (!document.Filnavn.ToLower().Contains("Magtanvendelse".ToLower()))
                {


                    string docName = document.Filnavn;
                    string docDescription = document.Beskrivelse;


                    var chosenFile = files.FirstOrDefault(x => x.Contains(document.Lbnummer));
                    if (chosenFile != null)
                    {
                        var chosenFileSplitList = chosenFile.Split(new char[] { '\\' });
                        string chosenFileName = chosenFileSplitList.Last();
                        //Upload document
                        //var pWayRefChildren = api.GetCitizenPathwayReferencesChildren(citizenCPR, pathwayName, pathwayReferenceName);
                        bool documentExists = DoesDocumentExist(processes, existingDocuments, chosenFileName);
                        if (!documentExists)
                        {
                            var uploadedDocument = processes.UploadPatientPathwayChildDocumentToNexus(citizenCPR, "Historiske dokumenter - BB", "Socialt og sundhedsfagligt Grundforløb", chosenFile, "Udfører: Døgnstøtte Nebs Møllegard", "Historiske dokumenter - BB", docDescription);
                        }
                    }
                    else
                    {
                        System.IO.File.AppendAllText("C:\\users\\msch\\desktop\\failedDocuments.txt", "Dok løbenr. " + document.Lbnummer + " - folder: " + documentFolderPath);
                    }

                }

                // loop docs end
            }
        }
        public void HandleNotesDocuments(NexusAPI_processes processes, string xmlDocumentFilePath, string documentFolderPath, string citizenCPR, string enviroment)
        {
            var api = processes.api;

            string xmlString = System.IO.File.ReadAllText(xmlDocumentFilePath);
            var xmlStringBytes = System.IO.File.ReadAllBytes(xmlDocumentFilePath);

            var inputEncoding = Encoding.GetEncoding("iso-8859-1");
            var text = inputEncoding.GetString(xmlStringBytes);
            var output = Encoding.UTF8.GetBytes(text);
            var UTF8Output = Encoding.UTF8.GetString(output);


            XmlDocument doc = new XmlDocument();
            doc.LoadXml(UTF8Output);

            string json = JsonConvert.SerializeXmlNode(doc);
            BB_Note_Doc_Root xmlDocumentObject = JsonConvert.DeserializeObject<BB_Note_Doc_Root>(json);

            // get document for upload
            var files = System.IO.Directory.GetFiles(documentFolderPath);
            string pathwayName = "Historiske dokumenter - BB";
            string pathwayReferenceName = "Socialt og sundhedsfagligt Grundforløb";
            string pathwayReferenceChildName = "Udfører: Døgnstøtte Nebs Møllegard";
            string documentTag = "Historiske dokumenter - BB";
            var existingDocuments = api.GetCitizenPathwayReferencesChildDocuments(citizenCPR, pathwayName, pathwayReferenceName, pathwayReferenceChildName);

            foreach (var document in xmlDocumentObject.Dokumenter.Dokument)
            {
                //do stuff
                if (!document.Dokument.ToLower().Contains("Magtanvendelse".ToLower()))
                {


                    string docName = document.Dokument;

                    var chosenFile = files.FirstOrDefault(x => x.Contains(document.DokDagbogsnoteId));
                    if (chosenFile != null)
                    {
                        var chosenFileSplitList = chosenFile.Split(new char[] { '\\' });
                        string chosenFileName = chosenFileSplitList.Last();
                        //Upload document
                        //var pWayRefChildren = api.GetCitizenPathwayReferencesChildren(citizenCPR, pathwayName, pathwayReferenceName);
                        bool documentExists = DoesDocumentExist(processes, existingDocuments, chosenFileName);
                        if (!documentExists)
                        {
                            var uploadedDocument = processes.UploadPatientPathwayChildDocumentToNexus(citizenCPR, "Historiske dokumenter - BB", "Socialt og sundhedsfagligt Grundforløb", chosenFile, "Udfører: Døgnstøtte Nebs Møllegard", "Historiske dokumenter - BB");
                        }
                    }

                }

                // loop docs end
            }
        }


        public bool DoesDocumentExist(NexusAPI_processes processes, List<PathwayReferences_Child> documents, string fileName)
        {
            var api = processes.api;

            bool exists = false;
            foreach (var item in documents)
            {
                if (item.Name == fileName)
                {
                    exists = true;
                    break;
                }
            }
            return exists;
        }

        [Test]
        public void UploadDocumentToNexusOnPathwayBB()
        {
            NexusAPI_processes processes = new NexusAPI_processes(reviewEnvironment);
            var api = processes.api;
            string filePath = "C:\\Users\\msch\\Desktop\\1237_Samtykkeerklæring vedr. indhente alle tilgængelige oplysninger fra psykologiske undersøgelser - TestPersonFornavn TestPersonEfternavn.txt";

            //var uploadedDocument = processes.UploadPatientPathwayDocumentToNexus(
            //    "1409106699",
            //    "Historiske dokumenter - BB", //Borgerforløb
            //    "Socialt og sundhedsfagligt Grundforløb", //Grundforløb
            //    "Udfører: Døgnstøtte Nebs Møllegard", //Forløb
            //    filePath,
            //    "Historiske dokumenter - BB");

            var uploadedDocument = processes.UploadPatientPathwayChildDocumentToNexus("", "Historiske dokumenter - BB", "Socialt og sundhedsfagligt Grundforløb", filePath, "Udfører: Døgnstøtte Nebs Møllegard", "Historiske dokumenter - BB", "testBeskrivelse");

            Assert.IsTrue(uploadedDocument.response.IsSuccessful);
        }



        public void HandleDagbogsNoter(NexusAPI_processes processes, string xmlFilePath, string xmlFileDocPath, string citizenCPR, string environment, List<PwayRefChildSelf_JournalNote_RefObj_Root> journalNotes)
        {
            DataHandler dataHandler = new DataHandler();

            string xmlString = System.IO.File.ReadAllText(xmlFilePath);
            var xmlStringBytes = System.IO.File.ReadAllBytes(xmlFilePath);

            var inputEncoding = Encoding.GetEncoding("iso-8859-1");
            var text = inputEncoding.GetString(xmlStringBytes);
            var output = Encoding.UTF8.GetBytes(text);
            var UTF8Output = Encoding.UTF8.GetString(output);


            XmlDocument notes = new XmlDocument();
            notes.LoadXml(UTF8Output);

            string json = JsonConvert.SerializeXmlNode(notes);
            BB_Root xmlNotesDocumentObject = JsonConvert.DeserializeObject<BB_Root>(json);

            string jsonDoc = dataHandler.ConvertXmlToJsonString(xmlFileDocPath);
            BB_Note_Doc_Root xmlNotesDocDocumentObject = JsonConvert.DeserializeObject<BB_Note_Doc_Root>(jsonDoc);

            foreach (var note in xmlNotesDocumentObject.Dagbogsnoter.Dagbogsnote)
            {
                if (note.Dato != "  /  /") // If dato is blank it has previously shown, that the note is not valid
                {
                    if (note.Medarbejder != null) // If medarbejder is blank it has previously shown, that the note is not valid
                    {
                        //do stuff
                        string pathwayName = "Historiske notater - BB";
                        string pathwayReferenceName = "Socialt og sundhedsfagligt Grundforløb";
                        string pathwayReferenceChildName = "Udfører: Døgnstøtte Nebs Møllegard";

                        // handling if there's a document attached to the note
                        string subject = string.Empty;
                        bool hasDocument = false;
                        foreach (var noteDoc in xmlNotesDocDocumentObject.Dokumenter.Dokument)
                        {
                            if (noteDoc.DokDagbogsnoteId == note.DagbogsnoteId)
                            {
                                hasDocument = true;
                                break;
                            }
                        }

                        if (!hasDocument)
                        {
                            subject = note.DagOverskrift;
                        }
                        else
                        {
                            subject = note.DagOverskrift + " - (Dokument nr. " + note.DagbogsnoteId + ")";
                        }


                        string observation = note.FokTekst + " (Oprettet af: " + note.Medarbejder + ")" + "\n\n" + note.DagTekst;

                        DateTime observationTimeStamp = new DateTime(Convert.ToInt32(note.Dato.Substring(6, 4)), Convert.ToInt32(note.Dato.Substring(3, 2)), Convert.ToInt32(note.Dato.Substring(0, 2)));
                        UploadJournalNote(processes, citizenCPR, pathwayName, pathwayReferenceName, pathwayReferenceChildName, subject, observation, observationTimeStamp, environment, journalNotes);
                        // loop notes end
                    }
                }

            }
        }

        [Test]
        public void DeleteAllJournalNotesOld()
        {
            NexusAPI_processes processes = new NexusAPI_processes(liveEnvironment);
            var api = processes.api;
            string pathwayName = "Historiske notater - BB";
            string pathwayReferenceName = "Socialt og sundhedsfagligt Grundforløb";
            string pathwayReferenceChildName = "Udfører: Døgnstøtte Nebs Møllegard";
            var journalNotes = processes.GetAllCitizenJournalNotes("", pathwayName, pathwayReferenceName, pathwayReferenceChildName);

            foreach (var note in journalNotes)
            {
                if (note.Tags[0].Name == "Historiske notater - BB")
                {
                    string deleteURL = note.Links.Delete.Href;
                    api.CallAPI(api, deleteURL, Method.Delete);
                }
            }
        }
        [Test]
        public void DeleteAllJournalNotes()
        {
            NexusAPI_processes processes = new NexusAPI_processes(liveEnvironment);
            var api = processes.api;
            string pathwayName = "Historiske notater - BB";
            string pathwayReferenceName = "Socialt og sundhedsfagligt Grundforløb";
            string pathwayReferenceChildName = "Udfører: Døgnstøtte Nebs Møllegard";
            processes.DeleteAllCitizenJournalNotesDirectly("", pathwayName, pathwayReferenceName, pathwayReferenceChildName, pathwayName);
        }

        
    }
} 
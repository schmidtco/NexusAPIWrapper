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
using NexusAPIWrapper.HomeRessource.Preferences.ACTIVITYLIST.ACTIVITYLIST_Content.Content.Pages.Links.Content._Root.Links.ReferencedObject._Root.Links.TransformedBody;
using NUnit.Framework;
using Org.BouncyCastle.Utilities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace NexusAPITest_Merudgifter
{
    public class Tests
    {
        

        string liveEnvironment = "live";
        string reviewEnvironment = "review";
        readonly string nancyBerggrenTestCPR = "251248-9996";

        [Test]
        public void UploadDocumentsOnMerudgifter()
        {
            NexusAPI_processes processes = new NexusAPI_processes(liveEnvironment);
            var api = processes.api;
            string rootFolder = "\\\\rkfil02\\koncerncenter\\Vitae-Nexus\\Historisk data\\Merudgifter";
            //Get citizen folders
            var citizenFolders = System.IO.Directory.GetDirectories(rootFolder);
            //foreach folder upload documents to Nexus on "historisk data fra vitae"

            string pathway = "Konvertering fra CSC  – Merudgifter (15.11.22)"; // borgerforløb
            string pathwayReference = "Historiske data fra Vitae"; // grundforløb

            foreach (var folder in citizenFolders)
            {
                var folderSplitList = api.dataHandler.SplitStringByString(folder);
                //var folderSplitList = folder.Split(new string[] { "\\" }, StringSplitOptions.None);
                string citizenCPR = folderSplitList.Last();
                
                //citizenCPR = nancyBerggrenTestCPR;

                var patient = api.GetPatientDetails(citizenCPR);
                var patientLinks = patient.Links;
                var patientPreferences = api.GetPatientPreferences(citizenCPR);
                var pathways = api.GetCitizenPathways(citizenCPR);
                var patientPathwayAss = api.HentAabneGrundforloeb(citizenCPR);
                var availableProgramPathways = api.GetPatientAvailableProgramPathways(citizenCPR);
                var chosenP = patientPathwayAss.FirstOrDefault(x => x.Name == pathwayReference);

                AvailablePathwayAssociations_Self_Root pathwaySelf = new AvailablePathwayAssociations_Self_Root();
                var folderFiles = System.IO.Directory.GetFiles(folder);

                if (chosenP == null && folderFiles.Length > 0)
                {
                    // grundforløb er ikke oprettet, og skal oprettes
                    string enrollmentLink = availableProgramPathways.FirstOrDefault(x => x.Name == pathwayReference).Links.Enroll.Href;
                    var gpathwaySelf = processes.OpretGrundforloeb(citizenCPR, pathwayReference);
                    
                }

                Dictionary<string, string> replacementDictionary = new Dictionary<string, string>()
                {
                    {"Hjælpemidler - ", "Hjælpemidler" },
                    {"Merudgifter (Version of 1.1) - ", "Merudgifter" },
                    {"Social myndighed - ", "Social" },
                    {"Biler - ", "Biler" },
                    {"Klageregistrering Myndighed - ", "Klageregistrering Myndighed" },
                    {"Kropsbårne hjælpemidler - ", "Kropsbårne hjælpemidler - " },
                    {"Boliger - Omsorg - ", "Boliger - Omsorg" },
                    {"FSIII - V1.7 - ", "FSIII" },
                    {"Kørsel - ", "Kørsel" },
                    {"BPA - ", "BPA" }
                };
                
                var pathwayDocuments = api.GetCitizenPathwayDocuments(citizenCPR,pathway);
                foreach (var file in folderFiles)
                {
                    string systemFileName = System.IO.Path.GetFileName(file);

                    if (systemFileName != "Thumbs.db" && !systemFileName.Contains("~$")) // Files can be hidden in the UI, but visible to the system.
                    {
                        string newFileFullPath = api.HandleFileNameConstraints(file,replacementDictionary);
                        string newFileName = api.dataHandler.SplitStringByString(newFileFullPath).Last();

                        if (newFileName.Contains("Merudgifter"))
                        {
                            if (!processes.DoesDocumentExist(pathwayDocuments, newFileName))
                            {
                                var webResult = processes.UploadPatientPathwayDocumentToNexus(citizenCPR, pathway, pathwayReference, file, newFileName, newFileName, "Historiske data fra CSC  – Merudgifter");//  kun merudgiftdokumenter skal have samme tag
                            }
                        }
                        
                    }
                }
            }
        }
        [Test]
        public void T1()
        {
            NexusAPI_processes processes = new NexusAPI_processes();
            var api = processes.api;
            string pathwayName = "Dokumenttilknytning fra Vitae"; // borgerforløb

            string pattern = @"(((0[1-9])|([12][0-9])|(3[01]))-((0[0-9])|(1[012]))-((20[012]\d|19\d\d)|(1\d|2[0123])))";
            string input = "Biler - _nr6_12-07-2018_Brev fra Ankestyrelsen, støtte til køb af ny bil_attachment_1_of_1";
            Match match = Regex.Match(input,pattern, RegexOptions.IgnoreCase);

            

        }

        [Test]
        public void EnrollToHistoriskeDataFraVitae()
        {
            NexusAPI_processes processes = new NexusAPI_processes();
            var api = processes.api;

            var d = processes.EnrollPatientToProgramPathway(nancyBerggrenTestCPR, "Historiske data fra Vitae");
            string selfReference = d.Links.SelfReference.Href;

            var webResult = api.CallAPI(api, selfReference, Method.Get); // will return a patientPathwayReference, where we can upload documents to
            var pathway = JsonConvert.DeserializeObject<PathwayReferences_Root>(webResult.Result.ToString());

            
            /*
             * {"type":"patientPathwayReference","id":null,"version":0,"name":"Historiske data fra Vitae","date":"2024-03-20T12:18:34.000+0000","children":[],"activityIdentifier":{"identifier":"PATIENT_PATHWAY:65452","type":"PATIENT_PATHWAY","activityId":65452,"_links":{}},"additionalInfo":null,"patientPathwayId":65452,"programPathwayId":4808,"pathwayTypeId":32,"parentPathwayId":null,"pathwayStatus":"ACTIVE","_links":{"availableNestedProgramPathways":{"href":"https://ringsted.nexus-review.kmd.dk:443/api/core/mobile/ringsted/v2/patientPathways/65452/availableNestedProgramPathways"},"close":{"href":"https://ringsted.nexus-review.kmd.dk:443/api/core/mobile/ringsted/v2/patientPathways/65452/closure"},"unclosableReferences":{"href":"https://ringsted.nexus-review.kmd.dk:443/api/core/mobile/ringsted/v2/patientPathways/65452/unclosableReferences"},"withdrawPatient":{"href":"https://ringsted.nexus-review.kmd.dk:443/api/core/mobile/ringsted/v2/patientPathways/65452"},"availableProfessionals":{"href":"https://ringsted.nexus-review.kmd.dk:443/api/core/mobile/ringsted/v2/patientPathways/65452/availableProfessionals"},"availableOrganisations":{"href":"https://ringsted.nexus-review.kmd.dk:443/api/core/mobile/ringsted/v2/patientPathways/65452/availableOrganisations"},"availableFormDefinitions":{"href":"https://ringsted.nexus-review.kmd.dk:443/api/core/mobile/ringsted/v2/pathways/65452/formDefinitions"},"availableLetterTemplates":{"href":"https://ringsted.nexus-review.kmd.dk:443/api/core/mobile/ringsted/v2/patientPathways/65452/availableLetterTemplates"},"dynamicTemplate":{"href":"https://ringsted.nexus-review.kmd.dk:443/api/core/mobile/ringsted/v2/dynamictemplate/1?pathwayId=65452"},"availablePathwayDistributions":{"href":"https://ringsted.nexus-review.kmd.dk:443/api/core/mobile/ringsted/v2/pathways/65452/pathwayDistributions"},"citizenAccountPrototype":{"href":"https://ringsted.nexus-review.kmd.dk:443/api/core/mobile/ringsted/v2/patientPathways/65452/citizenAccounts/prototype"},"patientNetworkContactPrototype":{"href":"https://ringsted.nexus-review.kmd.dk:443/api/core/mobile/ringsted/v2/patientPathways/1/contact/network/prototype?pathwayId=65452&placement=PATHWAY"},"availableAssociationGroupDefinitions":{"href":"https://ringsted.nexus-review.kmd.dk:443/api/core/mobile/ringsted/v2/patientPathways/65452/associationGroupDefinitions"},"activePrograms":{"href":"https://ringsted.nexus-review.kmd.dk:443/api/core/mobile/ringsted/v2/patients/1/programs/active"},"availableEvents":{"href":"https://ringsted.nexus-review.kmd.dk:443/api/core/mobile/ringsted/v2/events/65452/availableEvents"},"availableEventTypes":{"href":"https://ringsted.nexus-review.kmd.dk:443/api/core/mobile/ringsted/v2/eventTypes/active"},"addEventsToPathway":{"href":"https://ringsted.nexus-review.kmd.dk:443/api/core/mobile/ringsted/v2/events/65452/events"},"eventPrototype":{"href":"https://ringsted.nexus-review.kmd.dk:443/api/core/mobile/ringsted/v2/events/65452/prototype"},"documentPrototype":{"href":"https://ringsted.nexus-review.kmd.dk:443/api/document-microservice/ringsted/documents/prototype/65452?patientId=1&placement=PATHWAY"},"eopFfbProblemPrototypes":{"href":"https://ringsted.nexus-review.kmd.dk:443/api/core/mobile/ringsted/v2/eop/patients/1/problems/prototypes?type=FFB"},"eopFfbVisitationPrototype":{"href":"https://ringsted.nexus-review.kmd.dk:443/api/core/mobile/ringsted/v2/eop/patients/1/visitations/prototypes?type=FFB"},"eopFfbFollowUpPrototype":{"href":"https://ringsted.nexus-review.kmd.dk:443/api/core/mobile/ringsted/v2/eop/patients/1/followup/prototypes?type=FFB&pathwayId=65452"},"eopFfbActivities":{"href":"https://ringsted.nexus-review.kmd.dk:443/api/core/mobile/ringsted/v2/eop/patients/1/activities/FFB?pathwayId=65452&activityTypes=EOP_FOLLOW_UP"},"eopFfbVisitationsForCreatingProblem":{"href":"https://ringsted.nexus-review.kmd.dk:443/api/core/mobile/ringsted/v2/eop/patients/1/visitations/closed?type=FFB"},"eopFfbProblemPrototypeClassifications":{"href":"https://ringsted.nexus-review.kmd.dk:443/api/core/mobile/ringsted/v2/eop/classifications/scorable?type=FFB&patientId=1"},"self":{"href":"https://ringsted.nexus-review.kmd.dk:443/api/core/mobile/ringsted/v2/patientPathways/65452/selfReference"},"patientPathway":{"href":"https://ringsted.nexus-review.kmd.dk:443/api/core/mobile/ringsted/v2/patientPathways/65452"},"copyPrototype":{"href":"https://ringsted.nexus-review.kmd.dk:443/api/core/mobile/ringsted/v2/patientPathways/65452/copies/prototype"}}}
             */
        }

    }
} 
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NexusAPIWrapper;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
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
            NexusHomeRessource _ressource = new NexusHomeRessource(_NexusAPI.clientCredentials.Host,_NexusAPI.tokenObject.AccessToken);
            NexusResult result = _ressource.GetHomeRessource();
            Assert.IsTrue(result.httpStatusCode == System.Net.HttpStatusCode.OK);
        }
        [Test]
        public void testSearchPatientByCPR()
        {
            NexusAPI _NexusAPI = new NexusAPI(environment);
            var details = _NexusAPI.GetPatientDetailsByCPR("280234-0403");
            Assert.IsNotNull(details);

            var links = _NexusAPI.GetNestedData(details,"_links");
        }
        [Test]
        public void testGetPatientDetailsLinks()
        {
            NexusAPI _NexusAPI = new NexusAPI(environment);
            var links = _NexusAPI.GetPatientDetailsLinks_ByCPR("280234-0403");
            Assert.IsNotNull(links);
        }
        [Test]
        public void testGetPatientPreferences()
        { 
            NexusAPI _nexusAPI = new NexusAPI(environment);
            var preferences = _nexusAPI.GetPatientPreferences_ByCPR("280234-0403");
            Assert.IsNotNull(preferences);
        }
        [Test]
        public void testGetCitizenPathways()
        {
            NexusAPI _nexusAPI = new NexusAPI(environment);
            var pathways = _nexusAPI.GetCitizenPathways_ByCPR("280234-0403");
            Assert.IsNotNull(pathways);
        }
        [Test]
        public void testGetCitizenPathwayLink()
        {
            NexusAPI _nexusAPI = new NexusAPI(environment);
            var pathwayLink = _nexusAPI.GetCitizenPathwayLink_ByCPR("280234-0403", "Dokumenttilknytning fra Vitae");
            Assert.IsNotNull(pathwayLink);
        }

        [Test]
        public void test()
        {
            NexusAPI nexusAPI = new NexusAPI(environment);
            string CitizenCPR = "280234-0403";
            string pathwayName = "Dokumenttilknytning fra Vitae";
            //var pathwayLink = nexusAPI.GetCitizenPathwayLink_ByCPR(CitizenCPR, pathwayName);
            var df = nexusAPI.GetCitizenPathwayInfo_ByCPR(CitizenCPR, pathwayName);
            var links = df["_links"];
            var orgs = df["organizations"];

            var dict = nexusAPI.ConvertStringObjectToSortedDictionary(orgs);
        }




    }
}

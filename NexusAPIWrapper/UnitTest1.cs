using NexusAPIWrapper;
using NUnit.Framework;

namespace NexusAPITest
{
    public class Tests
    {
        //[SetUp]
        //public void Setup()
        //{
        //}
        [Test]
        public void testAccess()
        {
            NexusAPI _NexusAPI = new NexusAPI();
            Assert.IsFalse(string.IsNullOrEmpty(_NexusAPI.tokenObject.AccessToken));
        }
        [Test]
        public void testHomeResource()
        {
            NexusAPI _NexusAPI = new NexusAPI();
            NexusHomeRessource _ressource = new NexusHomeRessource();
            NexusResult result = _ressource.GetHomeRessource();
            Assert.IsTrue(result.httpStatusCode == System.Net.HttpStatusCode.OK);

        }
        [Test]
        public void testSearchPatientByCPR()
        {
            NexusAPI _NexusAPI = new NexusAPI();
            NexusResult result = _NexusAPI.GetPatientDetailsByCPR("210300-9996");
            Assert.IsTrue(result.httpStatusCode == System.Net.HttpStatusCode.OK);
        }
        
    }
}

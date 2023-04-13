using NexusAPIWrapper;
using NUnit.Framework;

namespace NexusAPITest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }
        [Test]
        public void testAccess()
        {
            NexusAPI _NexusAPI = new NexusAPI();
            Assert.IsFalse(string.IsNullOrEmpty(_NexusAPI.AccessToken));

            _NexusAPI.Initialize();
            Assert.IsFalse(string.IsNullOrEmpty(_NexusAPI.AccessToken));
        }
        [Test]
        public void testHomeResource()
        {
            NexusAPI _NexusAPI = new NexusAPI();
            NexusResult result = _NexusAPI.GetHomeResources();
            Assert.IsTrue(result.httpStatusCode == System.Net.HttpStatusCode.OK);

        }
        [Test]
        public void testSearchPatientByName()
        {
            NexusAPI _nkNexusAPI = new NexusAPI();
            NexusResult result = _nkNexusAPI.SearchPatient("Kaja Test Hansen");
            Assert.IsFalse(string.IsNullOrEmpty(_nkNexusAPI.AccessToken));
            Assert.IsTrue(result.httpStatusCode == System.Net.HttpStatusCode.OK);
        }
        [Test]
        public void testSearchPatientByCPR()
        {
            NexusAPI _nkNexusAPI = new NexusAPI();
            NexusResult result = _nkNexusAPI.SearchPatient("210300-9996");
            Assert.IsTrue(result.httpStatusCode == System.Net.HttpStatusCode.OK);
            NexusPatient patient = result.Result as NexusPatient;

            Assert.IsTrue(patient != null);
            Assert.IsTrue(patient.Data.PatientData.Id == 41275);

            foreach (string key in patient.Data.PatientData.detailsLink.Resources.Keys)
            {
                Assert.IsTrue(patient.Data.PatientData.detailsLink.Resources[key].Contains(patient.Data.PatientData.Id.ToString()));
            }

            Assert.IsTrue(patient.Data.PatientData.cpr == "210300-9996");
            Assert.IsTrue(patient.Data.PatientData.fullName == "Kaja Test Hansen");
            Assert.IsTrue(patient.Data.PatientData.Address.Street == "Testsøjlerne 4");
            Assert.IsTrue(patient.Data.PatientData.Age == 22);
            Assert.IsTrue(patient.Data.PatientData.Gender.ToLower() == "female");
            Assert.IsTrue(patient.Data.PatientData.PatientState.ToLower() == "aktiv");
        }
        [Test]
        public void testDocuments()
        {
            NexusAPI _nkNexusAPI = new NexusAPI();
            NexusResult result = _nkNexusAPI.Getdocuments(/*cpr*/"210300-9996");
        }

    }
}

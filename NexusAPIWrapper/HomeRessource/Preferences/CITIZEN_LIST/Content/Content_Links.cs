using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class Content_Links
    {
        [JsonProperty("patientData")]
        public Content_PatientData PatientData;

        [JsonProperty("patientGrantInformation")]
        public Content_PatientGrantInformation PatientGrantInformation;
    }

}
using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientDetailsSearch_Root
    {
        [JsonProperty("isPatientAccessible")]
        public bool? IsPatientAccessible;

        [JsonProperty("notAccessiblePatient")]
        public object NotAccessiblePatient;

        [JsonProperty("patient")]
        public PatientDetailsSearch_Patient Patient;

        [JsonProperty("patientAccessible")]
        public bool? PatientAccessible;

        [JsonProperty("_links")]
        public PatientDetailsSearch_Links Links;
    }

}
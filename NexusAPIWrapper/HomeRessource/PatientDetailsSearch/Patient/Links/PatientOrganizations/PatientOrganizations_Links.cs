using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientOrganizations_Links
    {
        [JsonProperty("self")]
        public PatientOrganizations_Self Self;

        [JsonProperty("stsOrganization")]
        public PatientOrganizations_StsOrganization StsOrganization;

        [JsonProperty("removeFromPatient")]
        public PatientOrganizations_RemoveFromPatient RemoveFromPatient;

        [JsonProperty("update")]
        public PatientOrganizations_Update Update;
    }

}
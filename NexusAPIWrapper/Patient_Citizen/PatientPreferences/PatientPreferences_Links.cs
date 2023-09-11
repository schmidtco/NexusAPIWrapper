using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientPreferences_Links
    {
        [JsonProperty("self")]
        public PatientPreferences_Self Self;

        [JsonProperty("stsOrganization")]
        public PatientPreferences_StsOrganization StsOrganization;
    }

}
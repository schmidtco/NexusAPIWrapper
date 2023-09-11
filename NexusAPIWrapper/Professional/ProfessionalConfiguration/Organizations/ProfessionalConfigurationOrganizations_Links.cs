using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class ProfessionalConfigurationOrganizations_Links
    {
        [JsonProperty("self")]
        public ProfessionalConfigurationOrganizations_Self Self;

        [JsonProperty("stsOrganization")]
        public ProfessionalConfigurationOrganizations_StsOrganization StsOrganization;
    }

}
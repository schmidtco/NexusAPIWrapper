using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class OrganizationsTree_Links
    {
        [JsonProperty("self")]
        public OrganizationsTree_Self Self;

        [JsonProperty("stsOrganization")]
        public OrganizationsTree_StsOrganization StsOrganization;
    }

}
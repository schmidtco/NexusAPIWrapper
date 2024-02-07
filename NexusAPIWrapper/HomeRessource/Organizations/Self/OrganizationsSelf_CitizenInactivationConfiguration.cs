using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class OrganizationsSelf_CitizenInactivationConfiguration
    {
        [JsonProperty("action")]
        public string Action;

        [JsonProperty("targetOrganization")]
        public object TargetOrganization;

        [JsonProperty("primary")]
        public bool? Primary;

        [JsonProperty("_links")]
        public OrganizationsSelf_Links Links;
    }

}
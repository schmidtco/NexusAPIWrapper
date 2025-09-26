using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatCond_Assign_OrganizationAssignee
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("displayName")]
        public string DisplayName;

        [JsonProperty("organizationId")]
        public int? OrganizationId;

        [JsonProperty("_links")]
        public PatCond_Assign_Links Links;
    }

}
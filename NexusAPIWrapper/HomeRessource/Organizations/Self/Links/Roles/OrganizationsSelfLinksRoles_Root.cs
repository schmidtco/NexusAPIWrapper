using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class OrganizationsSelfLinksRoles_Root
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("description")]
        public string Description;

        [JsonProperty("active")]
        public bool? Active;

        [JsonProperty("uid")]
        public string Uid;

        [JsonProperty("_links")]
        public OrganizationsSelfLinksRoles_Links Links;
    }

}
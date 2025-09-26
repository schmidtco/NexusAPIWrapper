using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class OrganizationsSelfLinksRolesSelf_Root
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
        public object Uid;

        [JsonProperty("_links")]
        public OrganizationsSelfLinksRolesSelf_Links Links;
    }

}
using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class CitPathwSelfDocPrototype_AvailableTags_Root
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("description")]
        public string Description;

        [JsonProperty("grouping")]
        public CitPathwSelfDocPrototype_AvailableTags_ Grouping;

        [JsonProperty("tagOrigin")]
        public string TagOrigin;

        [JsonProperty("active")]
        public bool? Active;

        [JsonProperty("_links")]
        public CitPathwSelfDocPrototype_AvailableTags_Links Links;
    }

}
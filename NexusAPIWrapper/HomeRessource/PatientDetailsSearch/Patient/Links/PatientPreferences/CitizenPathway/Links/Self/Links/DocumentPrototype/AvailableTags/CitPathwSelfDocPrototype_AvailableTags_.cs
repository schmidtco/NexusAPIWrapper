using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class CitPathwSelfDocPrototype_AvailableTags_
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("_links")]
        public CitPathwSelfDocPrototype_AvailableTags_Links Links;
    }

}
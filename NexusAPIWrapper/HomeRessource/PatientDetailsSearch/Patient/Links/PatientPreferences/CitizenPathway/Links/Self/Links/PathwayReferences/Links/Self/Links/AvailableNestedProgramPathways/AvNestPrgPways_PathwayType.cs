using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class AvNestPrgPways_PathwayType
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("active")]
        public bool? Active;

        [JsonProperty("root")]
        public bool? Root;

        [JsonProperty("_links")]
        public AvNestPrgPways_Links Links;
    }

}
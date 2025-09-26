using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class AvCondClassCondPrototype_Type
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("abbreviation")]
        public string Abbreviation;

        [JsonProperty("_links")]
        public AvCondClassCondPrototype_Links Links;
    }

}
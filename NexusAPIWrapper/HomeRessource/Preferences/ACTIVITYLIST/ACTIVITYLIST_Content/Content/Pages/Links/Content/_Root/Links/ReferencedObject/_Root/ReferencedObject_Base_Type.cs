using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class ReferencedObject_Base_Type
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("abbreviation")]
        public string Abbreviation;

        [JsonProperty("_links")]
        public ReferencedObject_Base_Links Links;
    }

}
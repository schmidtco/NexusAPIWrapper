using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class Content_Page_Type
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("abbreviation")]
        public string Abbreviation;

        [JsonProperty("_links")]
        public Content_Page_Links Links;
    }

}
using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class Content_Page_CitizenRegistryConfiguration
    {
        [JsonProperty("updateEnabled")]
        public bool? UpdateEnabled;

        [JsonProperty("_links")]
        public Content_Page_Links Links;
    }
}
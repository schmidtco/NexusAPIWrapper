using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class Content_Page
    {
        [JsonProperty("_links")]
        public Content_Links Links;
    }

}
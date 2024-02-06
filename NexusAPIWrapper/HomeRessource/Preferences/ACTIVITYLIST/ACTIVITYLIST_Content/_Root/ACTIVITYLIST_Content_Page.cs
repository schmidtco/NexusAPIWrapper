using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class ACTIVITYLIST_Content_Page
    {
        [JsonProperty("_links")]
        public ACTIVITYLIST_Content_Links Links;
    }

}
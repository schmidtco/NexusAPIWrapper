using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class ACTIVITYLIST_ViewType
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("_links")]
        public ACTIVITYLIST_Links Links;
    }

}
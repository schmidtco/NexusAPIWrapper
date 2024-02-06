using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class ACTIVITYLIST_Pages_Content_GeoCoordinates
    {
        [JsonProperty("x")]
        public object X;

        [JsonProperty("y")]
        public object Y;

        [JsonProperty("present")]
        public bool? Present;
    }

}
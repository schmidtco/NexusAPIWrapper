using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PathwayReferences_ActivityIdentifier
    {
        [JsonProperty("identifier")]
        public string Identifier;

        [JsonProperty("type")]
        public string Type;

        [JsonProperty("activityId")]
        public int? ActivityId;

        [JsonProperty("_links")]
        public PathwayReferences_Links Links;
    }

}
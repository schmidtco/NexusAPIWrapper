using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PathwayReferencesChildSelf_BB_ActivityIdentifier
    {
        [JsonProperty("identifier")]
        public string Identifier;

        [JsonProperty("type")]
        public string Type;

        [JsonProperty("activityId")]
        public int? ActivityId;

        [JsonProperty("_links")]
        public PathwayReferencesChildSelf_BB_Links Links;
    }

}
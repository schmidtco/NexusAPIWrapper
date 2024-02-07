using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PathwayReferencesSelf_ActivityIdentifier
    {
        [JsonProperty("identifier")]
        public string Identifier;

        [JsonProperty("type")]
        public string Type;

        [JsonProperty("activityId")]
        public int? ActivityId;

        [JsonProperty("_links")]
        public PathwayReferencesSelf_Links Links;
    }

}
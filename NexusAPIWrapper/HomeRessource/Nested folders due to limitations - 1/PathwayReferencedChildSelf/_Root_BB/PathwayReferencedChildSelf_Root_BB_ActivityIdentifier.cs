using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PathwayReferencedChildSelf_Root_BB_ActivityIdentifier
    {
        [JsonProperty("identifier")]
        public string Identifier;

        [JsonProperty("type")]
        public string Type;

        [JsonProperty("activityId")]
        public int? ActivityId;

        [JsonProperty("_links")]
        public PathwayReferencedChildSelf_Root_BB_Links Links;
    }

}
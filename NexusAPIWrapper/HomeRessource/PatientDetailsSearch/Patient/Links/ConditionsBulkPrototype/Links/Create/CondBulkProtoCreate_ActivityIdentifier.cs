using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class CondBulkProtoCreate_ActivityIdentifier
    {
        [JsonProperty("identifier")]
        public string Identifier;

        [JsonProperty("type")]
        public string Type;

        [JsonProperty("activityId")]
        public int? ActivityId;

        [JsonProperty("_links")]
        public CondBulkProtoCreate_Links Links;
    }

}
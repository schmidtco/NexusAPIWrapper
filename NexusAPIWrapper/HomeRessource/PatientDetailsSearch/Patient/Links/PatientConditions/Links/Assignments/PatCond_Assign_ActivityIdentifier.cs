using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatCond_Assign_ActivityIdentifier
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("activityId")]
        public int? ActivityId;

        [JsonProperty("identifier")]
        public string Identifier;
    }

}
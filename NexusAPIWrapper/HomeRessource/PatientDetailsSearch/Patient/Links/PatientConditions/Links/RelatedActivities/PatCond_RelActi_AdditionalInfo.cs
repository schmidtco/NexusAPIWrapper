using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatCond_RelActi_AdditionalInfo
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("color")]
        public string Color;

        [JsonProperty("key")]
        public string Key;

        [JsonProperty("value")]
        public string Value;
    }

}
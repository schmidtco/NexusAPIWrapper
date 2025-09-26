using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatCond_Observ_ExpectedLevelDescription
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("id")]
        public object Id;

        [JsonProperty("version")]
        public object Version;

        [JsonProperty("value")]
        public object Value;

        [JsonProperty("updateDate")]
        public object UpdateDate;

        [JsonProperty("_links")]
        public PatCond_Observ_Links Links;
    }

}
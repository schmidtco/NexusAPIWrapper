using Newtonsoft.Json; 
using System; 
namespace NexusAPIWrapper{ 

    public class PatCond_Observ_CurrentAssessment
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("id")]
        public object Id;

        [JsonProperty("version")]
        public object Version;

        [JsonProperty("value")]
        public string Value;

        [JsonProperty("updateDate")]
        public DateTime? UpdateDate;

        [JsonProperty("_links")]
        public PatCond_Observ_Links Links;
    }

}
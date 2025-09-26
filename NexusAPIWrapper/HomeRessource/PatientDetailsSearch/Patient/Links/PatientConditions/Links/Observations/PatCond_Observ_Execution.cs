using Newtonsoft.Json; 
using System.Collections.Generic; 
using System; 
namespace NexusAPIWrapper{ 

    public class PatCond_Observ_Execution
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("id")]
        public object Id;

        [JsonProperty("version")]
        public object Version;

        [JsonProperty("value")]
        public PatCond_Observ_Value Value;

        [JsonProperty("updateDate")]
        public DateTime? UpdateDate;

        [JsonProperty("possibleValues")]
        public List<PatCond_Observ_PossibleValue> PossibleValues;

        [JsonProperty("_links")]
        public PatCond_Observ_Links Links;
    }

}
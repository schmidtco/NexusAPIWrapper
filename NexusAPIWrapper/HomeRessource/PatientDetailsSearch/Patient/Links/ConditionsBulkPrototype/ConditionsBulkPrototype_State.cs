using Newtonsoft.Json; 
using System.Collections.Generic; 
using System; 
namespace NexusAPIWrapper{ 

    public class ConditionsBulkPrototype_State
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("id")]
        public object Id;

        [JsonProperty("version")]
        public object Version;

        [JsonProperty("value")]
        public ConditionsBulkPrototype_Value Value;

        [JsonProperty("updateDate")]
        public DateTime? UpdateDate;

        [JsonProperty("possibleValues")]
        public List<ConditionsBulkPrototype_PossibleValue> PossibleValues;

        [JsonProperty("_links")]
        public ConditionsBulkPrototype_Links Links;
    }

}
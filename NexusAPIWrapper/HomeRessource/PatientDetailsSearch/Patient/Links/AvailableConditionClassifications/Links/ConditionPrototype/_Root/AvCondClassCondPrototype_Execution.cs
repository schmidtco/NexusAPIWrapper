using Newtonsoft.Json; 
using System.Collections.Generic; 
using System; 
namespace NexusAPIWrapper{ 

    public class AvCondClassCondPrototype_Execution
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
        public DateTime? UpdateDate;

        [JsonProperty("possibleValues")]
        public List<AvCondClassCondPrototype_PossibleValue> PossibleValues;

        [JsonProperty("_links")]
        public AvCondClassCondPrototype_Links Links;
    }

}
using Newtonsoft.Json; 
using System.Collections.Generic; 
using System; 
namespace NexusAPIWrapper{ 

    public class ObservationsPrototype_Execution
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("id")]
        public object Id;

        [JsonProperty("version")]
        public object Version;

        [JsonProperty("value")]
        public ObservationsPrototype_Value Value;

        [JsonProperty("updateDate")]
        public DateTime? UpdateDate;

        [JsonProperty("possibleValues")]
        public List<ObservationsPrototype_PossibleValue> PossibleValues;

        [JsonProperty("_links")]
        public ObservationsPrototype_Links Links;
    }

}
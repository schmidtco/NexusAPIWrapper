using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class PatientDetailsSearch_AddressIndicatorValueSchedule
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("valuePeriods")]
        public List<object> ValuePeriods;

        [JsonProperty("_links")]
        public PatientDetailsSearch_Links Links;

        [JsonProperty("currentValue")]
        public string CurrentValue;
    }

}
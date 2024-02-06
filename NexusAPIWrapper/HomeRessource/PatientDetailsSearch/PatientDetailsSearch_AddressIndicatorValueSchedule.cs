using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class PatientDetails_AddressIndicatorValueSchedule
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("id")]
        public int Id;

        [JsonProperty("version")]
        public int Version;

        [JsonProperty("valuePeriods")]
        public List<object> ValuePeriods;

        [JsonProperty("_links")]
        public PatientDetails_Links Links;

        [JsonProperty("currentValue")]
        public object CurrentValue;
    }

}
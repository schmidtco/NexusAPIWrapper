using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class PatientDetailsSearch_PatientStateValueSchedule
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("valuePeriods")]
        public List<PatientDetailsSearch_ValuePeriod> ValuePeriods;

        [JsonProperty("currentValue")]
        public PatientDetailsSearch_CurrentValue CurrentValue;

        [JsonProperty("_links")]
        public PatientDetailsSearch_Links Links;
    }

}
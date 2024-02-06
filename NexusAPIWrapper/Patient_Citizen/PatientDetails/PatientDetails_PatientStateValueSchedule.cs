using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class PatientDetails_PatientStateValueSchedule
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("id")]
        public int Id;

        [JsonProperty("version")]
        public int Version;

        [JsonProperty("valuePeriods")]
        public List<PatientDetails_ValuePeriod> ValuePeriods;

        [JsonProperty("currentValue")]
        public PatientDetails_CurrentValue CurrentValue;

        [JsonProperty("_links")]
        public PatientDetails_Links Links;
    }

}
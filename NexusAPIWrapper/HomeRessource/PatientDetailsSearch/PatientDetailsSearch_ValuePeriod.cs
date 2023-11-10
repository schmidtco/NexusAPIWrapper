using Newtonsoft.Json; 
using System.Collections.Generic; 
using System; 
namespace NexusAPIWrapper{ 

    public class PatientDetailsSearch_ValuePeriod
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("valueScheduleId")]
        public int? ValueScheduleId;

        [JsonProperty("startDate")]
        public DateTime? StartDate;

        [JsonProperty("endDate")]
        public object EndDate;

        [JsonProperty("value")]
        public PatientDetailsSearch_Value Value;

        [JsonProperty("valueEditable")]
        public bool? ValueEditable;

        [JsonProperty("startDateEditable")]
        public bool? StartDateEditable;

        [JsonProperty("endDateEditable")]
        public bool? EndDateEditable;

        [JsonProperty("deletable")]
        public bool? Deletable;

        [JsonProperty("possibleValues")]
        public List<PatientDetailsSearch_PossibleValue> PossibleValues;

        [JsonProperty("_links")]
        public PatientDetailsSearch_Links Links;
    }

}
using Newtonsoft.Json; 
using System.Collections.Generic; 
using System; 
namespace NexusAPIWrapper{ 

    public class PatientDetails_ValuePeriod
    {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("valueScheduleId")]
        public int ValueScheduleId;

        [JsonProperty("startDate")]
        public DateTime StartDate;

        [JsonProperty("endDate")]
        public object EndDate;

        [JsonProperty("value")]
        public PatientDetails_Value Value;

        [JsonProperty("valueEditable")]
        public bool ValueEditable;

        [JsonProperty("startDateEditable")]
        public bool StartDateEditable;

        [JsonProperty("endDateEditable")]
        public bool EndDateEditable;

        [JsonProperty("deletable")]
        public bool Deletable;

        [JsonProperty("possibleValues")]
        public List<PatientDetails_PossibleValue> PossibleValues;

        [JsonProperty("_links")]
        public PatientDetails_Links Links;
    }

}
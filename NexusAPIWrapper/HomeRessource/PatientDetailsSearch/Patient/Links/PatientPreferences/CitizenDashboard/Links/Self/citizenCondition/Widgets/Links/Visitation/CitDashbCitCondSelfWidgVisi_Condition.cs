using Newtonsoft.Json; 
using System; 
namespace NexusAPIWrapper{ 

    public class CitDashbCitCondSelfWidgVisi_Condition
    {
        [JsonProperty("law")]
        public string Law;

        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("patientId")]
        public int? PatientId;

        [JsonProperty("state")]
        public string State;

        [JsonProperty("classification")]
        public CitDashbCitCondSelfWidgVisi_Classification Classification;

        [JsonProperty("description")]
        public string Description;

        [JsonProperty("updatedAt")]
        public DateTime? UpdatedAt;

        [JsonProperty("followUpDate")]
        public object FollowUpDate;

        [JsonProperty("currentScore")]
        public int? CurrentScore;

        [JsonProperty("expectedScore")]
        public int? ExpectedScore;

        [JsonProperty("_links")]
        public CitDashbCitCondSelfWidgVisi_Links Links;
    }

}


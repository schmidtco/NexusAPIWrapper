using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientDetails_PatientReimbursementInformation
    {
        [JsonProperty("residentialMunicipalityValueSchedule")]
        public object ResidentialMunicipalityValueSchedule;

        [JsonProperty("payingMunicipalityValueSchedule")]
        public object PayingMunicipalityValueSchedule;

        [JsonProperty("actingMunicipalityValueSchedule")]
        public object ActingMunicipalityValueSchedule;

        [JsonProperty("notes")]
        public object Notes;

        [JsonProperty("_links")]
        public PatientDetails_Links Links;

        [JsonProperty("id")]
        public object Id;

        [JsonProperty("version")]
        public int Version;

        [JsonProperty("href")]
        public string Href;
    }

}
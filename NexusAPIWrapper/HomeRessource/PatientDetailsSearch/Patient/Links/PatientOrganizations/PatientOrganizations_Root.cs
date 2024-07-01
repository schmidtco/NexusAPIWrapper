using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientOrganizations_Root
    {
        [JsonProperty("patientActivityType")]
        public string PatientActivityType;

        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("identifier")]
        public object Identifier;

        [JsonProperty("organization")]
        public PatientOrganizations_Organization Organization;

        [JsonProperty("patientId")]
        public int? PatientId;

        [JsonProperty("effectiveStartDate")]
        public string EffectiveStartDate;

        [JsonProperty("effectiveEndDate")]
        public object EffectiveEndDate;

        [JsonProperty("primaryOrganization")]
        public bool? PrimaryOrganization;

        [JsonProperty("effectiveAtPresent")]
        public bool? EffectiveAtPresent;

        [JsonProperty("dailyPhoneNumber")]
        public object DailyPhoneNumber;

        [JsonProperty("_links")]
        public PatientOrganizations_Links Links;
    }

}
using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class ACTIVITYLIST_Pages_Content_PatientOrganization
    {
        [JsonProperty("patientActivityType")]
        public string PatientActivityType;

        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("identifier")]
        public object Identifier;

        [JsonProperty("organization")]
        public ACTIVITYLIST_Pages_Content_Organization Organization;

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
        public ACTIVITYLIST_Pages_Content_Links Links;

        [JsonProperty("href")]
        public string Href;
    }

}
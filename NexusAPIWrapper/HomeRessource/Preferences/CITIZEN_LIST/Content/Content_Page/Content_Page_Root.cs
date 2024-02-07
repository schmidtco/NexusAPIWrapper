using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class Content_Page_Root
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("firstName")]
        public string FirstName;

        [JsonProperty("lastName")]
        public string LastName;

        [JsonProperty("middleName")]
        public string MiddleName;

        [JsonProperty("fullName")]
        public string FullName;

        [JsonProperty("fullReversedName")]
        public string FullReversedName;

        [JsonProperty("homePhoneNumber")]
        public string HomePhoneNumber;

        [JsonProperty("mobilePhoneNumber")]
        public string MobilePhoneNumber;

        [JsonProperty("workPhoneNumber")]
        public object WorkPhoneNumber;

        [JsonProperty("patientIdentifier")]
        public Content_Page_PatientIdentifier PatientIdentifier;

        [JsonProperty("currentAddress")]
        public Content_Page_CurrentAddress CurrentAddress;

        [JsonProperty("currentAddressIndicator")]
        public string CurrentAddressIndicator;

        [JsonProperty("age")]
        public int? Age;

        [JsonProperty("monthsAfterBirthday")]
        public int? MonthsAfterBirthday;

        [JsonProperty("gender")]
        public string Gender;

        [JsonProperty("patientStatus")]
        public string PatientStatus;

        [JsonProperty("patientState")]
        public Content_Page_PatientState PatientState;

        [JsonProperty("telephonesRestricted")]
        public bool? TelephonesRestricted;

        [JsonProperty("imageId")]
        public string ImageId;

        [JsonProperty("_links")]
        public Content_Page_Links Links;
    }

}
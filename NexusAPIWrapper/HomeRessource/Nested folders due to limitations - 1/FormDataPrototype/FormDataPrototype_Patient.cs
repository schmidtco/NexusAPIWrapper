using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class FormDataPrototype_Patient
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
        public object MiddleName;

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
        public FormDataPrototype_PatientIdentifier PatientIdentifier;

        [JsonProperty("currentAddress")]
        public FormDataPrototype_CurrentAddress CurrentAddress;

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
        public FormDataPrototype_PatientState PatientState;

        [JsonProperty("telephonesRestricted")]
        public bool? TelephonesRestricted;

        [JsonProperty("imageId")]
        public object ImageId;

        [JsonProperty("_links")]
        public FormDataPrototype_Links Links;
    }

}
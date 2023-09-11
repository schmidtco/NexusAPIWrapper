using Newtonsoft.Json; 
using System.Collections.Generic; 
using System; 
namespace NexusAPIWrapper{ 

    public class PatientDetails_Root
    {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("version")]
        public int Version;

        [JsonProperty("patientIdentifier")]
        public PatientDetails_PatientIdentifier PatientIdentifier;

        [JsonProperty("firstName")]
        public string FirstName;

        [JsonProperty("middleName")]
        public object MiddleName;

        [JsonProperty("lastName")]
        public string LastName;

        [JsonProperty("fullName")]
        public string FullName;

        [JsonProperty("knownAs")]
        public object KnownAs;

        [JsonProperty("title")]
        public object Title;

        [JsonProperty("notes")]
        public object Notes;

        [JsonProperty("birthDate")]
        public DateTime BirthDate;

        [JsonProperty("deceasedDate")]
        public object DeceasedDate;

        [JsonProperty("gender")]
        public string Gender;

        [JsonProperty("hasBirthday")]
        public bool HasBirthday;

        [JsonProperty("maritalStatus")]
        public string MaritalStatus;

        [JsonProperty("maritalStatusUpdateDate")]
        public DateTime MaritalStatusUpdateDate;

        [JsonProperty("homeTelephone")]
        public string HomeTelephone;

        [JsonProperty("mobileTelephone")]
        public string MobileTelephone;

        [JsonProperty("workTelephone")]
        public object WorkTelephone;

        [JsonProperty("primaryEmailAddress")]
        public object PrimaryEmailAddress;

        [JsonProperty("secondaryEmailAddress")]
        public object SecondaryEmailAddress;

        [JsonProperty("telephonesRestricted")]
        public bool TelephonesRestricted;

        [JsonProperty("primaryAddress")]
        public PatientDetails_PrimaryAddress PrimaryAddress;

        [JsonProperty("secondaryAddress")]
        public PatientDetails_SecondaryAddress SecondaryAddress;

        [JsonProperty("supplementaryAddress")]
        public PatientDetails_SupplementaryAddress SupplementaryAddress;

        [JsonProperty("temporaryAddress")]
        public PatientDetails_TemporaryAddress TemporaryAddress;

        [JsonProperty("planningAddress")]
        public PatientDetails_PlanningAddress PlanningAddress;

        [JsonProperty("patientStatus")]
        public string PatientStatus;

        [JsonProperty("patientCategory")]
        public PatientDetails_PatientCategory PatientCategory;

        [JsonProperty("patientState")]
        public PatientDetails_PatientState PatientState;

        [JsonProperty("patientStateStartDate")]
        public DateTime PatientStateStartDate;

        [JsonProperty("patientStateEndDate")]
        public object PatientStateEndDate;

        [JsonProperty("patientStateValueSchedule")]
        public PatientDetails_PatientStateValueSchedule PatientStateValueSchedule;

        [JsonProperty("currentAddressIndicator")]
        public string CurrentAddressIndicator;

        [JsonProperty("addressIndicatorValueSchedule")]
        public PatientDetails_AddressIndicatorValueSchedule AddressIndicatorValueSchedule;

        [JsonProperty("freeChoiceValueSchedule")]
        public object FreeChoiceValueSchedule;

        [JsonProperty("patientReimbursementInformation")]
        public PatientDetails_PatientReimbursementInformation PatientReimbursementInformation;

        [JsonProperty("netsMandates")]
        public PatientDetails_NetsMandates NetsMandates;

        [JsonProperty("taxRate")]
        public PatientDetails_TaxRate TaxRate;

        [JsonProperty("patientIncome")]
        public object PatientIncome;

        [JsonProperty("smsReminder")]
        public PatientDetails_SmsReminder SmsReminder;

        [JsonProperty("imageId")]
        public object ImageId;

        [JsonProperty("nexusVideoConfiguration")]
        public PatientDetails_NexusVideoConfiguration NexusVideoConfiguration;

        [JsonProperty("payoutRecipientMediaList")]
        public PatientDetails_PayoutRecipientMediaList PayoutRecipientMediaList;

        [JsonProperty("organDonation")]
        public object OrganDonation;

        [JsonProperty("preferredLanguage")]
        public PatientDetails_PreferredLanguage PreferredLanguage;

        [JsonProperty("treatmentWill")]
        public object TreatmentWill;

        [JsonProperty("livingWill")]
        public object LivingWill;

        [JsonProperty("healthInsuranceGroup")]
        public object HealthInsuranceGroup;

        [JsonProperty("_links")]
        public PatientDetails_Links Links;
    }

}
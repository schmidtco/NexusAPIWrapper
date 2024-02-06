using Newtonsoft.Json; 
using System.Collections.Generic; 
using System; 
namespace NexusAPIWrapper{ 

    public class PatientDetailsSearch_Patient
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("patientIdentifier")]
        public PatientDetailsSearch_PatientIdentifier PatientIdentifier;

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
        public DateTime? BirthDate;

        [JsonProperty("deceasedDate")]
        public object DeceasedDate;

        [JsonProperty("gender")]
        public string Gender;

        [JsonProperty("hasBirthday")]
        public bool? HasBirthday;

        [JsonProperty("maritalStatus")]
        public string MaritalStatus;

        [JsonProperty("maritalStatusUpdateDate")]
        public DateTime? MaritalStatusUpdateDate;

        [JsonProperty("homeTelephone")]
        public object HomeTelephone;

        [JsonProperty("mobileTelephone")]
        public string MobileTelephone;

        [JsonProperty("workTelephone")]
        public object WorkTelephone;

        [JsonProperty("primaryEmailAddress")]
        public string PrimaryEmailAddress;

        [JsonProperty("secondaryEmailAddress")]
        public object SecondaryEmailAddress;

        [JsonProperty("telephonesRestricted")]
        public bool? TelephonesRestricted;

        [JsonProperty("primaryAddress")]
        public PatientDetailsSearch_PrimaryAddress PrimaryAddress;

        [JsonProperty("secondaryAddress")]
        public PatientDetailsSearch_SecondaryAddress SecondaryAddress;

        [JsonProperty("supplementaryAddress")]
        public PatientDetailsSearch_SupplementaryAddress SupplementaryAddress;

        [JsonProperty("temporaryAddress")]
        public PatientDetailsSearch_TemporaryAddress TemporaryAddress;

        [JsonProperty("planningAddress")]
        public PatientDetailsSearch_PlanningAddress PlanningAddress;

        [JsonProperty("patientStatus")]
        public string PatientStatus;

        [JsonProperty("patientCategory")]
        public PatientDetailsSearch_PatientCategory PatientCategory;

        [JsonProperty("patientState")]
        public PatientDetailsSearch_PatientState PatientState;

        [JsonProperty("patientStateStartDate")]
        public DateTime? PatientStateStartDate;

        [JsonProperty("patientStateEndDate")]
        public object PatientStateEndDate;

        [JsonProperty("patientStateValueSchedule")]
        public PatientDetailsSearch_PatientStateValueSchedule PatientStateValueSchedule;

        [JsonProperty("currentAddressIndicator")]
        public string CurrentAddressIndicator;

        [JsonProperty("addressIndicatorValueSchedule")]
        public PatientDetailsSearch_AddressIndicatorValueSchedule AddressIndicatorValueSchedule;

        [JsonProperty("freeChoiceValueSchedule")]
        public object FreeChoiceValueSchedule;

        [JsonProperty("patientReimbursementInformation")]
        public PatientDetailsSearch_PatientReimbursementInformation PatientReimbursementInformation;

        [JsonProperty("netsMandates")]
        public PatientDetailsSearch_NetsMandates NetsMandates;

        [JsonProperty("taxRate")]
        public PatientDetailsSearch_TaxRate TaxRate;

        [JsonProperty("patientIncome")]
        public object PatientIncome;

        [JsonProperty("smsReminder")]
        public PatientDetailsSearch_SmsReminder SmsReminder;

        [JsonProperty("imageId")]
        public object ImageId;

        [JsonProperty("nexusVideoConfiguration")]
        public PatientDetailsSearch_NexusVideoConfiguration NexusVideoConfiguration;

        [JsonProperty("payoutRecipientMediaList")]
        public PatientDetailsSearch_PayoutRecipientMediaList PayoutRecipientMediaList;

        [JsonProperty("organDonation")]
        public object OrganDonation;

        [JsonProperty("preferredLanguage")]
        public PatientDetailsSearch_PreferredLanguage PreferredLanguage;

        [JsonProperty("treatmentWill")]
        public object TreatmentWill;

        [JsonProperty("livingWill")]
        public object LivingWill;

        [JsonProperty("healthInsuranceGroup")]
        public object HealthInsuranceGroup;

        [JsonProperty("_links")]
        public PatientDetailsSearch_Links Links;
    }

}
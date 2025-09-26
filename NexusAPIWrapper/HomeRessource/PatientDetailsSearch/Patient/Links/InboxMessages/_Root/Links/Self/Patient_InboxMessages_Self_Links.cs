using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class Patient_InboxMessages_Self_Links
    {
        [JsonProperty("availableCountries")]
        public Patient_InboxMessages_Self_AvailableCountries AvailableCountries;

        [JsonProperty("searchPostalDistrict")]
        public Patient_InboxMessages_Self_SearchPostalDistrict SearchPostalDistrict;

        [JsonProperty("self")]
        public Patient_InboxMessages_Self_Self Self;

        [JsonProperty("patientOverview")]
        public Patient_InboxMessages_Self_PatientOverview PatientOverview;

        [JsonProperty("patientProperties")]
        public Patient_InboxMessages_Self_PatientProperties PatientProperties;

        [JsonProperty("citizenOverviewForms")]
        public Patient_InboxMessages_Self_CitizenOverviewForms CitizenOverviewForms;

        [JsonProperty("measurementData")]
        public Patient_InboxMessages_Self_MeasurementData MeasurementData;

        [JsonProperty("measurementInstructions")]
        public Patient_InboxMessages_Self_MeasurementInstructions MeasurementInstructions;

        [JsonProperty("patientConditions")]
        public Patient_InboxMessages_Self_PatientConditions PatientConditions;

        [JsonProperty("patientOrganizations")]
        public Patient_InboxMessages_Self_PatientOrganizations PatientOrganizations;

        [JsonProperty("activityLinksPrototypes")]
        public Patient_InboxMessages_Self_ActivityLinksPrototypes ActivityLinksPrototypes;
    }

}
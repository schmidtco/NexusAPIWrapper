using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientEnrolled_Links
    {
        [JsonProperty("self")]
        public PatientEnrolled_Self Self;

        [JsonProperty("availableCountries")]
        public PatientEnrolled_AvailableCountries AvailableCountries;

        [JsonProperty("searchPostalDistrict")]
        public PatientEnrolled_SearchPostalDistrict SearchPostalDistrict;

        [JsonProperty("patientOverview")]
        public PatientEnrolled_PatientOverview PatientOverview;

        [JsonProperty("citizenOverviewForms")]
        public PatientEnrolled_CitizenOverviewForms CitizenOverviewForms;

        [JsonProperty("measurementData")]
        public PatientEnrolled_MeasurementData MeasurementData;

        [JsonProperty("measurementInstructions")]
        public PatientEnrolled_MeasurementInstructions MeasurementInstructions;

        [JsonProperty("patientConditions")]
        public PatientEnrolled_PatientConditions PatientConditions;

        [JsonProperty("patientOrganizations")]
        public PatientEnrolled_PatientOrganizations PatientOrganizations;

        [JsonProperty("activityLinksPrototypes")]
        public PatientEnrolled_ActivityLinksPrototypes ActivityLinksPrototypes;

        [JsonProperty("programPathway")]
        public PatientEnrolled_ProgramPathway ProgramPathway;

        [JsonProperty("selfReference")]
        public PatientEnrolled_SelfReference SelfReference;

        [JsonProperty("update")]
        public PatientEnrolled_Update Update;

        [JsonProperty("professionals")]
        public PatientEnrolled_Professionals Professionals;
    }

}
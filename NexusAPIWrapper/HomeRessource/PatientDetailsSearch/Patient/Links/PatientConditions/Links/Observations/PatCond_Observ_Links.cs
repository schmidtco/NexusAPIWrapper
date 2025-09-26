using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatCond_Observ_Links
    {
        [JsonProperty("availableCountries")]
        public PatCond_Observ_AvailableCountries AvailableCountries;

        [JsonProperty("searchPostalDistrict")]
        public PatCond_Observ_SearchPostalDistrict SearchPostalDistrict;

        [JsonProperty("self")]
        public PatCond_Observ_Self Self;

        [JsonProperty("patientOverview")]
        public PatCond_Observ_PatientOverview PatientOverview;

        [JsonProperty("patientProperties")]
        public PatCond_Observ_PatientProperties PatientProperties;

        [JsonProperty("citizenOverviewForms")]
        public PatCond_Observ_CitizenOverviewForms CitizenOverviewForms;

        [JsonProperty("measurementData")]
        public PatCond_Observ_MeasurementData MeasurementData;

        [JsonProperty("measurementInstructions")]
        public PatCond_Observ_MeasurementInstructions MeasurementInstructions;

        [JsonProperty("patientConditions")]
        public PatCond_Observ_PatientConditions PatientConditions;

        [JsonProperty("patientOrganizations")]
        public PatCond_Observ_PatientOrganizations PatientOrganizations;

        [JsonProperty("activityLinksPrototypes")]
        public PatCond_Observ_ActivityLinksPrototypes ActivityLinksPrototypes;

        [JsonProperty("relatedActivities")]
        public PatCond_Observ_RelatedActivities RelatedActivities;

        [JsonProperty("relatedActivitiesWithHistory")]
        public PatCond_Observ_RelatedActivitiesWithHistory RelatedActivitiesWithHistory;
    }

}
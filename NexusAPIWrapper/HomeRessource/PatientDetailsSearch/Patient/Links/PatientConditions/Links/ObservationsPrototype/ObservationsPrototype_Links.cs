using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class ObservationsPrototype_Links
    {
        [JsonProperty("availableCountries")]
        public ObservationsPrototype_AvailableCountries AvailableCountries;

        [JsonProperty("searchPostalDistrict")]
        public ObservationsPrototype_SearchPostalDistrict SearchPostalDistrict;

        [JsonProperty("self")]
        public ObservationsPrototype_Self Self;

        [JsonProperty("patientOverview")]
        public ObservationsPrototype_PatientOverview PatientOverview;

        [JsonProperty("patientProperties")]
        public ObservationsPrototype_PatientProperties PatientProperties;

        [JsonProperty("citizenOverviewForms")]
        public ObservationsPrototype_CitizenOverviewForms CitizenOverviewForms;

        [JsonProperty("measurementData")]
        public ObservationsPrototype_MeasurementData MeasurementData;

        [JsonProperty("measurementInstructions")]
        public ObservationsPrototype_MeasurementInstructions MeasurementInstructions;

        [JsonProperty("patientConditions")]
        public ObservationsPrototype_PatientConditions PatientConditions;

        [JsonProperty("patientOrganizations")]
        public ObservationsPrototype_PatientOrganizations PatientOrganizations;

        [JsonProperty("activityLinksPrototypes")]
        public ObservationsPrototype_ActivityLinksPrototypes ActivityLinksPrototypes;

        [JsonProperty("create")]
        public ObservationsPrototype_Create Create;

        [JsonProperty("relatedActivities")]
        public ObservationsPrototype_RelatedActivities RelatedActivities;

        [JsonProperty("relatedActivitiesWithHistory")]
        public ObservationsPrototype_RelatedActivitiesWithHistory RelatedActivitiesWithHistory;
    }

}
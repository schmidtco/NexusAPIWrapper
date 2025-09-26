using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class ConditionsBulkPrototype_Links
    {
        [JsonProperty("availableCountries")]
        public ConditionsBulkPrototype_AvailableCountries AvailableCountries;

        [JsonProperty("searchPostalDistrict")]
        public ConditionsBulkPrototype_SearchPostalDistrict SearchPostalDistrict;

        [JsonProperty("self")]
        public ConditionsBulkPrototype_Self Self;

        [JsonProperty("patientOverview")]
        public ConditionsBulkPrototype_PatientOverview PatientOverview;

        [JsonProperty("patientProperties")]
        public ConditionsBulkPrototype_PatientProperties PatientProperties;

        [JsonProperty("citizenOverviewForms")]
        public ConditionsBulkPrototype_CitizenOverviewForms CitizenOverviewForms;

        [JsonProperty("measurementData")]
        public ConditionsBulkPrototype_MeasurementData MeasurementData;

        [JsonProperty("measurementInstructions")]
        public ConditionsBulkPrototype_MeasurementInstructions MeasurementInstructions;

        [JsonProperty("patientConditions")]
        public ConditionsBulkPrototype_PatientConditions PatientConditions;

        [JsonProperty("patientOrganizations")]
        public ConditionsBulkPrototype_PatientOrganizations PatientOrganizations;

        [JsonProperty("activityLinksPrototypes")]
        public ConditionsBulkPrototype_ActivityLinksPrototypes ActivityLinksPrototypes;

        [JsonProperty("create")]
        public ConditionsBulkPrototype_Create Create;
    }

}
using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class AvCondClassCondPrototype_Links
    {
        [JsonProperty("availableCountries")]
        public AvCondClassCondPrototype_AvailableCountries AvailableCountries;

        [JsonProperty("searchPostalDistrict")]
        public AvCondClassCondPrototype_SearchPostalDistrict SearchPostalDistrict;

        [JsonProperty("self")]
        public AvCondClassCondPrototype_Self Self;

        [JsonProperty("patientOverview")]
        public AvCondClassCondPrototype_PatientOverview PatientOverview;

        [JsonProperty("patientProperties")]
        public AvCondClassCondPrototype_PatientProperties PatientProperties;

        [JsonProperty("citizenOverviewForms")]
        public AvCondClassCondPrototype_CitizenOverviewForms CitizenOverviewForms;

        [JsonProperty("measurementData")]
        public AvCondClassCondPrototype_MeasurementData MeasurementData;

        [JsonProperty("measurementInstructions")]
        public AvCondClassCondPrototype_MeasurementInstructions MeasurementInstructions;

        [JsonProperty("patientConditions")]
        public AvCondClassCondPrototype_PatientConditions PatientConditions;

        [JsonProperty("patientOrganizations")]
        public AvCondClassCondPrototype_PatientOrganizations PatientOrganizations;

        [JsonProperty("activityLinksPrototypes")]
        public AvCondClassCondPrototype_ActivityLinksPrototypes ActivityLinksPrototypes;

        [JsonProperty("create")]
        public AvCondClassCondPrototype_Create Create;
    }

}
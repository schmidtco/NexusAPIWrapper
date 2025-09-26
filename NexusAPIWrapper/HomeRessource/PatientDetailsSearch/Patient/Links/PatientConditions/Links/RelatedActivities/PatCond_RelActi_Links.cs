using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatCond_RelActi_Links
    {
        [JsonProperty("referencedObject")]
        public PatCond_RelActi_ReferencedObject ReferencedObject;

        [JsonProperty("deleteActivityLink")]
        public PatCond_RelActi_DeleteActivityLink DeleteActivityLink;

        [JsonProperty("availableCountries")]
        public PatCond_RelActi_AvailableCountries AvailableCountries;

        [JsonProperty("searchPostalDistrict")]
        public PatCond_RelActi_SearchPostalDistrict SearchPostalDistrict;

        [JsonProperty("self")]
        public PatCond_RelActi_Self Self;

        [JsonProperty("patientOverview")]
        public PatCond_RelActi_PatientOverview PatientOverview;

        [JsonProperty("patientProperties")]
        public PatCond_RelActi_PatientProperties PatientProperties;

        [JsonProperty("citizenOverviewForms")]
        public PatCond_RelActi_CitizenOverviewForms CitizenOverviewForms;

        [JsonProperty("measurementData")]
        public PatCond_RelActi_MeasurementData MeasurementData;

        [JsonProperty("measurementInstructions")]
        public PatCond_RelActi_MeasurementInstructions MeasurementInstructions;

        [JsonProperty("patientConditions")]
        public PatCond_RelActi_PatientConditions PatientConditions;

        [JsonProperty("patientOrganizations")]
        public PatCond_RelActi_PatientOrganizations PatientOrganizations;

        [JsonProperty("activityLinksPrototypes")]
        public PatCond_RelActi_ActivityLinksPrototypes ActivityLinksPrototypes;
    }

}
using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatCond_RelActiWHist_Links
    {
        [JsonProperty("referencedObject")]
        public PatCond_RelActiWHist_ReferencedObject ReferencedObject;

        [JsonProperty("self")]
        public PatCond_RelActiWHist_Self Self;

        [JsonProperty("stsOrganization")]
        public PatCond_RelActiWHist_StsOrganization StsOrganization;

        [JsonProperty("configuration")]
        public PatCond_RelActiWHist_Configuration Configuration;

        [JsonProperty("defaultSenderOrganization")]
        public PatCond_RelActiWHist_DefaultSenderOrganization DefaultSenderOrganization;

        [JsonProperty("defaultSenderOrganizationForReply")]
        public PatCond_RelActiWHist_DefaultSenderOrganizationForReply DefaultSenderOrganizationForReply;

        [JsonProperty("tabletAppConfiguration")]
        public PatCond_RelActiWHist_TabletAppConfiguration TabletAppConfiguration;

        [JsonProperty("deleteActivityLink")]
        public PatCond_RelActiWHist_DeleteActivityLink DeleteActivityLink;

        [JsonProperty("availableCountries")]
        public PatCond_RelActiWHist_AvailableCountries AvailableCountries;

        [JsonProperty("searchPostalDistrict")]
        public PatCond_RelActiWHist_SearchPostalDistrict SearchPostalDistrict;

        [JsonProperty("patientOverview")]
        public PatCond_RelActiWHist_PatientOverview PatientOverview;

        [JsonProperty("patientProperties")]
        public PatCond_RelActiWHist_PatientProperties PatientProperties;

        [JsonProperty("citizenOverviewForms")]
        public PatCond_RelActiWHist_CitizenOverviewForms CitizenOverviewForms;

        [JsonProperty("measurementData")]
        public PatCond_RelActiWHist_MeasurementData MeasurementData;

        [JsonProperty("measurementInstructions")]
        public PatCond_RelActiWHist_MeasurementInstructions MeasurementInstructions;

        [JsonProperty("patientConditions")]
        public PatCond_RelActiWHist_PatientConditions PatientConditions;

        [JsonProperty("patientOrganizations")]
        public PatCond_RelActiWHist_PatientOrganizations PatientOrganizations;

        [JsonProperty("activityLinksPrototypes")]
        public PatCond_RelActiWHist_PatCond_RelActiWHist_ ActivityLinksPrototypes;
    }

}
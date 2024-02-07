using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class ACTIVITYLIST_Pages_Content_Links
    {
        [JsonProperty("availableCountries")]
        public ACTIVITYLIST_Pages_Content_AvailableCountries AvailableCountries;

        [JsonProperty("searchPostalDistrict")]
        public ACTIVITYLIST_Pages_Content_SearchPostalDistrict SearchPostalDistrict;

        [JsonProperty("self")]
        public ACTIVITYLIST_Pages_Content_Self Self;

        [JsonProperty("patientOverview")]
        public ACTIVITYLIST_Pages_Content_PatientOverview PatientOverview;

        [JsonProperty("citizenOverviewForms")]
        public ACTIVITYLIST_Pages_Content_CitizenOverviewForms CitizenOverviewForms;

        [JsonProperty("measurementData")]
        public ACTIVITYLIST_Pages_Content_MeasurementData MeasurementData;

        [JsonProperty("measurementInstructions")]
        public ACTIVITYLIST_Pages_Content_MeasurementInstructions MeasurementInstructions;

        [JsonProperty("patientConditions")]
        public ACTIVITYLIST_Pages_Content_PatientConditions PatientConditions;

        [JsonProperty("patientOrganizations")]
        public ACTIVITYLIST_Pages_Content_PatientOrganization PatientOrganizations;

        [JsonProperty("activityLinksPrototypes")]
        public ACTIVITYLIST_Pages_Content_ActivityLinksPrototypes ActivityLinksPrototypes;

        [JsonProperty("stsOrganization")]
        public ACTIVITYLIST_Pages_Content_StsOrganization StsOrganization;

        [JsonProperty("removeFromPatient")]
        public ACTIVITYLIST_Pages_Content_RemoveFromPatient RemoveFromPatient;

        [JsonProperty("update")]
        public ACTIVITYLIST_Pages_Content_Update Update;

        [JsonProperty("referencedObject")]
        public ACTIVITYLIST_Pages_Content_ReferencedObject ReferencedObject;

        [JsonProperty("archiveMedcom")]
        public ACTIVITYLIST_Pages_Content_ArchiveMedcom ArchiveMedcom;
    }

}
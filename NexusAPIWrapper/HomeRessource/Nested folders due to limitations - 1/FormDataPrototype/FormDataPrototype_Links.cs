using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class FormDataPrototype_Links
    {
        [JsonProperty("availableActions")]
        public FormDataPrototype_AvailableActions AvailableActions;

        [JsonProperty("modifiableTags")]
        public FormDataPrototype_ModifiableTags ModifiableTags;

        [JsonProperty("availableTags")]
        public FormDataPrototype_AvailableTags AvailableTags;

        [JsonProperty("preview")]
        public FormDataPrototype_Preview Preview;

        [JsonProperty("availablePathwayAssociation")]
        public FormDataPrototype_AvailablePathwayAssociation AvailablePathwayAssociation;

        [JsonProperty("availableRootProgramPathways")]
        public FormDataPrototype_AvailableRootProgramPathways AvailableRootProgramPathways;

        [JsonProperty("availablePathwayPlacements")]
        public FormDataPrototype_AvailablePathwayPlacements AvailablePathwayPlacements;

        [JsonProperty("availableCountries")]
        public FormDataPrototype_AvailableCountries AvailableCountries;

        [JsonProperty("searchPostalDistrict")]
        public FormDataPrototype_SearchPostalDistrict SearchPostalDistrict;

        [JsonProperty("self")]
        public FormDataPrototype_Self Self;

        [JsonProperty("patientOverview")]
        public FormDataPrototype_PatientOverview PatientOverview;

        [JsonProperty("citizenOverviewForms")]
        public FormDataPrototype_CitizenOverviewForms CitizenOverviewForms;

        [JsonProperty("measurementData")]
        public FormDataPrototype_MeasurementData MeasurementData;

        [JsonProperty("measurementInstructions")]
        public FormDataPrototype_MeasurementInstructions MeasurementInstructions;

        [JsonProperty("patientConditions")]
        public FormDataPrototype_PatientConditions PatientConditions;

        [JsonProperty("patientOrganizations")]
        public FormDataPrototype_PatientOrganizations PatientOrganizations;

        [JsonProperty("activityLinksPrototypes")]
        public FormDataPrototype_ActivityLinksPrototypes ActivityLinksPrototypes;
    }

}
using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class PatientGrantByIdSelf_Links
    {
        [JsonProperty("prepareEdit")]
        public PatientGrantByIdSelf_PrepareEdit PrepareEdit;

        [JsonProperty("orderGrantEditList")]
        public PatientGrantByIdSelf_OrderGrantEditList OrderGrantEditList;

        [JsonProperty("availableCountries")]
        public PatientGrantByIdSelf_AvailableCountries AvailableCountries;

        [JsonProperty("searchPostalDistrict")]
        public PatientGrantByIdSelf_SearchPostalDistrict SearchPostalDistrict;

        [JsonProperty("self")]
        public PatientGrantByIdSelf_Self Self;

        [JsonProperty("patientOverview")]
        public PatientGrantByIdSelf_PatientOverview PatientOverview;

        [JsonProperty("citizenOverviewForms")]
        public PatientGrantByIdSelf_CitizenOverviewForms CitizenOverviewForms;

        [JsonProperty("measurementData")]
        public PatientGrantByIdSelf_MeasurementData MeasurementData;

        [JsonProperty("measurementInstructions")]
        public PatientGrantByIdSelf_MeasurementInstructions MeasurementInstructions;

        [JsonProperty("patientConditions")]
        public PatientGrantByIdSelf_PatientConditions PatientConditions;

        [JsonProperty("patientOrganizations")]
        public PatientGrantByIdSelf_PatientOrganizations PatientOrganizations;

        [JsonProperty("activityLinksPrototypes")]
        public PatientGrantByIdSelf_ActivityLinksPrototypes ActivityLinksPrototypes;

        [JsonProperty("audit")]
        public PatientGrantByIdSelf_Audit Audit;

        [JsonProperty("availableBaskets")]
        public PatientGrantByIdSelf_AvailableBaskets AvailableBaskets;

        [JsonProperty("relatedActivities")]
        public PatientGrantByIdSelf_RelatedActivities RelatedActivities;

        [JsonProperty("relatedActivitiesWithHistory")]
        public PatientGrantByIdSelf_RelatedActivitiesWithHistory RelatedActivitiesWithHistory;

        [JsonProperty("assignments")]
        public PatientGrantByIdSelf_Assignments Assignments;

        [JsonProperty("activeAssignments")]
        public PatientGrantByIdSelf_ActiveAssignments ActiveAssignments;

        [JsonProperty("availableAssignmentTypes")]
        public PatientGrantByIdSelf_AvailableAssignmentTypes AvailableAssignmentTypes;

        [JsonProperty("currentOrderedGrant")]
        public PatientGrantByIdSelf_CurrentOrderedGrant CurrentOrderedGrant;

        [JsonProperty("availableHousingResources")]
        public PatientGrantByIdSelf_AvailableHousingResources AvailableHousingResources;

        [JsonProperty("availableParagraphs")]
        public PatientGrantByIdSelf_AvailableParagraphs AvailableParagraphs;

        [JsonProperty("availableSuppliers")]
        public PatientGrantByIdSelf_AvailableSuppliers AvailableSuppliers;

        [JsonProperty("refreshOnChange")]
        public PatientGrantByIdSelf_RefreshOnChange RefreshOnChange;

        [JsonProperty("availableKLMappings")]
        public PatientGrantByIdSelf_AvailableKLMappings AvailableKLMappings;

        [JsonProperty("availableKleNumbers")]
        public PatientGrantByIdSelf_AvailableKleNumbers AvailableKleNumbers;

        [JsonProperty("availableActionFacets")]
        public PatientGrantByIdSelf_AvailableActionFacets AvailableActionFacets;
    }

}
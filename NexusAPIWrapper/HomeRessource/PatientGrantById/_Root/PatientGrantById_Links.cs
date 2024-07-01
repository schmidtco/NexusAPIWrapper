using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class PatientGrantById_Links
    {
        [JsonProperty("prepareEdit")]
        public PatientGrantById_PrepareEdit PrepareEdit;

        [JsonProperty("orderGrantEditList")]
        public PatientGrantById_OrderGrantEditList OrderGrantEditList;

        [JsonProperty("availableCountries")]
        public PatientGrantById_AvailableCountries AvailableCountries;

        [JsonProperty("searchPostalDistrict")]
        public PatientGrantById_SearchPostalDistrict SearchPostalDistrict;

        [JsonProperty("self")]
        public PatientGrantById_Self Self;

        [JsonProperty("patientOverview")]
        public PatientGrantById_PatientOverview PatientOverview;

        [JsonProperty("citizenOverviewForms")]
        public PatientGrantById_CitizenOverviewForms CitizenOverviewForms;

        [JsonProperty("measurementData")]
        public PatientGrantById_MeasurementData MeasurementData;

        [JsonProperty("measurementInstructions")]
        public PatientGrantById_MeasurementInstructions MeasurementInstructions;

        [JsonProperty("patientConditions")]
        public PatientGrantById_PatientConditions PatientConditions;

        [JsonProperty("patientOrganizations")]
        public PatientGrantById_PatientOrganizations PatientOrganizations;

        [JsonProperty("activityLinksPrototypes")]
        public PatientGrantById_ActivityLinksPrototypes ActivityLinksPrototypes;

        [JsonProperty("audit")]
        public PatientGrantById_Audit Audit;

        [JsonProperty("availableBaskets")]
        public PatientGrantById_AvailableBaskets AvailableBaskets;

        [JsonProperty("relatedActivities")]
        public PatientGrantById_RelatedActivities RelatedActivities;

        [JsonProperty("relatedActivitiesWithHistory")]
        public PatientGrantByIdSelf_RelatedActivitiesWithHistory RelatedActivitiesWithHistory;

        [JsonProperty("assignments")]
        public PatientGrantById_Assignments Assignments;

        [JsonProperty("activeAssignments")]
        public PatientGrantById_ActiveAssignments ActiveAssignments;

        [JsonProperty("availableAssignmentTypes")]
        public PatientGrantById_AvailableAssignmentTypes AvailableAssignmentTypes;

        [JsonProperty("currentOrderedGrant")]
        public PatientGrantById_CurrentOrderedGrant CurrentOrderedGrant;

        [JsonProperty("availableHousingResources")]
        public PatientGrantById_AvailableHousingResources AvailableHousingResources;

        [JsonProperty("availableParagraphs")]
        public PatientGrantById_AvailableParagraphs AvailableParagraphs;

        [JsonProperty("availableSuppliers")]
        public PatientGrantById_AvailableSuppliers AvailableSuppliers;

        [JsonProperty("refreshOnChange")]
        public PatientGrantById_RefreshOnChange RefreshOnChange;

        [JsonProperty("availableKLMappings")]
        public PatientGrantById_AvailableKLMappings AvailableKLMappings;

        [JsonProperty("availableKleNumbers")]
        public PatientGrantById_AvailableKleNumbers AvailableKleNumbers;

        [JsonProperty("availableActionFacets")]
        public PatientGrantById_AvailableActionFacets AvailableActionFacets;
    }

}
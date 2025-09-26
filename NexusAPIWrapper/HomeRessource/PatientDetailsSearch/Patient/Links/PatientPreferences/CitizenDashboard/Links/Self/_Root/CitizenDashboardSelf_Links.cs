using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class CitizenDashboardSelf_Links
    {
        [JsonProperty("self")]
        public CitizenDashboardSelf_Self Self;

        [JsonProperty("stsOrganization")]
        public CitizenDashboardSelf_StsOrganization StsOrganization;

        [JsonProperty("copyPrototype")]
        public CitizenDashboardSelf_CopyPrototype CopyPrototype;

        [JsonProperty("editablePreference")]
        public CitizenDashboardSelf_EditablePreference EditablePreference;

        [JsonProperty("availablePrototypes")]
        public CitizenDashboardSelf_AvailablePrototypes AvailablePrototypes;

        [JsonProperty("availableTags")]
        public CitizenDashboardSelf_AvailableTags AvailableTags;

        [JsonProperty("documentPrototype")]
        public CitizenDashboardSelf_DocumentPrototype DocumentPrototype;

        [JsonProperty("suggestedGrantsForConditions")]
        public CitizenDashboardSelf_SuggestedGrantsForConditions SuggestedGrantsForConditions;

        [JsonProperty("pathwayReferences")]
        public CitizenDashboardSelf_PathwayReferences PathwayReferences;

        [JsonProperty("additionalInformation")]
        public CitizenDashboardSelf_AdditionalInformation AdditionalInformation;

        [JsonProperty("orderGrantAdditionalInformation")]
        public CitizenDashboardSelf_OrderGrantAdditionalInformation OrderGrantAdditionalInformation;

        [JsonProperty("basketGrantAdditionalInformation")]
        public CitizenDashboardSelf_BasketGrantAdditionalInformation BasketGrantAdditionalInformation;

        [JsonProperty("bulkGet")]
        public CitizenDashboardSelf_BulkGet BulkGet;

        [JsonProperty("availableAssignmentTypes")]
        public CitizenDashboardSelf_AvailableAssignmentTypes AvailableAssignmentTypes;

        [JsonProperty("content")]
        public CitizenDashboardSelf_Content Content;

        [JsonProperty("copyableForms")]
        public CitizenDashboardSelf_CopyableForms CopyableForms;

        [JsonProperty("availableFormDefinitions")]
        public CitizenDashboardSelf_AvailableFormDefinitions AvailableFormDefinitions;


    }

}
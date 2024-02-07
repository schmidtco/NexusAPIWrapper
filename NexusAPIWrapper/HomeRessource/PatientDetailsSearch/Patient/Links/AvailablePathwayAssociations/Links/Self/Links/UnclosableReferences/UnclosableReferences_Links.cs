using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class UnclosableReferences_Links
    {
        [JsonProperty("availableNestedProgramPathways")]
        public UnclosableReferences_AvailableNestedProgramPathways AvailableNestedProgramPathways;

        [JsonProperty("close")]
        public UnclosableReferences_Close Close;

        [JsonProperty("unclosableReferences")]
        public UnclosableReferences_UnclosableReferences UnclosableReferences;

        [JsonProperty("withdrawPatient")]
        public UnclosableReferences_WithdrawPatient WithdrawPatient;

        [JsonProperty("availableProfessionals")]
        public UnclosableReferences_AvailableProfessionals AvailableProfessionals;

        [JsonProperty("availableOrganisations")]
        public UnclosableReferences_AvailableOrganisations AvailableOrganisations;

        [JsonProperty("availableFormDefinitions")]
        public UnclosableReferences_AvailableFormDefinitions AvailableFormDefinitions;

        [JsonProperty("availableLetterTemplates")]
        public UnclosableReferences_AvailableLetterTemplates AvailableLetterTemplates;

        [JsonProperty("dynamicTemplate")]
        public UnclosableReferences_DynamicTemplate DynamicTemplate;

        [JsonProperty("availablePathwayDistributions")]
        public UnclosableReferences_AvailablePathwayDistributions AvailablePathwayDistributions;

        [JsonProperty("citizenAccountPrototype")]
        public UnclosableReferences_CitizenAccountPrototype CitizenAccountPrototype;

        [JsonProperty("patientNetworkContactPrototype")]
        public UnclosableReferences_PatientNetworkContactPrototype PatientNetworkContactPrototype;

        [JsonProperty("availableAssociationGroupDefinitions")]
        public UnclosableReferences_AvailableAssociationGroupDefinitions AvailableAssociationGroupDefinitions;

        [JsonProperty("activePrograms")]
        public UnclosableReferences_ActivePrograms ActivePrograms;

        [JsonProperty("availableEvents")]
        public UnclosableReferences_AvailableEvents AvailableEvents;

        [JsonProperty("availableEventTypes")]
        public UnclosableReferences_AvailableEventTypes AvailableEventTypes;

        [JsonProperty("addEventsToPathway")]
        public UnclosableReferences_AddEventsToPathway AddEventsToPathway;

        [JsonProperty("eventPrototype")]
        public UnclosableReferences_EventPrototype EventPrototype;

        [JsonProperty("documentPrototype")]
        public UnclosableReferences_DocumentPrototype DocumentPrototype;

        [JsonProperty("eopFfbProblemPrototypes")]
        public UnclosableReferences_EopFfbProblemPrototypes EopFfbProblemPrototypes;

        [JsonProperty("eopFfbVisitationPrototype")]
        public UnclosableReferences_EopFfbVisitationPrototype EopFfbVisitationPrototype;

        [JsonProperty("eopFfbFollowUpPrototype")]
        public UnclosableReferences_EopFfbFollowUpPrototype EopFfbFollowUpPrototype;

        [JsonProperty("eopFfbActivities")]
        public UnclosableReferences_EopFfbActivities EopFfbActivities;

        [JsonProperty("self")]
        public UnclosableReferences_Self Self;

        [JsonProperty("patientPathway")]
        public UnclosableReferences_PatientPathway PatientPathway;

        [JsonProperty("copyPrototype")]
        public UnclosableReferences_CopyPrototype CopyPrototype;
    }

}
using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class AvailablePathwayAssociations_Self_Links
    {
        [JsonProperty("availableNestedProgramPathways")]
        public AvailablePathwayAssociations_Self_AvailableNestedProgramPathways AvailableNestedProgramPathways;

        [JsonProperty("close")]
        public AvailablePathwayAssociations_Self_Close Close;

        [JsonProperty("unclosableReferences")]
        public AvailablePathwayAssociations_Self_UnclosableReferences UnclosableReferences;

        [JsonProperty("withdrawPatient")]
        public AvailablePathwayAssociations_Self_WithdrawPatient WithdrawPatient;

        [JsonProperty("availableProfessionals")]
        public AvailablePathwayAssociations_Self_AvailableProfessionals AvailableProfessionals;

        [JsonProperty("availableOrganisations")]
        public AvailablePathwayAssociations_Self_AvailableOrganisations AvailableOrganisations;

        [JsonProperty("availableFormDefinitions")]
        public AvailablePathwayAssociations_Self_AvailableFormDefinitions AvailableFormDefinitions;

        [JsonProperty("availableLetterTemplates")]
        public AvailablePathwayAssociations_Self_AvailableLetterTemplates AvailableLetterTemplates;

        [JsonProperty("dynamicTemplate")]
        public AvailablePathwayAssociations_Self_DynamicTemplate DynamicTemplate;

        [JsonProperty("availablePathwayDistributions")]
        public AvailablePathwayAssociations_Self_AvailablePathwayDistributions AvailablePathwayDistributions;

        [JsonProperty("citizenAccountPrototype")]
        public AvailablePathwayAssociations_Self_CitizenAccountPrototype CitizenAccountPrototype;

        [JsonProperty("patientNetworkContactPrototype")]
        public AvailablePathwayAssociations_Self_PatientNetworkContactPrototype PatientNetworkContactPrototype;

        [JsonProperty("availableAssociationGroupDefinitions")]
        public AvailablePathwayAssociations_Self_AvailableAssociationGroupDefinitions AvailableAssociationGroupDefinitions;

        [JsonProperty("activePrograms")]
        public AvailablePathwayAssociations_Self_ActivePrograms ActivePrograms;

        [JsonProperty("availableEvents")]
        public AvailablePathwayAssociations_Self_AvailableEvents AvailableEvents;

        [JsonProperty("availableEventTypes")]
        public AvailablePathwayAssociations_Self_AvailableEventTypes AvailableEventTypes;

        [JsonProperty("addEventsToPathway")]
        public AvailablePathwayAssociations_Self_AddEventsToPathway AddEventsToPathway;

        [JsonProperty("eventPrototype")]
        public AvailablePathwayAssociations_Self_EventPrototype EventPrototype;

        [JsonProperty("documentPrototype")]
        public AvailablePathwayAssociations_Self_DocumentPrototype DocumentPrototype;

        [JsonProperty("eopFfbProblemPrototypes")]
        public AvailablePathwayAssociations_Self_EopFfbProblemPrototypes EopFfbProblemPrototypes;

        [JsonProperty("eopFfbVisitationPrototype")]
        public AvailablePathwayAssociations_Self_EopFfbVisitationPrototype EopFfbVisitationPrototype;

        [JsonProperty("eopFfbFollowUpPrototype")]
        public AvailablePathwayAssociations_Self_EopFfbFollowUpPrototype EopFfbFollowUpPrototype;

        [JsonProperty("eopFfbActivities")]
        public AvailablePathwayAssociations_Self_EopFfbActivities EopFfbActivities;

        [JsonProperty("self")]
        public AvailablePathwayAssociations_Self_Self Self;

        [JsonProperty("patientPathway")]
        public AvailablePathwayAssociations_Self_PatientPathway PatientPathway;
    }

}
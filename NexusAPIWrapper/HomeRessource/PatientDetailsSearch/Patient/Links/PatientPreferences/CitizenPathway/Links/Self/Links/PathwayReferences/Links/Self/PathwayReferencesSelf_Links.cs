using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PathwayReferencesSelf_Links
    {
        [JsonProperty("availableNestedProgramPathways")]
        public PathwayReferencesSelf_AvailableNestedProgramPathways AvailableNestedProgramPathways;

        [JsonProperty("close")]
        public PathwayReferencesSelf_Close Close;

        [JsonProperty("unclosableReferences")]
        public PathwayReferencesSelf_UnclosableReferences UnclosableReferences;

        [JsonProperty("withdrawPatient")]
        public PathwayReferencesSelf_WithdrawPatient WithdrawPatient;

        [JsonProperty("availableProfessionals")]
        public PathwayReferencesSelf_AvailableProfessionals AvailableProfessionals;

        [JsonProperty("availableOrganisations")]
        public PathwayReferencesSelf_AvailableOrganisations AvailableOrganisations;

        [JsonProperty("availableFormDefinitions")]
        public PathwayReferencesSelf_AvailableFormDefinitions AvailableFormDefinitions;

        [JsonProperty("availableLetterTemplates")]
        public PathwayReferencesSelf_AvailableLetterTemplates AvailableLetterTemplates;

        [JsonProperty("dynamicTemplate")]
        public PathwayReferencesSelf_DynamicTemplate DynamicTemplate;

        [JsonProperty("availablePathwayDistributions")]
        public PathwayReferencesSelf_AvailablePathwayDistributions AvailablePathwayDistributions;

        [JsonProperty("citizenAccountPrototype")]
        public PathwayReferencesSelf_CitizenAccountPrototype CitizenAccountPrototype;

        [JsonProperty("patientNetworkContactPrototype")]
        public PathwayReferencesSelf_PatientNetworkContactPrototype PatientNetworkContactPrototype;

        [JsonProperty("availableAssociationGroupDefinitions")]
        public PathwayReferencesSelf_AvailableAssociationGroupDefinitions AvailableAssociationGroupDefinitions;

        [JsonProperty("activePrograms")]
        public PathwayReferencesSelf_ActivePrograms ActivePrograms;

        [JsonProperty("availableEvents")]
        public PathwayReferencesSelf_AvailableEvents AvailableEvents;

        [JsonProperty("availableEventTypes")]
        public PathwayReferencesSelf_AvailableEventTypes AvailableEventTypes;

        [JsonProperty("addEventsToPathway")]
        public PathwayReferencesSelf_AddEventsToPathway AddEventsToPathway;

        [JsonProperty("eventPrototype")]
        public PathwayReferencesSelf_EventPrototype EventPrototype;

        [JsonProperty("documentPrototype")]
        public PathwayReferencesSelf_DocumentPrototype DocumentPrototype;

        [JsonProperty("eopFfbProblemPrototypes")]
        public PathwayReferencesSelf_EopFfbProblemPrototypes EopFfbProblemPrototypes;

        [JsonProperty("eopFfbVisitationPrototype")]
        public PathwayReferencesSelf_EopFfbVisitationPrototype EopFfbVisitationPrototype;

        [JsonProperty("eopFfbFollowUpPrototype")]
        public PathwayReferencesSelf_EopFfbFollowUpPrototype EopFfbFollowUpPrototype;

        [JsonProperty("eopFfbActivities")]
        public PathwayReferencesSelf_EopFfbActivities EopFfbActivities;

        [JsonProperty("self")]
        public PathwayReferencesSelf_Self Self;

        [JsonProperty("patientPathway")]
        public PathwayReferencesSelf_PatientPathway PatientPathway;

        [JsonProperty("copyPrototype")]
        public PathwayReferencesSelf_CopyPrototype CopyPrototype;
    }

}
using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PathwayReferencesChildSelf_BB_Links
    {
        [JsonProperty("availableNestedProgramPathways")]
        public PathwayReferencesChildSelf_BB_AvailableNestedProgramPathways AvailableNestedProgramPathways;

        [JsonProperty("close")]
        public PathwayReferencesChildSelf_BB_Close Close;

        [JsonProperty("unclosableReferences")]
        public PathwayReferencesChildSelf_BB_UnclosableReferences UnclosableReferences;

        [JsonProperty("withdrawPatient")]
        public PathwayReferencesChildSelf_BB_WithdrawPatient WithdrawPatient;

        [JsonProperty("availableProfessionals")]
        public PathwayReferencesChildSelf_BB_AvailableProfessionals AvailableProfessionals;

        [JsonProperty("availableOrganisations")]
        public PathwayReferencesChildSelf_BB_AvailableOrganisations AvailableOrganisations;

        [JsonProperty("availableFormDefinitions")]
        public AvailableFormDefinitions AvailableFormDefinitions;

        [JsonProperty("availableLetterTemplates")]
        public PathwayReferencesChildSelf_BB_AvailableLetterTemplates AvailableLetterTemplates;

        [JsonProperty("dynamicTemplate")]
        public PathwayReferencesChildSelf_BB_DynamicTemplate DynamicTemplate;

        [JsonProperty("availablePathwayDistributions")]
        public PathwayReferencesChildSelf_BB_AvailablePathwayDistributions AvailablePathwayDistributions;

        [JsonProperty("citizenAccountPrototype")]
        public PathwayReferencesChildSelf_BB_CitizenAccountPrototype CitizenAccountPrototype;

        [JsonProperty("patientNetworkContactPrototype")]
        public PathwayReferencesChildSelf_BB_PatientNetworkContactPrototype PatientNetworkContactPrototype;

        [JsonProperty("availableAssociationGroupDefinitions")]
        public PathwayReferencesChildSelf_BB_AvailableAssociationGroupDefinitions AvailableAssociationGroupDefinitions;

        [JsonProperty("activePrograms")]
        public PathwayReferencesChildSelf_BB_ActivePrograms ActivePrograms;

        [JsonProperty("availableEvents")]
        public PathwayReferencesChildSelf_BB_AvailableEvents AvailableEvents;

        [JsonProperty("availableEventTypes")]
        public PathwayReferencesChildSelf_BB_AvailableEventTypes AvailableEventTypes;

        [JsonProperty("addEventsToPathway")]
        public PathwayReferencesChildSelf_BB_AddEventsToPathway AddEventsToPathway;

        [JsonProperty("eventPrototype")]
        public PathwayReferencesChildSelf_BB_EventPrototype EventPrototype;

        [JsonProperty("documentPrototype")]
        public PathwayReferencesChildSelf_BB_DocumentPrototype DocumentPrototype;

        [JsonProperty("eopFfbProblemPrototypes")]
        public PathwayReferencesChildSelf_BB_EopFfbProblemPrototypes EopFfbProblemPrototypes;

        [JsonProperty("eopFfbVisitationPrototype")]
        public PathwayReferencesChildSelf_BB_EopFfbVisitationPrototype EopFfbVisitationPrototype;

        [JsonProperty("eopFfbFollowUpPrototype")]
        public PathwayReferencesChildSelf_BB_EopFfbFollowUpPrototype EopFfbFollowUpPrototype;

        [JsonProperty("eopFfbActivities")]
        public PathwayReferencesChildSelf_BB_EopFfbActivities EopFfbActivities;

        [JsonProperty("eopFfbVisitationsForCreatingProblem")]
        public PathwayReferencesChildSelf_BB_EopFfbVisitationsForCreatingProblem EopFfbVisitationsForCreatingProblem;

        [JsonProperty("eopFfbProblemPrototypeClassifications")]
        public PathwayReferencesChildSelf_BB_EopFfbProblemPrototypeClassifications EopFfbProblemPrototypeClassifications;

        [JsonProperty("self")]
        public PathwayReferencesChildSelf_BB_Self Self;

        [JsonProperty("patientPathway")]
        public PathwayReferencesChildSelf_BB_PatientPathway PatientPathway;

        [JsonProperty("copyPrototype")]
        public PathwayReferencesChildSelf_BB_CopyPrototype CopyPrototype;
    }

}
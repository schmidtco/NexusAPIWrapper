using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class ReferencedObject_Base_Links
    {
        [JsonProperty("medcomRecipient")]
        public ReferencedObject_Base_MedcomRecipient MedcomRecipient;

        [JsonProperty("self")]
        public ReferencedObject_Base_Self Self;

        [JsonProperty("patientOverview")]
        public ReferencedObject_Base_PatientOverview PatientOverview;

        [JsonProperty("citizenOverviewForms")]
        public ReferencedObject_Base_CitizenOverviewForms CitizenOverviewForms;

        [JsonProperty("measurementData")]
        public ReferencedObject_Base_MeasurementData MeasurementData;

        [JsonProperty("measurementInstructions")]
        public ReferencedObject_Base_MeasurementInstructions MeasurementInstructions;

        [JsonProperty("patientConditions")]
        public ReferencedObject_Base_PatientConditions PatientConditions;

        [JsonProperty("patientOrganizations")]
        public ReferencedObject_Base_PatientOrganizations PatientOrganizations;

        [JsonProperty("activityLinksPrototypes")]
        public ReferencedObject_Base_ActivityLinksPrototypes ActivityLinksPrototypes;

        [JsonProperty("availablePathwayAssociation")]
        public ReferencedObject_Base_AvailablePathwayAssociation AvailablePathwayAssociation;

        [JsonProperty("availableRootProgramPathways")]
        public ReferencedObject_Base_AvailableRootProgramPathways AvailableRootProgramPathways;

        [JsonProperty("availablePathwayPlacements")]
        public ReferencedObject_Base_AvailablePathwayPlacements AvailablePathwayPlacements;

        [JsonProperty("audit")]
        public ReferencedObject_Base_Audit Audit;

        [JsonProperty("relatedCommunication")]
        public ReferencedObject_Base_RelatedCommunication RelatedCommunication;

        [JsonProperty("activeAssignments")]
        public ReferencedObject_Base_ActiveAssignments ActiveAssignments;

        [JsonProperty("assignments")]
        public ReferencedObject_Base_Assignments Assignments;

        [JsonProperty("availableAssignmentTypes")]
        public ReferencedObject_Base_AvailableAssignmentTypes AvailableAssignmentTypes;

        [JsonProperty("autoAssignmentsPrototype")]
        public ReferencedObject_Base_AutoAssignmentsPrototype AutoAssignmentsPrototype;

        [JsonProperty("update")]
        public ReferencedObject_Base_Update Update;

        [JsonProperty("transformedBody")]
        public ReferencedObject_Base_TransformedBody TransformedBody;

        [JsonProperty("print")]
        public ReferencedObject_Base_Print Print;

        [JsonProperty("reply")]
        public ReferencedObject_Base_Reply Reply;

        [JsonProperty("replyTo")]
        public ReferencedObject_Base_ReplyTo ReplyTo;

        [JsonProperty("archive")]
        public ReferencedObject_Base_Archive Archive;

        [JsonProperty("availableCountries")]
        public ReferencedObject_Base_AvailableCountries AvailableCountries;

        [JsonProperty("searchPostalDistrict")]
        public ReferencedObject_Base_SearchPostalDistrict SearchPostalDistrict;
    }

}
using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class CondBulkProtoCreate_Links
    {
        [JsonProperty("self")]
        public CondBulkProtoCreate_Self Self;

        [JsonProperty("configuration")]
        public CondBulkProtoCreate_Configuration Configuration;

        [JsonProperty("defaultSenderOrganization")]
        public CondBulkProtoCreate_DefaultSenderOrganization DefaultSenderOrganization;

        [JsonProperty("defaultSenderOrganizationForReply")]
        public CondBulkProtoCreate_DefaultSenderOrganizationForReply DefaultSenderOrganizationForReply;

        [JsonProperty("tabletAppConfiguration")]
        public CondBulkProtoCreate_TabletAppConfiguration TabletAppConfiguration;

        [JsonProperty("observationsPrototype")]
        public CondBulkProtoCreate_ObservationsPrototype ObservationsPrototype;

        [JsonProperty("audit")]
        public CondBulkProtoCreate_Audit Audit;

        [JsonProperty("relatedActivities")]
        public CondBulkProtoCreate_RelatedActivities RelatedActivities;

        [JsonProperty("update")]
        public CondBulkProtoCreate_Update Update;

        [JsonProperty("relatedActivitiesWithHistory")]
        public CondBulkProtoCreate_RelatedActivitiesWithHistory RelatedActivitiesWithHistory;

        [JsonProperty("currentObservations")]
        public CondBulkProtoCreate_CurrentObservations CurrentObservations;

        [JsonProperty("print")]
        public CondBulkProtoCreate_Print Print;

        [JsonProperty("patientConditionTimeline")]
        public CondBulkProtoCreate_PatientConditionTimeline PatientConditionTimeline;

        [JsonProperty("assignments")]
        public CondBulkProtoCreate_Assignments Assignments;

        [JsonProperty("activeAssignments")]
        public CondBulkProtoCreate_ActiveAssignments ActiveAssignments;

        [JsonProperty("availableAssignmentTypes")]
        public CondBulkProtoCreate_AvailableAssignmentTypes AvailableAssignmentTypes;

        [JsonProperty("allObservations")]
        public CondBulkProtoCreate_AllObservations AllObservations;

        [JsonProperty("activityDashboard")]
        public CondBulkProtoCreate_ActivityDashboard ActivityDashboard;

        [JsonProperty("autoAssignmentsPrototype")]
        public CondBulkProtoCreate_AutoAssignmentsPrototype AutoAssignmentsPrototype;

        [JsonProperty("possibleCauseConditions")]
        public CondBulkProtoCreate_PossibleCauseConditions PossibleCauseConditions;

        [JsonProperty("correspondingConditions")]
        public CondBulkProtoCreate_CorrespondingConditions CorrespondingConditions;
    }

}
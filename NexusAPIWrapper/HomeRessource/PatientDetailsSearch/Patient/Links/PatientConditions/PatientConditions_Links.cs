using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientConditions_Links
    {
        [JsonProperty("observationsPrototype")]
        public PatientConditions_ObservationsPrototype ObservationsPrototype;

        [JsonProperty("audit")]
        public PatientConditions_Audit Audit;

        [JsonProperty("relatedActivities")]
        public PatientConditions_RelatedActivities RelatedActivities;

        [JsonProperty("update")]
        public PatientConditions_Update Update;

        [JsonProperty("relatedActivitiesWithHistory")]
        public PatientConditions_RelatedActivitiesWithHistory RelatedActivitiesWithHistory;

        [JsonProperty("currentObservations")]
        public PatientConditions_CurrentObservations CurrentObservations;

        [JsonProperty("print")]
        public PatientConditions_Print Print;

        [JsonProperty("patientConditionTimeline")]
        public PatientConditions_PatientConditionTimeline PatientConditionTimeline;

        [JsonProperty("assignments")]
        public PatientConditions_Assignments Assignments;

        [JsonProperty("activeAssignments")]
        public PatientConditions_ActiveAssignments ActiveAssignments;

        [JsonProperty("availableAssignmentTypes")]
        public PatientConditions_AvailableAssignmentTypes AvailableAssignmentTypes;

        [JsonProperty("allObservations")]
        public PatientConditions_AllObservations AllObservations;

        [JsonProperty("activityDashboard")]
        public PatientConditions_ActivityDashboard ActivityDashboard;

        [JsonProperty("autoAssignmentsPrototype")]
        public PatientConditions_AutoAssignmentsPrototype AutoAssignmentsPrototype;

        [JsonProperty("possibleCauseConditions")]
        public PatientConditions_PossibleCauseConditions PossibleCauseConditions;

        [JsonProperty("correspondingConditions")]
        public PatientConditions_CorrespondingConditions CorrespondingConditions;

        [JsonProperty("self")]
        public PatientConditions_Self Self;

        [JsonProperty("stsOrganization")]
        public PatientConditions_StsOrganization StsOrganization;

        [JsonProperty("configuration")]
        public PatientConditions_Configuration Configuration;

        [JsonProperty("defaultSenderOrganization")]
        public PatientConditions_DefaultSenderOrganization DefaultSenderOrganization;

        [JsonProperty("defaultSenderOrganizationForReply")]
        public PatientConditions_DefaultSenderOrganizationForReply DefaultSenderOrganizationForReply;

        [JsonProperty("tabletAppConfiguration")]
        public PatientConditions_TabletAppConfiguration TabletAppConfiguration;
    }

}
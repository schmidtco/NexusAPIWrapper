using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class CitizenDashboardCitizenConditionSelf_Links
    {
        [JsonProperty("patientConditions")]
        public CitizenDashboardCitizenConditionSelf_PatientConditions PatientConditions;

        [JsonProperty("linkablePatientConditions")]
        public CitizenDashboardCitizenConditionSelf_LinkablePatientConditions LinkablePatientConditions;

        [JsonProperty("patientConditionsTimeline")]
        public CitizenDashboardCitizenConditionSelf_PatientConditionsTimeline PatientConditionsTimeline;

        [JsonProperty("closestFs3Assignments")]
        public CitizenDashboardCitizenConditionSelf_ClosestFs3Assignments ClosestFs3Assignments;

        [JsonProperty("suggestedConditions")]
        public CitizenDashboardCitizenConditionSelf_SuggestedConditions SuggestedConditions;

        [JsonProperty("conditions")]
        public CitizenDashboardCitizenConditionSelf_Conditions Conditions;

        [JsonProperty("visitation")]
        public CitizenDashboardCitizenConditionSelf_Visitation Visitation;

        [JsonProperty("visitationPrototype")]
        public CitizenDashboardCitizenConditionSelf_VisitationPrototype VisitationPrototype;

        [JsonProperty("self")]
        public CitizenDashboardCitizenConditionSelf_Self Self;

        [JsonProperty("copyPrototype")]
        public CitizenDashboardCitizenConditionSelf_CopyPrototype CopyPrototype;

        [JsonProperty("editablePreference")]
        public CitizenDashboardCitizenConditionSelf_EditablePreference EditablePreference; 
    }

}
using Newtonsoft.Json; 
using System; 
namespace NexusAPIWrapper{ 

    public class PatCond_Assign_Root
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("patientId")]
        public int? PatientId;

        [JsonProperty("activityIdentifier")]
        public PatCond_Assign_ActivityIdentifier ActivityIdentifier;

        [JsonProperty("type")]
        public PatCond_Assign_Type Type;

        [JsonProperty("title")]
        public string Title;

        [JsonProperty("dueDate")]
        public string DueDate;

        [JsonProperty("dueTime")]
        public string DueTime;

        [JsonProperty("description")]
        public object Description;

        [JsonProperty("relatedActivityIdentifier")]
        public string RelatedActivityIdentifier;

        [JsonProperty("relatedActivityType")]
        public string RelatedActivityType;

        [JsonProperty("relatedActivityTitle")]
        public object RelatedActivityTitle;

        [JsonProperty("workflowState")]
        public PatCond_Assign_WorkflowState WorkflowState;

        [JsonProperty("organizationAssignee")]
        public PatCond_Assign_OrganizationAssignee OrganizationAssignee;

        [JsonProperty("professionalAssignee")]
        public object ProfessionalAssignee;

        [JsonProperty("lastStateChangeDate")]
        public DateTime? LastStateChangeDate;

        [JsonProperty("startDate")]
        public string StartDate;

        [JsonProperty("startTime")]
        public string StartTime;

        [JsonProperty("paused")]
        public bool? Paused;

        [JsonProperty("creationDate")]
        public DateTime? CreationDate;

        [JsonProperty("_links")]
        public PatCond_Assign_Links Links;
    }

}
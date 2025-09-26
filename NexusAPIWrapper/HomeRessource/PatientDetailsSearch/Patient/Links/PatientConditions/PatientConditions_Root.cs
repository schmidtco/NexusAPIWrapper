using Newtonsoft.Json; 
using System; 
namespace NexusAPIWrapper{ 

    public class PatientConditions_Root
    {
        [JsonProperty("patientActivityType")]
        public string PatientActivityType;

        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("activityIdentifier")]
        public PatientConditions_ActivityIdentifier ActivityIdentifier;

        [JsonProperty("createDate")]
        public DateTime? CreateDate;

        [JsonProperty("updateDate")]
        public DateTime? UpdateDate;

        [JsonProperty("lastActivationDate")]
        public DateTime? LastActivationDate;

        [JsonProperty("lastEvaluationDate")]
        public DateTime? LastEvaluationDate;

        [JsonProperty("conditionClassificationItem")]
        public PatientConditions_ConditionClassificationItem ConditionClassificationItem;

        [JsonProperty("state")]
        public PatientConditions_State State;

        [JsonProperty("lastUpdater")]
        public PatientConditions_LastUpdater LastUpdater;

        [JsonProperty("currentLevel")]
        public PatientConditions_CurrentLevel CurrentLevel;

        [JsonProperty("expectedLevel")]
        public PatientConditions_ExpectedLevel ExpectedLevel;

        [JsonProperty("expectedLevelDescription")]
        public string ExpectedLevelDescription;

        [JsonProperty("status")]
        public string Status;

        [JsonProperty("_links")]
        public PatientConditions_Links Links;

        [JsonProperty("currentLevelDescription")]
        public string CurrentLevelDescription;

        [JsonProperty("currentAssessment")]
        public string CurrentAssessment;

        [JsonProperty("expectedAssessment")]
        public string ExpectedAssessment;

        [JsonProperty("goals")]
        public string Goals;

        [JsonProperty("execution")]
        public PatientConditions_Execution Execution;

        [JsonProperty("limitations")]
        public PatientConditions_Limitations Limitations;
    }

}
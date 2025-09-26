using Newtonsoft.Json; 
using System; 
namespace NexusAPIWrapper{ 

    public class PatCond_Observ_Root
    {
        [JsonProperty("state")]
        public PatCond_Observ_State State;

        [JsonProperty("stateUnambiguousValue")]
        public bool? StateUnambiguousValue;

        [JsonProperty("currentLevel")]
        public PatCond_Observ_CurrentLevel CurrentLevel;

        [JsonProperty("currentLevelUnambiguousValue")]
        public bool? CurrentLevelUnambiguousValue;

        [JsonProperty("currentLevelDescription")]
        public PatCond_Observ_CurrentLevelDescription CurrentLevelDescription;

        [JsonProperty("currentLevelDescriptionUnambiguousValue")]
        public bool? CurrentLevelDescriptionUnambiguousValue;

        [JsonProperty("currentAssessment")]
        public PatCond_Observ_CurrentAssessment CurrentAssessment;

        [JsonProperty("currentAssessmentUnambiguousValue")]
        public bool? CurrentAssessmentUnambiguousValue;

        [JsonProperty("expectedLevel")]
        public PatCond_Observ_ExpectedLevel ExpectedLevel;

        [JsonProperty("expectedLevelUnambiguousValue")]
        public bool? ExpectedLevelUnambiguousValue;

        [JsonProperty("expectedLevelDescription")]
        public PatCond_Observ_ExpectedLevelDescription ExpectedLevelDescription;

        [JsonProperty("expectedLevelDescriptionUnambiguousValue")]
        public bool? ExpectedLevelDescriptionUnambiguousValue;

        [JsonProperty("expectedAssessment")]
        public PatCond_Observ_ExpectedAssessment ExpectedAssessment;

        [JsonProperty("expectedAssessmentUnambiguousValue")]
        public bool? ExpectedAssessmentUnambiguousValue;

        [JsonProperty("goals")]
        public PatCond_Observ_Goals Goals;

        [JsonProperty("goalsUnambiguousValue")]
        public bool? GoalsUnambiguousValue;

        [JsonProperty("execution")]
        public PatCond_Observ_Execution Execution;

        [JsonProperty("executionUnambiguousValue")]
        public bool? ExecutionUnambiguousValue;

        [JsonProperty("limitations")]
        public PatCond_Observ_Limitations Limitations;

        [JsonProperty("limitationsUnambiguousValue")]
        public bool? LimitationsUnambiguousValue;

        [JsonProperty("updateDate")]
        public DateTime? UpdateDate;

        [JsonProperty("patient")]
        public PatCond_Observ_Patient Patient;

        [JsonProperty("_links")]
        public PatCond_Observ_Links Links;
    }

}
using Newtonsoft.Json; 
using System; 
namespace NexusAPIWrapper{ 

    public class ConditionsBulkPrototype_Root
    {
        [JsonProperty("state")]
        public ConditionsBulkPrototype_State State;

        [JsonProperty("stateUnambiguousValue")]
        public bool? StateUnambiguousValue;

        [JsonProperty("currentLevel")]
        public ConditionsBulkPrototype_CurrentLevel CurrentLevel;

        [JsonProperty("currentLevelUnambiguousValue")]
        public bool? CurrentLevelUnambiguousValue;

        [JsonProperty("currentLevelDescription")]
        public ConditionsBulkPrototype_CurrentLevelDescription CurrentLevelDescription;

        [JsonProperty("currentLevelDescriptionUnambiguousValue")]
        public bool? CurrentLevelDescriptionUnambiguousValue;

        [JsonProperty("currentAssessment")]
        public ConditionsBulkPrototype_CurrentAssessment CurrentAssessment;

        [JsonProperty("currentAssessmentUnambiguousValue")]
        public bool? CurrentAssessmentUnambiguousValue;

        [JsonProperty("expectedLevel")]
        public ConditionsBulkPrototype_ExpectedLevel ExpectedLevel;

        [JsonProperty("expectedLevelUnambiguousValue")]
        public bool? ExpectedLevelUnambiguousValue;

        [JsonProperty("expectedLevelDescription")]
        public ConditionsBulkPrototype_ExpectedLevelDescription ExpectedLevelDescription;

        [JsonProperty("expectedLevelDescriptionUnambiguousValue")]
        public bool? ExpectedLevelDescriptionUnambiguousValue;

        [JsonProperty("expectedAssessment")]
        public ConditionsBulkPrototype_ExpectedAssessment ExpectedAssessment;

        [JsonProperty("expectedAssessmentUnambiguousValue")]
        public bool? ExpectedAssessmentUnambiguousValue;

        [JsonProperty("goals")]
        public ConditionsBulkPrototype_Goals Goals;

        [JsonProperty("goalsUnambiguousValue")]
        public bool? GoalsUnambiguousValue;

        [JsonProperty("execution")]
        public ConditionsBulkPrototype_Execution Execution;

        [JsonProperty("executionUnambiguousValue")]
        public bool? ExecutionUnambiguousValue;

        [JsonProperty("limitations")]
        public ConditionsBulkPrototype_Limitations Limitations;

        [JsonProperty("limitationsUnambiguousValue")]
        public bool? LimitationsUnambiguousValue;

        [JsonProperty("updateDate")]
        public DateTime? UpdateDate;

        [JsonProperty("patient")]
        public ConditionsBulkPrototype_Patient Patient;

        [JsonProperty("_links")]
        public ConditionsBulkPrototype_Links Links;

    public void UpdateStateValue(ConditionsBulkPrototype_PossibleValue newValue)
        {
            State.Value.Active = newValue.Active;
            State.Value.Version = newValue.Version;
            State.Value.Marker = newValue.Marker;
            State.Value.AdditionalInformation = newValue.AdditionalInformation;
            State.Value.Code = newValue.Code;
            State.Value.Id = newValue.Id;
        }


    }

}
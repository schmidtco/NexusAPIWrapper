using Newtonsoft.Json; 
using System; 
namespace NexusAPIWrapper{ 

    public class AvCondClassCondPrototype_Root
    {
        [JsonProperty("state")]
        public AvCondClassCondPrototype_State State;

        [JsonProperty("stateUnambiguousValue")]
        public bool? StateUnambiguousValue;

        [JsonProperty("currentLevel")]
        public AvCondClassCondPrototype_CurrentLevel CurrentLevel;

        [JsonProperty("currentLevelUnambiguousValue")]
        public bool? CurrentLevelUnambiguousValue;

        [JsonProperty("currentLevelDescription")]
        public AvCondClassCondPrototype_CurrentLevelDescription CurrentLevelDescription;

        [JsonProperty("currentLevelDescriptionUnambiguousValue")]
        public bool? CurrentLevelDescriptionUnambiguousValue;

        [JsonProperty("expectedLevel")]
        public AvCondClassCondPrototype_ExpectedLevel ExpectedLevel;

        [JsonProperty("expectedLevelUnambiguousValue")]
        public bool? ExpectedLevelUnambiguousValue;

        [JsonProperty("expectedLevelDescription")]
        public AvCondClassCondPrototype_ExpectedLevelDescription ExpectedLevelDescription;

        [JsonProperty("expectedLevelDescriptionUnambiguousValue")]
        public bool? ExpectedLevelDescriptionUnambiguousValue;

        [JsonProperty("goals")]
        public AvCondClassCondPrototype_Goals Goals;

        [JsonProperty("goalsUnambiguousValue")]
        public bool? GoalsUnambiguousValue;

        [JsonProperty("execution")]
        public AvCondClassCondPrototype_Execution Execution;

        [JsonProperty("executionUnambiguousValue")]
        public bool? ExecutionUnambiguousValue;

        [JsonProperty("limitations")]
        public AvCondClassCondPrototype_Limitations Limitations;

        [JsonProperty("limitationsUnambiguousValue")]
        public bool? LimitationsUnambiguousValue;

        [JsonProperty("updateDate")]
        public DateTime? UpdateDate;

        [JsonProperty("patient")]
        public AvCondClassCondPrototype_Patient Patient;

        [JsonProperty("_links")]
        public AvCondClassCondPrototype_Links Links;
    }

}
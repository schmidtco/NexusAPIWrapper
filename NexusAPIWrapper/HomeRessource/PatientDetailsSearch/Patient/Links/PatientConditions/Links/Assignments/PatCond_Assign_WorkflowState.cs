using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatCond_Assign_WorkflowState
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("stateType")]
        public string StateType;
    }

}
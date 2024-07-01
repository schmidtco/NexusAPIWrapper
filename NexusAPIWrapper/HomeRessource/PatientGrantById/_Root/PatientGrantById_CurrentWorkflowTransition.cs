using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientGrantById_CurrentWorkflowTransition
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("actionType")]
        public string ActionType;

        [JsonProperty("resultWith")]
        public PatientGrantById_ResultWith ResultWith;

        [JsonProperty("validateOnTransition")]
        public object ValidateOnTransition;

        [JsonProperty("_links")]
        public PatientGrantById_Links Links;
    }

}
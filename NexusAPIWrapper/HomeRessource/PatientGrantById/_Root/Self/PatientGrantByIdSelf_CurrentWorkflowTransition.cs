using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientGrantByIdSelf_CurrentWorkflowTransition
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("actionType")]
        public string ActionType;

        [JsonProperty("resultWith")]
        public PatientGrantByIdSelf_ResultWith ResultWith;

        [JsonProperty("validateOnTransition")]
        public object ValidateOnTransition;

        [JsonProperty("_links")]
        public PatientGrantByIdSelf_Links Links;
    }

}
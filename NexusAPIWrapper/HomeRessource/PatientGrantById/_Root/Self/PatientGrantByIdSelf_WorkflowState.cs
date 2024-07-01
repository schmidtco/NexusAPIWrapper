using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientGrantByIdSelf_WorkflowState
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("key")]
        public object Key;

        [JsonProperty("_links")]
        public PatientGrantByIdSelf_Links Links;
    }

}
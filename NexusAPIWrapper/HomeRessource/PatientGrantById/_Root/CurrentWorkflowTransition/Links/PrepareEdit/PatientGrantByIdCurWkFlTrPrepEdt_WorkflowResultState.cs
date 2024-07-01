using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientGrantByIdCurWkFlTrPrepEdt_WorkflowResultState
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("key")]
        public object Key;

        [JsonProperty("_links")]
        public PatientGrantByIdCurWkFlTrPrepEdt_Links Links;
    }

}
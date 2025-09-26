using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatCond_RelActiWHist_WorkflowState
    {
        [JsonProperty("id")]
        public object Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("key")]
        public object Key;

        [JsonProperty("_links")]
        public PatCond_RelActiWHist_Links Links;
    }

}
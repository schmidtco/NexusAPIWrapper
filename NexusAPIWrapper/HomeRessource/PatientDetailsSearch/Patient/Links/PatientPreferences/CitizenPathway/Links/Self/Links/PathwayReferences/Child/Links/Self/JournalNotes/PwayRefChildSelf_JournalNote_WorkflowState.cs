using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PwayRefChildSelf_JournalNote_WorkflowState
    {
        [JsonProperty("id")]
        public object Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("key")]
        public object Key;

        [JsonProperty("_links")]
        public PwayRefChildSelf_JournalNote_Links Links;
    }

}
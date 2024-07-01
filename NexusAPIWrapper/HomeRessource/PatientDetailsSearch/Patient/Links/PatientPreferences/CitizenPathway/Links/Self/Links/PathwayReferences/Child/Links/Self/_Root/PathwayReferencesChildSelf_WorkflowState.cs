using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PathwayReferencesChildSelf_WorkflowState
    {
        [JsonProperty("id")]
        public object Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("key")]
        public object Key;

        [JsonProperty("_links")]
        public PathwayReferencesChildSelf_Links Links;
    }

}
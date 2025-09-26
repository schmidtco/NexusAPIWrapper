using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class ConditionsBulkPrototype_CitizenRegistryConfiguration
    {
        [JsonProperty("updateEnabled")]
        public bool? UpdateEnabled;

        [JsonProperty("_links")]
        public ConditionsBulkPrototype_Links Links;
    }

}
using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class ConditionsBulkPrototype_Type
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("abbreviation")]
        public string Abbreviation;

        [JsonProperty("_links")]
        public ConditionsBulkPrototype_Links Links;
    }

}
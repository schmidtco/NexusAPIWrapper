using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class AvailableConditionClassifications_Root
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("group")]
        public AvailableConditionClassifications_Group Group;

        [JsonProperty("active")]
        public bool? Active;

        [JsonProperty("klMappingCode")]
        public string KlMappingCode;

        [JsonProperty("nexusMappingCode")]
        public string NexusMappingCode;

        [JsonProperty("classificationCode")]
        public string ClassificationCode;

        [JsonProperty("_links")]
        public AvailableConditionClassifications_Links Links;
    }

}
using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class CondBulkProtoCreate_ConditionClassificationItem
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("group")]
        public CondBulkProtoCreate_Group Group;

        [JsonProperty("active")]
        public bool? Active;

        [JsonProperty("klMappingCode")]
        public string KlMappingCode;

        [JsonProperty("nexusMappingCode")]
        public object NexusMappingCode;

        [JsonProperty("classificationCode")]
        public string ClassificationCode;

        [JsonProperty("_links")]
        public CondBulkProtoCreate_Links Links;
    }

}
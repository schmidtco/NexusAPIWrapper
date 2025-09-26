using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientConditions_Group
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("shortName")]
        public string ShortName;

        [JsonProperty("law")]
        public string Law;

        [JsonProperty("klMappingCode")]
        public string KlMappingCode;

        [JsonProperty("classificationCode")]
        public string ClassificationCode;

        [JsonProperty("_links")]
        public PatientConditions_Links Links;
    }

}
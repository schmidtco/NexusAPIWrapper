using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientGrantByIdAudit_Change
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("key")]
        public string Key;

        [JsonProperty("translation")]
        public PatientGrantByIdAudit_Translation Translation;

        [JsonProperty("additionalInfo")]
        public object AdditionalInfo;

        [JsonProperty("oldValue")]
        public string OldValue;

        [JsonProperty("newValue")]
        public string NewValue;

        [JsonProperty("_links")]
        public PatientGrantByIdAudit_Links Links;
    }

}
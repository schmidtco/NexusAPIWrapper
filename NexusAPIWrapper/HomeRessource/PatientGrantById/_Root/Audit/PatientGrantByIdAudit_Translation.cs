using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientGrantByIdAudit_Translation
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("keyName")]
        public string KeyName;

        [JsonProperty("newValue")]
        public string NewValue;

        [JsonProperty("oldValue")]
        public string OldValue;

        [JsonProperty("_links")]
        public PatientGrantByIdAudit_Links Links;

        [JsonProperty("actionName")]
        public string ActionName;
    }

}
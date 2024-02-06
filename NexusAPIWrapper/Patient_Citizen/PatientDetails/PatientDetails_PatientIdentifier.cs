using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientDetails_PatientIdentifier
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("identifier")]
        public string Identifier;

        [JsonProperty("managedExternally")]
        public bool ManagedExternally;

        [JsonProperty("_links")]
        public PatientDetails_Links Links;
    }

}
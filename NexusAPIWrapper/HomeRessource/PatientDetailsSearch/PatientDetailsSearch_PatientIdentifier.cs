using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientDetailsSearch_PatientIdentifier
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("identifier")]
        public string Identifier;

        [JsonProperty("managedExternally")]
        public bool? ManagedExternally;

        [JsonProperty("_links")]
        public PatientDetailsSearch_Links Links;
    }

}
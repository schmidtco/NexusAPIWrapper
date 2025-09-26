using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatCond_Observ_PatientIdentifier
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("identifier")]
        public string Identifier;

        [JsonProperty("managedExternally")]
        public bool? ManagedExternally;

        [JsonProperty("_links")]
        public PatCond_Observ_Links Links;
    }

}
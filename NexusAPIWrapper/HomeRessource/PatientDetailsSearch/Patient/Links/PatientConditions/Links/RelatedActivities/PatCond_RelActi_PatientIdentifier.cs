using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatCond_RelActi_PatientIdentifier
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("identifier")]
        public string Identifier;

        [JsonProperty("managedExternally")]
        public bool? ManagedExternally;

        [JsonProperty("_links")]
        public PatCond_RelActi_Links Links;
    }

}
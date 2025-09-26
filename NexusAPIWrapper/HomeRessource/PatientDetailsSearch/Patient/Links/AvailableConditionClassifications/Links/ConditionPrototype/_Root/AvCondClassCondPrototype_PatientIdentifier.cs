using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class AvCondClassCondPrototype_PatientIdentifier
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("identifier")]
        public string Identifier;

        [JsonProperty("managedExternally")]
        public bool? ManagedExternally;

        [JsonProperty("_links")]
        public AvCondClassCondPrototype_Links Links;
    }

}
using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientGrantByIdSelf_PatientIdentifier
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("identifier")]
        public string Identifier;

        [JsonProperty("managedExternally")]
        public bool? ManagedExternally;

        [JsonProperty("_links")]
        public PatientGrantByIdSelf_Links Links;
    }

}
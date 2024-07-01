using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PwayRefChildSelf_JournalNote_RefObj_PatientIdentifier
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("identifier")]
        public string Identifier;

        [JsonProperty("managedExternally")]
        public bool? ManagedExternally;

        [JsonProperty("_links")]
        public PwayRefChildSelf_JournalNote_RefObj_Links Links;
    }

}
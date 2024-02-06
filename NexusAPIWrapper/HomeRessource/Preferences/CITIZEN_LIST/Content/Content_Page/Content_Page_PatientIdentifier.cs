using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class Content_Page_PatientIdentifier
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("identifier")]
        public string Identifier;

        [JsonProperty("managedExternally")]
        public bool? ManagedExternally;

        [JsonProperty("_links")]
        public Content_Page_Links Links;
    }

}
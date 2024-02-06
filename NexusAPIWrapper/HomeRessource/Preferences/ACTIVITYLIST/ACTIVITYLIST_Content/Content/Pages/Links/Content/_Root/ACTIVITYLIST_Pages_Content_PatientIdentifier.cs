using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class ACTIVITYLIST_Pages_Content_PatientIdentifier
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("identifier")]
        public string Identifier;

        [JsonProperty("managedExternally")]
        public bool? ManagedExternally;

        [JsonProperty("_links")]
        public ACTIVITYLIST_Pages_Content_Links Links;
    }

}
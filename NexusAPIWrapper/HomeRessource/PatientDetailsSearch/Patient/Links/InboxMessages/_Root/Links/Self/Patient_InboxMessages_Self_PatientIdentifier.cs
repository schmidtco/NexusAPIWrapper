using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class Patient_InboxMessages_Self_PatientIdentifier
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("identifier")]
        public string Identifier;

        [JsonProperty("managedExternally")]
        public bool? ManagedExternally;

        [JsonProperty("_links")]
        public Patient_InboxMessages_Self_Links Links;
    }

}
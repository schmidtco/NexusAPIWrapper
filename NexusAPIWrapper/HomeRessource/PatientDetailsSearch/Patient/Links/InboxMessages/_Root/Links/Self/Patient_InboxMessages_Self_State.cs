using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class Patient_InboxMessages_Self_State
    {
        [JsonProperty("direction")]
        public string Direction;

        [JsonProperty("status")]
        public string Status;

        [JsonProperty("response")]
        public string Response;

        [JsonProperty("handshake")]
        public string Handshake;

        [JsonProperty("processed")]
        public string Processed;

        [JsonProperty("_links")]
        public Patient_InboxMessages_Self_Links Links;
    }

}
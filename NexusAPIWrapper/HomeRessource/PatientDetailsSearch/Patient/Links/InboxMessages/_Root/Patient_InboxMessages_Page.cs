using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class Patient_InboxMessages_Page
    {
        [JsonProperty("_links")]
        public Patient_InboxMessages_Links Links;
    }

}
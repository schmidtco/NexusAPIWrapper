using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class Patient_InboxMessages_Self_Type
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("abbreviation")]
        public string Abbreviation;

        [JsonProperty("_links")]
        public Patient_InboxMessages_Self_Links Links;
    }

}
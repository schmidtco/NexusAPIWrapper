using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class ReferencedObject_Base_Recipient
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("recipientAddress")]
        public object RecipientAddress;

        [JsonProperty("recipientType")]
        public string RecipientType;

        [JsonProperty("_links")]
        public ReferencedObject_Base_Links Links;
    }

}
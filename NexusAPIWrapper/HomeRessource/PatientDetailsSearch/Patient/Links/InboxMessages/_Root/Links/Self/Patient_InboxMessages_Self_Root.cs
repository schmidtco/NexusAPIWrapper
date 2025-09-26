using Newtonsoft.Json; 
using System; 
namespace NexusAPIWrapper{ 

    public class Patient_InboxMessages_Self_Root
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("type")]
        public string Type;

        [JsonProperty("subject")]
        public string Subject;

        [JsonProperty("senderDisplayValue")]
        public string SenderDisplayValue;

        [JsonProperty("recipientsDisplayValue")]
        public string RecipientsDisplayValue;

        [JsonProperty("date")]
        public DateTime? Date;

        [JsonProperty("patient")]
        public Patient_InboxMessages_Self_Patient Patient;

        [JsonProperty("state")]
        public Patient_InboxMessages_Self_State State;

        [JsonProperty("statusName")]
        public string StatusName;

        [JsonProperty("externalId")]
        public object ExternalId;

        [JsonProperty("_links")]
        public Patient_InboxMessages_Self_Links Links;
    }

}
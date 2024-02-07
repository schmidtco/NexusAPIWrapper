using Newtonsoft.Json; 
using System; 
namespace NexusAPIWrapper{
    /// <summary>
    /// Call api.GetActivityListContentBaseObject and pass a "ACTIVITYLIST_Pages_Content_Root" object to get the ReferencedObject_Base_Root object
    /// </summary>
    public class ReferencedObject_Base_Root
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("sender")]
        public ReferencedObject_Base_Sender Sender;

        [JsonProperty("recipient")]
        public ReferencedObject_Base_Recipient Recipient;

        [JsonProperty("subject")]
        public string Subject;

        [JsonProperty("date")]
        public DateTime? Date;

        [JsonProperty("state")]
        public ReferencedObject_Base_State State;

        [JsonProperty("customer")]
        public object Customer;

        [JsonProperty("raw")]
        public string Raw;

        [JsonProperty("externalId")]
        public object ExternalId;

        [JsonProperty("ccRecipient")]
        public object CcRecipient;

        [JsonProperty("copiedMessageId")]
        public object CopiedMessageId;

        [JsonProperty("letterType")]
        public object LetterType;

        [JsonProperty("serviceTagCode")]
        public object ServiceTagCode;

        [JsonProperty("messageType")]
        public string MessageType;

        [JsonProperty("versionCode")]
        public string VersionCode;

        [JsonProperty("conversationId")]
        public object ConversationId;

        [JsonProperty("activityIdentifier")]
        public ReferencedObject_Base_ActivityIdentifier ActivityIdentifier;

        [JsonProperty("patient")]
        public ReferencedObject_Base_Patient Patient;

        [JsonProperty("pathwayAssociation")]
        public ReferencedObject_Base_PathwayAssociation PathwayAssociation;

        [JsonProperty("body")]
        public string Body;

        [JsonProperty("priority")]
        public string Priority;

        [JsonProperty("previousMessageId")]
        public object PreviousMessageId;

        [JsonProperty("_links")]
        public ReferencedObject_Base_Links Links;

        [JsonProperty("level")]
        public object Level;

    }

}
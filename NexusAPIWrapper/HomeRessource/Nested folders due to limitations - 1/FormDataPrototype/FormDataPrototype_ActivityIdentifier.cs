using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class FormDataPrototype_ActivityIdentifier
    {
        [JsonProperty("identifier")]
        public string Identifier;

        [JsonProperty("type")]
        public string Type;

        [JsonProperty("activityId")]
        public object ActivityId;

        [JsonProperty("_links")]
        public FormDataPrototype_Links Links;
    }

}
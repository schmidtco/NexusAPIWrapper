using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class FormDataPrototype_AvailableActions_Root
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("actionType")]
        public object ActionType;

        [JsonProperty("resultWith")]
        public FormDataPrototype_AvailableActions_ResultWith ResultWith;

        [JsonProperty("validateOnTransition")]
        public bool? ValidateOnTransition;

        [JsonProperty("_links")]
        public FormDataPrototype_AvailableActions_Links Links;
    }

}
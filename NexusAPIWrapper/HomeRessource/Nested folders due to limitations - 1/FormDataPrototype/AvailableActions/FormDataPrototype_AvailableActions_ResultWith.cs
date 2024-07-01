using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class FormDataPrototype_AvailableActions_ResultWith
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("key")]
        public object Key;

        [JsonProperty("_links")]
        public FormDataPrototype_AvailableActions_Links Links;
    }

}
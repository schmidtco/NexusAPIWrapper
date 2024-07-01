using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class FormDataPrototype_AvailableTags_Grouping
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("_links")]
        public FormDataPrototype_AvailableTags_Links Links;
    }

}
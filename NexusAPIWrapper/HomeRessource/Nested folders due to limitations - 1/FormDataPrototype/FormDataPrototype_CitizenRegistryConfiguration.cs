using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class FormDataPrototype_CitizenRegistryConfiguration
    {
        [JsonProperty("updateEnabled")]
        public bool? UpdateEnabled;

        [JsonProperty("_links")]
        public FormDataPrototype_Links Links;
    }

}
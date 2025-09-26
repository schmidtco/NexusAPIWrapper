using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class ObservationsPrototype_CitizenRegistryConfiguration
    {
        [JsonProperty("updateEnabled")]
        public bool? UpdateEnabled;

        [JsonProperty("_links")]
        public ObservationsPrototype_Links Links;
    }

}
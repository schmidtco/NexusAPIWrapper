using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatCond_Observ_CitizenRegistryConfiguration
    {
        [JsonProperty("updateEnabled")]
        public bool? UpdateEnabled;

        [JsonProperty("_links")]
        public PatCond_Observ_Links Links;
    }

}
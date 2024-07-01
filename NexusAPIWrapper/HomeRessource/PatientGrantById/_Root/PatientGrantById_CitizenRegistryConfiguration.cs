using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientGrantById_CitizenRegistryConfiguration
    {
        [JsonProperty("updateEnabled")]
        public bool? UpdateEnabled;

        [JsonProperty("_links")]
        public PatientGrantById_Links Links;
    }

}
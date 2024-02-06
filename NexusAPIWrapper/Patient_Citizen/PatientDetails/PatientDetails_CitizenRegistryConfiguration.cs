using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientDetails_CitizenRegistryConfiguration
    {
        [JsonProperty("updateEnabled")]
        public bool UpdateEnabled;

        [JsonProperty("_links")]
        public PatientDetails_Links Links;
    }

}
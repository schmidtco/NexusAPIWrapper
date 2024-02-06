using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientDetailsSearch_CitizenRegistryConfiguration
    {
        [JsonProperty("updateEnabled")]
        public bool? UpdateEnabled;

        [JsonProperty("_links")]
        public PatientDetailsSearch_Links Links;
    }

}
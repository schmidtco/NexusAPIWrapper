using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientEnrolled_CitizenRegistryConfiguration
    {
        [JsonProperty("updateEnabled")]
        public bool? UpdateEnabled;

        [JsonProperty("_links")]
        public PatientEnrolled_Links Links;
    }

}
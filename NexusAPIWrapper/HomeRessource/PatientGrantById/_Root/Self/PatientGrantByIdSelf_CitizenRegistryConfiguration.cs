using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientGrantByIdSelf_CitizenRegistryConfiguration
    {
        [JsonProperty("updateEnabled")]
        public bool? UpdateEnabled;

        [JsonProperty("_links")]
        public PatientGrantByIdSelf_Links Links;
    }

}
using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class ProfessionalConfiguration_ActiveDirectoryConfiguration
    {
        [JsonProperty("upn")]
        public string Upn;

        [JsonProperty("_links")]
        public ProfessionalConfiguration_Links Links;
    }

}
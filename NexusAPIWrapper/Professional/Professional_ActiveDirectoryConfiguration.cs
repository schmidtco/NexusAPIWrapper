using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class Professional_ActiveDirectoryConfiguration
    {
        [JsonProperty("upn")]
        public string Upn;

        [JsonProperty("_links")]
        public Professional_Links Links;
    }

}
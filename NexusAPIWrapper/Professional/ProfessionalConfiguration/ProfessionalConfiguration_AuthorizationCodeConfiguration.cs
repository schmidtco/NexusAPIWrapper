using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class ProfessionalConfiguration_AuthorizationCodeConfiguration
    {
        [JsonProperty("authorizationCode")]
        public string AuthorizationCode;

        [JsonProperty("_links")]
        public ProfessionalConfiguration_Links Links;
    }

}
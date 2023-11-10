using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class Organizations_Links
    {
        [JsonProperty("self")]
        public Organizations_Self Self;

        [JsonProperty("stsOrganization")]
        public Organizations_StsOrganization StsOrganization;
    }

}
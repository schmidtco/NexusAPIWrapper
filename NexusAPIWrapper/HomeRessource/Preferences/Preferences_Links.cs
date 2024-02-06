using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class Preferences_Links
    {
        [JsonProperty("self")]
        public Preferences_Self Self;

        [JsonProperty("stsOrganization")]
        public Preferences_StsOrganization StsOrganization;
    }

}
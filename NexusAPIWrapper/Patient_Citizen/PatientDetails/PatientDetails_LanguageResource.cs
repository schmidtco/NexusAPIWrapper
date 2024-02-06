using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientDetails_LanguageResource
    {
        [JsonProperty("isoCode")]
        public string IsoCode;

        [JsonProperty("displayName")]
        public string DisplayName;

        [JsonProperty("_links")]
        public PatientDetails_Links Links;
    }

}
using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientDetailsSearch_LanguageResource
    {
        [JsonProperty("isoCode")]
        public string IsoCode;

        [JsonProperty("displayName")]
        public string DisplayName;

        [JsonProperty("_links")]
        public PatientDetailsSearch_Links Links;
    }

}
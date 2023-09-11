using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientDetails_PreferredLanguage
    {
        [JsonProperty("languageResource")]
        public PatientDetails_LanguageResource LanguageResource;

        [JsonProperty("href")]
        public string Href;
    }

}
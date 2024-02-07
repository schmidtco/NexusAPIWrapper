using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientDetailsSearch_PreferredLanguage
    {
        [JsonProperty("languageResource")]
        public PatientDetailsSearch_LanguageResource LanguageResource;

        [JsonProperty("href")]
        public string Href;
    }

}
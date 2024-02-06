using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientDetailsSearch_Type
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("abbreviation")]
        public string Abbreviation;

        [JsonProperty("_links")]
        public PatientDetailsSearch_Links Links;
    }

}
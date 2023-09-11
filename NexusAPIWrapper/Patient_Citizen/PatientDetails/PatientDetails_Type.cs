using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientDetails_Type
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("abbreviation")]
        public string Abbreviation;

        [JsonProperty("_links")]
        public PatientDetails_Links Links;
    }

}
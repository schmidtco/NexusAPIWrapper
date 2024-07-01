using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientGrantById_Type
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("abbreviation")]
        public string Abbreviation;

        [JsonProperty("_links")]
        public PatientGrantById_Links Links;
    }

}
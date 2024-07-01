using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientGrantById_ResultWith
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("key")]
        public object Key;

        [JsonProperty("_links")]
        public PatientGrantById_Links Links;
    }

}
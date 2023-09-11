using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientDetails_PatientCategory
    {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("_links")]
        public PatientDetails_Links Links;
    }

}
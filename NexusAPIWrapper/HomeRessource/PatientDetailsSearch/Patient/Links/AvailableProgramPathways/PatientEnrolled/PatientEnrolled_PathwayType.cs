using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientEnrolled_PathwayType
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("active")]
        public bool? Active;

        [JsonProperty("root")]
        public bool? Root;

        [JsonProperty("_links")]
        public PatientEnrolled_Links Links;
    }

}
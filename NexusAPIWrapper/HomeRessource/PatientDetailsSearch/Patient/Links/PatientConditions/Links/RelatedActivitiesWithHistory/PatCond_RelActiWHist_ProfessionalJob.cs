using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatCond_RelActiWHist_ProfessionalJob
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("active")]
        public bool? Active;

        [JsonProperty("_links")]
        public PatCond_RelActiWHist_Links Links;
    }

}
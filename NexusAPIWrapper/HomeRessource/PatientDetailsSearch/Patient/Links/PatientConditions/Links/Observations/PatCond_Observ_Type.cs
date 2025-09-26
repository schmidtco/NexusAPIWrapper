using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatCond_Observ_Type
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("abbreviation")]
        public string Abbreviation;

        [JsonProperty("_links")]
        public PatCond_Observ_Links Links;
    }

}
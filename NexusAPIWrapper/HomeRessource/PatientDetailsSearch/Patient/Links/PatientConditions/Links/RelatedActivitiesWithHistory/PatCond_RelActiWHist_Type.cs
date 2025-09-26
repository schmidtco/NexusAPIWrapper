using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatCond_RelActiWHist_Type
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("abbreviation")]
        public string Abbreviation;

        [JsonProperty("_links")]
        public PatCond_RelActiWHist_Links Links;
    }

}
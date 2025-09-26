using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class CitDashbCitCondSelfWidgCond_Root
    {
        [JsonProperty("classification")]
        public CitDashbCitCondSelfWidgCond_Classification Classification;

        [JsonProperty("_links")]
        public CitDashbCitCondSelfWidgCond_Links Links;
    }

}
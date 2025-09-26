using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class CitDashbCitCondSelfWidgCond_Classification
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("law")]
        public CitDashbCitCondSelfWidgCond_Law Law;

        [JsonProperty("_links")]
        public CitDashbCitCondSelfWidgCond_Links Links;
    }

}
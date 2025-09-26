using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class CitDashbCitCondSelfWidgVisi_GroupClassification
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("law")]
        public CitDashbCitCondSelfWidgVisi_Law Law;

        [JsonProperty("_links")]
        public CitDashbCitCondSelfWidgVisi_Links Links;
    }

}
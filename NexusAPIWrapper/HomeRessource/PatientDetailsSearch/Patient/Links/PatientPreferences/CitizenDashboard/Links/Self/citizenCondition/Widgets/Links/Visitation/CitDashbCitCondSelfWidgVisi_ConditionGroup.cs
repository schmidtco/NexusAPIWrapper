using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class CitDashbCitCondSelfWidgVisi_ConditionGroup
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("groupClassification")]
        public CitDashbCitCondSelfWidgVisi_GroupClassification GroupClassification;

        [JsonProperty("description")]
        public string Description;

        [JsonProperty("_links")]
        public CitDashbCitCondSelfWidgVisi_Links Links;
    }

}
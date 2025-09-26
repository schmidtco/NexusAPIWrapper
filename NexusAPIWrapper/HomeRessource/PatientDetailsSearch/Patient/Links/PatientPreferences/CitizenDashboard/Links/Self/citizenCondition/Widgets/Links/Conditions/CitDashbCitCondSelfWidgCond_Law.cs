using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class CitDashbCitCondSelfWidgCond_Law
    {
        [JsonProperty("code")]
        public string Code;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("_links")]
        public CitDashbCitCondSelfWidgCond_Links Links;
    }

}
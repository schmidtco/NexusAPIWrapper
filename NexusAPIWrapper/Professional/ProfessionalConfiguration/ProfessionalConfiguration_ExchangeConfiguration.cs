using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class ProfessionalConfiguration_ExchangeConfiguration
    {
        [JsonProperty("sendToExchange")]
        public bool SendToExchange;

        [JsonProperty("fetchFromExchange")]
        public bool FetchFromExchange;

        [JsonProperty("_links")]
        public ProfessionalConfiguration_Links Links;
    }

}
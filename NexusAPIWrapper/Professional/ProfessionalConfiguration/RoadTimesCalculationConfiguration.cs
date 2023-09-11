using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class RoadTimesCalculationConfiguration
    {
        [JsonProperty("transportType")]
        public object TransportType;

        [JsonProperty("_links")]
        public ProfessionalConfiguration_Links Links;
    }

}
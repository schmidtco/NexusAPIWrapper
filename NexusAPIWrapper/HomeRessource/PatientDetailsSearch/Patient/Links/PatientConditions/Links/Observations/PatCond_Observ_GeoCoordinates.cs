using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatCond_Observ_GeoCoordinates
    {
        [JsonProperty("x")]
        public object X;

        [JsonProperty("y")]
        public object Y;

        [JsonProperty("present")]
        public bool? Present;
    }

}
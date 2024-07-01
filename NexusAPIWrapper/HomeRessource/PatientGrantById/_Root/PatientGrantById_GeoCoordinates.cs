using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientGrantById_GeoCoordinates
    {
        [JsonProperty("x")]
        public object X;

        [JsonProperty("y")]
        public object Y;

        [JsonProperty("present")]
        public bool? Present;
    }

}
using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class AvNestPrgPways_Links
    {
        [JsonProperty("self")]
        public AvNestPrgPways_Self Self;

        [JsonProperty("enroll")]
        public AvNestPrgPways_Enroll Enroll;
    }

}
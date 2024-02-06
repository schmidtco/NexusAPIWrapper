using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class AvailableProgramPathways_Links
    {
        [JsonProperty("self")]
        public AvailableProgramPathways_Self Self;

        [JsonProperty("enroll")]
        public AvailableProgramPathways_Enroll Enroll;
    }

}
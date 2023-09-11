using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientDetails_NexusVideoConfiguration
    {
        [JsonProperty("allowVideoCalls")]
        public bool AllowVideoCalls;

        [JsonProperty("_links")]
        public PatientDetails_Links Links;
    }

}
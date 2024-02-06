using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientDetailsSearch_NexusVideoConfiguration
    {
        [JsonProperty("allowVideoCalls")]
        public bool? AllowVideoCalls;

        [JsonProperty("_links")]
        public PatientDetailsSearch_Links Links;
    }

}
using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientGrantById_KleNumber
    {
        [JsonProperty("uid")]
        public string Uid;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("description")]
        public string Description;

        [JsonProperty("_links")]
        public PatientGrantById_Links Links;
    }

}
using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class AvailableProgramPathways_KleNumber
    {
        [JsonProperty("uid")]
        public string Uid;

        [JsonProperty("_links")]
        public AvailableProgramPathways_Links Links;
    }

}
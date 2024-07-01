using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class AvNestPrgPways_KleNumber
    {
        [JsonProperty("uid")]
        public string Uid;

        [JsonProperty("_links")]
        public AvNestPrgPways_Links Links;
    }

}
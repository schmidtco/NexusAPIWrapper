using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class AvNestPrgPways_ActionFacet
    {
        [JsonProperty("uid")]
        public string Uid;

        [JsonProperty("_links")]
        public AvNestPrgPways_Links Links;
    }

}
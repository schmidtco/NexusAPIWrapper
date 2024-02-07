using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class ActivePrograms_ActionFacet
    {
        [JsonProperty("uid")]
        public string Uid;

        [JsonProperty("_links")]
        public ActivePrograms_Links Links;
    }

}
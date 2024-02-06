using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class CITIZEN_LIST_Sorting
    {
        [JsonProperty("column")]
        public string Column;

        [JsonProperty("direction")]
        public string Direction;

        [JsonProperty("_links")]
        public CITIZEN_LIST_Links Links;
    }

}
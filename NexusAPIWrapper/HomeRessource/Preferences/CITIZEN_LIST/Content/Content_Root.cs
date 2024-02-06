using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class Content_Root
    {
        [JsonProperty("totalItems")]
        public int? TotalItems;

        [JsonProperty("pageSize")]
        public int? PageSize;

        [JsonProperty("pages")]
        public List<Content_Page> Pages;

        [JsonProperty("_links")]
        public Content_Links Links;
    }

}
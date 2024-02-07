using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class ACTIVITYLIST_Content_Root
    {
        [JsonProperty("totalItems")]
        public int? TotalItems;

        [JsonProperty("pageSize")]
        public int? PageSize;

        [JsonProperty("pages")]
        public List<ACTIVITYLIST_Content_Page> Pages;

        [JsonProperty("totalSelectableItems")]
        public int? TotalSelectableItems;

        [JsonProperty("_links")]
        public ACTIVITYLIST_Content_Links Links;
    }

}
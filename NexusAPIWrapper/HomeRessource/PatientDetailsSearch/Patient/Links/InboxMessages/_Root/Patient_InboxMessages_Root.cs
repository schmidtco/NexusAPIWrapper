using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class Patient_InboxMessages_Root
    {
        [JsonProperty("totalItems")]
        public int? TotalItems;

        [JsonProperty("pageSize")]
        public int? PageSize;

        [JsonProperty("pages")]
        public List<Patient_InboxMessages_Page> Pages;

        [JsonProperty("_links")]
        public Patient_InboxMessages_Links Links;
    }

}
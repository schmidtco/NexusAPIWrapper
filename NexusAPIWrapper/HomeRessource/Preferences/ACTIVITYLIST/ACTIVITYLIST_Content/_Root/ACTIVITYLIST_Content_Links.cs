using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class ACTIVITYLIST_Content_Links
    {
        [JsonProperty("content")]
        public ACTIVITYLIST_Content_Content Content;

        [JsonProperty("medcomBulkArchiveWithIds")]
        public ACTIVITYLIST_Content_MedcomBulkArchiveWithIds MedcomBulkArchiveWithIds;
    }

}
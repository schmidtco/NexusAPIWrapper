using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatCond_RelActiWHist_Activity
    {
        [JsonProperty("activityReference")]
        public PatCond_RelActiWHist_ActivityReference ActivityReference;

        [JsonProperty("auditResource")]
        public PatCond_RelActiWHist_AuditResource AuditResource;

        [JsonProperty("_links")]
        public PatCond_RelActiWHist_Links Links;
    }

}
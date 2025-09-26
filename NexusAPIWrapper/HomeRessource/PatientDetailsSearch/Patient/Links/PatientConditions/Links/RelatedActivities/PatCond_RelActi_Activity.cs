using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatCond_RelActi_Activity
    {
        [JsonProperty("activityReference")]
        public PatCond_RelActi_ActivityReference ActivityReference;

        [JsonProperty("auditResource")]
        public object AuditResource;

        [JsonProperty("_links")]
        public PatCond_RelActi_Links Links;
    }

}
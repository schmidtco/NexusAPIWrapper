using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class PatCond_RelActiWHist_AuditResource
    {
        [JsonProperty("auditEntries")]
        public List<PatCond_RelActiWHist_AuditEntry> AuditEntries;

        [JsonProperty("_links")]
        public PatCond_RelActiWHist_Links Links;
    }

}
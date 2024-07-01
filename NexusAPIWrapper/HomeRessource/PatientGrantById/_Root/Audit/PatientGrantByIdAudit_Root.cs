using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class PatientGrantByIdAudit_Root
    {
        [JsonProperty("auditEntries")]
        public List<PatientGrantByIdAudit_AuditEntry> AuditEntries;

        [JsonProperty("_links")]
        public PatientGrantByIdAudit_Links Links;
    }

}
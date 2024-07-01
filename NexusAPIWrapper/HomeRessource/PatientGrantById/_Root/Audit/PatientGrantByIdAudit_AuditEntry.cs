using Newtonsoft.Json; 
using System.Collections.Generic; 
using System; 
namespace NexusAPIWrapper{ 

    public class PatientGrantByIdAudit_AuditEntry
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("changes")]
        public List<PatientGrantByIdAudit_Change> Changes;

        [JsonProperty("professional")]
        public PatientGrantByIdAudit_Professional Professional;

        [JsonProperty("professionalDescription")]
        public object ProfessionalDescription;

        [JsonProperty("additionalInfo")]
        public object AdditionalInfo;

        [JsonProperty("date")]
        public DateTime? Date;

        [JsonProperty("translation")]
        public PatientGrantByIdAudit_Translation Translation;

        [JsonProperty("auditObjectId")]
        public int? AuditObjectId;

        [JsonProperty("_links")]
        public PatientGrantByIdAudit_Links Links;
    }

}
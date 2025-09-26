using Newtonsoft.Json; 
using System.Collections.Generic; 
using System; 
namespace NexusAPIWrapper{ 

    public class PatCond_RelActiWHist_AuditEntry
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("changes")]
        public List<object> Changes;

        [JsonProperty("professional")]
        public PatCond_RelActiWHist_Professional Professional;

        [JsonProperty("professionalDescription")]
        public string ProfessionalDescription;

        [JsonProperty("additionalInfo")]
        public object AdditionalInfo;

        [JsonProperty("date")]
        public DateTime? Date;

        [JsonProperty("translation")]
        public PatCond_RelActiWHist_Translation Translation;

        [JsonProperty("auditObjectId")]
        public int? AuditObjectId;

        [JsonProperty("_links")]
        public PatCond_RelActiWHist_Links Links;
    }

}
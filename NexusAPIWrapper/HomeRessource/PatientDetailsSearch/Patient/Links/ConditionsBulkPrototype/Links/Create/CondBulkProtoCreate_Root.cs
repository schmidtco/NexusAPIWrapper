using Newtonsoft.Json; 
using System; 
namespace NexusAPIWrapper{ 

    public class CondBulkProtoCreate_Root
    {
        [JsonProperty("patientActivityType")]
        public string PatientActivityType;

        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("activityIdentifier")]
        public CondBulkProtoCreate_ActivityIdentifier ActivityIdentifier;

        [JsonProperty("createDate")]
        public DateTime? CreateDate;

        [JsonProperty("updateDate")]
        public DateTime? UpdateDate;

        [JsonProperty("lastActivationDate")]
        public DateTime? LastActivationDate;

        [JsonProperty("lastEvaluationDate")]
        public DateTime? LastEvaluationDate;

        [JsonProperty("conditionClassificationItem")]
        public CondBulkProtoCreate_ConditionClassificationItem ConditionClassificationItem;

        [JsonProperty("state")]
        public CondBulkProtoCreate_State State;

        [JsonProperty("lastUpdater")]
        public CondBulkProtoCreate_LastUpdater LastUpdater;

        [JsonProperty("status")]
        public string Status;

        [JsonProperty("_links")]
        public CondBulkProtoCreate_Links Links;
    }

}
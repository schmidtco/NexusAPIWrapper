using Newtonsoft.Json; 
using System.Collections.Generic; 
using System; 
namespace NexusAPIWrapper{ 

    public class PwayRefChildSelf_JournalNote_Root
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("date")]
        public DateTime? Date;

        [JsonProperty("children")]
        public List<object> Children;

        [JsonProperty("activityIdentifier")]
        public PwayRefChildSelf_JournalNote_ActivityIdentifier ActivityIdentifier;

        [JsonProperty("additionalInfo")]
        public List<PwayRefChildSelf_JournalNote_AdditionalInfo> AdditionalInfo;

        [JsonProperty("formDataId")]
        public int? FormDataId;

        [JsonProperty("workflowState")]
        public PwayRefChildSelf_JournalNote_WorkflowState WorkflowState;

        [JsonProperty("deleted")]
        public bool? Deleted;

        [JsonProperty("locked")]
        public bool? Locked;

        [JsonProperty("formDataStatus")]
        public string FormDataStatus;

        [JsonProperty("_links")]
        public PwayRefChildSelf_JournalNote_Links Links;
    }

}
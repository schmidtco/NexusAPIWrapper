using Newtonsoft.Json; 
using System.Collections.Generic; 
using System; 
namespace NexusAPIWrapper{ 

    public class PwayRefChildSelf_JournalNote_RefObj_Root
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("uid")]
        public string Uid;

        [JsonProperty("pathwayAssociation")]
        public PwayRefChildSelf_JournalNote_RefObj_PathwayAssociation PathwayAssociation;

        [JsonProperty("patient")]
        public PwayRefChildSelf_JournalNote_RefObj_Patient Patient;

        [JsonProperty("formDefinition")]
        public PwayRefChildSelf_JournalNote_RefObj_FormDefinition FormDefinition;

        [JsonProperty("createDate")]
        public DateTime? CreateDate;

        [JsonProperty("updateDate")]
        public DateTime? UpdateDate;

        [JsonProperty("observationTimestamp")]
        public DateTime? ObservationTimestamp;

        [JsonProperty("dueDate")]
        public object DueDate;

        [JsonProperty("dueDateSupported")]
        public bool? DueDateSupported;

        [JsonProperty("lastStateChange")]
        public DateTime? LastStateChange;

        [JsonProperty("workflowState")]
        public PwayRefChildSelf_JournalNote_RefObj_WorkflowState WorkflowState;

        [JsonProperty("items")]
        public List<PwayRefChildSelf_JournalNote_RefObj_Item> Items;

        [JsonProperty("tags")]
        public List<PwayRefChildSelf_JournalNote_RefObj_Tag> Tags;

        [JsonProperty("activityIdentifier")]
        public PwayRefChildSelf_JournalNote_RefObj_ActivityIdentifier ActivityIdentifier;

        [JsonProperty("_links")]
        public PwayRefChildSelf_JournalNote_RefObj_Links Links;
    }

}
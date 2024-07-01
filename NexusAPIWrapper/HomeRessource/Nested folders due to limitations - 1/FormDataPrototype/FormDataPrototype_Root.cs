using Newtonsoft.Json; 
using System.Collections.Generic; 
using System; 
namespace NexusAPIWrapper{ 

    public class FormDataPrototype_Root
    {
        [JsonProperty("id")]
        public object Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("uid")]
        public object Uid;

        [JsonProperty("pathwayAssociation")]
        public FormDataPrototype_PathwayAssociation PathwayAssociation;

        [JsonProperty("patient")]
        public FormDataPrototype_Patient Patient;

        [JsonProperty("formDefinition")]
        public FormDataPrototype_FormDefinition FormDefinition;

        [JsonProperty("createDate")]
        public object CreateDate;

        [JsonProperty("updateDate")]
        public object UpdateDate;

        [JsonProperty("observationTimestamp")]
        public DateTime? ObservationTimestamp;

        [JsonProperty("dueDate")]
        public object DueDate;

        [JsonProperty("dueDateSupported")]
        public bool? DueDateSupported;

        [JsonProperty("lastStateChange")]
        public object LastStateChange;

        [JsonProperty("workflowState")]
        public object WorkflowState;

        [JsonProperty("items")]
        public List<FormDataPrototype_Item> Items;

        [JsonProperty("tags")]
        public List<object> Tags;

        [JsonProperty("activityIdentifier")]
        public FormDataPrototype_ActivityIdentifier ActivityIdentifier;

        [JsonProperty("_links")]
        public FormDataPrototype_Links Links;
    }

}
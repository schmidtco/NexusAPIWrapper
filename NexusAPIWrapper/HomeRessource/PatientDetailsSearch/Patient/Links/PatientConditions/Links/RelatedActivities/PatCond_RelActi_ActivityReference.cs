using Newtonsoft.Json; 
using System.Collections.Generic; 
using System; 
namespace NexusAPIWrapper{ 

    public class PatCond_RelActi_ActivityReference
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("grantIdentifier")]
        public PatCond_RelActi_GrantIdentifier GrantIdentifier;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("date")]
        public DateTime? Date;

        [JsonProperty("children")]
        public List<object> Children;

        [JsonProperty("activityIdentifier")]
        public PatCond_RelActi_ActivityIdentifier ActivityIdentifier;

        [JsonProperty("additionalInfo")]
        public List<PatCond_RelActi_AdditionalInfo> AdditionalInfo;

        [JsonProperty("parentPathwayId")]
        public int? ParentPathwayId;

        [JsonProperty("workflowState")]
        public PatCond_RelActi_WorkflowState WorkflowState;

        [JsonProperty("grantId")]
        public int? GrantId;

        [JsonProperty("catalogId")]
        public int? CatalogId;

        [JsonProperty("_links")]
        public PatCond_RelActi_Links Links;

        [JsonProperty("formDataId")]
        public int? FormDataId;

        [JsonProperty("deleted")]
        public bool? Deleted;

        [JsonProperty("locked")]
        public bool? Locked;

        [JsonProperty("formDataStatus")]
        public string FormDataStatus;
    }

}
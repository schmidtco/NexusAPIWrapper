using Newtonsoft.Json; 
using System.Collections.Generic; 
using System; 
namespace NexusAPIWrapper{ 

    public class PathwayReferencesChildSelf_Root
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("id")]
        public object Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("grantIdentifier")]
        public PathwayReferencesChildSelf_GrantIdentifier GrantIdentifier;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("date")]
        public DateTime? Date;

        [JsonProperty("children")]
        public List<object> Children;

        [JsonProperty("activityIdentifier")]
        public PathwayReferencesChildSelf_ActivityIdentifier ActivityIdentifier;

        [JsonProperty("additionalInfo")]
        public object AdditionalInfo;

        [JsonProperty("workflowState")]
        public PathwayReferencesChildSelf_WorkflowState WorkflowState;

        [JsonProperty("grantId")]
        public int? GrantId;

        [JsonProperty("catalogId")]
        public int? CatalogId;

        [JsonProperty("_links")]
        public PathwayReferencesChildSelf_Links Links;
    }

}
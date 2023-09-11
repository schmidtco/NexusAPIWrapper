using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class OrganizationsTree_Child
    {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("version")]
        public int Version;

        [JsonProperty("uid")]
        public string Uid;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("children")]
        public List<OrganizationsTree_Child> Children;

        [JsonProperty("systemDefined")]
        public bool SystemDefined;

        [JsonProperty("plannedMove")]
        public object PlannedMove;

        [JsonProperty("effectiveStartDate")]
        public string EffectiveStartDate;

        [JsonProperty("effectiveEndDate")]
        public object EffectiveEndDate;

        [JsonProperty("sensitive")]
        public bool Sensitive;

        [JsonProperty("willBeActiveInFuture")]
        public bool WillBeActiveInFuture;

        [JsonProperty("willBeInactiveInFuture")]
        public bool WillBeInactiveInFuture;

        [JsonProperty("active")]
        public bool Active;

        [JsonProperty("syncId")]
        public string SyncId;

        [JsonProperty("_links")]
        public OrganizationsTree_Links Links;
    }

}
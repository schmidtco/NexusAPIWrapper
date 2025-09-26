using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class PatCond_Assign_Type
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("states")]
        public List<object> States;

        [JsonProperty("active")]
        public bool? Active;

        [JsonProperty("deadlineLocked")]
        public bool? DeadlineLocked;

        [JsonProperty("deadlineMandatory")]
        public bool? DeadlineMandatory;

        [JsonProperty("organizationAssigneeLocked")]
        public bool? OrganizationAssigneeLocked;

        [JsonProperty("deadlineUnit")]
        public object DeadlineUnit;

        [JsonProperty("_links")]
        public PatCond_Assign_Links Links;
    }

}
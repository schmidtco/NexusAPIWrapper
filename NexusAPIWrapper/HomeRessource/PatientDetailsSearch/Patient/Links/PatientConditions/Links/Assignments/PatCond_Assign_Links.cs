using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatCond_Assign_Links
    {
        [JsonProperty("self")]
        public PatCond_Assign_Self Self;

        [JsonProperty("assignmentPrototype")]
        public PatCond_Assign_AssignmentPrototype AssignmentPrototype;

        [JsonProperty("bulkAssignmentPrototype")]
        public PatCond_Assign_BulkAssignmentPrototype BulkAssignmentPrototype;
    }

}
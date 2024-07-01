using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class PatientGrantByIdSelf_Root
    {
        [JsonProperty("workflowState")]
        public PatientGrantByIdSelf_WorkflowState WorkflowState;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("identifier")]
        public PatientGrantByIdSelf_Identifier Identifier;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("basketId")]
        public int? BasketId;

        [JsonProperty("uid")]
        public string Uid;

        [JsonProperty("currentElements")]
        public List<PatientGrantByIdSelf_CurrentElement> CurrentElements;

        [JsonProperty("currentWorkflowTransitions")]
        public List<PatientGrantByIdSelf_CurrentWorkflowTransition> CurrentWorkflowTransitions;

        [JsonProperty("patient")]
        public PatientGrantByIdSelf_Patient Patient;

        [JsonProperty("basketGrantId")]
        public int? BasketGrantId;

        [JsonProperty("currentOrderGrantId")]
        public int? CurrentOrderGrantId;

        [JsonProperty("futureOrderGrantId")]
        public object FutureOrderGrantId;

        [JsonProperty("color")]
        public string Color;

        [JsonProperty("bulkIdentifiers")]
        public object BulkIdentifiers;

        [JsonProperty("model")]
        public string Model;

        [JsonProperty("futureElements")]
        public object FutureElements;

        [JsonProperty("futureWorkflowTransitions")]
        public List<object> FutureWorkflowTransitions;

        [JsonProperty("_links")]
        public PatientGrantByIdSelf_Links Links;
    }

}
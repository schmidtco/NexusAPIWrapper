using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class PatientGrantById_Root
    {
        [JsonProperty("workflowState")]
        public PatientGrantById_WorkflowState WorkflowState;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("identifier")]
        public PatientGrantById_Identifier Identifier;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("basketId")]
        public int? BasketId;

        [JsonProperty("uid")]
        public string Uid;

        [JsonProperty("currentElements")]
        public List<PatientGrantById_CurrentElement> CurrentElements;

        [JsonProperty("currentWorkflowTransitions")]
        public List<PatientGrantById_CurrentWorkflowTransition> CurrentWorkflowTransitions;

        [JsonProperty("patient")]
        public PatientGrantById_Patient Patient;

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
        public PatientGrantById_Links Links;
    }

}
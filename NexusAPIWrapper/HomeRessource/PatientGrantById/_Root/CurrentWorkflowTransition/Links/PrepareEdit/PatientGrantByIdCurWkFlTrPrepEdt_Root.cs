using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class PatientGrantByIdCurWkFlTrPrepEdt_Root
    {
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("patientGrantIdentifier")]
        public PatientGrantByIdCurWkFlTrPrepEdt_PatientGrantIdentifier PatientGrantIdentifier;

        [JsonProperty("basketId")]
        public int? BasketId;

        [JsonProperty("patientId")]
        public int? PatientId;

        [JsonProperty("workflowDefinitionResource")]
        public PatientGrantByIdCurWkFlTrPrepEdt_WorkflowDefinitionResource WorkflowDefinitionResource;

        [JsonProperty("model")]
        public string Model;

        [JsonProperty("elements")]
        public List<PatientGrantByIdCurWkFlTrPrepEdt_Element> Elements;

        [JsonProperty("workflowTransitionId")]
        public int? WorkflowTransitionId;

        [JsonProperty("workflowResultState")]
        public PatientGrantByIdCurWkFlTrPrepEdt_WorkflowResultState WorkflowResultState;

        [JsonProperty("editNecessary")]
        public bool? EditNecessary;

        [JsonProperty("isOrdered")]
        public bool? IsOrdered;

        [JsonProperty("previousId")]
        public object PreviousId;

        [JsonProperty("versionLookup")]
        public PatientGrantByIdCurWkFlTrPrepEdt_VersionLookup VersionLookup;

        [JsonProperty("requiredCommandParameters")]
        public List<object> RequiredCommandParameters;

        [JsonProperty("ordered")]
        public bool? Ordered;

        [JsonProperty("_links")]
        public PatientGrantByIdCurWkFlTrPrepEdt_Links Links;
    }

}
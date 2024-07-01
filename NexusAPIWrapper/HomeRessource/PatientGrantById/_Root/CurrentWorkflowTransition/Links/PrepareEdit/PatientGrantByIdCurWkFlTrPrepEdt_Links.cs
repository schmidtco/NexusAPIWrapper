using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientGrantByIdCurWkFlTrPrepEdt_Links
    {
        [JsonProperty("save")]
        public PatientGrantByIdCurWkFlTrPrepEdt_Save Save;

        [JsonProperty("self")]
        public PatientGrantByIdCurWkFlTrPrepEdt_Self Self;

        [JsonProperty("grantCatalog")]
        public PatientGrantByIdCurWkFlTrPrepEdt_GrantCatalog GrantCatalog;

        [JsonProperty("validateElements")]
        public PatientGrantByIdCurWkFlTrPrepEdt_ValidateElements ValidateElements;

        [JsonProperty("availableBaskets")]
        public PatientGrantByIdCurWkFlTrPrepEdt_AvailableBaskets AvailableBaskets;

        [JsonProperty("availableParagraphs")]
        public PatientGrantByIdCurWkFlTrPrepEdt_AvailableParagraphs AvailableParagraphs;

        [JsonProperty("availableSuppliers")]
        public PatientGrantByIdCurWkFlTrPrepEdt_AvailableSuppliers AvailableSuppliers;

        [JsonProperty("refreshOnChange")]
        public PatientGrantByIdCurWkFlTrPrepEdt_RefreshOnChange RefreshOnChange;

        [JsonProperty("availableKLMappings")]
        public PatientGrantByIdCurWkFlTrPrepEdt_AvailableKLMappings AvailableKLMappings;

        [JsonProperty("availableKleNumbers")]
        public PatientGrantByIdCurWkFlTrPrepEdt_AvailableKleNumbers AvailableKleNumbers;

        [JsonProperty("availableActionFacets")]
        public PatientGrantByIdCurWkFlTrPrepEdt_AvailableActionFacets AvailableActionFacets;
    }

}
using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientGrantById_Metadata
    {
        [JsonProperty("disabled")]
        public bool? Disabled;

        [JsonProperty("required")]
        public bool? Required;

        [JsonProperty("hiddenOnCatalog")]
        public bool? HiddenOnCatalog;

        [JsonProperty("hiddenOnPatient")]
        public bool? HiddenOnPatient;

        [JsonProperty("disabledOnPatient")]
        public bool? DisabledOnPatient;
    }

}
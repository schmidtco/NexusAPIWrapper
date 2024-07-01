using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientGrantByIdSelf_Identifier
    {
        [JsonProperty("patientGrantId")]
        public int? PatientGrantId;

        [JsonProperty("catalogGrantId")]
        public object CatalogGrantId;

        [JsonProperty("parentPatientGrantId")]
        public object ParentPatientGrantId;

        [JsonProperty("previousId")]
        public object PreviousId;

        [JsonProperty("basketId")]
        public object BasketId;

        [JsonProperty("index")]
        public object Index;

        [JsonProperty("isPackage")]
        public bool? IsPackage;

        [JsonProperty("packageIdentifier")]
        public object PackageIdentifier;

        [JsonProperty("package")]
        public bool? Package;

        [JsonProperty("_links")]
        public PatientGrantByIdSelf_Links Links;
    }

}
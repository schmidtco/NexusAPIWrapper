using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientGrantByIdSelf_Supplier
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("active")]
        public bool? Active;

        [JsonProperty("cvrNumber")]
        public object CvrNumber;

        [JsonProperty("paragraph")]
        public string Paragraph;

        [JsonProperty("systemType")]
        public string SystemType;

        [JsonProperty("grantOfferPortalSupplierStatus")]
        public string GrantOfferPortalSupplierStatus;

        [JsonProperty("_links")]
        public PatientGrantByIdSelf_Links Links;
    }

}
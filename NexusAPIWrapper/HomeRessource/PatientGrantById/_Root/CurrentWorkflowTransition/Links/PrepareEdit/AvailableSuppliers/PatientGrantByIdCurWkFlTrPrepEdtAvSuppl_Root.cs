using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientGrantByIdCurWkFlTrPrepEdtAvSuppl_Root
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
        public string CvrNumber;

        [JsonProperty("paragraph")]
        public string Paragraph;

        [JsonProperty("systemType")]
        public string SystemType;

        [JsonProperty("grantOfferPortalSupplierStatus")]
        public string GrantOfferPortalSupplierStatus;

        [JsonProperty("_links")]
        public PatientGrantByIdCurWkFlTrPrepEdtAvSuppl_Links Links;
    }

}
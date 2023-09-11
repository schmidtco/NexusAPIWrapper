using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class ProfessionalConfiguration_DefaultOrganizationSupplier
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("id")]
        public int Id;

        [JsonProperty("version")]
        public int Version;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("active")]
        public bool Active;

        [JsonProperty("cvrNumber")]
        public object CvrNumber;

        [JsonProperty("paragraph")]
        public string Paragraph;

        [JsonProperty("organization")]
        public string Organization;

        [JsonProperty("organizationId")]
        public int OrganizationId;

        [JsonProperty("_links")]
        public ProfessionalConfiguration_Links Links;
    }

}
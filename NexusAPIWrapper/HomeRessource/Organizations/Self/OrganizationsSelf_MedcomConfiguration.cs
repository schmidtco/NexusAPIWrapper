using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class OrganizationsSelf_MedcomConfiguration
    {
        [JsonProperty("ean")]
        public object Ean;

        [JsonProperty("site")]
        public object Site;

        [JsonProperty("program")]
        public object Program;

        [JsonProperty("medcomContactType")]
        public object MedcomContactType;

        [JsonProperty("openAncestorsPathwaysIfDefaultPathwayIsClosed")]
        public object OpenAncestorsPathwaysIfDefaultPathwayIsClosed;

        [JsonProperty("_links")]
        public OrganizationsSelf_Links Links;
    }

}
using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientGrantByIdSelf_Paragraph
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("section")]
        public string Section;

        [JsonProperty("description")]
        public string Description;

        [JsonProperty("group")]
        public PatientGrantByIdSelf_Group Group;

        [JsonProperty("medcomGroup")]
        public object MedcomGroup;

        [JsonProperty("hclDstParagraphMapping")]
        public string HclDstParagraphMapping;

        [JsonProperty("paragraphGrantOfferPortalSupplierOfferType")]
        public object ParagraphGrantOfferPortalSupplierOfferType;

        [JsonProperty("active")]
        public bool? Active;

        [JsonProperty("_links")]
        public PatientGrantByIdSelf_Links Links;
    }

}
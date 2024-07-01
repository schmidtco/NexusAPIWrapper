using Newtonsoft.Json; 
using System.Collections.Generic; 
using System; 
namespace NexusAPIWrapper{ 

    public class PatientGrantByIdSelf_CurrentElement
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("metadata")]
        public PatientGrantByIdSelf_Metadata Metadata;

        [JsonProperty("priority")]
        public int? Priority;

        [JsonProperty("elementGrouping")]
        public string ElementGrouping;

        [JsonProperty("paragraph")]
        public PatientGrantByIdSelf_Paragraph Paragraph;

        [JsonProperty("disableable")]
        public bool? Disableable;

        [JsonProperty("disabledOnCatalog")]
        public bool? DisabledOnCatalog;

        [JsonProperty("hideable")]
        public bool? Hideable;

        [JsonProperty("_links")]
        public PatientGrantByIdSelf_Links Links;

        [JsonProperty("selectedValues")]
        public List<string> SelectedValues;

        [JsonProperty("possibleValues")]
        public List<string> PossibleValues;

        [JsonProperty("identifier")]
        public object Identifier;

        [JsonProperty("value")]
        public object Value;

        [JsonProperty("municipality")]
        public object Municipality;

        [JsonProperty("municipalityList")]
        public List<PatientGrantByIdSelf_MunicipalityList> MunicipalityList;

        [JsonProperty("date")]
        public DateTime? Date;

        [JsonProperty("hasCommonValues")]
        public bool? HasCommonValues;

        [JsonProperty("next")]
        public object Next;

        [JsonProperty("count")]
        public object Count;

        [JsonProperty("pattern")]
        public object Pattern;

        [JsonProperty("availablePatterns")]
        public List<string> AvailablePatterns;

        [JsonProperty("paragraphs")]
        public List<object> Paragraphs;

        [JsonProperty("supplier")]
        public PatientGrantByIdSelf_Supplier Supplier;

        [JsonProperty("decimal")]
        public double? Decimal;

        [JsonProperty("text")]
        public object Text;

        [JsonProperty("maxLength")]
        public object MaxLength;

        [JsonProperty("klCatalogGrant")]
        public object KlCatalogGrant;

        [JsonProperty("kleNumber")]
        public PatientGrantByIdSelf_KleNumber KleNumber;

        [JsonProperty("actionFacet")]
        public PatientGrantByIdSelf_ActionFacet ActionFacet;
    }

}
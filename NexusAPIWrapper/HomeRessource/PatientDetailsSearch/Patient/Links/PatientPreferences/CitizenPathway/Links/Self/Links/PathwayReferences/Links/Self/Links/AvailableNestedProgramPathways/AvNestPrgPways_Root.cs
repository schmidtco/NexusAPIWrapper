using Newtonsoft.Json; 
using System; 
namespace NexusAPIWrapper{ 

    public class AvNestPrgPways_Root
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("uid")]
        public string Uid;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("active")]
        public bool? Active;

        [JsonProperty("createDate")]
        public DateTime? CreateDate;

        [JsonProperty("activatedDate")]
        public DateTime? ActivatedDate;

        [JsonProperty("inactivatedDate")]
        public object InactivatedDate;

        [JsonProperty("pathwayType")]
        public AvNestPrgPways_PathwayType PathwayType;

        [JsonProperty("sensitiveData")]
        public bool? SensitiveData;

        [JsonProperty("uniqueForPatient")]
        public bool? UniqueForPatient;

        [JsonProperty("kleNumber")]
        public AvNestPrgPways_KleNumber KleNumber;

        [JsonProperty("actionFacet")]
        public AvNestPrgPways_ActionFacet ActionFacet;

        [JsonProperty("_links")]
        public AvNestPrgPways_Links Links;
    }

}
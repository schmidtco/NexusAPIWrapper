using Newtonsoft.Json; 
using System; 
namespace NexusAPIWrapper{ 

    public class AvailableProgramPathways_Root
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
        public AvailableProgramPathways_PathwayType PathwayType;

        [JsonProperty("sensitiveData")]
        public bool? SensitiveData;

        [JsonProperty("uniqueForPatient")]
        public bool? UniqueForPatient;

        [JsonProperty("kleNumber")]
        public AvailableProgramPathways_KleNumber KleNumber;

        [JsonProperty("actionFacet")]
        public AvailableProgramPathways_ActionFacet ActionFacet;

        [JsonProperty("_links")]
        public AvailableProgramPathways_Links Links;
    }

}
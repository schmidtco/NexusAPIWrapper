using Newtonsoft.Json; 
using System; 
namespace NexusAPIWrapper{ 

    public class ActivePrograms_Root
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
        public DateTime? InactivatedDate;

        [JsonProperty("pathwayType")]
        public ActivePrograms_PathwayType PathwayType;

        [JsonProperty("sensitiveData")]
        public bool? SensitiveData;

        [JsonProperty("uniqueForPatient")]
        public bool? UniqueForPatient;

        [JsonProperty("kleNumber")]
        public ActivePrograms_KleNumber KleNumber;

        [JsonProperty("actionFacet")]
        public ActivePrograms_ActionFacet ActionFacet;

        [JsonProperty("_links")]
        public ActivePrograms_Links Links;
    }

}
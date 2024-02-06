using Newtonsoft.Json; 
using System.Collections.Generic; 
using System; 
namespace NexusAPIWrapper{ 

    public class PathwayReferencesSelf_Root
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("id")]
        public object Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("date")]
        public DateTime? Date;

        [JsonProperty("children")]
        public List<object> Children;

        [JsonProperty("activityIdentifier")]
        public PathwayReferencesSelf_ActivityIdentifier ActivityIdentifier;

        [JsonProperty("additionalInfo")]
        public object AdditionalInfo;

        [JsonProperty("patientPathwayId")]
        public int? PatientPathwayId;

        [JsonProperty("programPathwayId")]
        public int? ProgramPathwayId;

        [JsonProperty("pathwayTypeId")]
        public int? PathwayTypeId;

        [JsonProperty("parentPathwayId")]
        public object ParentPathwayId;

        [JsonProperty("pathwayStatus")]
        public string PathwayStatus;

        [JsonProperty("_links")]
        public PathwayReferencesSelf_Links Links;
    }

}
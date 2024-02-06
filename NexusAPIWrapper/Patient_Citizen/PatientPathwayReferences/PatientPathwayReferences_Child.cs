using Newtonsoft.Json; 
using System.Collections.Generic; 
using System; 
namespace NexusAPIWrapper{ 

    public class PatientPathwayReferences_Child
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("id")]
        public int Id;

        [JsonProperty("version")]
        public int Version;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("date")]
        public DateTime Date;

        [JsonProperty("children")]
        public List<PatientPathwayReferences_Child> Children;

        [JsonProperty("activityIdentifier")]
        public PatientPathwayReferences_ActivityIdentifier ActivityIdentifier;

        [JsonProperty("additionalInfo")]
        public object AdditionalInfo;

        [JsonProperty("patientPathwayId")]
        public int PatientPathwayId;

        [JsonProperty("programPathwayId")]
        public int ProgramPathwayId;

        [JsonProperty("pathwayTypeId")]
        public int PathwayTypeId;

        [JsonProperty("parentPathwayId")]
        public object ParentPathwayId;

        [JsonProperty("pathwayStatus")]
        public string PathwayStatus;

        [JsonProperty("_links")]
        public PatientPathwayReferences_Links Links;

        [JsonProperty("status")]
        public string Status;

        [JsonProperty("documentId")]
        public int DocumentId;
    }

}
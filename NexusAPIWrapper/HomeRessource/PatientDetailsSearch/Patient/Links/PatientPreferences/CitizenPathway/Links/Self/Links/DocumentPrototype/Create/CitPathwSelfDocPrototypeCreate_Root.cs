using Newtonsoft.Json; 
using System.Collections.Generic; 
using System; 
namespace NexusAPIWrapper{ 

    public class CitPathwSelfDocPrototypeCreate_Root
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("uid")]
        public string Uid;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("notes")]
        public object Notes;

        [JsonProperty("originalFileName")]
        public string OriginalFileName;

        [JsonProperty("uploadingDate")]
        public object UploadingDate;

        [JsonProperty("relevanceDate")]
        public DateTime? RelevanceDate;

        [JsonProperty("status")]
        public string Status;

        [JsonProperty("pathwayAssociation")]
        public CitPathwSelfDocPrototypeCreate_PathwayAssociation PathwayAssociation;

        [JsonProperty("patientId")]
        public int? PatientId;

        [JsonProperty("fileType")]
        public string FileType;

        [JsonProperty("tags")]
        public List<object> Tags;

        [JsonProperty("origin")]
        public object Origin;

        [JsonProperty("externalId")]
        public object ExternalId;

        [JsonProperty("fileExternalId")]
        public object FileExternalId;

        [JsonProperty("_links")]
        public CitPathwSelfDocPrototypeCreate_Links Links;

        [JsonProperty("patientActivityType")]
        public string PatientActivityType;
    }

}
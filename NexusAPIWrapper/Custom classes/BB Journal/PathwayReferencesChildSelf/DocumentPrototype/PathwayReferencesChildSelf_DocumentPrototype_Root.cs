using Newtonsoft.Json; 
using System.Collections.Generic; 
using System; 
namespace NexusAPIWrapper{ 

    public class PathwayReferencesChildSelf_DocumentPrototype_Root
    {
        [JsonProperty("id")]
        public object Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("uid")]
        public string Uid;

        [JsonProperty("name")]
        public object Name;

        [JsonProperty("notes")]
        public object Notes;

        [JsonProperty("originalFileName")]
        public object OriginalFileName;

        [JsonProperty("uploadingDate")]
        public object UploadingDate;

        [JsonProperty("relevanceDate")]
        public DateTime? RelevanceDate;

        [JsonProperty("status")]
        public string Status;

        [JsonProperty("pathwayAssociation")]
        public PathwayReferencesChildSelf_DocumentPrototype_PathwayAssociation PathwayAssociation;

        [JsonProperty("patientId")]
        public int? PatientId;

        [JsonProperty("fileType")]
        public object FileType;

        [JsonProperty("tags")]
        public List<object> Tags;

        [JsonProperty("origin")]
        public object Origin;

        [JsonProperty("externalId")]
        public object ExternalId;

        [JsonProperty("fileExternalId")]
        public object FileExternalId;

        [JsonProperty("_links")]
        public PathwayReferencesChildSelf_DocumentPrototype_Links Links;

        [JsonProperty("patientActivityType")]
        public string PatientActivityType;
    }

}
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class CreateDocumentObject
    {
        [JsonProperty("id")]
        public object Id { get; set; }

        [JsonProperty("version")]
        public int Version { get; set; }

        [JsonProperty("uid")]
        public string Uid { get; set; }

        [JsonProperty("name")]
        public object Name { get; set; }

        [JsonProperty("notes")]
        public object Notes { get; set; }

        [JsonProperty("originalFileName")]
        public object OriginalFileName { get; set; }

        [JsonProperty("uploadingDate")]
        public object UploadingDate { get; set; }

        [JsonProperty("relevanceDate")]
        public DateTime RelevanceDate { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("pathwayAssociation")]
        public PathwayAssociation PathwayAssociation { get; set; }

        [JsonProperty("patientId")]
        public int PatientId { get; set; }

        [JsonProperty("fileType")]
        public object FileType { get; set; }

        [JsonProperty("tags")]
        public List<object> Tags { get; set; }

        [JsonProperty("origin")]
        public object Origin { get; set; }

        [JsonProperty("externalId")]
        public object ExternalId { get; set; }

        [JsonProperty("fileExternalId")]
        public object FileExternalId { get; set; }

        [JsonProperty("_links")]
        public Links Links { get; set; }

        [JsonProperty("patientActivityType")]
        public string PatientActivityType { get; set; }
    }

using Newtonsoft.Json;

public class PathwayAssociation
    {
        [JsonProperty("placement")]
        public Placement Placement { get; set; }

        [JsonProperty("parentReferenceId")]
        public object ParentReferenceId { get; set; }

        [JsonProperty("referenceId")]
        public object ReferenceId { get; set; }

        [JsonProperty("canAssociateWithPathway")]
        public bool CanAssociateWithPathway { get; set; }

        [JsonProperty("canAssociateWithPatient")]
        public bool CanAssociateWithPatient { get; set; }

        [JsonProperty("associatedWithPatient")]
        public bool AssociatedWithPatient { get; set; }

        [JsonProperty("_links")]
        public Links Links { get; set; }
    }

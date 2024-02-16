using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class Patient_DocumentPrototype_PathwayAssociation
    {
        [JsonProperty("placement")]
        public object Placement;

        [JsonProperty("parentReferenceId")]
        public object ParentReferenceId;

        [JsonProperty("referenceId")]
        public object ReferenceId;

        [JsonProperty("canAssociateWithPathway")]
        public bool? CanAssociateWithPathway;

        [JsonProperty("canAssociateWithPatient")]
        public bool? CanAssociateWithPatient;

        [JsonProperty("associatedWithPatient")]
        public bool? AssociatedWithPatient;

        [JsonProperty("_links")]
        public Patient_DocumentPrototype_Links Links;
    }

}
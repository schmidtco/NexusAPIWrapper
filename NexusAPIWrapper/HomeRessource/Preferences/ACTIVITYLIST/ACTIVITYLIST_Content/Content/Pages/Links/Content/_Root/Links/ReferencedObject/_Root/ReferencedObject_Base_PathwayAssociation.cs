using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class ReferencedObject_Base_PathwayAssociation
    {
        [JsonProperty("placement")]
        public ReferencedObject_Base_Placement Placement;

        [JsonProperty("parentReferenceId")]
        public object ParentReferenceId;

        [JsonProperty("referenceId")]
        public int? ReferenceId;

        [JsonProperty("canAssociateWithPathway")]
        public bool? CanAssociateWithPathway;

        [JsonProperty("canAssociateWithPatient")]
        public bool? CanAssociateWithPatient;

        [JsonProperty("associatedWithPatient")]
        public bool? AssociatedWithPatient;

        [JsonProperty("_links")]
        public ReferencedObject_Base_Links Links;
    }

}
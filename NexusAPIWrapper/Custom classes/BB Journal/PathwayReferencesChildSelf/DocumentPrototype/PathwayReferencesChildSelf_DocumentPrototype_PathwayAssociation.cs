using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PathwayReferencesChildSelf_DocumentPrototype_PathwayAssociation
    {
        [JsonProperty("placement")]
        public PathwayReferencesChildSelf_DocumentPrototype_Placement Placement;

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
        public PathwayReferencesChildSelf_DocumentPrototype_Links Links;
    }

}
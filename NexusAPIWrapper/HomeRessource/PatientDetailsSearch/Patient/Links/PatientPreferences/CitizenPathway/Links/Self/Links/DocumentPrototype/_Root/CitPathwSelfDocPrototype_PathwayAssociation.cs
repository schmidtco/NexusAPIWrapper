using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class CitPathwSelfDocPrototype_PathwayAssociation
    {
        [JsonProperty("placement")]
        public CitPathwSelfDocPrototype_Placement Placement;

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
        public CitPathwSelfDocPrototype_Links Links;
    }

}
using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PwayRefChildSelf_JournalNote_RefObj_PathwayAssociation
    {
        [JsonProperty("placement")]
        public PwayRefChildSelf_JournalNote_RefObj_Placement Placement;

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
        public PwayRefChildSelf_JournalNote_RefObj_Links Links;
    }

}
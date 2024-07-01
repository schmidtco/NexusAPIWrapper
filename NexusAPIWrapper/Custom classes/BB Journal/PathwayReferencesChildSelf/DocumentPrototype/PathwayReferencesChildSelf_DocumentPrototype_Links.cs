using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PathwayReferencesChildSelf_DocumentPrototype_Links
    {
        [JsonProperty("patientPathway")]
        public PathwayReferencesChildSelf_DocumentPrototype_PatientPathway PatientPathway;

        [JsonProperty("availableRootProgramPathways")]
        public PathwayReferencesChildSelf_DocumentPrototype_AvailableRootProgramPathways AvailableRootProgramPathways;

        [JsonProperty("availablePathwayAssociation")]
        public PathwayReferencesChildSelf_DocumentPrototype_AvailablePathwayAssociation AvailablePathwayAssociation;

        [JsonProperty("create")]
        public PathwayReferencesChildSelf_DocumentPrototype_Create Create;

        [JsonProperty("availableTags")]
        public PathwayReferencesChildSelf_DocumentPrototype_AvailableTags AvailableTags;
    }

}
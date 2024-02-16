using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class CitPathwSelfDocPrototype_Links
    {
        [JsonProperty("patientPathway")]
        public CitPathwSelfDocPrototype_PatientPathway PatientPathway;

        [JsonProperty("availableRootProgramPathways")]
        public CitPathwSelfDocPrototype_AvailableRootProgramPathways AvailableRootProgramPathways;

        [JsonProperty("availablePathwayAssociation")]
        public CitPathwSelfDocPrototype_AvailablePathwayAssociation AvailablePathwayAssociation;

        [JsonProperty("create")]
        public CitPathwSelfDocPrototype_Create Create;

        [JsonProperty("availableTags")]
        public CitPathwSelfDocPrototype_AvailableTags AvailableTags;
    }

}
using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class Patient_DocumentPrototype_Links
    {
        [JsonProperty("availableRootProgramPathways")]
        public Patient_DocumentPrototype_AvailableRootProgramPathways AvailableRootProgramPathways;

        [JsonProperty("availablePathwayAssociation")]
        public Patient_DocumentPrototype_AvailablePathwayAssociation AvailablePathwayAssociation;

        [JsonProperty("create")]
        public Patient_DocumentPrototype_Create Create;

        [JsonProperty("availableTags")]
        public Patient_DocumentPrototype_AvailableTags AvailableTags;
    }

}
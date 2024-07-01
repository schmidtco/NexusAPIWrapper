using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class CitPathwSelfDocPrototypeCreate_Links
    {
        [JsonProperty("patientPathway")]
        public CitPathwSelfDocPrototypeCreate_PatientPathway PatientPathway;

        [JsonProperty("availableRootProgramPathways")]
        public CitPathwSelfDocPrototypeCreate_AvailableRootProgramPathways AvailableRootProgramPathways;

        [JsonProperty("availablePathwayAssociation")]
        public CitPathwSelfDocPrototypeCreate_AvailablePathwayAssociation AvailablePathwayAssociation;

        [JsonProperty("self")]
        public CitPathwSelfDocPrototypeCreate_Self Self;

        [JsonProperty("availableTags")]
        public CitPathwSelfDocPrototypeCreate_AvailableTags AvailableTags;

        [JsonProperty("audit")]
        public PatientGrantById_Audit Audit;

        [JsonProperty("update")]
        public CitPathwSelfDocPrototypeCreate_Update Update;

        [JsonProperty("delete")]
        public CitPathwSelfDocPrototypeCreate_Delete Delete;

        [JsonProperty("upload")]
        public CitPathwSelfDocPrototypeCreate_Upload Upload;

        [JsonProperty("documentMessagePrototype")]
        public CitPathwSelfDocPrototypeCreate_DocumentMessagePrototype DocumentMessagePrototype;
    }

}
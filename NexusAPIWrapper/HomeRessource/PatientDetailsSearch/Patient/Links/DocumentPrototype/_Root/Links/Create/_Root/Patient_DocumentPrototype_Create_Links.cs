using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class Patient_DocumentPrototype_Create_Links
    {
        [JsonProperty("availableRootProgramPathways")]
        public Patient_DocumentPrototype_Create_AvailableRootProgramPathways AvailableRootProgramPathways;

        [JsonProperty("availablePathwayAssociation")]
        public Patient_DocumentPrototype_Create_AvailablePathwayAssociation AvailablePathwayAssociation;

        [JsonProperty("self")]
        public Patient_DocumentPrototype_Create_Self Self;

        [JsonProperty("availableTags")]
        public Patient_DocumentPrototype_Create_AvailableTags AvailableTags;

        [JsonProperty("audit")]
        public Patient_DocumentPrototype_Create_Audit Audit;

        [JsonProperty("update")]
        public Patient_DocumentPrototype_Create_Update Update;

        [JsonProperty("delete")]
        public Patient_DocumentPrototype_Create_Delete Delete;

        [JsonProperty("upload")]
        public Patient_DocumentPrototype_Create_Upload Upload;

        [JsonProperty("documentMessagePrototype")]
        public Patient_DocumentPrototype_Create_DocumentMessagePrototype DocumentMessagePrototype;
    }

}
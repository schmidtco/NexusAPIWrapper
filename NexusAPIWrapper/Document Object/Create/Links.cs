using Newtonsoft.Json;

public class Links
    {
        [JsonProperty("patientPathway")]
        public PatientPathway PatientPathway { get; set; }

        [JsonProperty("availableRootProgramPathways")]
        public AvailableRootProgramPathways AvailableRootProgramPathways { get; set; }

        [JsonProperty("availablePathwayAssociation")]
        public AvailablePathwayAssociation AvailablePathwayAssociation { get; set; }

        [JsonProperty("create")]
        public Create Create { get; set; }

        [JsonProperty("availableTags")]
        public AvailableTags AvailableTags { get; set; }
    }

using Newtonsoft.Json;

public class Placement
    {
        [JsonProperty("id")]
        public object Id { get; set; }

        [JsonProperty("version")]
        public object Version { get; set; }

        [JsonProperty("patientPathwayId")]
        public int PatientPathwayId { get; set; }

        [JsonProperty("programPathwayId")]
        public int ProgramPathwayId { get; set; }

        [JsonProperty("_links")]
        public Links Links { get; set; }
    }

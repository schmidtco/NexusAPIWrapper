using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class CitPathwSelfDocPrototype_Placement
    {
        [JsonProperty("id")]
        public object Id;

        [JsonProperty("version")]
        public object Version;

        [JsonProperty("patientPathwayId")]
        public int? PatientPathwayId;

        [JsonProperty("programPathwayId")]
        public int? ProgramPathwayId;

        [JsonProperty("_links")]
        public CitPathwSelfDocPrototype_Links Links;
    }

}
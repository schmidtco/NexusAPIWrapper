using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PwayRefChildSelf_JournalNote_RefObj_Placement
    {
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("programPathwayId")]
        public int? ProgramPathwayId;

        [JsonProperty("pathwayTypeId")]
        public int? PathwayTypeId;

        [JsonProperty("parentPathwayId")]
        public object ParentPathwayId;

        [JsonProperty("patientPathwayId")]
        public int? PatientPathwayId;

        [JsonProperty("_links")]
        public PwayRefChildSelf_JournalNote_RefObj_Links Links;
    }

}
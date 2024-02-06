using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class ReferencedObject_Base_Placement
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
        public ReferencedObject_Base_Links Links;
    }

}
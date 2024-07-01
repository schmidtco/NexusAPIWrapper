using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientGrantByIdCurWkFlTrPrepEdt_Group
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("nspName")]
        public object NspName;

        [JsonProperty("visitationType")]
        public PatientGrantByIdCurWkFlTrPrepEdt_VisitationType VisitationType;

        [JsonProperty("_links")]
        public PatientGrantByIdCurWkFlTrPrepEdt_Links Links;
    }

}
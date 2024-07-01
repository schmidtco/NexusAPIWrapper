using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientGrantByIdCurWkFlTrPrepEdt_VisitationType
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("_links")]
        public PatientGrantByIdCurWkFlTrPrepEdt_Links Links;
    }

}
using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientGrantById_VisitationType
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("_links")]
        public PatientGrantById_Links Links;
    }

}
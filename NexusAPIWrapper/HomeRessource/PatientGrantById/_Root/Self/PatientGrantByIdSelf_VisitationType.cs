using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientGrantByIdSelf_VisitationType
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("_links")]
        public PatientGrantByIdSelf_Links Links;
    }

}
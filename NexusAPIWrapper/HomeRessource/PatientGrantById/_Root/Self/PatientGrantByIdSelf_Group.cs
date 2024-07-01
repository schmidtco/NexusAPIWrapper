using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientGrantByIdSelf_Group
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("nspName")]
        public object NspName;

        [JsonProperty("visitationType")]
        public PatientGrantByIdSelf_VisitationType VisitationType;

        [JsonProperty("_links")]
        public PatientGrantByIdSelf_Links Links;
    }

}
using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientGrantById_Group
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("nspName")]
        public object NspName;

        [JsonProperty("visitationType")]
        public PatientGrantById_VisitationType VisitationType;

        [JsonProperty("_links")]
        public PatientGrantById_Links Links;
    }

}
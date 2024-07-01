using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientGrantByIdSelf_ActionFacet
    {
        [JsonProperty("uid")]
        public string Uid;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("description")]
        public string Description;

        [JsonProperty("_links")]
        public PatientGrantByIdSelf_Links Links;
    }

}
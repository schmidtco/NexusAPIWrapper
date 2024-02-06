using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class CitizenDataSelf_ViewType
    {
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("subtype")]
        public string Subtype;
    }

}
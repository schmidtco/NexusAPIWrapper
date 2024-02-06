using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class CitizenDashboardSelf_ViewType
    {
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("subtype")]
        public string Subtype;
    }

}
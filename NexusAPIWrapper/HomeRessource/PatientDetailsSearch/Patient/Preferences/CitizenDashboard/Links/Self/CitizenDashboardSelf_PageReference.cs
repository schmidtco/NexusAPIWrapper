using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class CitizenDashboardSelf_PageReference
    {
        [JsonProperty("targetType")]
        public string TargetType;

        [JsonProperty("pageType")]
        public string PageType;

        [JsonProperty("preference")]
        public CitizenDashboardSelf_Preference Preference;

        [JsonProperty("_links")]
        public CitizenDashboardSelf_Links Links;
    }

}
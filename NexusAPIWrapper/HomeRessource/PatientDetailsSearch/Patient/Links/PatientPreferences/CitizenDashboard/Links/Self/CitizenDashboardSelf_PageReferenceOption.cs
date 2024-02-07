using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class CitizenDashboardSelf_PageReferenceOption
    {
        [JsonProperty("pageReference")]
        public CitizenDashboardSelf_PageReference PageReference;

        [JsonProperty("openInNewTab")]
        public bool? OpenInNewTab;
    }

}
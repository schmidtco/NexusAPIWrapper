using Newtonsoft.Json; 
using System; 
namespace NexusAPIWrapper{ 

    public class CitizenDashboardSelf_Filter
    {
        [JsonProperty("from")]
        public object From;

        [JsonProperty("to")]
        public DateTime? To;

        [JsonProperty("_links")]
        public CitizenDashboardSelf_Links Links;
    }

}
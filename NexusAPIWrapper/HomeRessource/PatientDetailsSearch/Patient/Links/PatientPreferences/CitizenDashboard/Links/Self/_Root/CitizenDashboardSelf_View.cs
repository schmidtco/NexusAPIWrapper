using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class CitizenDashboardSelf_View
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("widgets")]
        public List<CitizenDashboardSelf_Widget> Widgets;

        [JsonProperty("_links")]
        public CitizenDashboardSelf_Links Links;
    }

}
using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class CitizenDashboardCitizenConditionSelf_View
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("widgets")]
        public List<CitizenDashboardCitizenConditionSelf_Widget> Widgets;

        [JsonProperty("_links")]
        public CitizenDashboardCitizenConditionSelf_Links Links;
    }

}
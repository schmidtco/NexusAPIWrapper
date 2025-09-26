using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class CitizenDashboardCitizenConditionSelf_Widget
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("headerColor")]
        public string HeaderColor;

        [JsonProperty("headerTitle")]
        public string HeaderTitle;

        [JsonProperty("widgetGroup")]
        public object WidgetGroup;

        [JsonProperty("pageReferenceOption")]
        public object PageReferenceOption;

        [JsonProperty("layoutMode")]
        public string LayoutMode;

        [JsonProperty("supportedActions")]
        public List<string> SupportedActions;

        [JsonProperty("quickFilter")]
        public CitizenDashboardCitizenConditionSelf_QuickFilter QuickFilter;

        [JsonProperty("_links")]
        public CitizenDashboardCitizenConditionSelf_Links Links;
    }

}
using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class CitizenDashboardSelf_Widget
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
        public CitizenDashboardSelf_PageReferenceOption PageReferenceOption;

        [JsonProperty("isHiddenTitle")]
        public bool? IsHiddenTitle;

        [JsonProperty("pathwayLayoutMode")]
        public string PathwayLayoutMode;

        [JsonProperty("viewPreference")]
        public CitizenDashboardSelf_ViewPreference ViewPreference;

        [JsonProperty("supportedActions")]
        public List<string> SupportedActions;

        [JsonProperty("creatableObjects")]
        public CitizenDashboardSelf_CreatableObjects CreatableObjects;

        [JsonProperty("activityLinkType")]
        public object ActivityLinkType;

        [JsonProperty("hiddenTitle")]
        public bool? HiddenTitle;

        [JsonProperty("_links")]
        public CitizenDashboardSelf_Links Links;

        [JsonProperty("programPathwayId")]
        public object ProgramPathwayId;

        [JsonProperty("placement")]
        public string Placement;

        [JsonProperty("viewMode")]
        public object ViewMode;

        [JsonProperty("pageSize")]
        public object PageSize;

        [JsonProperty("quickFilterOptions")]
        public List<object> QuickFilterOptions;
    }

}
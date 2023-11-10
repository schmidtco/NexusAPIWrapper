using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class CitizenDashboardSelf_ViewPreference
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("creatableObjects")]
        public object CreatableObjects;

        [JsonProperty("expandGroups")]
        public List<object> ExpandGroups;

        [JsonProperty("relatedObjects")]
        public object RelatedObjects;

        [JsonProperty("hideTitle")]
        public bool? HideTitle;

        [JsonProperty("actionsVisibility")]
        public CitizenDashboardSelf_ActionsVisibility ActionsVisibility;

        [JsonProperty("_links")]
        public CitizenDashboardSelf_Links Links;

        [JsonProperty("viewType")]
        public CitizenDashboardSelf_ViewType ViewType;

        [JsonProperty("filter")]
        public CitizenDashboardSelf_Filter Filter;

        [JsonProperty("possibleActions")]
        public List<object> PossibleActions;
    }

}
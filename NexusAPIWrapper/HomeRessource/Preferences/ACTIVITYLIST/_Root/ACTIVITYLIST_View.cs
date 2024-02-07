using Newtonsoft.Json; 
using System; 
namespace NexusAPIWrapper{ 

    public class ACTIVITYLIST_View
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("viewType")]
        public ACTIVITYLIST_ViewType ViewType;

        [JsonProperty("containsForms")]
        public bool? ContainsForms;

        [JsonProperty("selectedOrganizationsType")]
        public string SelectedOrganizationsType;

        [JsonProperty("selectedOrganizationAssignee")]
        public object SelectedOrganizationAssignee;

        [JsonProperty("selectedProfessionalAssignee")]
        public object SelectedProfessionalAssignee;

        [JsonProperty("sortingColumn")]
        public string SortingColumn;

        [JsonProperty("sortingDirection")]
        public string SortingDirection;

        [JsonProperty("from")]
        public DateTime? From;

        [JsonProperty("to")]
        public DateTime? To;

        [JsonProperty("_links")]
        public ACTIVITYLIST_Links Links;
    }

}
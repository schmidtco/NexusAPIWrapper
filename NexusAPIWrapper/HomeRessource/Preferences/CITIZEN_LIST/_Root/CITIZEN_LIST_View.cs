using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class CITIZEN_LIST_View
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("selectedOrganizationsType")]
        public string SelectedOrganizationsType;

        [JsonProperty("selectedProfessionalsType")]
        public string SelectedProfessionalsType;

        [JsonProperty("sorting")]
        public CITIZEN_LIST_Sorting Sorting;

        [JsonProperty("viewType")]
        public string ViewType;

        [JsonProperty("showBulkOperations")]
        public bool? ShowBulkOperations;

        [JsonProperty("possibleActions")]
        public List<string> PossibleActions;

        [JsonProperty("allowedActions")]
        public List<object> AllowedActions;

        [JsonProperty("_links")]
        public CITIZEN_LIST_Links Links;
    }

}
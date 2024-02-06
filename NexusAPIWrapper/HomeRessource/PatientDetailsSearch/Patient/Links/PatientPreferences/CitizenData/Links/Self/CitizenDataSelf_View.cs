using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class CitizenDataSelf_View
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("viewType")]
        public CitizenDataSelf_ViewType ViewType;

        [JsonProperty("filter")]
        public CitizenDataSelf_Filter Filter;

        [JsonProperty("creatableObjects")]
        public CitizenDataSelf_CreatableObjects CreatableObjects;

        [JsonProperty("possibleActions")]
        public List<object> PossibleActions;

        [JsonProperty("_links")]
        public CitizenDataSelf_Links Links;
    }

}
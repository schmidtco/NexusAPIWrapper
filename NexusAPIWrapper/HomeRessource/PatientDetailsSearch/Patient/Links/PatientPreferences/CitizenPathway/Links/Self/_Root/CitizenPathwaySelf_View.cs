using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class CitizenPathwaySelf_View
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("creatableObjects")]
        public List<CitizenPathwaySelf_CreatableObject> CreatableObjects;

        [JsonProperty("expandGroups")]
        public List<object> ExpandGroups;

        [JsonProperty("relatedObjects")]
        public object RelatedObjects;

        [JsonProperty("hideTitle")]
        public bool? HideTitle;

        [JsonProperty("actionsVisibility")]
        public CitizenPathwaySelf_ActionsVisibility ActionsVisibility;

        [JsonProperty("_links")]
        public CitizenPathwaySelf_Links Links;
    }

}
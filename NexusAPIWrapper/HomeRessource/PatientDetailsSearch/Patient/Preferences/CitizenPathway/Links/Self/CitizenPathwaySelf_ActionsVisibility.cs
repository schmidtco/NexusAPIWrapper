using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class CitizenPathwaySelf_ActionsVisibility
    {
        [JsonProperty("activities")]
        public string Activities;

        [JsonProperty("possibleActivities")]
        public List<string> PossibleActivities;

        [JsonProperty("_links")]
        public CitizenPathwaySelf_Links Links;
    }

}
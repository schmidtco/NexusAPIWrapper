using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class PatCond_RelActiWHist_Root
    {
        [JsonProperty("groupName")]
        public string GroupName;

        [JsonProperty("citizenActivitiesGroups")]
        public List<PatCond_RelActiWHist_CitizenActivitiesGroup> CitizenActivitiesGroups;

        [JsonProperty("linkType")]
        public PatCond_RelActiWHist_LinkType LinkType;

        [JsonProperty("_links")]
        public PatCond_RelActiWHist_Links Links;
    }

}
using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class PatCond_RelActi_Root
    {
        [JsonProperty("groupName")]
        public string GroupName;

        [JsonProperty("citizenActivitiesGroups")]
        public List<PatCond_RelActi_CitizenActivitiesGroup> CitizenActivitiesGroups;

        [JsonProperty("linkType")]
        public PatCond_RelActi_LinkType LinkType;

        [JsonProperty("_links")]
        public PatCond_RelActi_Links Links;
    }

}
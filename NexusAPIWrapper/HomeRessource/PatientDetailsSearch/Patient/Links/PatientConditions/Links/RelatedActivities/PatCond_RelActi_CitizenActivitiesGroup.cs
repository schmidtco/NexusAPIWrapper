using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class PatCond_RelActi_CitizenActivitiesGroup
    {
        [JsonProperty("patient")]
        public PatCond_RelActi_Patient Patient;

        [JsonProperty("activities")]
        public List<PatCond_RelActi_Activity> Activities;

        [JsonProperty("_links")]
        public PatCond_RelActi_Links Links;
    }

}
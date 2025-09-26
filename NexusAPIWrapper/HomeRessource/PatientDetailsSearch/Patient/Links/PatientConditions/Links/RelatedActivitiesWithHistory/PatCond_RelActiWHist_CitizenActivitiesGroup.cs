using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class PatCond_RelActiWHist_CitizenActivitiesGroup
    {
        [JsonProperty("patient")]
        public PatCond_RelActiWHist_Patient Patient;

        [JsonProperty("activities")]
        public List<PatCond_RelActiWHist_Activity> Activities;

        [JsonProperty("_links")]
        public PatCond_RelActiWHist_Links Links;
    }

}
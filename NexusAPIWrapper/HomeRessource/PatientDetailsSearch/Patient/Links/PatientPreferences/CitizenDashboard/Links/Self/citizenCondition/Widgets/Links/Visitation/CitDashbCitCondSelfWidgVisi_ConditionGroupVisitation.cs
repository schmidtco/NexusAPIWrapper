using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class CitDashbCitCondSelfWidgVisi_ConditionGroupVisitation
    {
        [JsonProperty("conditionGroup")]
        public CitDashbCitCondSelfWidgVisi_ConditionGroup ConditionGroup;

        [JsonProperty("conditions")]
        public List<CitDashbCitCondSelfWidgVisi_Condition> Conditions;

        [JsonProperty("_links")]
        public CitDashbCitCondSelfWidgVisi_Links Links;
    }

}
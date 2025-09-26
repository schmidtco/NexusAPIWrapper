using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    /// <summary>
    /// This is the "edit" button, calling the prototype link
    /// </summary>
    public class CitDashbCitCondSelfWidgVisi_Root
    {
        [JsonProperty("law")]
        public CitDashbCitCondSelfWidgVisi_Law Law;

        [JsonProperty("conditionGroupVisitation")]
        public List<CitDashbCitCondSelfWidgVisi_ConditionGroupVisitation> ConditionGroupVisitation;

        [JsonProperty("_links")]
        public CitDashbCitCondSelfWidgVisi_Links Links;
    }

}
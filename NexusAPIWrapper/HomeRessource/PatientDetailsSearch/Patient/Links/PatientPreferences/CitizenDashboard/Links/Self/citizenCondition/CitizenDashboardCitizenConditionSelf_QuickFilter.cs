using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class CitizenDashboardCitizenConditionSelf_QuickFilter
    {
        [JsonProperty("statuses")]
        public List<string> Statuses;

        [JsonProperty("laws")]
        public List<CitizenDashboardCitizenConditionSelf_Law> Laws;

        [JsonProperty("fields")]
        public object Fields;

        [JsonProperty("possibleStatuses")]
        public List<string> PossibleStatuses;

        [JsonProperty("possibleLaws")]
        public List<CitizenDashboardCitizenConditionSelf_PossibleLaw> PossibleLaws;

        [JsonProperty("possibleFields")]
        public List<string> PossibleFields;

        [JsonProperty("_links")]
        public CitizenDashboardCitizenConditionSelf_Links Links;
    }

}
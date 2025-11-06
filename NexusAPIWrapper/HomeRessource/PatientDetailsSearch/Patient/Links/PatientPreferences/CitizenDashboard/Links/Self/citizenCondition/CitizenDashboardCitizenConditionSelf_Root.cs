using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class CitizenDashboardCitizenConditionSelf_Root
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("description")]
        public object Description;

        [JsonProperty("defaultPreference")]
        public bool? DefaultPreference;

        [JsonProperty("organizations")]
        public List<object> Organizations;

        [JsonProperty("context")]
        public string Context;

        [JsonProperty("view")]
        public CitizenDashboardCitizenConditionSelf_View View;

        [JsonProperty("_links")]
        public CitizenDashboardCitizenConditionSelf_Links Links; 
    }

}
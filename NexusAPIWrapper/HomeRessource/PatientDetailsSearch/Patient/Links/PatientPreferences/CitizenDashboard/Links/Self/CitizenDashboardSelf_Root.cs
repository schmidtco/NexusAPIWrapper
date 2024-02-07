using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class CitizenDashboardSelf_Root
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
        public CitizenDashboardSelf_View View;

        [JsonProperty("_links")]
        public CitizenDashboardSelf_Links Links;
    }

}
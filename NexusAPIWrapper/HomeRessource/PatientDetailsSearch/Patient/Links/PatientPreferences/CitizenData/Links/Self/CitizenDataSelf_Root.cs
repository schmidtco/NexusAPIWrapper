using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class CitizenDataSelf_Root
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("description")]
        public string Description;

        [JsonProperty("defaultPreference")]
        public bool? DefaultPreference;

        [JsonProperty("organizations")]
        public List<CitizenDataSelf_Organization> Organizations;

        [JsonProperty("context")]
        public string Context;

        [JsonProperty("view")]
        public CitizenDataSelf_View View;

        [JsonProperty("_links")]
        public CitizenDataSelf_Links Links;
    }

}
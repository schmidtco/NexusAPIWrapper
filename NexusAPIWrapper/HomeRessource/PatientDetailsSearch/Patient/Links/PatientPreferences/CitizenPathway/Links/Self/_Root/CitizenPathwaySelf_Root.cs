using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class CitizenPathwaySelf_Root
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
        public List<CitizenPathwaySelf_Organization> Organizations;

        [JsonProperty("context")]
        public string Context;

        [JsonProperty("view")]
        public CitizenPathwaySelf_View View;

        [JsonProperty("_links")]
        public CitizenPathwaySelf_Links Links;
    }

}
using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class CITIZEN_LIST_Root
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
        public List<CITIZEN_LIST_Organization> Organizations;

        [JsonProperty("context")]
        public string Context;

        [JsonProperty("view")]
        public CITIZEN_LIST_View View;

        [JsonProperty("_links")]
        public CITIZEN_LIST_Links Links;
    }

}
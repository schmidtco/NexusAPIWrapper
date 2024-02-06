using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class Preferences_CATALOGGRANT
    {
        [JsonProperty("id")]
        public object Id;

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

        [JsonProperty("_links")]
        public Preferences_Links Links;
    }

}
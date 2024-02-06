using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class Preferences_MOBILEPROFILE
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
        public List<Preferences_Organization> Organizations;

        [JsonProperty("_links")]
        public Preferences_Links Links;
    }

}
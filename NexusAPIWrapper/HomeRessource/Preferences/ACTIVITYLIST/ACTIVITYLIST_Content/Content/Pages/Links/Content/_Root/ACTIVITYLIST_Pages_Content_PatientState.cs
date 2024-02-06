using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class ACTIVITYLIST_Pages_Content_PatientState
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("color")]
        public string Color;

        [JsonProperty("type")]
        public ACTIVITYLIST_Pages_Content_Type Type;

        [JsonProperty("active")]
        public bool? Active;

        [JsonProperty("defaultObject")]
        public bool? DefaultObject;

        [JsonProperty("citizenRegistryConfiguration")]
        public ACTIVITYLIST_Pages_Content_CitizenRegistryConfiguration CitizenRegistryConfiguration;

        [JsonProperty("_links")]
        public ACTIVITYLIST_Pages_Content_Links Links;
    }

}
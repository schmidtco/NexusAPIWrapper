using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientDetails_Value
    {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("version")]
        public int Version;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("color")]
        public string Color;

        [JsonProperty("type")]
        public PatientDetails_Type Type;

        [JsonProperty("active")]
        public bool Active;

        [JsonProperty("defaultObject")]
        public bool DefaultObject;

        [JsonProperty("citizenRegistryConfiguration")]
        public PatientDetails_CitizenRegistryConfiguration CitizenRegistryConfiguration;

        [JsonProperty("_links")]
        public PatientDetails_Links Links;
    }

}
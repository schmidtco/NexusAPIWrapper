using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class FormDataPrototype_PatientState
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
        public FormDataPrototype_Type Type;

        [JsonProperty("active")]
        public bool? Active;

        [JsonProperty("defaultObject")]
        public bool? DefaultObject;

        [JsonProperty("citizenRegistryConfiguration")]
        public FormDataPrototype_CitizenRegistryConfiguration CitizenRegistryConfiguration;

        [JsonProperty("_links")]
        public FormDataPrototype_Links Links;
    }

}
using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PwayRefChildSelf_JournalNote_RefObj_PatientState
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
        public PwayRefChildSelf_JournalNote_RefObj_Type Type;

        [JsonProperty("active")]
        public bool? Active;

        [JsonProperty("defaultObject")]
        public bool? DefaultObject;

        [JsonProperty("citizenRegistryConfiguration")]
        public PwayRefChildSelf_JournalNote_RefObj_CitizenRegistryConfiguration CitizenRegistryConfiguration;

        [JsonProperty("_links")]
        public PwayRefChildSelf_JournalNote_RefObj_Links Links;
    }

}
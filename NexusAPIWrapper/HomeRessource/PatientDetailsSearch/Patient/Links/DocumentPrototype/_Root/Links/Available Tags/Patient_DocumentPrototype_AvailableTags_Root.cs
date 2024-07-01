using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class Patient_DocumentPrototype_AvailableTags_Root
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("description")]
        public string Description;

        [JsonProperty("grouping")]
        public Patient_DocumentPrototype_AvailableTags_Grouping Grouping;

        [JsonProperty("tagOrigin")]
        public string TagOrigin;

        [JsonProperty("active")]
        public bool? Active;

        [JsonProperty("_links")]
        public Patient_DocumentPrototype_AvailableTags_Links Links;
    }

}
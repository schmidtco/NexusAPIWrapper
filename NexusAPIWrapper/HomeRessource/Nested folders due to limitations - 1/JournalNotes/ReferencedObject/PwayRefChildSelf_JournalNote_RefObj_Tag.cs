using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PwayRefChildSelf_JournalNote_RefObj_Tag
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("description")]
        public object Description;

        [JsonProperty("grouping")]
        public object Grouping;

        [JsonProperty("tagOrigin")]
        public object TagOrigin;

        [JsonProperty("active")]
        public bool? Active;

        [JsonProperty("_links")]
        public PwayRefChildSelf_JournalNote_RefObj_Links Links;
    }

}
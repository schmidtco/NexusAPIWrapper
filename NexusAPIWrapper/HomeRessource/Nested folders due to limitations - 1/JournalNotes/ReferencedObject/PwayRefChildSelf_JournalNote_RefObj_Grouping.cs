using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PwayRefChildSelf_JournalNote_RefObj_Grouping
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("_links")]
        public PwayRefChildSelf_JournalNote_RefObj_Links Links;
    }

}
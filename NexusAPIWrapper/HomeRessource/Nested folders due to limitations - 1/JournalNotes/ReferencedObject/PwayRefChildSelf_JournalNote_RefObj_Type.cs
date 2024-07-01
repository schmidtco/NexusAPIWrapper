using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PwayRefChildSelf_JournalNote_RefObj_Type
    {
        [JsonProperty("id")]
        public string Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("abbreviation")]
        public string Abbreviation;

        [JsonProperty("_links")]
        public PwayRefChildSelf_JournalNote_RefObj_Links Links;
    }

}
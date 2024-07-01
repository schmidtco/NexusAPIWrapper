using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PwayRefChildSelf_JournalNote_RefObj_CitizenRegistryConfiguration
    {
        [JsonProperty("updateEnabled")]
        public bool? UpdateEnabled;

        [JsonProperty("_links")]
        public PwayRefChildSelf_JournalNote_RefObj_Links Links;
    }

}
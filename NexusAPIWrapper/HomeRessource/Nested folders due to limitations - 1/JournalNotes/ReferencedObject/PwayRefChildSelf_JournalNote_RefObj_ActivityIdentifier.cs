using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PwayRefChildSelf_JournalNote_RefObj_ActivityIdentifier
    {
        [JsonProperty("identifier")]
        public string Identifier;

        [JsonProperty("type")]
        public string Type;

        [JsonProperty("activityId")]
        public int? ActivityId;

        [JsonProperty("_links")]
        public PwayRefChildSelf_JournalNote_RefObj_Links Links;
    }

}
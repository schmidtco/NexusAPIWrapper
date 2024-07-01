using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PwayRefChildSelf_JournalNote_RefObj_FormDefinition
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("title")]
        public string Title;

        [JsonProperty("dueDateSupported")]
        public bool? DueDateSupported;

        [JsonProperty("active")]
        public bool? Active;

        [JsonProperty("formSignificance")]
        public string FormSignificance;

        [JsonProperty("uid")]
        public string Uid;

        [JsonProperty("tagsRequired")]
        public bool? TagsRequired;

        [JsonProperty("allTagsAllowed")]
        public bool? AllTagsAllowed;

        [JsonProperty("occurrence")]
        public string Occurrence;

        [JsonProperty("fs3TrainingDocumentation")]
        public bool? Fs3TrainingDocumentation;

        [JsonProperty("_links")]
        public PwayRefChildSelf_JournalNote_RefObj_Links Links;
    }

}
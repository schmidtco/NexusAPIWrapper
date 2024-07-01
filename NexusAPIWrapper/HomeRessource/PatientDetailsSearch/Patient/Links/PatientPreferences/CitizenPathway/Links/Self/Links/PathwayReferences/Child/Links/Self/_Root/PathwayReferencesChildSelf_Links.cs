using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PathwayReferencesChildSelf_Links
    {
        [JsonProperty("availableActions")]
        public PathwayReferencesChildSelf_AvailableActions AvailableActions;

        [JsonProperty("self")]
        public PwayRefChildSelf_JournalNote_Self Self;

        [JsonProperty("print")]
        public PwayRefChildSelf_JournalNote_Print Print;

        [JsonProperty("referenceObject")]
        public PwayRefChildSelf_JournalNote_ReferenceObject ReferenceObject;

        [JsonProperty("copyPrototype")]
        public PwayRefChildSelf_JournalNote_CopyPrototype CopyPrototype;

        [JsonProperty("delete")]
        public PwayRefChildSelf_JournalNote_Delete Delete;
    }

}
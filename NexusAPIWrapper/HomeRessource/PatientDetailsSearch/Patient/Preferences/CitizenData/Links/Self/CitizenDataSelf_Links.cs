using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class CitizenDataSelf_Links
    {
        [JsonProperty("self")]
        public CitizenDataSelf_Self Self;

        [JsonProperty("stsOrganization")]
        public CitizenDataSelf_StsOrganization StsOrganization;

        [JsonProperty("formDataPrototype")]
        public CitizenDataSelf_FormDataPrototype FormDataPrototype;

        [JsonProperty("availableTags")]
        public CitizenDataSelf_AvailableTags AvailableTags;

        [JsonProperty("copyPrototype")]
        public CitizenDataSelf_CopyPrototype CopyPrototype;

        [JsonProperty("editablePreference")]
        public CitizenDataSelf_EditablePreference EditablePreference;

        [JsonProperty("content")]
        public CitizenDataSelf_Content Content;

        [JsonProperty("copyableForms")]
        public CitizenDataSelf_CopyableForms CopyableForms;

        [JsonProperty("availableFormDefinitions")]
        public CitizenDataSelf_AvailableFormDefinitions AvailableFormDefinitions;
    }

}
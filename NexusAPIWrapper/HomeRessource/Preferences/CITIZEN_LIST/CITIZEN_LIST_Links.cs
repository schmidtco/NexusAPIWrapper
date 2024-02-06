using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class CITIZEN_LIST_Links
    {
        [JsonProperty("self")]
        public CITIZEN_LIST_Self Self;

        [JsonProperty("stsOrganization")]
        public CITIZEN_LIST_StsOrganization StsOrganization;

        [JsonProperty("availableOrganizations")]
        public CITIZEN_LIST_AvailableOrganizations AvailableOrganizations;

        [JsonProperty("availableProfessionals")]
        public CITIZEN_LIST_AvailableProfessionals AvailableProfessionals;

        [JsonProperty("copyPrototype")]
        public CITIZEN_LIST_CopyPrototype CopyPrototype;

        [JsonProperty("editablePreference")]
        public CITIZEN_LIST_EditablePreference EditablePreference;

        [JsonProperty("content")]
        public CITIZEN_LIST_Content Content;
    }

}
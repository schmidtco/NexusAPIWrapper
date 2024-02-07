using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class ACTIVITYLIST_Links
    {
        [JsonProperty("self")]
        public ACTIVITYLIST_Self Self;

        [JsonProperty("stsOrganization")]
        public ACTIVITYLIST_StsOrganization StsOrganization;

        [JsonProperty("availableOrganizations")]
        public ACTIVITYLIST_AvailableOrganizations AvailableOrganizations;

        [JsonProperty("availableProfessionals")]
        public ACTIVITYLIST_AvailableProfessionals AvailableProfessionals;

        [JsonProperty("availableCitizens")]
        public ACTIVITYLIST_AvailableCitizens AvailableCitizens;

        [JsonProperty("copyPrototype")]
        public ACTIVITYLIST_CopyPrototype CopyPrototype;

        [JsonProperty("editablePreference")]
        public ACTIVITYLIST_EditablePreference EditablePreference;

        [JsonProperty("content")]
        public ACTIVITYLIST_Content Content;

        [JsonProperty("availableTags")]
        public ACTIVITYLIST_AvailableTags AvailableTags;

        [JsonProperty("treeContent")]
        public ACTIVITYLIST_TreeContent TreeContent;

        [JsonProperty("archiveMedcoms")]
        public ACTIVITYLIST_ArchiveMedcoms ArchiveMedcoms;
    }

}
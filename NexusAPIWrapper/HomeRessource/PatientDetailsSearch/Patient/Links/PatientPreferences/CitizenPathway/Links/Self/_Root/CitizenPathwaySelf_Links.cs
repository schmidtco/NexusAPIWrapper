using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class CitizenPathwaySelf_Links
    {
        [JsonProperty("self")]
        public CitizenPathwaySelf_Self Self;

        [JsonProperty("stsOrganization")]
        public CitizenPathwaySelf_StsOrganization StsOrganization;

        [JsonProperty("copyPrototype")]
        public CitizenPathwaySelf_CopyPrototype CopyPrototype;

        [JsonProperty("editablePreference")]
        public CitizenPathwaySelf_EditablePreference EditablePreference;

        [JsonProperty("pathwayReferences")]
        public CitizenPathwaySelf_PathwayReferences PathwayReferences;

        [JsonProperty("availableGroupedActivities")]
        public CitizenPathwaySelf_AvailableGroupedActivities AvailableGroupedActivities;

        [JsonProperty("additionalInformation")]
        public CitizenPathwaySelf_AdditionalInformation AdditionalInformation;

        [JsonProperty("orderGrantAdditionalInformation")]
        public CitizenPathwaySelf_OrderGrantAdditionalInformation OrderGrantAdditionalInformation;

        [JsonProperty("basketGrantAdditionalInformation")]
        public CitizenPathwaySelf_BasketGrantAdditionalInformation BasketGrantAdditionalInformation;
    }

}
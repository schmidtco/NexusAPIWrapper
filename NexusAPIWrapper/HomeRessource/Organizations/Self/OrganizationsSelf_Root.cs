using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class OrganizationsSelf_Root
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("uid")]
        public string Uid;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("children")]
        public List<object> Children;

        [JsonProperty("systemDefined")]
        public bool? SystemDefined;

        [JsonProperty("plannedMove")]
        public object PlannedMove;

        [JsonProperty("effectiveStartDate")]
        public string EffectiveStartDate;

        [JsonProperty("effectiveEndDate")]
        public object EffectiveEndDate;

        [JsonProperty("sensitive")]
        public bool? Sensitive;

        [JsonProperty("willBeActiveInFuture")]
        public bool? WillBeActiveInFuture;

        [JsonProperty("willBeInactiveInFuture")]
        public bool? WillBeInactiveInFuture;

        [JsonProperty("active")]
        public bool? Active;

        [JsonProperty("syncId")]
        public string SyncId;

        [JsonProperty("description")]
        public object Description;

        [JsonProperty("advisGroup")]
        public bool? AdvisGroup;

        [JsonProperty("primaryMedcom")]
        public bool? PrimaryMedcom;

        [JsonProperty("dispensary")]
        public bool? Dispensary;

        [JsonProperty("dispensaryInUse")]
        public bool? DispensaryInUse;

        [JsonProperty("allowAssignments")]
        public bool? AllowAssignments;

        [JsonProperty("patientAssignable")]
        public bool? PatientAssignable;

        [JsonProperty("drugInventory")]
        public object DrugInventory;

        [JsonProperty("sorCode")]
        public object SorCode;

        [JsonProperty("sorEntityType")]
        public object SorEntityType;

        [JsonProperty("dailyPhoneNumber")]
        public string DailyPhoneNumber;

        [JsonProperty("dailyOpeningTimes")]
        public object DailyOpeningTimes;

        [JsonProperty("nightlyPhoneNumber")]
        public object NightlyPhoneNumber;

        [JsonProperty("nightlyOpeningTimes")]
        public object NightlyOpeningTimes;

        [JsonProperty("contactInfoProtected")]
        public bool? ContactInfoProtected;

        [JsonProperty("publicDailyPhoneNumber")]
        public object PublicDailyPhoneNumber;

        [JsonProperty("publicDailyOpeningTimes")]
        public object PublicDailyOpeningTimes;

        [JsonProperty("publicNightlyPhoneNumber")]
        public object PublicNightlyPhoneNumber;

        [JsonProperty("publicNightlyOpeningTimes")]
        public object PublicNightlyOpeningTimes;

        [JsonProperty("parent")]
        public OrganizationsSelf_Parent Parent;

        [JsonProperty("citizenInactivationConfiguration")]
        public OrganizationsSelf_CitizenInactivationConfiguration CitizenInactivationConfiguration;

        [JsonProperty("professionalInactivationConfiguration")]
        public OrganizationsSelf_ProfessionalInactivationConfiguration ProfessionalInactivationConfiguration;

        [JsonProperty("medcomConfiguration")]
        public OrganizationsSelf_MedcomConfiguration MedcomConfiguration;

        [JsonProperty("_links")]
        public OrganizationsSelf_Links Links;
    }

}
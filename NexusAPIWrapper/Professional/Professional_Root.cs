using Newtonsoft.Json; 
namespace NexusAPIWrapper
{ 

    public class Professional_Root
    {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("version")]
        public int Version;

        [JsonProperty("uid")]
        public string Uid;

        [JsonProperty("firstName")]
        public string FirstName;

        [JsonProperty("middleName")]
        public string MiddleName;

        [JsonProperty("lastName")]
        public string LastName;

        [JsonProperty("fullName")]
        public string FullName;

        [JsonProperty("initials")]
        public string Initials;

        [JsonProperty("primaryIdentifier")]
        public string PrimaryIdentifier;

        [JsonProperty("homeTelephone")]
        public object HomeTelephone;

        [JsonProperty("mobileTelephone")]
        public string MobileTelephone;

        [JsonProperty("workTelephone")]
        public object WorkTelephone;

        [JsonProperty("telephoneHours")]
        public object TelephoneHours;

        [JsonProperty("primaryEmailAddress")]
        public object PrimaryEmailAddress;

        [JsonProperty("secondaryEmailAddress")]
        public object SecondaryEmailAddress;

        [JsonProperty("primaryAddress")]
        public Professional_PrimaryAddress PrimaryAddress;

        [JsonProperty("secondaryAddress")]
        public Professional_SecondaryAddress SecondaryAddress;

        [JsonProperty("organizationName")]
        public string OrganizationName;

        [JsonProperty("departmentName")]
        public string DepartmentName;

        [JsonProperty("unitName")]
        public string UnitName;

        [JsonProperty("user")]
        public object User;

        [JsonProperty("active")]
        public bool Active;

        [JsonProperty("autosignatureId")]
        public int? AutosignatureId;

        [JsonProperty("defaultOrganizationSupplier")]
        public Professional_DefaultOrganizationSupplier DefaultOrganizationSupplier;

        [JsonProperty("activeDirectoryConfiguration")]
        public Professional_ActiveDirectoryConfiguration ActiveDirectoryConfiguration;

        [JsonProperty("sidConfiguration")]
        public object SidConfiguration;

        [JsonProperty("_links")]
        public Professional_Links Links;
    }

}
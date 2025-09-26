using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatCond_RelActiWHist_Professional
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("uid")]
        public string Uid;

        [JsonProperty("firstName")]
        public string FirstName;

        [JsonProperty("middleName")]
        public object MiddleName;

        [JsonProperty("color")]
        public object Color;

        [JsonProperty("lastName")]
        public string LastName;

        [JsonProperty("fullName")]
        public string FullName;

        [JsonProperty("primaryIdentifier")]
        public string PrimaryIdentifier;

        [JsonProperty("initials")]
        public string Initials;

        [JsonProperty("professionalJob")]
        public PatCond_RelActiWHist_ProfessionalJob ProfessionalJob;

        [JsonProperty("primaryOrganization")]
        public PatCond_RelActiWHist_PrimaryOrganization PrimaryOrganization;

        [JsonProperty("active")]
        public bool? Active;

        [JsonProperty("mobile")]
        public object Mobile;

        [JsonProperty("_links")]
        public PatCond_RelActiWHist_Links Links;
    }

}
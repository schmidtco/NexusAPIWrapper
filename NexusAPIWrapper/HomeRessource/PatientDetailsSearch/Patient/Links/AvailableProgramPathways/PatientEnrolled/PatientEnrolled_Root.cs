using Newtonsoft.Json; 
using System; 
namespace NexusAPIWrapper{ 

    public class PatientEnrolled_Root
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("uid")]
        public string Uid;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("active")]
        public bool? Active;

        [JsonProperty("createDate")]
        public DateTime? CreateDate;

        [JsonProperty("activatedDate")]
        public DateTime? ActivatedDate;

        [JsonProperty("inactivatedDate")]
        public object InactivatedDate;

        [JsonProperty("pathwayType")]
        public PatientEnrolled_PathwayType PathwayType;

        [JsonProperty("patient")]
        public PatientEnrolled_Patient Patient;

        [JsonProperty("sensitiveData")]
        public bool? SensitiveData;

        [JsonProperty("responsibleOrganization")]
        public object ResponsibleOrganization;

        [JsonProperty("responsibleProfessional")]
        public object ResponsibleProfessional;

        [JsonProperty("_links")]
        public PatientEnrolled_Links Links;
    }

}
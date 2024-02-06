using Newtonsoft.Json; 
using System.Collections.Generic; 
using System; 
namespace NexusAPIWrapper{ 

    public class ACTIVITYLIST_Pages_Content_Root
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("description")]
        public object Description;

        [JsonProperty("status")]
        public string Status;

        [JsonProperty("patients")]
        public List<ACTIVITYLIST_Pages_Content_Patient> Patients;

        [JsonProperty("date")]
        public DateTime? Date;

        [JsonProperty("sender")]
        public string Sender;

        [JsonProperty("recipient")]
        public string Recipient;

        [JsonProperty("patientOrganizations")]
        public List<ACTIVITYLIST_Pages_Content_PatientOrganization> PatientOrganizations;

        [JsonProperty("_links")]
        public ACTIVITYLIST_Pages_Content_Links Links;

        [JsonProperty("id")]
        public int? Id;
    }

}
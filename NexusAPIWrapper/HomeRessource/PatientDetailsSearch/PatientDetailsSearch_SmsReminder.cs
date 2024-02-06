using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientDetails_SmsReminder
    {
        [JsonProperty("smsReminder")]
        public bool SmsReminder;

        [JsonProperty("_links")]
        public PatientDetails_Links Links;
    }

}
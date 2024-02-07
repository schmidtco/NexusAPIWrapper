using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientDetailsSearch_SmsReminder
    {
        [JsonProperty("smsReminder")]
        public bool? SmsReminder;

        [JsonProperty("_links")]
        public PatientDetailsSearch_Links Links;
    }

}
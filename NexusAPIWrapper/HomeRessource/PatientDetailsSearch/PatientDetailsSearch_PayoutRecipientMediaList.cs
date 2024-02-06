using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class PatientDetails_PayoutRecipientMediaList
    {
        [JsonProperty("payoutRecipientMedia")]
        public List<object> PayoutRecipientMedia;

        [JsonProperty("_links")]
        public PatientDetails_Links Links;
    }

}
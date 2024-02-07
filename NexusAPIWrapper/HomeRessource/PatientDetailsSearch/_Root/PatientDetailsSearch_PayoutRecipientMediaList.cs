using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class PatientDetailsSearch_PayoutRecipientMediaList
    {
        [JsonProperty("payoutRecipientMedia")]
        public List<object> PayoutRecipientMedia;

        [JsonProperty("_links")]
        public PatientDetailsSearch_Links Links;
    }

}
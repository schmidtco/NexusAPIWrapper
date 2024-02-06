using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientDetails_TaxRate
    {
        [JsonProperty("taxRate")]
        public object TaxRate;

        [JsonProperty("_links")]
        public PatientDetails_Links Links;
    }

}
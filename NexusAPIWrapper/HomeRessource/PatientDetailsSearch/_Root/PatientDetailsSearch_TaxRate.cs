using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientDetailsSearch_TaxRate
    {
        [JsonProperty("taxRate")]
        public object TaxRate;

        [JsonProperty("_links")]
        public PatientDetailsSearch_Links Links;
    }

}
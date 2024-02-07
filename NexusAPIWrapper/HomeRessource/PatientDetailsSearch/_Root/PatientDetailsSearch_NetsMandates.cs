using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class PatientDetailsSearch_NetsMandates
    {
        [JsonProperty("supplierMapping")]
        public List<object> SupplierMapping;

        [JsonProperty("financialAccountNumber")]
        public object FinancialAccountNumber;

        [JsonProperty("_links")]
        public PatientDetailsSearch_Links Links;
    }

}
using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class PatientDetails_NetsMandates
    {
        [JsonProperty("supplierMapping")]
        public List<object> SupplierMapping;

        [JsonProperty("financialAccountNumber")]
        public object FinancialAccountNumber;

        [JsonProperty("_links")]
        public PatientDetails_Links Links;
    }

}
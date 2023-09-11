using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class ProfessionalConfiguration_KmdVagtplanConfiguration
    {
        [JsonProperty("cprExtra")]
        public List<int> CprExtra;

        [JsonProperty("_links")]
        public ProfessionalConfiguration_Links Links;
    }

}
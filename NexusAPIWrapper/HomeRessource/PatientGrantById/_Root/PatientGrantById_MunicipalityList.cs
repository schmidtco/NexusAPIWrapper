using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class PatientGrantById_MunicipalityList
    {
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("municipalityCode")]
        public string MunicipalityCode;

        [JsonProperty("_links")]
        public PatientGrantById_Links Links;
    }

}
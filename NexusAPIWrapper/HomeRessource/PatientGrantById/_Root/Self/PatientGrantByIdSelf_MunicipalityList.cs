using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class PatientGrantByIdSelf_MunicipalityList
    {
        [JsonProperty("name")]
        public string Name;

        [JsonProperty("municipalityCode")]
        public string MunicipalityCode;

        [JsonProperty("_links")]
        public PatientGrantByIdSelf_Links Links;
    }

}
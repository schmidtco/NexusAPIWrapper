using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class ProfessionalConfiguration_EventPlaningConfiguration
    {
        [JsonProperty("competences")]
        public List<object> Competences;

        [JsonProperty("_links")]
        public ProfessionalConfiguration_Links Links;
    }

}
using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class CitizenDataSelf_CreatableObjects
    {
        [JsonProperty("forms")]
        public List<CitizenDataSelf_Form> Forms;
    }

}
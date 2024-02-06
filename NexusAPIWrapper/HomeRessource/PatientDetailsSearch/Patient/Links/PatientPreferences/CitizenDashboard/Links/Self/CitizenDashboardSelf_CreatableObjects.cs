using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class CitizenDashboardSelf_CreatableObjects
    {
        [JsonProperty("forms")]
        public List<object> Forms;

        [JsonProperty("associationGroups")]
        public List<object> AssociationGroups;

        [JsonProperty("letters")]
        public List<object> Letters;

        [JsonProperty("_links")]
        public CitizenDashboardSelf_Links Links;
    }

}
using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class CitizenPathwaySelf_CreatableObject
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("_links")]
        public CitizenPathwaySelf_Links Links;

        [JsonProperty("types")]
        public List<CitizenPathwaySelf_Type> Types;

        [JsonProperty("showAll")]
        public bool? ShowAll;
    }

}
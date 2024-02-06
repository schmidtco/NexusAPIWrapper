using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class CitizenPathwaySelf_Type
    {
        [JsonProperty("showAll")]
        public bool? ShowAll;

        [JsonProperty("programs")]
        public List<CitizenPathwaySelf_Program> Programs;

        [JsonProperty("_links")]
        public CitizenPathwaySelf_Links Links;

        [JsonProperty("id")]
        public int? Id;
    }

}
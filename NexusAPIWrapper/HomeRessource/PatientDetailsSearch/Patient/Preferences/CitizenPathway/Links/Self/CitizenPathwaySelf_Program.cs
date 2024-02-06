using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class CitizenPathwaySelf_Program
    {
        [JsonProperty("_links")]
        public CitizenPathwaySelf_Links Links;

        [JsonProperty("id")]
        public int? Id;
    }

}
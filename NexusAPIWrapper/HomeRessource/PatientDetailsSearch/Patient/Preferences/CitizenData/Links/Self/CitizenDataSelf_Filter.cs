using Newtonsoft.Json; 
using System; 
namespace NexusAPIWrapper{ 

    public class CitizenDataSelf_Filter
    {
        [JsonProperty("from")]
        public object From;

        [JsonProperty("to")]
        public DateTime? To;

        [JsonProperty("_links")]
        public CitizenDataSelf_Links Links;
    }

}
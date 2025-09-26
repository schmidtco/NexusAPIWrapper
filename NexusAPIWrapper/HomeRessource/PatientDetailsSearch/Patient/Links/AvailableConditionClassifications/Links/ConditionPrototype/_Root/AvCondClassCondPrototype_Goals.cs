using Newtonsoft.Json; 
using System; 
namespace NexusAPIWrapper{ 

    public class AvCondClassCondPrototype_Goals
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("id")]
        public object Id;

        [JsonProperty("version")]
        public object Version;

        [JsonProperty("value")]
        public object Value;

        [JsonProperty("updateDate")]
        public DateTime? UpdateDate;

        [JsonProperty("_links")]
        public AvCondClassCondPrototype_Links Links;
    }

}
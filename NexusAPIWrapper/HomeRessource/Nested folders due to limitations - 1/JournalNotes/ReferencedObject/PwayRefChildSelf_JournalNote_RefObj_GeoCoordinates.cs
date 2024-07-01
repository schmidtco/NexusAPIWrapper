using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PwayRefChildSelf_JournalNote_RefObj_GeoCoordinates
    {
        [JsonProperty("x")]
        public object X;

        [JsonProperty("y")]
        public object Y;

        [JsonProperty("present")]
        public bool? Present;
    }

}
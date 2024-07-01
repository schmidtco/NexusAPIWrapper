using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class BB_Root
    {
        [JsonProperty("?xml")]
        public BB_Xml Xml;

        [JsonProperty("dagbogsnoter")]
        public BB_Dagbogsnoter Dagbogsnoter;
    }

}
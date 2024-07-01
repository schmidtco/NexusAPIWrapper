using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class BB_Doc_Root
    {
        [JsonProperty("?xml")]
        public BB_Doc_Xml Xml;

        [JsonProperty("Dokumenter")]
        public BB_Doc_Dokumenter Dokumenter;
    }

}
using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class BB_Note_Doc_Root
    {
        [JsonProperty("?xml")]
        public BB_Note_Doc_Xml Xml;

        [JsonProperty("dokumenter")]
        public BB_Note_Doc_Dokumenter Dokumenter;
    }

}
using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class BB_Doc_Xml
    {
        [JsonProperty("@version")]
        public string Version;

        [JsonProperty("@encoding")]
        public string Encoding;
    }

}
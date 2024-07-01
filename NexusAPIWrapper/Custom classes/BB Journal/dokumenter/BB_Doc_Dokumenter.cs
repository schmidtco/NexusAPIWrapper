using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class BB_Doc_Dokumenter
    {
        [JsonProperty("document")]
        public List<BB_Doc_Document> Document;
    }

}
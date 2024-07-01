using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class BB_Note_Doc_Dokumenter
    {
        [JsonProperty("dokument")]
        public List<BB_Note_Doc_Dokument> Dokument;
    }

}
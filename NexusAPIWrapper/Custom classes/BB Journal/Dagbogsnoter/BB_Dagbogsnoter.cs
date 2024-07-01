using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class BB_Dagbogsnoter
    {
        [JsonProperty("dagbogsnote")]
        public List<BB_Dagbogsnote> Dagbogsnote;
    }

}
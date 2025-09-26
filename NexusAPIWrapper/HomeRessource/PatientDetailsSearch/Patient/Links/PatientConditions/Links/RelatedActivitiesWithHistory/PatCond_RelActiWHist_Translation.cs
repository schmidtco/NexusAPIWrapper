using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatCond_RelActiWHist_Translation
    {
        [JsonProperty("actionName")]
        public string ActionName;

        [JsonProperty("_links")]
        public PatCond_RelActiWHist_Links Links;
    }

}
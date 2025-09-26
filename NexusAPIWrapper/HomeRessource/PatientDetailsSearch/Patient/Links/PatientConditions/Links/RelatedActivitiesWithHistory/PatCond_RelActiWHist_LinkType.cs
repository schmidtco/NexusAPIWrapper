using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class PatCond_RelActiWHist_LinkType
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("sourceSideName")]
        public string SourceSideName;

        [JsonProperty("targetSideName")]
        public string TargetSideName;

        [JsonProperty("possibleSources")]
        public List<string> PossibleSources;

        [JsonProperty("possibleTargets")]
        public List<string> PossibleTargets;

        [JsonProperty("linkGroup")]
        public string LinkGroup;

        [JsonProperty("_links")]
        public PatCond_RelActiWHist_Links Links;
    }

}
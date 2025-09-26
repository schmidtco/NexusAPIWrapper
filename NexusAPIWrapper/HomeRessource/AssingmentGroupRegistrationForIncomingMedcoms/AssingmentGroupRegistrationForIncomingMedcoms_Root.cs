using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class AssingmentGroupRegistrationForIncomingMedcoms_Root
    {
        [JsonProperty("displayName")]
        public string DisplayName;

        [JsonProperty("identity")]
        public string Identity;

        [JsonProperty("client")]
        public string Client;

        [JsonProperty("supportAutoAssignments")]
        public bool? SupportAutoAssignments;

        [JsonProperty("options")]
        public List<AssingmentGroupRegistrationForIncomingMedcoms_Option> Options;
    }

}
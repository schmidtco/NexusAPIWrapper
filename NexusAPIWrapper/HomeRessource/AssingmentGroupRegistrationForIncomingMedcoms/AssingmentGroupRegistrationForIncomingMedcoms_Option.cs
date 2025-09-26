using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class AssingmentGroupRegistrationForIncomingMedcoms_Option
    {
        [JsonProperty("displayName")]
        public string DisplayName;

        [JsonProperty("identity")]
        public string Identity;

        [JsonProperty("parentIdentity")]
        public string ParentIdentity;

        [JsonProperty("active")]
        public object Active;

        [JsonProperty("requirements")]
        public List<AssingmentGroupRegistrationForIncomingMedcoms_Requirement> Requirements;

        [JsonProperty("params")]
        public List<object> Params;

        [JsonProperty("systemAssignmentTypes")]
        public List<AssingmentGroupRegistrationForIncomingMedcoms_SystemAssignmentType> SystemAssignmentTypes;
    }

}
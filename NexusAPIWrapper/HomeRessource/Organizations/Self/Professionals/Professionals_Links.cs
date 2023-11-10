using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class Professionals_Links
    {
        [JsonProperty("self")]
        public Professionals_Self Self;

        [JsonProperty("roles")]
        public Professionals_Roles Roles;

        [JsonProperty("updateRoles")]
        public Professionals_UpdateRoles UpdateRoles;

        [JsonProperty("availableRoles")]
        public Professionals_AvailableRoles AvailableRoles;

        [JsonProperty("roleAssignments")]
        public Professionals_RoleAssignments RoleAssignments;

        [JsonProperty("configuration")]
        public Professionals_Configuration Configuration;

        [JsonProperty("stsRoles")]
        public Professionals_StsRoles StsRoles;

        [JsonProperty("removeOrganization")]
        public Professionals_RemoveOrganization RemoveOrganization;

        [JsonProperty("tabletAppConfiguration")]
        public Professionals_TabletAppConfiguration TabletAppConfiguration;
    }

}
using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class OrganizationsSelfLinksRolesSelf_Links
    {
        [JsonProperty("self")]
        public OrganizationsSelfLinksRolesSelf_Self Self;

        [JsonProperty("professionals")]
        public OrganizationsSelfLinksRolesSelf_Professionals Professionals;

        [JsonProperty("availableProfessionals")]
        public OrganizationsSelfLinksRolesSelf_AvailableProfessionals AvailableProfessionals;

        [JsonProperty("permissions")]
        public OrganizationsSelfLinksRolesSelf_Permissions Permissions;

        [JsonProperty("permissionsTree")]
        public OrganizationsSelfLinksRolesSelf_PermissionsTree PermissionsTree;

        [JsonProperty("availablePermissions")]
        public OrganizationsSelfLinksRolesSelf_AvailablePermissions AvailablePermissions;

        [JsonProperty("updateProfessionals")]
        public OrganizationsSelfLinksRolesSelf_UpdateProfessionals UpdateProfessionals;

        [JsonProperty("updatePermissions")]
        public OrganizationsSelfLinksRolesSelf_UpdatePermissions UpdatePermissions;

        [JsonProperty("availablePermissionsTree")]
        public OrganizationsSelfLinksRolesSelf_AvailablePermissionsTree AvailablePermissionsTree;

        [JsonProperty("stsProfessionalAssignments")]
        public OrganizationsSelfLinksRolesSelf_StsProfessionalAssignments StsProfessionalAssignments;

        [JsonProperty("audit")]
        public OrganizationsSelfLinksRolesSelf_Audit Audit;

        [JsonProperty("update")]
        public OrganizationsSelfLinksRolesSelf_Update Update;

        [JsonProperty("copyPrototype")]
        public OrganizationsSelfLinksRolesSelf_CopyPrototype CopyPrototype;
    }

}
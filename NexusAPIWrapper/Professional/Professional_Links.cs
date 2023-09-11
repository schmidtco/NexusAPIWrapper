using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class Professional_Links
    {
        [JsonProperty("availableCountries")]
        public Professional_AvailableCountries AvailableCountries;

        [JsonProperty("searchPostalDistrict")]
        public Professional_SearchPostalDistrict SearchPostalDistrict;

        [JsonProperty("self")]
        public Professional_Self Self;

        [JsonProperty("permissionAssignments")]
        public Professional_PermissionAssignments PermissionAssignments;

        [JsonProperty("updateOrganizations")]
        public Professional_UpdateOrganizations UpdateOrganizations;

        [JsonProperty("organizations")]
        public Professional_Organizations Organizations;

        [JsonProperty("organizationsFromApi")]
        public Professional_OrganizationsFromApi OrganizationsFromApi;

        [JsonProperty("organizationsFromSts")]
        public Professional_OrganizationsFromSts OrganizationsFromSts;

        [JsonProperty("permissionsTree")]
        public Professional_PermissionsTree PermissionsTree;

        [JsonProperty("roles")]
        public Professional_Roles Roles;

        [JsonProperty("stsRoles")]
        public Professional_StsRoles StsRoles;

        [JsonProperty("roleAssignments")]
        public Professional_RoleAssignments RoleAssignments;

        [JsonProperty("availableRoles")]
        public Professional_AvailableRoles AvailableRoles;

        [JsonProperty("updateRoles")]
        public Professional_UpdateRoles UpdateRoles;

        [JsonProperty("update")]
        public Professional_Update Update;

        [JsonProperty("fmkRole")]
        public Professional_FmkRole FmkRole;

        [JsonProperty("updateSmdbConfiguration")]
        public Professional_UpdateSmdbConfiguration UpdateSmdbConfiguration;

        [JsonProperty("updateAuthorizationCode")]
        public Professional_UpdateAuthorizationCode UpdateAuthorizationCode;

        [JsonProperty("updateEventPlaningConfiguration")]
        public Professional_UpdateEventPlaningConfiguration UpdateEventPlaningConfiguration;

        [JsonProperty("updateExchangeConfiguration")]
        public Professional_UpdateExchangeConfiguration UpdateExchangeConfiguration;

        [JsonProperty("availableOrganizationSuppliers")]
        public Professional_AvailableOrganizationSuppliers AvailableOrganizationSuppliers;

        [JsonProperty("availableProfessionalJobs")]
        public Professional_AvailableProfessionalJobs AvailableProfessionalJobs;

        [JsonProperty("patients")]
        public Professional_Patients Patients;

        [JsonProperty("defaultPagePreference")]
        public Professional_DefaultPagePreference DefaultPagePreference;

        [JsonProperty("configuration")]
        public Professional_Configuration Configuration;

        [JsonProperty("availableProfessionalAutosignatures")]
        public Professional_AvailableProfessionalAutosignatures AvailableProfessionalAutosignatures;

        [JsonProperty("prioritizedOrganizationsIds")]
        public Professional_PrioritizedOrganizationsIds PrioritizedOrganizationsIds;

        [JsonProperty("audit")]
        public Professional_Audit Audit;

        [JsonProperty("externalProfessionalPermissionAssignments")]
        public Professional_ExternalProfessionalPermissionAssignments ExternalProfessionalPermissionAssignments;

        [JsonProperty("stsProfessional")]
        public Professional_StsProfessional StsProfessional;
    }

}
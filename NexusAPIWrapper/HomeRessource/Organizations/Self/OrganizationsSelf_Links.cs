using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class OrganizationsSelf_Links
    {
        [JsonProperty("self")]
        public OrganizationsSelf_Self Self;

        [JsonProperty("stsOrganization")]
        public OrganizationsSelf_StsOrganization StsOrganization;

        [JsonProperty("availableTargetOrganizations")]
        public OrganizationsSelf_AvailableTargetOrganizations AvailableTargetOrganizations;

        [JsonProperty("availableSites")]
        public OrganizationsSelf_AvailableSites AvailableSites;

        [JsonProperty("availablePrograms")]
        public OrganizationsSelf_AvailablePrograms AvailablePrograms;

        [JsonProperty("availableMedcomContactTypes")]
        public OrganizationsSelf_AvailableMedcomContactTypes AvailableMedcomContactTypes;

        [JsonProperty("update")]
        public OrganizationsSelf_Update Update;

        [JsonProperty("childPrototype")]
        public OrganizationsSelf_ChildPrototype ChildPrototype;

        [JsonProperty("audit")]
        public OrganizationsSelf_Audit Audit;

        [JsonProperty("availableParents")]
        public OrganizationsSelf_AvailableParents AvailableParents;

        [JsonProperty("updateInactivation")]
        public OrganizationsSelf_UpdateInactivation UpdateInactivation;

        [JsonProperty("availableProfessionals")]
        public OrganizationsSelf_AvailableProfessionals AvailableProfessionals;

        [JsonProperty("addProfessional")]
        public OrganizationsSelf_AddProfessional AddProfessional;

        [JsonProperty("permissions")]
        public OrganizationsSelf_Permissions Permissions;

        [JsonProperty("availablePermissionsTree")]
        public OrganizationsSelf_AvailablePermissionsTree AvailablePermissionsTree;

        [JsonProperty("professionals")]
        public OrganizationsSelf_Professionals Professionals;

        [JsonProperty("patients")]
        public OrganizationsSelf_Patients Patients;

        [JsonProperty("updatePermissions")]
        public OrganizationsSelf_UpdatePermissions UpdatePermissions;

        [JsonProperty("professionalPrototype")]
        public OrganizationsSelf_ProfessionalPrototype ProfessionalPrototype;

        [JsonProperty("rolePrototype")]
        public OrganizationsSelf_RolePrototype RolePrototype;

        [JsonProperty("updateMedcomConfiguration")]
        public OrganizationsSelf_UpdateMedcomConfiguration UpdateMedcomConfiguration;

        [JsonProperty("importProfessionalFromSts")]
        public OrganizationsSelf_ImportProfessionalFromSts ImportProfessionalFromSts;

        [JsonProperty("organizationsTree")]
        public OrganizationsSelf_OrganizationsTree OrganizationsTree;

        [JsonProperty("availableDrugInventories")]
        public OrganizationsSelf_AvailableDrugInventories AvailableDrugInventories;

        [JsonProperty("searchSorEntities")]
        public OrganizationsSelf_SearchSorEntities SearchSorEntities;
    }

}
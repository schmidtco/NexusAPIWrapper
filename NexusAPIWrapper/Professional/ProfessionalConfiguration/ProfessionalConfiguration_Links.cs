using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class ProfessionalConfiguration_Links
    {
        [JsonProperty("self")]
        public ProfessionalConfiguration_Self Self;

        [JsonProperty("availableKmdExtraCprs")]
        public ProfessionalConfiguration_AvailableKmdExtraCprs AvailableKmdExtraCprs;

        [JsonProperty("availableCompetences")]
        public ProfessionalConfiguration_AvailableCompetences AvailableCompetences;

        [JsonProperty("update")]
        public ProfessionalConfiguration_Update Update;

        [JsonProperty("organizations")]
        public ProfessionalConfiguration_Organizations Organizations;

        [JsonProperty("updateOrganizations")]
        public ProfessionalConfiguration_UpdateOrganizations UpdateOrganizations;

        [JsonProperty("fmkRole")]
        public ProfessionalConfiguration_FmkRole FmkRole;

        [JsonProperty("updateSmdbConfiguration")]
        public ProfessionalConfiguration_UpdateSmdbConfiguration UpdateSmdbConfiguration;

        [JsonProperty("updateAuthorizationCode")]
        public ProfessionalConfiguration_UpdateAuthorizationCode UpdateAuthorizationCode;

        [JsonProperty("updateEventPlaningConfiguration")]
        public ProfessionalConfiguration_UpdateEventPlaningConfiguration UpdateEventPlaningConfiguration;

        [JsonProperty("updateExchangeConfiguration")]
        public ProfessionalConfiguration_UpdateExchangeConfiguration UpdateExchangeConfiguration;

        [JsonProperty("availableOrganizationSuppliers")]
        public ProfessionalConfiguration_AvailableOrganizationSuppliers AvailableOrganizationSuppliers;

        [JsonProperty("availableProfessionalJobs")]
        public ProfessionalConfiguration_AvailableProfessionalJobs AvailableProfessionalJobs;

        [JsonProperty("patients")]
        public ProfessionalConfiguration_Patients Patients;

        [JsonProperty("organizationsWithEAN")]
        public ProfessionalConfiguration_OrganizationsWithEAN OrganizationsWithEAN;

        [JsonProperty("availableNationalRoles")]
        public ProfessionalConfiguration_AvailableNationalRoles AvailableNationalRoles;

        [JsonProperty("stsSn")]
        public ProfessionalConfiguration_StsSn StsSn;

        [JsonProperty("deleteStsSn")]
        public ProfessionalConfiguration_DeleteStsSn DeleteStsSn;
    }

}
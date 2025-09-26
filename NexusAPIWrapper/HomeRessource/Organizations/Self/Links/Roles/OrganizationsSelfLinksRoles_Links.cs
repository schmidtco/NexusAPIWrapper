using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class OrganizationsSelfLinksRoles_Links
    {
        [JsonProperty("self")]
        public OrganizationsSelfLinksRoles_Self Self;

        [JsonProperty("updateProfessionals")]
        public OrganizationsSelfLinksRoles_UpdateProfessionals UpdateProfessionals;
    }

}
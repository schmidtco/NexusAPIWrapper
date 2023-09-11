using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class ProfessionalConfiguration_Root
    {
        [JsonProperty("id")]
        public int Id;

        [JsonProperty("version")]
        public int Version;

        [JsonProperty("color")]
        public string Color;

        [JsonProperty("primaryIdentifier")]
        public string PrimaryIdentifier;

        [JsonProperty("defaultOrganizationSupplier")]
        public ProfessionalConfiguration_DefaultOrganizationSupplier DefaultOrganizationSupplier;

        [JsonProperty("cpr")]
        public string Cpr;

        [JsonProperty("stsSn")]
        public string StsSn;

        [JsonProperty("basisReportUsername")]
        public object BasisReportUsername;

        [JsonProperty("activeDirectoryConfiguration")]
        public ProfessionalConfiguration_ActiveDirectoryConfiguration ActiveDirectoryConfiguration;

        [JsonProperty("kmdVagtplanConfiguration")]
        public ProfessionalConfiguration_KmdVagtplanConfiguration KmdVagtplanConfiguration;

        [JsonProperty("smdbConfiguration")]
        public ProfessionalConfiguration_SmdbConfiguration SmdbConfiguration;

        [JsonProperty("authorizationCodeConfiguration")]
        public ProfessionalConfiguration_AuthorizationCodeConfiguration AuthorizationCodeConfiguration;

        [JsonProperty("exchangeConfiguration")]
        public ProfessionalConfiguration_ExchangeConfiguration ExchangeConfiguration;

        [JsonProperty("sidConfigurationResource")]
        public object SidConfigurationResource;

        [JsonProperty("professionalJob")]
        public object ProfessionalJob;

        [JsonProperty("primaryOrganization")]
        public object PrimaryOrganization;

        [JsonProperty("defaultMedcomSenderOrganizationId")]
        public int DefaultMedcomSenderOrganizationId;

        [JsonProperty("replyToDefaultMedcomSenderOrganization")]
        public bool ReplyToDefaultMedcomSenderOrganization;

        [JsonProperty("nationalRoleConfiguration")]
        public object NationalRoleConfiguration;

        [JsonProperty("active")]
        public bool Active;

        [JsonProperty("patientBlackList")]
        public List<object> PatientBlackList;

        [JsonProperty("roadTimesCalculationConfiguration")]
        public RoadTimesCalculationConfiguration RoadTimesCalculationConfiguration;

        [JsonProperty("eventPlaningConfiguration")]
        public ProfessionalConfiguration_EventPlaningConfiguration EventPlaningConfiguration;

        [JsonProperty("_links")]
        public ProfessionalConfiguration_Links Links;
    }

}
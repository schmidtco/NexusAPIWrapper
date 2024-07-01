using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientGrantByIdAudit_Links
    {
        [JsonProperty("self")]
        public PatientGrantByIdAudit_Self Self;

        [JsonProperty("configuration")]
        public PatientGrantByIdAudit_Configuration Configuration;

        [JsonProperty("defaultSenderOrganization")]
        public PatientGrantByIdAudit_DefaultSenderOrganization DefaultSenderOrganization;

        [JsonProperty("defaultSenderOrganizationForReply")]
        public PatientGrantByIdAudit_DefaultSenderOrganizationForReply DefaultSenderOrganizationForReply;

        [JsonProperty("tabletAppConfiguration")]
        public PatientGrantByIdAudit_TabletAppConfiguration TabletAppConfiguration;

        [JsonProperty("createComment")]
        public PatientGrantByIdAudit_CreateComment CreateComment;
    }

}
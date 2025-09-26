using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class CitDashbCitCondSelfWidgVisi_Links
    {
        [JsonProperty("audit")]
        public CitDashbCitCondSelfWidgVisi_Audit Audit;

        [JsonProperty("activate")]
        public CitDashbCitCondSelfWidgVisi_Activate Activate;

        [JsonProperty("self")]
        public CitDashbCitCondSelfWidgVisi_Self Self;

        [JsonProperty("relatedActivities")]
        public CitDashbCitCondSelfWidgVisi_RelatedActivities RelatedActivities;

        [JsonProperty("deactivate")]
        public CitDashbCitCondSelfWidgVisi_Deactivate Deactivate;

        [JsonProperty("visit")]
        public CitDashbCitCondSelfWidgVisi_Visit Visit;
    }

}
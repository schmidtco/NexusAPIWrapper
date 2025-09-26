using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class CitDashbCitCondSelfWidgCond_Links
    {
        [JsonProperty("self")]
        public CitDashbCitCondSelfWidgCond_Self Self;

        [JsonProperty("relatedActivities")]
        public CitDashbCitCondSelfWidgCond_RelatedActivities RelatedActivities;

        [JsonProperty("linkWithGrant")]
        public LinkWithGrant LinkWithGrant;
    }

}
using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class Preferences_Root
    {
        [JsonProperty("HCL_REPAIR")]
        public List<object> HCLREPAIR;

        [JsonProperty("HCL_ORDER")]
        public List<object> HCLORDER;

        [JsonProperty("CROSS_CITIZEN_DATA")]
        public List<Preferences_CROSSCITIZENDATA> CROSSCITIZENDATA;

        [JsonProperty("MEDCOM_LIST")]
        public List<object> MEDCOMLIST;

        [JsonProperty("DEFAULT_PAGE")]
        public List<object> DEFAULTPAGE;

        [JsonProperty("HCL_PRODUCT_ITEM")]
        public List<object> HCLPRODUCTITEM;

        [JsonProperty("CROSS_CITIZEN_DASHBOARD")]
        public List<Preferences_CROSSCITIZENDASHBOARD> CROSSCITIZENDASHBOARD;

        [JsonProperty("NAB_FLOW")]
        public List<object> NABFLOW;

        [JsonProperty("CROSS_CITIZEN_CALENDAR")]
        public List<Preferences_CROSSCITIZENCALENDAR> CROSSCITIZENCALENDAR;

        [JsonProperty("ACTIVITY_LIST")]
        public List<Preferences_ACTIVITYLIST> ACTIVITYLIST;

        [JsonProperty("PROGRAM_PATHWAY")]
        public List<object> PROGRAMPATHWAY;

        [JsonProperty("CITIZEN_LIST")]
        public List<Preferences_CITIZENLIST> CITIZENLIST;

        [JsonProperty("PHYSICAL_RESOURCE_ASSIGNMENT_LIST")]
        public List<Preferences_PHYSICALRESOURCEASSIGNMENTLIST> PHYSICALRESOURCEASSIGNMENTLIST;

        [JsonProperty("SMDB_FLOW")]
        public List<object> SMDBFLOW;

        [JsonProperty("MOBILE_PROFILE")]
        public List<Preferences_MOBILEPROFILE> MOBILEPROFILE;

        [JsonProperty("PATHWAY_DISTRIBUTION_LIST")]
        public List<object> PATHWAYDISTRIBUTIONLIST;

        [JsonProperty("CATALOG_GRANT")]
        public List<Preferences_CATALOGGRANT> CATALOGGRANT;

        [JsonProperty("TEXT_WIDGET")]
        public List<object> TEXTWIDGET;
    }

}
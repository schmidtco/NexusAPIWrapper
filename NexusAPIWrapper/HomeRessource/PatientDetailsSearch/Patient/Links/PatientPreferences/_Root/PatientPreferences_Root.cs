using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class PatientPreferences_Root
    {
        [JsonProperty("EOP_VISITATION")]
        public List<object> EOPVISITATION;

        [JsonProperty("MOBILITY_AIDS")]
        public List<object> MOBILITYAIDS;

        [JsonProperty("SUNDHEDSJOURNALEN_LINK")]
        public List<object> SUNDHEDSJOURNALENLINK;

        [JsonProperty("CITIZEN_FINANCE_INSIGHT")]
        public List<PatientPreferences_CITIZENFINANCEINSIGHT> CITIZENFINANCEINSIGHT;

        [JsonProperty("EOP_SUBGOAL")]
        public List<object> EOPSUBGOAL;

        [JsonProperty("CITIZEN_CALENDAR")]
        public List<PatientPreferences_CITIZENCALENDAR> CITIZENCALENDAR;

        [JsonProperty("CITIZEN_DASHBOARD")]
        public List<PatientPreferences_CITIZENDASHBOARD> CITIZENDASHBOARD;

        [JsonProperty("ACTIVITY_DASHBOARD")]
        public List<object> ACTIVITYDASHBOARD;

        [JsonProperty("ASSIGNMENT")]
        public List<object> ASSIGNMENT;

        [JsonProperty("MEASUREMENT")]
        public List<PatientPreferences_MEASUREMENT> MEASUREMENT;

        [JsonProperty("PATIENT_CONDITION")]
        public List<PatientPreferences_PATIENTCONDITION> PATIENTCONDITION;

        [JsonProperty("MOBILE_PROFILE")]
        public List<PatientPreferences_MOBILEPROFILE> MOBILEPROFILE;

        [JsonProperty("EOP_PROBLEM")]
        public List<object> EOPPROBLEM;

        [JsonProperty("CITIZEN_PATHWAY")]
        public List<PatientPreferences_CITIZENPATHWAY> CITIZENPATHWAY;

        [JsonProperty("CITIZEN_DATA")]
        public List<PatientPreferences_CITIZENDATA> CITIZENDATA;
    }

}
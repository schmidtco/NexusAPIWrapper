using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class ProfessionalConfiguration_SmdbConfiguration
    {
        [JsonProperty("doctorInDrugTreatment")]
        public bool DoctorInDrugTreatment;

        [JsonProperty("_links")]
        public ProfessionalConfiguration_Links Links;
    }

}
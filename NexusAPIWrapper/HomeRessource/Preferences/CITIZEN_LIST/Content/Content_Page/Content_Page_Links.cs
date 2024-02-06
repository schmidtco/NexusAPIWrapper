using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class Content_Page_Links
    {
        [JsonProperty("availableCountries")]
        public Content_Page_AvailableCountries AvailableCountries;

        [JsonProperty("searchPostalDistrict")]
        public Content_Page_SearchPostalDistrict SearchPostalDistrict;

        [JsonProperty("self")]
        public Content_Page_Self Self;

        [JsonProperty("patientOverview")]
        public Content_Page_PatientOverview PatientOverview;

        [JsonProperty("citizenOverviewForms")]
        public Content_Page_CitizenOverviewForms CitizenOverviewForms;

        [JsonProperty("measurementData")]
        public Content_Page_MeasurementData MeasurementData;

        [JsonProperty("measurementInstructions")]
        public Content_Page_MeasurementInstructions MeasurementInstructions;

        [JsonProperty("patientConditions")]
        public Content_Page_PatientConditions PatientConditions;

        [JsonProperty("patientOrganizations")]
        public Content_Page_PatientOrganizations PatientOrganizations;

        [JsonProperty("activityLinksPrototypes")]
        public Content_Page_ActivityLinksPrototypes ActivityLinksPrototypes;

        [JsonProperty("pdfImage")]
        public Content_Page_PdfImage PdfImage;

        [JsonProperty("imageDownload")]
        public Content_Page_ImageDownload ImageDownload;

        [JsonProperty("imageThumbnail")]
        public Content_Page_ImageThumbnail ImageThumbnail;
    }

}
using Newtonsoft.Json; 
using System; 
namespace NexusAPIWrapper{ 

    public class Content_Page_CurrentAddress
    {
        [JsonProperty("addressLine1")]
        public string AddressLine1;

        [JsonProperty("addressLine2")]
        public string AddressLine2;

        [JsonProperty("addressLine3")]
        public string AddressLine3;

        [JsonProperty("addressLine4")]
        public object AddressLine4;

        [JsonProperty("addressLine5")]
        public object AddressLine5;

        [JsonProperty("administrativeAreaCode")]
        public string AdministrativeAreaCode;

        [JsonProperty("countryCode")]
        public string CountryCode;

        [JsonProperty("postalCode")]
        public string PostalCode;

        [JsonProperty("postalDistrict")]
        public string PostalDistrict;

        [JsonProperty("restricted")]
        public bool? Restricted;

        [JsonProperty("geoCoordinates")]
        public Content_Page_GeoCoordinates GeoCoordinates;

        [JsonProperty("startDate")]
        public DateTime? StartDate;

        [JsonProperty("endDate")]
        public DateTime? EndDate;

        [JsonProperty("_links")]
        public Content_Page_Links Links;
    }

}
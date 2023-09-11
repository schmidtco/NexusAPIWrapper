using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class Professional_PrimaryAddress
    {
        [JsonProperty("addressLine1")]
        public string AddressLine1;

        [JsonProperty("addressLine2")]
        public object AddressLine2;

        [JsonProperty("addressLine3")]
        public object AddressLine3;

        [JsonProperty("addressLine4")]
        public object AddressLine4;

        [JsonProperty("addressLine5")]
        public object AddressLine5;

        [JsonProperty("administrativeAreaCode")]
        public object AdministrativeAreaCode;

        [JsonProperty("countryCode")]
        public string CountryCode;

        [JsonProperty("postalCode")]
        public string PostalCode;

        [JsonProperty("postalDistrict")]
        public object PostalDistrict;

        [JsonProperty("restricted")]
        public bool Restricted;

        [JsonProperty("geoCoordinates")]
        public Professional_GeoCoordinates GeoCoordinates;

        [JsonProperty("startDate")]
        public object StartDate;

        [JsonProperty("endDate")]
        public object EndDate;

        [JsonProperty("_links")]
        public Professional_Links Links;
    }

}
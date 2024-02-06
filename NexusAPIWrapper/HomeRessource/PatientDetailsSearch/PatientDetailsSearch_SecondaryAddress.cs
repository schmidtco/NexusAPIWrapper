using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientDetails_SecondaryAddress
    {
        [JsonProperty("addressLine1")]
        public object AddressLine1;

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
        public object CountryCode;

        [JsonProperty("postalCode")]
        public object PostalCode;

        [JsonProperty("postalDistrict")]
        public object PostalDistrict;

        [JsonProperty("restricted")]
        public bool Restricted;

        [JsonProperty("geoCoordinates")]
        public object GeoCoordinates;

        [JsonProperty("startDate")]
        public object StartDate;

        [JsonProperty("endDate")]
        public object EndDate;

        [JsonProperty("_links")]
        public PatientDetails_Links Links;
    }

}
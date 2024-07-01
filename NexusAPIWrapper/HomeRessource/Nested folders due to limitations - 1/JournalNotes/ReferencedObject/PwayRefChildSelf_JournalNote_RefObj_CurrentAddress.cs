using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PwayRefChildSelf_JournalNote_RefObj_CurrentAddress
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
        public string PostalDistrict;

        [JsonProperty("restricted")]
        public bool? Restricted;

        [JsonProperty("geoCoordinates")]
        public PwayRefChildSelf_JournalNote_RefObj_GeoCoordinates GeoCoordinates;

        [JsonProperty("startDate")]
        public object StartDate;

        [JsonProperty("endDate")]
        public object EndDate;

        [JsonProperty("_links")]
        public PwayRefChildSelf_JournalNote_RefObj_Links Links;
    }

}
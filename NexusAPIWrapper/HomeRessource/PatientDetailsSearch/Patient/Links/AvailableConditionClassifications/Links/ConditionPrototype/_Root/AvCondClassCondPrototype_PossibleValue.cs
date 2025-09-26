using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class AvCondClassCondPrototype_PossibleValue
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("code")]
        public string Code;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("additionalInformation")]
        public object AdditionalInformation;

        [JsonProperty("active")]
        public bool? Active;

        [JsonProperty("numericRepresentation")]
        public object NumericRepresentation;

        [JsonProperty("marker")]
        public object Marker;

        [JsonProperty("_links")]
        public AvCondClassCondPrototype_Links Links;
    }

}
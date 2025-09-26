using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class PatientConditions_State
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
        public PatientConditions_Links Links;
    }

}
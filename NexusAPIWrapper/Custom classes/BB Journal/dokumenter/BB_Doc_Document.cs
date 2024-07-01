using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class BB_Doc_Document
    {
        [JsonProperty("dato_opret")]
        public string DatoOpret;

        [JsonProperty("navn_medarbejder")]
        public string NavnMedarbejder;

        [JsonProperty("navn_godkender")]
        public string NavnGodkender;

        [JsonProperty("dato_godkendelse")]
        public string DatoGodkendelse;

        [JsonProperty("beskrivelse")]
        public string Beskrivelse;

        [JsonProperty("filnavn")]
        public string Filnavn;

        [JsonProperty("lbnummer")]
        public string Lbnummer;
    }

}
using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class BB_Dagbogsnote
    {
        [JsonProperty("dato")]
        public string Dato;

        [JsonProperty("tidspunkt")]
        public string Tidspunkt;

        [JsonProperty("medarbejder")]
        public string Medarbejder;

        [JsonProperty("dag_overskrift")]
        public string DagOverskrift;

        [JsonProperty("dag_tekst")]
        public string DagTekst;

        [JsonProperty("fok_tekst")]
        public string FokTekst;

        [JsonProperty("dagbogsnote_id")]
        public string DagbogsnoteId;
    }

}
using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class BB_Note_Doc_Dokument
    {
        [JsonProperty("dokument_dato")]
        public string DokumentDato;

        [JsonProperty("dokument")]
        public string Dokument;

        [JsonProperty("borger")]
        public string Borger;

        [JsonProperty("dok_dagbogsnote_id")]
        public string DokDagbogsnoteId;
    }

}
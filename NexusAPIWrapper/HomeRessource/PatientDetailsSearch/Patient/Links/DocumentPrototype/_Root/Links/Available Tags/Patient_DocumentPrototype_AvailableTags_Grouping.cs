using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class Patient_DocumentPrototype_AvailableTags_Grouping
    {
        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("_links")]
        public Patient_DocumentPrototype_AvailableTags_Links Links;
    }

}
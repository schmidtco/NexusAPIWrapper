using Newtonsoft.Json; 
using System.Collections.Generic; 
namespace NexusAPIWrapper{ 

    public class FormDataPrototype_Item
    {
        [JsonProperty("type")]
        public string Type;

        [JsonProperty("id")]
        public int? Id;

        [JsonProperty("version")]
        public int? Version;

        [JsonProperty("label")]
        public string Label;

        [JsonProperty("calculationType")]
        public object CalculationType;

        [JsonProperty("tags")]
        public List<object> Tags;

        [JsonProperty("importantInformation")]
        public bool? ImportantInformation;

        [JsonProperty("value")]
        public object Value;

        [JsonProperty("tooltip")]
        public object Tooltip;

        [JsonProperty("unit")]
        public object Unit;

        [JsonProperty("required")]
        public bool? Required;

        [JsonProperty("systemManaged")]
        public bool? SystemManaged;

        [JsonProperty("markup")]
        public object Markup;

        [JsonProperty("conceptName")]
        public string ConceptName;

        [JsonProperty("_links")]
        public FormDataPrototype_Links Links;
    }

}
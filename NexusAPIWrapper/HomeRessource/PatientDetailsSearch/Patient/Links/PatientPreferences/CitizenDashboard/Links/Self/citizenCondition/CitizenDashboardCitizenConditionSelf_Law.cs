using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class CitizenDashboardCitizenConditionSelf_Law
    {
        [JsonProperty("classificationLaw")]
        public string ClassificationLaw;

        [JsonProperty("name")]
        public string Name;

        [JsonProperty("conditionClassificationLaw")]
        public string ConditionClassificationLaw;

        [JsonProperty("_links")]
        public CitizenDashboardCitizenConditionSelf_Links Links;
    }

}
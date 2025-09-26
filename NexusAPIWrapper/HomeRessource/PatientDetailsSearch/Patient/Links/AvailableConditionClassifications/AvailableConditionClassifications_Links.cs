using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class AvailableConditionClassifications_Links
    {
        [JsonProperty("newObservationsPrototype")]
        public AvailableConditionClassifications_NewObservationsPrototype NewObservationsPrototype;

        [JsonProperty("conditionPrototype")]
        public AvailableConditionClassifications_ConditionPrototype ConditionPrototype;
    }

}
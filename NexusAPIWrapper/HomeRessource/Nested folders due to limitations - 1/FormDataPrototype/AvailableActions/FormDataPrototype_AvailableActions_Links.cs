using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class FormDataPrototype_AvailableActions_Links
    {
        [JsonProperty("createFormData")]
        public FormDataPrototype_AvailableActions_CreateFormData CreateFormData;

        [JsonProperty("shareAsClinicalEmail")]
        public FormDataPrototype_AvailableActions_ShareAsClinicalEmail ShareAsClinicalEmail;
    }

}
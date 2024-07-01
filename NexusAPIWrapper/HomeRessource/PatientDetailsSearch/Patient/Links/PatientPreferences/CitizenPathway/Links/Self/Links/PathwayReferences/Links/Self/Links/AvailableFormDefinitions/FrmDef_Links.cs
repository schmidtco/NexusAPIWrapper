using Newtonsoft.Json; 
namespace NexusAPIWrapper{ 

    public class FrmDef_Links
    {
        [JsonProperty("formDataPrototype")]
        public FrmDef_FormDataPrototype FormDataPrototype;

        [JsonProperty("createFormDefinitionReference")]
        public FrmDef_CreateFormDefinitionReference CreateFormDefinitionReference;
    }

}
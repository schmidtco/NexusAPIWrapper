using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusAPIWrapper
{
    public class WebRequest
    {
        public RestClient restClient { get; set; }
        public RestClientOptions clientOptions { get; set; }
        public RestRequest request { get; set; }
        public RestResponse response { get; set; }
        
        public WebRequest(string baseURL, string endpointURL, string requestMethod) 
        {
            
            clientOptions = new RestClientOptions(baseURL);
            restClient = new RestClient(clientOptions);
            switch (requestMethod.ToUpper())
            {
                case "GET":
                    request = new RestRequest(endpointURL, Method.Get);
                    break;
                case "POST":
                    request = new RestRequest(endpointURL, Method.Post);
                    break;
                default:
                    throw new Exception("Request method unknown");
            }
            
            
        }

        public RestResponse ExecuteRequest(string method)
        {
            return restClient.ExecuteGet(request);
            restClient.executepo
        }
    }
}

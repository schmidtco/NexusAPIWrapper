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
        public RestSharp.Method method { get; set; }
        
        public WebRequest(string baseURL, string endpointURL, RestSharp.Method requestMethod) 
        {
            
            clientOptions = new RestClientOptions(baseURL);
            restClient = new RestClient(clientOptions);
            method = requestMethod;
            
            request = new RestRequest(endpointURL,requestMethod);
            
        }

        public RestResponse Execute(RestSharp.Method requestMethod)
        {
            switch (requestMethod)
            {
                case Method.Get:
                    return restClient.ExecuteGet(request);
                case Method.Post:
                    return restClient.ExecutePost(request);
                case Method.Put:
                    return restClient.ExecutePut(request);
                case Method.Delete:
                    return restClient.Delete(request);
                case Method.Head:
                    return restClient.Head(request);
                case Method.Options:
                    return restClient.Options(request);
                case Method.Patch:
                    return restClient.Patch(request);
                default:
                    throw new NotImplementedException("Request method not implemented.");
            }
        }


    }
}

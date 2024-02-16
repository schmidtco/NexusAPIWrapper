using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        public string accessToken { get; set; }
        public RestSharp.Method method { get; set; }

        public WebRequest(string baseURL, string endpointURL, RestSharp.Method requestMethod)
        {
            //this requires access token to be set manually
            clientOptions = new RestClientOptions(baseURL);
            restClient = new RestClient(clientOptions);
            method = requestMethod;

            request = new RestRequest(endpointURL);

        }
        public WebRequest(string baseURL, string endpointURL, RestSharp.Method requestMethod, string accessToken)
        {

            clientOptions = new RestClientOptions(baseURL);
            restClient = new RestClient(clientOptions);
            method = requestMethod;

            request = new RestRequest(endpointURL);
            this.accessToken = accessToken;

        }
        /// <summary>
        /// Executing the web request based on the method input
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void Execute()
        {
            switch (method)
            {
                case Method.Get:
                    response = restClient.ExecuteGet(request);
                    break;
                case Method.Post:
                    response = restClient.ExecutePost(request);
                    break;
                case Method.Put:
                    response = restClient.ExecutePut(request);
                    break;
                case Method.Delete:
                    response = restClient.Delete(request);
                    break;
                case Method.Head:
                    response = restClient.Head(request);
                    break;
                case Method.Options:
                    response = restClient.Options(request);
                    break;
                case Method.Patch:
                    response = restClient.Patch(request);
                    break;
                default:
                    throw new NotImplementedException("Request method not implemented.");
            }
        }
        /// <summary>
        /// Adding the Bearer token and json application content type
        /// </summary>
        public void AddStandardHeaders()
        {
            AddBearerToken();
            AddContentTypeJson();
        }
        public void AddBearerToken()
        {
            request.AddHeader("Authorization", $"Bearer {this.accessToken}");
        }
        public void AddContentTypeJson()
        {
            request.AddHeader("Content-Type", "application/json");
        }
        public void AddAcceptTypeXML()
        {
            request.AddHeader("Accept", "application/xhtml+xml");
        }
        public void AddHeaderContentType(string contentType)
        {
            request.AddHeader("Content-Type", contentType);
        }


    }
}
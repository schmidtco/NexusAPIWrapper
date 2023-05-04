using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using System.Text;

namespace NexusAPIWrapper
{
    public class NexusResult
    {
        public HttpStatusCode httpStatusCode { get; private set; }
        public String httpStatusText { get; private set; }
        public Object Result { get; private set; }
        public SortedDictionary<string, string> dict { get; set; }


       public NexusResult(HttpStatusCode statuscode, String statustext, Object result)
        {
            httpStatusCode  = statuscode;
            httpStatusText  = statustext;
            Result          = result;
        }
        public NexusResult()
        {
            
        }

        #region Shared methods
        
        



        #endregion Shared methods
        #region HomeRessource API calls

        public void GetPatientDetailsByCPR(NexusAPI api, string CitizenCPR)
        /*
                * This one uses the WebRequest class RestClient setup
                * It uses the RestClient like the manual version - except the setup is done automatically.
                * You only need to add headers and body, before running the Execute method.
                */
        {
            WebRequest webRequest = new WebRequest(api.clientCredentials.Host, api.GetPatientDetailsSearchLink(), Method.Post);
            webRequest.request.AddHeader("Authorization", $"Bearer {api.tokenObject.AccessToken}");
            webRequest.request.AddHeader("Content-Type", "application/json");
            JObject jsonBody = new JObject();
            jsonBody.Add("businessKey", $"{CitizenCPR}");
            jsonBody.Add("keyType", "CPR");
            string requestBody = "{" + $"{jsonBody.First}, {jsonBody.Last}" + "}";
            webRequest.request.AddJsonBody(requestBody);
            webRequest.Execute();
            //NexusHomeRessource nexusHomeRessource = new NexusHomeRessource(_clientCredentials.url,_tokenObject.AccessToken);
            if (webRequest.response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(webRequest.response.StatusDescription);
            }
            else
            {
                NexusResult result = new NexusResult(
                    webRequest.response.StatusCode,
                    webRequest.response.StatusDescription,
                    webRequest.response.Content);
                
                api.result = result;
            }

        }
        //public NexusResult GetPatientDetailsByCPR(NexusAPI api, string CitizenCPR)
        ///*
        //        * This one uses the WebRequest class RestClient setup
        //        * It uses the RestClient like the manual version - except the setup is done automatically.
        //        * You only need to add headers and body, before running the Execute method.
        //        */
        //{
        //    WebRequest webRequest = new WebRequest(api.clientCredentials.url, api.GetPatientDetailsSearchLink(), Method.Post);
        //    webRequest.request.AddHeader("Authorization", $"Bearer {api.tokenObject.AccessToken}");
        //    webRequest.request.AddHeader("Content-Type", "application/json");
        //    JObject jsonBody = new JObject();
        //    jsonBody.Add("businessKey", $"{CitizenCPR}");
        //    jsonBody.Add("keyType", "CPR");
        //    string requestBody = "{" + $"{jsonBody.First}, {jsonBody.Last}" + "}";
        //    webRequest.request.AddJsonBody(requestBody);
        //    webRequest.Execute();
        //    //NexusHomeRessource nexusHomeRessource = new NexusHomeRessource(_clientCredentials.url,_tokenObject.AccessToken);
        //    if (webRequest.response.StatusCode != System.Net.HttpStatusCode.OK)
        //    {
        //        throw new Exception(webRequest.response.StatusDescription);
        //    }
        //    else
        //    {
        //        NexusResult result = new NexusResult(
        //            webRequest.response.StatusCode,
        //            webRequest.response.StatusDescription,
        //            webRequest.response.Content);
        //        return result;
        //    }

        //}
        

        #region PatientDetailsSearch sub calls

        
        public void GetPatientPreferences(NexusAPI api, string CitizenCPR)
        {
            WebRequest webRequest = new WebRequest(api.clientCredentials.Host, api.GetPatientPreferencesLink_ByCPR(CitizenCPR), Method.Get, api.tokenObject.AccessToken);
            webRequest.AddBearerToken();
            webRequest.Execute();
            //NexusHomeRessource nexusHomeRessource = new NexusHomeRessource(_clientCredentials.url,_tokenObject.AccessToken);
            if (webRequest.response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(webRequest.response.StatusDescription);
            }
            else
            {
                NexusResult result = new NexusResult(
                    webRequest.response.StatusCode,
                    webRequest.response.StatusDescription,
                    webRequest.response.Content);
                api.result = result;
            }
        }

        public void GetCitizenPathwayInfo_ByCPR(NexusAPI api, string CitizenCPR, string pathwayName)
        {
            WebRequest webRequest = StandardWebRequest(api,api.GetCitizenPathwayLink_ByCPR(CitizenCPR,pathwayName),Method.Get);
            
            webRequest.Execute();
            if (webRequest.response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(webRequest.response.StatusDescription);
            }
            else
            {
                NexusResult result = new NexusResult(
                    webRequest.response.StatusCode,
                    webRequest.response.StatusDescription,
                    webRequest.response.Content);
                api.result = result;
            }
        }

        #endregion PatientDetailsSearch sub calls


        #endregion HomeRessource API calls
        #region Shared methods

        public WebRequest StandardWebRequest(NexusAPI api, string endpointURL, Method method)
        {
            WebRequest webRequest = new WebRequest(api.clientCredentials.Host, endpointURL, method, api.tokenObject.AccessToken);
            webRequest.AddBearerToken();

            return webRequest;
        }


        #endregion Shared methods
    }
}

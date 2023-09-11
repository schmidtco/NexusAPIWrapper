using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.CSharp;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace NexusAPIWrapper
{
    public class NexusHomeRessource
    {
        #region Properties
        string url;
        string accessToken;
        const string homeRessourceEndpointURL = "/api/core/mobile/ringsted/v2/";

        public SortedDictionary<string, string> Links;

        #endregion
        //public NexusHomeRessource()
        //{
        //    Links = new SortedDictionary<string, string>();
        //}
        
        public NexusHomeRessource(string url, string accessToken)
        {
            this.url = url;
            this.accessToken = accessToken;
            Links = new SortedDictionary<string, string>();
            //GetHomeRessource();
        }
        public void AddResource(string linkName, string href)
        {
            if (Links.ContainsKey(linkName))
                return;
            Links.Add(linkName, href);
        }
        public NexusResult GetHomeRessource() 
        {
            //string homeRessourceEndpoint = homeRessourceEndpointURL;
            var options = new RestClientOptions(this.url)
                {
                MaxTimeout = -1,
            };
            var restClient = new RestClient(options);
            var request = new RestRequest(homeRessourceEndpointURL);
            request.AddHeader("Authorization", $"Bearer {accessToken}");
            RestResponse response = restClient.ExecuteGet(request);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(response.StatusDescription);
            }
            else
            {
                NexusResult result = new NexusResult(
                    response.StatusCode, 
                    response.StatusDescription, 
                    MakeResult(response.Content));
                return result;
            }


        }
        public SortedDictionary<string, string> MakeResult(string response)
        {
            dynamic result = JObject.Parse(response);
            dynamic links = result["_links"];
            //NexusHomeRessource Ressources = new NexusHomeRessource(url, accessToken);

            foreach (var item in links)
            {
                string ressourceName = Convert.ToString(item.Name);
                string href = Convert.ToString(item.First["href"]);
                this.AddResource(ressourceName, href);
            }
            return Links;
        }
        //public NexusHomeRessource MakeResult(string response)
        //{
        //    dynamic result = JObject.Parse(response);
        //    dynamic links = result["_links"];
        //    NexusHomeRessource Ressources = new NexusHomeRessource(url, accessToken);

        //    foreach (var item in links)
        //    {
        //        string ressourceName = Convert.ToString(item.Name);
        //        string href = Convert.ToString(item.First["href"]);
        //        Ressources.AddResource(ressourceName, href);
        //    }
        //    return Ressources;
        //}
    }

    
}

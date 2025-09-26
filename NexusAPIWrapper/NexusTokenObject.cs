using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CSharp;
using System.Runtime.CompilerServices;
using CsQuery.ExtensionMethods.Internal;

namespace NexusAPIWrapper
{
    public class NexusTokenObject
    {
        #region Properties
        static Timer refreshTokenTimer;
        //const string tokenEndpointURL = "/authx/realms/ringsted/.well-known/openid-configuration";
        //const string tokenEndpointURL = "https://iam.nexus.kmd.dk/authx/realms/ringsted/.well-known/openid-configuration";
        //const string tokenEndpointURL = "/authx/realms/ringsted/protocol/openid-connect/token";

        string _url;
        string _clientID;
        string _clientSecret;

        string _access_token;
        int _expires_in;
        string _token_type;
        int _not_before_policy;
        string _session_state;

        ClientCredentials credentials;


        public string AccessToken
        {
            get => string.IsNullOrEmpty(_access_token) ? throw new Exception("_access_token") : _access_token;
            private set => _access_token = value;
        }

        public int ExpiresIn
        {
            get => _expires_in;
            private set => _expires_in = value;
        }
        
        public string TokenType
        {
            get => string.IsNullOrEmpty(_token_type) ? throw new Exception("_token_type") : _token_type;
            private set => _token_type = value;
        }
        public int NotBeforePolicy
        {
            get => _not_before_policy;
            private set => _not_before_policy = value;
        }
        public string SessionState
        {
            get => string.IsNullOrEmpty(_session_state) ? throw new Exception("_session_state") : _session_state;
            private set => _session_state = value;
        }
        public string url { get => string.IsNullOrEmpty(_url) ? throw new Exception("_Url") : _url; private set => _url = value; }
        public string clientID
        {
            get => string.IsNullOrEmpty(_clientID) ? throw new Exception("_clientID") : _clientID;
            private set => _clientID = value;
        }
        public string clientSecret
        {
            get => string.IsNullOrEmpty(_clientSecret) ? throw new Exception("_clientSecret") : _clientSecret;
            private set => _clientSecret = value;
        }

        #endregion


        public NexusTokenObject(ClientCredentials credentials)
        {
            this.credentials = credentials;
            url = credentials.Host;
            clientID = credentials.Client_id;
            clientSecret = credentials.Client_secret;

            CheckProperties();
        }
        //public NexusTokenObject(string url, string clientid, string secret)
        //{
        //    this.url = url;
        //    clientID = clientid;
        //    clientSecret = secret;

        //    CheckProperties();
        //}

        private void CheckProperties()
        {
            if (string.IsNullOrEmpty(_access_token))
            {
                GetAccessTokenFromNexus();
            }
        }
        private void SetProperties(string response)
        {

            dynamic result = JObject.Parse(response);
            this.AccessToken = Convert.ToString(result["access_token"]);
            this.ExpiresIn = Convert.ToInt32(result["expires_in"]);
            this.TokenType = Convert.ToString(result["token_type"]);
            this.NotBeforePolicy = Convert.ToInt32(result["not-before-policy"]);
            this.SessionState = Convert.ToString(result["session_state"]);
        }

        //public void GetToken(bool userefreshtoken) // BOOL to be removed
        //{
        //    var options = new RestClientOptions(url)
        //    {
        //        MaxTimeout = -1,
        //    };
        //    string grantType = userefreshtoken ? "refresh_token" : "client_credentials"; // change to only client_credentials
        //    var client = new RestClient(options);
        //    var request = new RestRequest(tokenEndpointURL, Method.Post);
        //    request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
        //    request.AddParameter("grant_type", $"{grantType}");
        //    request.AddParameter("client_id", clientID);
        //    request.AddParameter("client_secret", clientSecret);
        //    if (userefreshtoken) // to be removed
        //    { // to be removed
        //        request.AddParameter("refresh_token", $"{this.RefreshToken}"); // to be removed
        //    } // to be removed

        //    RestResponse response = client.ExecutePost(request);
        //    if (response.StatusCode != System.Net.HttpStatusCode.OK)
        //    {
        //        throw new Exception(response.StatusDescription);
        //    }
        //    else
        //    {
        //        SetProperties(response.Content);
        //        SetTimer();
        //    }
        //}
        public void GetToken() 
        {
            
            var options = new RestClientOptions(url)
            {
                MaxTimeout = -1,
            };
            
            string grantType = "client_credentials"; 
            var client = new RestClient(options);
            string tokenEnpoint = credentials.Token_endpoint; //GetTokenEndpoint();
            var request = new RestRequest(tokenEnpoint, Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", $"{grantType}");
            request.AddParameter("client_id", clientID);
            request.AddParameter("client_secret", clientSecret);

            RestResponse response = client.ExecutePost(request);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(response.StatusDescription);
            }
            else
            {
                SetProperties(response.Content);
                SetTimer();
            }
        }
        public string GetTokenEndpoint()
        {
            string host = credentials.Host;
            string tokenEndpoint = credentials.Token_endpoint;
            string newTokenEndpoint = tokenEndpoint.After("kmd.dk");
            return tokenEndpoint;
        }
        private void SetTimer()
        {
            refreshTokenTimer = new Timer(_ => GetAccessTokenFromNexus(), null, this.ExpiresIn * 900, Timeout.Infinite);
        }


        
        void GetAccessTokenFromNexus()
        {
            GetToken();
        }
        //void RefreshAccessToken()
        //{
        //    GetToken(true);
        //}
    }
}

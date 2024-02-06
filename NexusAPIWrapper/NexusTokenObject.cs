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

namespace NexusAPIWrapper
{
    public class NexusTokenObject
    {
        #region Properties
        static Timer refreshTokenTimer;
        const string tokenEndpointURL = "/authx/realms/ringsted/protocol/openid-connect/token";

        string _url;
        string _clientID;
        string _clientSecret;

        string _access_token;
        int _expires_in;
        int _refresh_expires_in;
        string _refresh_token;
        string _token_type;
        int _not_before_policy;
        string _session_state;


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
        public int RefreshExpiresIn
        {
            get => _refresh_expires_in;
            private set => _refresh_expires_in = value;
        }
        public string RefreshToken
        {
            get => string.IsNullOrEmpty(_refresh_token) ? throw new Exception("_refresh_token") : _refresh_token;
            private set => _refresh_token = value;
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
            this.RefreshExpiresIn = Convert.ToInt32(result["refresh_expires_in"]);
            this.RefreshToken = Convert.ToString(result["refresh_token"]);
            this.TokenType = Convert.ToString(result["token_type"]);
            this.NotBeforePolicy = Convert.ToInt32(result["not-before-policy"]);
            this.SessionState = Convert.ToString(result["session_state"]);
        }

        public void GetToken(bool userefreshtoken)
        {
            var options = new RestClientOptions(url)
            {
                MaxTimeout = -1,
            };
            string grantType = userefreshtoken ? "refresh_token" : "client_credentials";
            var client = new RestClient(options);
            var request = new RestRequest(tokenEndpointURL, Method.Post);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", $"{grantType}");
            request.AddParameter("client_id", clientID);
            request.AddParameter("client_secret", clientSecret);
            if (userefreshtoken)
            {
                request.AddParameter("refresh_token", $"{this.RefreshToken}");
            }

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

        private void SetTimer()
        {
            refreshTokenTimer = new Timer(new TimerCallback(_RefreshToken)); //Callback method to be called, when the timer expires. This will use the refresh token to get a new access token
            refreshTokenTimer.Change(this.ExpiresIn * 1000, 0); // Multiplied by 1000 to convert from seconds to milliseconds which is used in the timer
        }

        private void _RefreshToken(object state)
        {
            refreshTokenTimer.Dispose();
            RefreshAccessToken();
        }
        void GetAccessTokenFromNexus()
        {
            GetToken(false);
        }
        void RefreshAccessToken()
        {
            GetToken(true);
        }
    }
}

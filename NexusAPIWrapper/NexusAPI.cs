using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace NexusAPIWrapper
{
    public class NexusAPI
    {
        #region Properties
        

        

        private NexusTokenObject _tokenObject;
        private NexusHomeRessource _ressource;
        private ClientCredentials _clientCredentials;


        public NexusTokenObject tokenObject
        {
            get => _tokenObject;
        }
        #endregion

        public NexusAPI()
        {
            _clientCredentials = new ClientCredentials();
            _tokenObject = new NexusTokenObject(_clientCredentials.url, _clientCredentials.clientId, _clientCredentials.clientSecret);
            _ressource = GetHomeRessource();
        }

        public NexusHomeRessource GetHomeRessource()
        {
            NexusHomeRessource nexusHomeRessource = new NexusHomeRessource(_clientCredentials.url, _tokenObject.AccessToken);
            return nexusHomeRessource;
        }
        public void CheckTokens()
        {

        }

        public string GetHomeRessourceLink(string linkName)
        {
            if (_ressource.Links.Count == 0)
            {
                _ressource.MakeResult(_ressource.GetHomeRessource().ToString());
                return _ressource.Links[linkName];
            }
            else
            {
                return _ressource.Links[linkName];
            }
            
        }
        public string GetPatientDetailsSearchLink()
        {
            return GetHomeRessourceLink("patientDetailsSearch");
        }
        
        public NexusResult GetPatientDetailsByCPR(string CitizenCPR)
        {
            WebRequest request = new WebRequest(_clientCredentials.url, GetPatientDetailsSearchLink(), Method.Get);
            request.Execute(Method.Get);
            NexusHomeRessource nexusHomeRessource = new NexusHomeRessource();
            NexusResult result = new NexusResult(request.response.StatusCode,request.response.StatusDescription,nexusHomeRessource.MakeResult(request.response.ToString()));
            return result;
            
        }
    }
}

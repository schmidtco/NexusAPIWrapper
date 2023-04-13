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
            return _ressource.Links[linkName];
        }
        public string GetPatientDetailsSearchLink()
        {
            return GetHomeRessourceLink("patientDetailsSearch");
        }
        
        public void GetPatientDetails(string CitizenCPR)
        {

        }
    }
}

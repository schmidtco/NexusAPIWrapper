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




        #endregion

        public NexusAPI()
        {
            
        }

        NexusTokenObject tokenObject = new NexusTokenObject(liveURL, clientId, reviewClientSecret);
        public void GetHomeRessource(string environment)
        {
            if (environment.ToLower() == "live") 
            {
                NexusHomeRessource nexusHomeRessource = new NexusHomeRessource(liveURL);
            }
            else
            {
                NexusHomeRessource nexusHomeRessource = new NexusHomeRessource(reviewURL);
            }
            
        }
    }
}

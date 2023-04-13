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
        const string reviewURL = "https://ringsted.nexus-review.dk";
        const string liveURL = "https://ringsted.nexus.dk";

        const string clientId = "Ringsted_client";

        const string reviewClientSecret = "";
        const string liveClientSecret = "";

        

        private NexusTokenObject _tokenObject;




        #endregion

        public void GetHomeRessource(string environment)
        {
            RestClient restClient = new RestSharp.RestClient();
            restClient.op
        }
    }
}

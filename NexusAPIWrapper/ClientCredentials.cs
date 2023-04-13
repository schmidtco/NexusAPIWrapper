using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusAPIWrapper
{
    public  class ClientCredentials
    {
        const string reviewURL = "https://ringsted.nexus-review.dk";
        const string liveURL = "https://ringsted.nexus.dk";

        const string clientId = "Ringsted_client";

        const string reviewClientSecret = "";
        const string liveClientSecret = "";

        string connectionString = "Provider=SQLOLEDB;Password=Sol@1427;Persist Security Info=True;User ID=rpasql01;Initial Catalog=RKSQLRPA01;Data Source=RKSQL03";
        string SQLQuery = "SELECT * from Nexus_API_Client_Credentials WHERE environment = '%environment%'";
        public ClientCredentials()
        {
            
        }
    }
}

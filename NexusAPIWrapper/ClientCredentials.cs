using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NexusAPIWrapper
{
    public  class ClientCredentials
    {
        const string reviewURL = "https://ringsted.nexus-review.dk";
        const string liveURL = "https://ringsted.nexus.dk";

        const string cclientId = "Ringsted_client";

        const string reviewClientSecret = "";
        const string liveClientSecret = "";

        static readonly string connectionString = "Provider=SQLOLEDB;Password=Sol@1427;Persist Security Info=True;User ID=rpasql01;Initial Catalog=RKSQLRPA01;Data Source=RKSQL03";
        static readonly string SQLQuery = "SELECT * from Nexus_API_Client_Credentials WHERE environment = '%environment%'";

        public string url = reviewURL;
        public string clientId = cclientId;
        public string clientSecret = reviewClientSecret;

    }
}

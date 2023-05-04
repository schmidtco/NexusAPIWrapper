using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace NexusAPIWrapper
{
    public  class ClientCredentials
    {
        private string _Nexus_Customer;
        private string _Nexus_customer_instance;
        private string _Third_part;
        private string _Client_id;
        private string _Client_secret;
        private string _Token_endpoint;
        private string _API_homeressource_endpoint;
        private string _Host;
        private string _Environment;
        #region d
        public string Nexus_Customer
        {
            get => _Nexus_Customer;
            private set => _Nexus_Customer = value;
        }
        public string Nexus_customer_instance
        {
            get => _Nexus_customer_instance;
            private set => _Nexus_customer_instance = value;
        }
        public string Third_part
        {
            get => _Third_part;
            private set => _Third_part = value;
        }
        public string Client_id
        {
            get => _Client_id;
            private set => _Client_id = value;
        }
        public string Client_secret
        {
            get => _Client_secret;
            private set => _Client_secret = value;
        }
        public string Token_endpoint
        {
            get => _Token_endpoint;
            private set => _Token_endpoint = value;
        }
        public string API_homeressource_endpoint
        {
            get => _API_homeressource_endpoint;
            private set => _API_homeressource_endpoint = value;

        }
        public string Host
        {
            get => _Host;
            private set => _Host = value;
        }
        public string Environment
        {
            get => _Environment; 
            private set => _Environment = value;
        }


        #endregion
        
        public ClientCredentials(string environment)
        {
            string connectionString = "Data Source=RKSQL03;Initial Catalog=RKSQLRPA01;Persist Security Info=True;User ID=rpasql01;Password=Sol@1427";
            SqlConnection sqlConnection = new SqlConnection(connectionString);
            string queryString = $"SELECT * from Nexus_API_Client_Credentials WHERE environment = '{CapitalizeFirstLetter(environment)}'";
            SqlCommand command = new SqlCommand(queryString, sqlConnection);    
            
            SortedDictionary<string,string> credentials = new SortedDictionary<string,string>();
            using (sqlConnection)
            {
                command.Connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    this.Nexus_Customer = reader["Nexus_Customer"].ToString();
                    this.Nexus_customer_instance = reader["Nexus_customer_instance"].ToString();
                    this.Third_part = reader["Third_part"].ToString();
                    this.Client_id = reader["Client_id"].ToString();
                    this.Client_secret = reader["Client_secret"].ToString();
                    this.Token_endpoint = "https://" + reader["Token_endpoint"].ToString();
                    this.API_homeressource_endpoint = reader["API_homeressource_endpoint"].ToString();
                    this.Host = "https://" + reader["Host"].ToString();
                    this.Environment = reader["Environment"].ToString();
                }
            }
        }

        public string CapitalizeFirstLetter(string input)
        {
            return char.ToUpper(input[0]) + input.Substring(1);
        }
    }
}

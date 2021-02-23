using MySql.Data.MySqlClient;
using System;
using System.Configuration;

namespace Aurora_mySQL_Intransit_Core
{
    class Program
    {
        static void Main(string[] args)
        {
           
            TestConnection( true);

            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();

            return;

          

        }

        static string GetConnectionString( bool ssl)
        {
            // Server=myServerAddress;Port=1234;Database=myDataBase;Uid=myUsername;Pwd=myPassword;
            var appConfig = ConfigurationManager.AppSettings;

            string dbname = appConfig["RDS_DB_NAME"];
            string username = appConfig["RDS_USERNAME"];
            string password = appConfig["RDS_PASSWORD"];
            string hostname = appConfig["RDS_HOSTNAME"];
            string port = appConfig["RDS_PORT"];

          

            string message = message = @"*** SSL: No User: " + username + " Endpoint: " + hostname;
            if (ssl == true)
                message = @"*** SSL: Yes User: " + username + " Endpoint: " + hostname;

            if (ssl == true)
            {
                message += " using SSL connection";
            }

            

            Console.WriteLine(message);

            var connString = "Data Source=" + hostname + ";port=" + port + ";database=" + dbname + ";User ID=" + username + ";Password=" + password + 
                ";SSLMode=REQUIRED";
            if (ssl == true )
            {
                connString = "Server=" + hostname + ";port=" + port + "; Database=" + dbname + ";User ID=" + username + ";password=" + password + //";";
                ";SSLMode=VerifyCA;" ;
               // + "SslCa=rds-ca-2019-root.pem"; 
         
            }
           

            return connString;
        }

        static void TestConnection( bool ssl)
        {
            var ConnectionString = GetConnectionString( ssl);

            MySqlConnection conn = null;

            try
            {
                conn = new MySqlConnection(ConnectionString);

                Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                using (MySqlCommand command = new MySqlCommand("select AURORA_VERSION()", conn))

                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("{0}", reader.GetString(0));
                        break;
                    }
                }

                conn.Close();
                Console.WriteLine("Testing connection is success.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                Console.WriteLine("Inner Exception: " + ex.InnerException.Message);
            }

            Console.WriteLine("");
        }
    }
}

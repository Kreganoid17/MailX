using System.Data.SqlClient;

namespace Mail_X.Other_Classes
{
    public class Connection
    {
        public SqlConnection con = new SqlConnection("Data Source = (localdb)\\Mail_X; Initial Catalog = Mail_X; Integrated Security = True");

        public void OpenDB()
        {

            con.Open();

        }

        public void CloseDB()
        {

            con.Close();    

        }

    }

    
}

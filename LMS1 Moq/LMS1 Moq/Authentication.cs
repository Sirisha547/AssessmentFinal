using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS1_Moq
{
    public class Authentication
    {
        public SqlConnection con;
        public SqlDataAdapter adp;
        public DataSet ds;
        

        public Authentication()
        {
            con = new SqlConnection("Server=IN-7G3K9S3; database=moq; Integrated Security=true");
            adp = new SqlDataAdapter("SELECT * FROM Users", con);
            ds = new DataSet();           
            adp.Fill(ds,"Users");
        }
        public bool Authenticate(string userid, string password)
        {
            string query = $"user_name = '{userid}' AND password = '{password}'";

            DataRow[] rows = ds.Tables["Users"].Select(query);

            if (rows.Length > 0)
            {
                return true;
            }
            return false;
        }

    }
}

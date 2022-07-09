using Mail_X.Models;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;

namespace Mail_X.Other_Classes
{
    public class ValidateDetails
    {

        public bool IsValidPassword(string Password, string EmpID) {

            Connection conn = new Connection();

            SqlCommand cmd = new SqlCommand("dbo.FetchAll", conn.con);

            bool IsValid = false;

            try
            {

                conn.OpenDB();

                cmd.Parameters.AddWithValue("@EmpID", EmpID);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {


                    if (Password == reader.GetString(5))
                    {

                        IsValid = true;

                    }
                    else {

                        IsValid = false;
                    
                    }

                }

                conn.CloseDB();

                return IsValid;

            }
            catch (Exception ex)
            {
                conn.CloseDB();
                throw ex;

            }

        }

        public bool DetailsValid(string ID, string password) {

            Connection conn = new Connection();

            SqlCommand cmd = new SqlCommand("dbo.GetCred", conn.con);

            bool IsValid = false;

            try
            {

                conn.OpenDB();

                cmd.Parameters.AddWithValue("@ID", ID);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {


                    if (password == reader.GetString(2))
                    {

                        if (reader.GetInt32(3) == 1) {

                            IsValid = true;

                        }

                    }
                    else
                    {

                        IsValid = false;

                    }

                }

                conn.CloseDB();

                return IsValid;

            }
            catch (Exception ex)
            {
                conn.CloseDB();
                throw ex;

            }

        }

    }
}

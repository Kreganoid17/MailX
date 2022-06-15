using Mail_X.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace Mail_X.Other_Classes
{
    public class LoginValidate
    {

        public bool Validate(string EmpID, string Password) {

            Connection conn = new Connection();

            SqlCommand cmd = new SqlCommand("dbo.FetchAll", conn.con);

            try
            {

                conn.OpenDB();

                cmd.Parameters.AddWithValue("@EmpID", EmpID);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();

                Login logindata = new Login();


                while (reader.Read())
                {

                    logindata.EmpID = reader.GetString(0);
                    logindata.Password = reader.GetString(1);

                }

                if (EmpID == logindata.EmpID && Password == logindata.Password)
                {

                    conn.CloseDB();
                    return true;

                }
                else
                {

                    conn.CloseDB();
                    return false;

                }

            }
            catch (Exception ex)
            {
                conn.CloseDB();
                return false;
                throw ex;

            }

            return false;

        }

        public int GetRole(string EmpID) {

            Connection conn = new Connection();

            SqlCommand cmd = new SqlCommand("dbo.GetEmpRole", conn.con);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cmd.Parameters.AddWithValue("@EmpID", EmpID);

                conn.OpenDB();
                
                SqlDataReader reader = cmd.ExecuteReader();

                Role role = new Role();

                while (reader.Read())
                {

                    role.RoleEmp = reader.GetInt32(0);


                }

                conn.CloseDB();

                return role.RoleEmp;

            }
            catch (Exception ex)
            {

                conn.CloseDB();
                throw ex;

            }

        }

        public UserDetails GetUserDetails(string EmpID) {

            Connection conn = new Connection();

            SqlCommand cmd = new SqlCommand("dbo.FetchAll", conn.con);

            try
            {

                conn.OpenDB();

                cmd.Parameters.AddWithValue("@EmpID", EmpID);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();


                UserDetails userdetails = new UserDetails();

                while (reader.Read())
                {
                    

                    userdetails.UserName = reader.GetString(2);
                    userdetails.Email = reader.GetString(3);
                    userdetails.UserID = reader.GetString(0);

                }

                conn.CloseDB();

                return userdetails;

            }
            catch (Exception ex)
            {
                conn.CloseDB();
                throw ex;

            }

        }

    }

    }

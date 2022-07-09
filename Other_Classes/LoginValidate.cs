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
                    logindata.Password = reader.GetString(3);

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

        

        public UserDetails GetUserDetails(string EmpID) {

            Connection conn = new Connection();

            SqlCommand cmd = new SqlCommand("dbo.FetchAll", conn.con);


            try
            {

                conn.OpenDB();

                cmd.Parameters.AddWithValue("@EmpID", EmpID);

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();

                string Deptname;

                UserDetails userdetails = new UserDetails();

                while (reader.Read())
                { 

                    userdetails.EmpID = reader.GetString(0);
                    userdetails.UserName = reader.GetString(1);
                    userdetails.Email = reader.GetString(2);
                    userdetails.DeptID = reader.GetInt32(4);

                    Deptname = GetDeptName(reader.GetInt32(4));

                    userdetails.EmailPassword = reader.GetString(5);
                    userdetails.IsLeader = reader.GetInt32(6);
                    userdetails.DeptName = Deptname;

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

        public string GetDeptName(int DeptID)
        {

            Connection con = new Connection();

            SqlCommand cmd = new SqlCommand("dbo.GetDept", con.con);

            

            string DeptName = "";

            

            try
            {
                con.OpenDB();

                cmd.Parameters.Add("@DeptID", SqlDbType.Int);
                cmd.Parameters["@DeptID"].Value = DeptID;

                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {

                    DeptName = reader.GetString(0);

                }

                return DeptName;
                
            }
            catch (Exception ex)
            {

                con.CloseDB();
                throw ex;

            }

        }

    }

    }

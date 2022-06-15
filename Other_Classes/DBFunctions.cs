using Mail_X.Models;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace Mail_X.Other_Classes
{
    public class DBFunctions
    {

        public void AddSignOff(int ID, SignOff SO) {

            Connection con = new Connection();

            SqlCommand cmd = new SqlCommand("dbo.AddSignOff", con.con);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cmd.Parameters.AddWithValue("@FormID", SqlDbType.Int);
                cmd.Parameters["@FormID"].Value = ID;

                cmd.Parameters.AddWithValue("@FormNameSurname", SO.SignName);
                cmd.Parameters.AddWithValue("@Signature", SO.Signature);
                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                cmd.Parameters.AddWithValue("@Comment", SO.Comments);
                cmd.Parameters.AddWithValue("@DeptName", SO.DeptName);



                con.OpenDB();
                cmd.ExecuteNonQuery();
                con.CloseDB();
                
            }
            catch (Exception ex) {

                con.CloseDB();
                throw ex;
            
            }
        
        }

        public List<HomePage> FetchAll (){

            Connection con = new Connection();

            SqlCommand cmd = new("dbo.FetchAllForm",con.con);

            List<HomePage> ResultData = new List<HomePage>();

            try
            {
              
                con.OpenDB();

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();


                    while(reader.Read()) {

                        HomePage HP = new HomePage();

                        HP.FormID = reader.GetInt32(0);
                        HP.FormName = reader.GetString(1);
                        HP.FDate = reader.GetDateTime(2);

                    if (reader.GetString(3) == "1") {

                        HP.Status = "Done";

                    } else if (reader.GetString(3) == "0") {

                        HP.Status = "Still Deploying";

                    }    

                        ResultData.Add(HP);
                    }

                con.CloseDB();

                return ResultData;


            }
            catch (Exception ex) {

                con.CloseDB();
                throw ex;
            
            }

            
        
        }

        public List<string> GetDeptName() {

            Connection con = new Connection();

            SqlCommand cmd = new SqlCommand("dbo.GetDept", con.con);

            cmd.CommandType = CommandType.StoredProcedure;

            SqlDataReader reader;

            try
            {

                con.OpenDB();

                reader = cmd.ExecuteReader();

                List<string> DeptName = new List<string>();

                while (reader.Read()) { 
                
                    
                    DeptName.Add(reader.GetString(0));
                
                }

                return DeptName;

            }
            catch (Exception ex) {

                con.CloseDB();
                throw ex;
            
            }

        }

        
        public FormDetails GetForm(int ID) {

            Connection con = new Connection();

            SqlCommand cmd = new SqlCommand("dbo.FetchFormID", con.con);
            SqlCommand cmd2 = new SqlCommand("dbo.FetchSignOff", con.con);

            try
            { 

                cmd.CommandType = CommandType.StoredProcedure;
                cmd2.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ID", SqlDbType.Int);
                cmd.Parameters["@ID"].Value = ID;

                cmd2.Parameters.Add("@ID", SqlDbType.Int);
                cmd2.Parameters["@ID"].Value = ID;

                con.OpenDB();

                SqlDataReader reader = cmd.ExecuteReader();

                FormDetails FD = new FormDetails();

                List<SignOff> SO = new List<SignOff>();

                if (reader.HasRows) {

                    while (reader.Read())
                    {

                        FD.StartDate = reader.GetDateTime(3);
                        FD.EndDate = reader.GetDateTime(4);
                        FD.AppName = reader.GetString(5);
                        FD.ServerName = reader.GetString(6);
                        FD.AdditionalNotes = reader.GetString(7);
                        FD.SignOff = reader.GetString(8); 

                    }

                }

                reader.Close();     

                SqlDataReader reader2 = cmd2.ExecuteReader();

                if (reader2.HasRows)
                {

                    while (reader2.Read())
                    {

                        SignOff Signoff = new SignOff();

                        Signoff.SignName = reader2.GetString(2);
                        Signoff.Signature = reader2.GetString(3);
                        Signoff.SignDate = reader2.GetDateTime(4);
                        Signoff.Comments = reader2.GetString(5);
                        Signoff.DeptName = reader2.GetString(6);

                        SO.Add(Signoff);

                    }


                }

                FD.SignOffs = SO;

                reader2.Close();

                con.CloseDB();

                return FD;

            }
            catch (Exception ex)
            {

                con.CloseDB();
                throw ex;

            }

        }

        
        public void AddForm(FormDetails FD){

            string FormName = FD.AppName;

            Connection con = new Connection();

            SqlCommand cmd = new SqlCommand("dbo.InsertForm", con.con);
            cmd.CommandType = CommandType.StoredProcedure;
            
            try
            {

                cmd.Parameters.AddWithValue("@FormName", FormName);
                cmd.Parameters.AddWithValue("@DateCreated", DateTime.Now);
                cmd.Parameters.AddWithValue("@DateStart", FD.StartDate);
                cmd.Parameters.AddWithValue("@DateEnd", FD.EndDate);
                cmd.Parameters.AddWithValue("@AppName", FD.AppName);
                cmd.Parameters.AddWithValue("@ServerName", FD.ServerName);
                cmd.Parameters.AddWithValue("@AddNotes", FD.AdditionalNotes);
                cmd.Parameters.AddWithValue("@SignOff", FD.SignOff);
                cmd.Parameters.AddWithValue("@Status", "0");

                con.OpenDB();
                cmd.ExecuteNonQuery();
                con.CloseDB();

            }
            catch (Exception ex) {

                con.CloseDB();
                throw ex;
            
            }



        }

        public void DeleteForm(int ID) {

            Connection con = new Connection();

            SqlCommand cmd = new SqlCommand("dbo.DeleteFormDetails", con.con);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {

                cmd.Parameters.Add("@FormID", SqlDbType.Int);
                cmd.Parameters["@FormID"].Value = ID;

                DeleteSignOff(ID);

                con.OpenDB();

                cmd.ExecuteNonQuery();

                con.CloseDB();

            }
            catch (Exception ex)
            {

                con.CloseDB();
                throw ex;

            }

        }

        public void DeleteSignOff(int ID)
        {

            Connection con = new Connection();

            SqlCommand cmd = new SqlCommand("dbo.DeleteSignOff", con.con);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {

                cmd.Parameters.Add("@FormID", SqlDbType.Int);
                cmd.Parameters["@FormID"].Value = ID;

                con.OpenDB();

                cmd.ExecuteNonQuery();

                con.CloseDB();

            }
            catch (Exception ex)
            {

                con.CloseDB();
                throw ex;

            }

        }

        public void UpdateForm(FormDetails FD, int ID) {

            string FormName = FD.AppName;

            Connection con = new Connection();

            SqlCommand cmd = new SqlCommand("dbo.UpdateFormDetails", con.con);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {
                cmd.Parameters.Add("@FormID", SqlDbType.Int);
                cmd.Parameters["@FormID"].Value = ID;

                cmd.Parameters.AddWithValue("@FormName", FormName);
                cmd.Parameters.AddWithValue("@StartDate", FD.StartDate);
                cmd.Parameters.AddWithValue("@EndDate", FD.EndDate);
                cmd.Parameters.AddWithValue("@AppName", FD.AppName);
                cmd.Parameters.AddWithValue("@ServerName", FD.ServerName);
                cmd.Parameters.AddWithValue("@AddNotes", FD.AdditionalNotes);
                cmd.Parameters.AddWithValue("@SignOff", FD.SignOff);

                con.OpenDB();
                cmd.ExecuteNonQuery();
                con.CloseDB();

            }
            catch (Exception ex)
            {

                con.CloseDB();
                throw ex;

            }

        }

        public List<Senders> FetchSendData()
        {

            Connection con = new Connection();

            SqlCommand cmd = new SqlCommand("dbo.FetchMailInfo", con.con);

            List<Senders> send = new List<Senders>();

            try
            {

                con.OpenDB();

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {

                    Senders senders = new Senders();

                    senders.ID = reader.GetString(0);
                    senders.EmpName = reader.GetString(1);
                    senders.Email = reader.GetString(2);
                    senders.DeptName = reader.GetString(3);

                    send.Add(senders);

                }

                con.CloseDB();

                return send;


            }
            catch (Exception ex)
            {

                con.CloseDB();
                throw ex;

            }


        }

        public void SetStatus(int ID) {

            Connection con = new Connection();

            SqlCommand cmd = new SqlCommand("dbo.SetStatus", con.con);

            List<Senders> send = new List<Senders>();

            try
            {

                con.OpenDB();

                cmd.Parameters.Add("@ID", SqlDbType.Int);
                cmd.Parameters["@ID"].Value = ID;

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();

                con.CloseDB();


            }
            catch (Exception ex)
            {

                con.CloseDB();
                throw ex;

            }

        }

        public void AddApproval()
        {



        }

    }

    

 
}

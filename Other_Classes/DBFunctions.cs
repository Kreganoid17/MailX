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

                cmd.Parameters.AddWithValue("@Date", DateTime.Now);
                cmd.Parameters.AddWithValue("@Comment", SO.Comments);
                cmd.Parameters.AddWithValue("@DeptName", SO.DeptName);
                cmd.Parameters.AddWithValue("@Employee", SO.SignName);
                cmd.Parameters.AddWithValue("@EmployeeID", SO.EmpID);


                con.OpenDB();
                cmd.ExecuteNonQuery();
                con.CloseDB();
                
            }
            catch (Exception ex) {

                con.CloseDB();
                throw ex;
            
            }
        
        }

        public void AddHistory(History history) {

            Connection con = new Connection();

            SqlCommand cmd = new SqlCommand("dbo.AddHistory", con.con);

            cmd.CommandType = CommandType.StoredProcedure;

            try
            {

                cmd.Parameters.AddWithValue("@EmpID", history.EmpID);
                cmd.Parameters.AddWithValue("@EmpName", history.EmpName);
                cmd.Parameters.AddWithValue("@DateCreated", history.DateCreated);
                cmd.Parameters.AddWithValue("@DeptID", history.DeptID);
                cmd.Parameters.AddWithValue("@Desc", history.Description);


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

        public List<Activity> GetAudit() {

            Connection con = new Connection();

            SqlCommand cmd = new("dbo.GetAudit", con.con);

            List<Activity> ActivityData = new List<Activity>();

            try
            {

                con.OpenDB();

                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {

                    Activity activity = new Activity();

                    activity.DateCreated = reader.GetDateTime(0);
                    activity.EmpID = reader.GetString(2);
                    activity.EmpName = reader.GetString(3);
                    activity.Dept = reader.GetString(4);
                    activity.Desc = reader.GetString(1);

                    ActivityData.Add(activity);
                }

                con.CloseDB();

                return ActivityData;


            }
            catch (Exception ex)
            {

                con.CloseDB();
                throw ex;

            }


        }

        public List<Activity> GetRecentActivity()
        {

            Connection con = new Connection();

            SqlCommand cmd = new("dbo.GetRecentActivity", con.con);

            List<Activity> ActivityData = new List<Activity>();

            try
            {

                con.OpenDB();

                cmd.CommandType = CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {

                    Activity activity = new Activity();

                    activity.DateCreated = reader.GetDateTime(0);
                    activity.Desc = reader.GetString(1);
                    activity.EmpID = reader.GetString(2);
                    activity.EmpName = reader.GetString(3);
                    activity.Dept = reader.GetString(4);

                    ActivityData.Add(activity);
                }

                con.CloseDB();

                return ActivityData;


            }
            catch (Exception ex)
            {

                con.CloseDB();
                throw ex;

            }



        }

        public List<HomePage> FetchAll (string Proc){

            Connection con = new Connection();

            SqlCommand cmd = new(Proc,con.con);

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
                        HP.PName = reader.GetString(4);
                        HP.PID = reader.GetString(5);

                    if (reader.GetString(3) == "1") {

                        HP.Status = "Rejected";

                    } else if (reader.GetString(3) == "0") {

                        HP.Status = "Pending Approval";

                    }
                    else if (reader.GetString(3) == "2")
                    {

                        HP.Status = "To Be Deployed";

                    }
                    else if (reader.GetString(3) == "3")
                    {

                        HP.Status = "Deployed To " + reader.GetString(6);
                        HP.Environment = reader.GetString(6);

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

        public List<HomePage> FetchAllArchive()
        {

            Connection con = new Connection();

            SqlCommand cmd = new("dbo.FetchAllArchive", con.con);

            List<HomePage> ResultData = new List<HomePage>();

            try
            {

                cmd.Parameters.Add("@ArchiveID", SqlDbType.Int);
                cmd.Parameters["@ArchiveID"].Value = 1;

                con.OpenDB();

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {

                    HomePage HP = new HomePage();

                    HP.FormID = reader.GetInt32(0);
                    HP.FormName = reader.GetString(1);
                    HP.FDate = reader.GetDateTime(2);
                    HP.PName = reader.GetString(4);
                    HP.PID = reader.GetString(5);

                    if (reader.GetString(3) == "1")
                    {

                        HP.Status = "Rejected";

                    }
                    else if (reader.GetString(3) == "0")
                    {

                        HP.Status = "Pending Approval";

                    }
                    else if (reader.GetString(3) == "2")
                    {

                        HP.Status = "To Be Deployed";

                    }
                    else if (reader.GetString(3) == "3")
                    {

                        HP.Status = "Deployed To " + reader.GetString(6);
                        HP.Environment = reader.GetString(6);

                    }

                    ResultData.Add(HP);
                }

                con.CloseDB();

                return ResultData;


            }
            catch (Exception ex)
            {

                con.CloseDB();
                throw ex;

            }



        }

        public int GetRecentAdded() {

            Connection con = new Connection();

            SqlCommand cmd = new("dbo.FetchIDForm", con.con);

            int id = 0;

            try
            {
                con.OpenDB();

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read()) { 
                
                    id = reader.GetInt32(0);
                
                }

                con.CloseDB();

                return id;

            }
            catch(Exception ex) {

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
                        FD.ProjectName = reader.GetString(9);
                        FD.ProjectID = reader.GetString(10);
                        FD.PullRequests = reader.GetString(11);
                        FD.Environment = reader.GetString(12);
                        FD.Comments = reader.GetString(13);

                    }

                }

                reader.Close();     

                SqlDataReader reader2 = cmd2.ExecuteReader();

                if (reader2.HasRows)
                {

                    while (reader2.Read())
                    {

                        SignOff Signoff = new SignOff();

                        Signoff.SignName = reader2.GetString(5);
                        Signoff.SignDate = reader2.GetDateTime(2);
                        Signoff.Comments = reader2.GetString(3);
                        Signoff.DeptName = reader2.GetString(4);
                        Signoff.EmpID = reader2.GetString(6);

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
                cmd.Parameters.AddWithValue("@Status", "0");
                cmd.Parameters.AddWithValue("@ProjectName", FD.ProjectName);
                cmd.Parameters.AddWithValue("@ProjectID", FD.ProjectID);
                cmd.Parameters.AddWithValue("@PullRequest", FD.PullRequests);
                cmd.Parameters.AddWithValue("@Environment", FD.Environment);
                cmd.Parameters.AddWithValue("@Comments", FD.Comments);

                con.OpenDB();
                cmd.ExecuteNonQuery();
                con.CloseDB();

            }
            catch (Exception ex) {

                con.CloseDB();
                throw ex;
            
            }



        }

        public void ArchiveForm(int ID) {

            Connection con = new Connection();

            SqlCommand cmd = new SqlCommand("dbo.SetArchive", con.con);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {

                cmd.Parameters.Add("@FormID", SqlDbType.Int);
                cmd.Parameters["@FormID"].Value = ID;
                cmd.Parameters.Add("@ArchiveInt", SqlDbType.Int);
                cmd.Parameters["@ArchiveInt"].Value = 1;

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

        public void RestoreForm(int ID) {

            Connection con = new Connection();

            SqlCommand cmd = new SqlCommand("dbo.SetArchive", con.con);
            cmd.CommandType = CommandType.StoredProcedure;

            try
            {

                cmd.Parameters.Add("@FormID", SqlDbType.Int);
                cmd.Parameters["@FormID"].Value = ID;
                cmd.Parameters.Add("@ArchiveInt", SqlDbType.Int);
                cmd.Parameters["@ArchiveInt"].Value = 0;

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
                cmd.Parameters.AddWithValue("@ProjectID", FD.ProjectID);
                cmd.Parameters.AddWithValue("@ProjectName", FD.ProjectName);
                cmd.Parameters.AddWithValue("@Pullrequest", FD.PullRequests);
                cmd.Parameters.AddWithValue("@Comments", FD.Comments);


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

        public void SetStatus(int ID, int StatusID) {

            Connection con = new Connection();

            SqlCommand cmd = new SqlCommand("dbo.SetStatus", con.con);

            List<Senders> send = new List<Senders>();

            try
            {

                con.OpenDB();

                cmd.Parameters.Add("@ID", SqlDbType.Int);
                cmd.Parameters.Add("@StatusID", SqlDbType.Int);
                cmd.Parameters["@ID"].Value = ID;
                cmd.Parameters["@StatusID"].Value = StatusID;

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

        public List<HomePage> GetHomePageByStatus(int StatusID)
        {

            Connection con = new Connection();

            SqlCommand cmd = new("dbo.GetStatus", con.con);

            List<HomePage> ResultData = new List<HomePage>();

            try
            {

                con.OpenDB();

                cmd.Parameters.Add("@StatusID", SqlDbType.Int);
                cmd.Parameters["@StatusID"].Value = StatusID;

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();


                while (reader.Read())
                {

                    HomePage HP = new HomePage();

                    HP.FormID = reader.GetInt32(0);
                    HP.FormName = reader.GetString(1);
                    HP.FDate = reader.GetDateTime(2);
                    HP.PName = reader.GetString(5);
                    HP.PID = reader.GetString(10);

                    if (reader.GetString(8) == "1")
                    {

                        HP.Status = "Rejected";

                    }
                    else if (reader.GetString(8) == "0")
                    {

                        HP.Status = "Pending Approval";

                    }
                    else if (reader.GetString(8) == "2")
                    {

                        HP.Status = "To Be Deployed";

                    }
                    else if (reader.GetString(8) == "3")
                    {

                        HP.Status = "Deployed To " + reader.GetString(12);

                    }

                    ResultData.Add(HP);
                }

                con.CloseDB();

                return ResultData;


            }
            catch (Exception ex)
            {

                con.CloseDB();
                throw ex;

            }



        }

        public Dashboard GetDashboardData(string DeptID) {

            Connection con = new Connection();

            SqlCommand cmd = new SqlCommand("dbo.GetDashBoardData", con.con);

            List<int> values = new List<int>();

            Dashboard dashboard = new Dashboard();

            try
            {

                con.OpenDB();

                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader reader = cmd.ExecuteReader();

                while(reader.Read()) {


                    if (DeptID == "1")
                    {

                        values.Add(reader.GetInt32(0));
                        values.Add(reader.GetInt32(1));
                        values.Add(reader.GetInt32(2));
                        values.Add(reader.GetInt32(3));

                        dashboard.StatusCount = values;
                        dashboard.StatusLabel = new string[4] { "Pending Approval", "Rejected", "To Be Deployed", "Deployed" };
                        dashboard.Color = new string[4] { "rgba(54, 162, 235, 1)", "rgba(255, 99, 132, 1)", "rgba(255, 206, 86, 1)", "rgba(75, 192, 192, 1)" };

                    }
                    else if (DeptID == "1003")
                    {

                        values.Add(reader.GetInt32(0));
                        values.Add(reader.GetInt32(1));

                        dashboard.StatusCount = values;
                        dashboard.StatusLabel = new string[2] { "Pending Approval", "Rejected" };
                        dashboard.Color = new string[2] { "rgba(54, 162, 235, 1)", "rgba(255, 99, 132, 1)"};

                    }
                    else  {

                        values.Add(reader.GetInt32(2));
                        values.Add(reader.GetInt32(3));

                        dashboard.StatusCount = values;
                        dashboard.StatusLabel = new string[2] { "To Be Deployed", "Deployed" };
                        dashboard.Color = new string[2] {"rgba(255, 206, 86, 1)", "rgba(75, 192, 192, 1)" };


                    }

                }

                con.CloseDB();

                return dashboard;


            }
            catch (Exception ex)
            {

                con.CloseDB();
                throw ex;

            }

        }


    }

    

 
}

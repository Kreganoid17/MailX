using Mail_X.Models;
using Mail_X.Other_Classes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;

namespace Mail_X.Controllers
{
    public class SendController : Controller
    {
        public string SetProc()
        {

            return HttpContext.Session.GetString("Proc");

        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult ReturnToHome()
        {

            DBFunctions DBF = new DBFunctions();

            List<HomePage> HP = new List<HomePage>();

            HP = DBF.FetchAll(SetProc());

            ViewBag.Username = HttpContext.Session.GetString("Username");
            return RedirectToAction("ReturnHome", "HomePage", HP);

        }

        public ActionResult Validate(string password, string email, string FormName, string FormID)
        {

            ValidateDetails VD = new ValidateDetails();
            VD.IsValidPassword(password, HttpContext.Session.GetString("ID"));

            if (VD.IsValidPassword(password, HttpContext.Session.GetString("ID")) == true)
            {

                string[] EmailArr = JsonConvert.DeserializeObject<string[]>(email);
                Send(FormName, FormID, password, EmailArr);

                DBFunctions DBF = new DBFunctions();

                List<HomePage> HP = new List<HomePage>();

                HP = DBF.FetchAll(SetProc());

                TempData["Message"] = "Email(s) Sent Successfully";

                return RedirectToAction("ReturnHome","HomePage", HP);

            }
            else {

                return Content("Invalid Password");


            }
            

        }

        public void Send(string FormName, string ID, string password, string[] RecipientEmail)
        {

            string SenderEmail = HttpContext.Session.GetString("Email");
            string SenderName = HttpContext.Session.GetString("Username");



            try
            {

                SmtpClient smtp = new SmtpClient();

                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                NetworkCredential NetworkCred = new NetworkCredential(SenderEmail, password); 
                smtp.Credentials = NetworkCred;
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Port = 25;
      

                for (int i = 0; i < RecipientEmail.Count(); i++)
                {

                    MailMessage mm = new MailMessage(SenderEmail, RecipientEmail[i]);

                    mm.Subject = "Deployment Requested";
                    mm.Body = "A new deployment is requested with Deploy name:" + FormName + " and Deploy ID:" + ID;

                    smtp.Send(mm);

                }

                smtp.Dispose();

                

            }
            catch(Exception ex) {

                throw ex;
            
            }
        }

            
    }

    

}


using Mail_X.Models;
using Mail_X.Other_Classes;
using Microsoft.AspNetCore.Mvc;

namespace Mail_X.Controllers
{
    public class MarkAsDoneController : Controller
    {
        public string SetProc()
        {

            return HttpContext.Session.GetString("Proc");

        }

        public IActionResult Index()
        {
            return View();
        }

        public ActionResult ValidateDetailsDevOps(int FormID, string ID, string Password) {

            DBFunctions DBF = new DBFunctions();

            List<HomePage> HP = new List<HomePage>();

            HP = DBF.FetchAll(SetProc());

            string Home = HttpContext.Session.GetString("Home");

            try
            {

                byte[] data = System.Convert.FromBase64String(Password);

                string DecodedPassword = System.Text.ASCIIEncoding.ASCII.GetString(data);

                ValidateDetails VD = new ValidateDetails();

                    VD.DetailsValid(ID, Password);


                    if (VD.DetailsValid(ID, DecodedPassword) == true)
                    {

                        DBF.SetStatus(FormID,3);


                        TempData["Message"] = String.Format("Successfuly Marked As Done");
                        return View(Home, HP);

                    }

                else {

                    TempData["Message"] = String.Format("Invalid Password Entered");
                    return View(Home, HP);

                }

            }
            catch (Exception ex) {

                throw ex;
                return View(Home, HP);

            }

            

        }
    }
}

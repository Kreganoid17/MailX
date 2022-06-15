using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Mail_X.Models;
using Mail_X.Other_Classes;
using Microsoft.AspNetCore.Http;

namespace Mail_X.Controllers
{
    public class LoginController : Controller
    {

        public IActionResult Login()
        {

            return View();

        }

        private void SetUser(string Username, string Email, string ID, string Home) {

            HttpContext.Session.SetString("Username", Username);
            HttpContext.Session.SetString("Email", Email);
            HttpContext.Session.SetString("ID", ID);
            HttpContext.Session.SetString("Home", Home);

        } 


        [HttpPost]
        public ActionResult Validate(string EmpID, string Password)
        {

            LoginValidate LV = new LoginValidate();

            LV.Validate(EmpID, Password);


            try
            {

                if(ModelState.IsValid){

                    if (LV.Validate(EmpID, Password) == true)
                    {

                        DBFunctions DBF = new DBFunctions();

                        List<HomePage> HP = new List<HomePage>();

                        LV.GetRole(EmpID);

                        HP = DBF.FetchAll();

                        if (LV.GetRole(EmpID) == 1)
                        {

                            SetUser(LV.GetUserDetails(EmpID).UserName, LV.GetUserDetails(EmpID).Email, LV.GetUserDetails(EmpID).UserID, "~/Views/HomePage/HomePageDev.cshtml");

                            return View("~/Views/HomePage/HomePageDev.cshtml", HP);

                        }
                        else if (LV.GetRole(EmpID) == 2)
                        {
                            SetUser(LV.GetUserDetails(EmpID).UserName, LV.GetUserDetails(EmpID).Email, LV.GetUserDetails(EmpID).UserID, "~/Views/HomePage/HomePageDevOps.cshtml");

                            return View("~/Views/HomePage/HomePageDevOps.cshtml", HP);

                        }
                        else {

                            SetUser(LV.GetUserDetails(EmpID).UserName, LV.GetUserDetails(EmpID).Email, LV.GetUserDetails(EmpID).UserID, "~/Views/HomePage/HomePageOther.cshtml");

                            return View("~/Views/HomePage/HomePageOther.cshtml", HP);

                        }

                        

                    }
                    else {

                        ViewBag.Message = String.Format("Invalid User Credentials");
                    }

                }
 

            }
            catch (Exception ex)
            {

                return Content(ex.ToString());
            }

            return View("~/Views/Login/Login.cshtml");

        }
    }

    
}
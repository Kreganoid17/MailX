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

        private void SetUser(UserDetails userDetails, string Home, string Proc) {

            HttpContext.Session.SetString("ID", userDetails.EmpID);
            HttpContext.Session.SetString("Username", userDetails.UserName);
            HttpContext.Session.SetString("Email", userDetails.Email);
            HttpContext.Session.SetString("DeptID", userDetails.DeptID.ToString());
            HttpContext.Session.SetString("DeptName", userDetails.DeptName);
            HttpContext.Session.SetString("EPassword", userDetails.EmailPassword);
            HttpContext.Session.SetString("IsLeader", userDetails.IsLeader.ToString());
            HttpContext.Session.SetString("Home", Home);
            HttpContext.Session.SetString("Proc", Proc);

        } 


        [HttpPost]
        public ActionResult Validate(string EmpID, string Password)
        {

            LoginValidate LV = new LoginValidate();

            bool IsValid = LV.Validate(EmpID, Password);

            try
            {

                if (ModelState.IsValid)
                {

                    if (IsValid == true)
                    {

                        DBFunctions DBF = new DBFunctions();

                        UserDetails userDetails = new UserDetails();

                        userDetails = LV.GetUserDetails(EmpID);

                        if (userDetails.DeptID == 1)
                        {

                            SetUser(userDetails, "~/Views/HomePage/HomePageDev.cshtml", "dbo.FetchAllFormDev");

                            return RedirectToAction("Dashboard", "Dashboard");

                        }
                        else if (userDetails.DeptID == 2)
                        {

                            SetUser(userDetails, "~/Views/HomePage/HomePageDevOps.cshtml", "dbo.FetchAllFormDevOps");

                            return RedirectToAction("Dashboard", "Dashboard");

                        }
                        else if (userDetails.DeptID == 1003)
                        {

                            SetUser(userDetails, "~/Views/HomePage/HomePageOther.cshtml", "dbo.FetchAllFormTechLead");

                            return RedirectToAction("Dashboard", "Dashboard");


                        }
                        else {

                            SetUser(userDetails, "~/Views/HomePage/HomePageOther.cshtml", "dbo.FetchAllFormDevOps");

                            return RedirectToAction("Dashboard", "Dashboard");

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


        public ActionResult LogOut() {

            HttpContext.Session.Clear();
            return View("~/Views/Login/Login.cshtml");

        }

    }

    
}
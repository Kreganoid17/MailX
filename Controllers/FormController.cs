using Mail_X.Models;
using Mail_X.Other_Classes;
using Microsoft.AspNetCore.Mvc;

namespace Mail_X.Controllers
{
    public class FormController : Controller
    {
        public string SetProc() {

            return HttpContext.Session.GetString("Proc");

        }

        public IActionResult Index()
        {
            return View();
        }

        public History PopHistory(string Desc) {

            History history = new History();

            history.EmpID = HttpContext.Session.GetString("ID");
            history.EmpName = HttpContext.Session.GetString("Username");
            history.DeptID = HttpContext.Session.GetString("DeptID");
            history.Description = Desc;

            return history;

        }

        public ActionResult ShowForm() {

            List<string> Environment = new List<string> {"UAT", "Pre-Live", "Live"};
            ViewBag.list = Environment;

            return View("~/Views/CreateForm/CreateForm.cshtml");
        
        }

        [HttpPost]
        public ActionResult AddForm(FormDetails FD) {

            try
            {

                ModelState.Remove("SignOffs");

                if (ModelState.IsValid)
                {

                    DBFunctions DBF = new DBFunctions();

                    List<HomePage> HP = new List<HomePage>();

                    SignOff SO = new SignOff();

                    SO.SignName = HttpContext.Session.GetString("Username");
                    SO.Comments = FD.Comments;
                    SO.DeptName = HttpContext.Session.GetString("DeptName");
                    SO.EmpID = HttpContext.Session.GetString("ID");

                    DBF.AddForm(FD);

                    DBF.AddHistory(PopHistory("Created Deployment Form with Project ID: " + FD.ProjectID + " and Project Name: " + FD.ProjectName));

                    int ID = DBF.GetRecentAdded();

                    DBF.AddSignOff(ID,SO);

                    DBF.AddHistory(PopHistory("Signed Off On Form with Project ID: " + FD.ProjectID + " and Project Name : " + FD.ProjectName));

                    HP = DBF.FetchAll(SetProc());

                    TempData["Message"] = "Form Created Successfuly";

                    return RedirectToAction("ReturnHome", "HomePage");

                }
                else
                {

                    ViewBag.Message = String.Format("Invalid Details Entered");

                }


            }
            catch (Exception ex) {

                throw ex;
            
            }

            List<string> Environment = new List<string> { "UAT", "Pre-Live", "Live" };
            ViewBag.list = Environment;
            return View("~/Views/CreateForm/CreateForm.cshtml");

        }

        [HttpPost]
        public ActionResult UpdateForm(int ID, FormDetails FD) {

            try
            {
                ModelState.Remove("SignOffs");

                ModelState.Remove("Environment");

                if (ModelState.IsValid)
                {
                    

                    DBFunctions DBF = new DBFunctions();

                    List<HomePage> HP = new List<HomePage>();

                    DBF.UpdateForm(FD, ID);

                    DBF.AddHistory(PopHistory("Updated Deployment Form with Project ID: " + FD.ProjectID + " and Project Name: " + FD.ProjectName));

                    HP = DBF.FetchAll(SetProc());

                    TempData["Message"] = "Form Updated Successfuly";

                    return RedirectToAction("ReturnHome", "HomePage", HP);

                }
                else {

                    ViewBag.Message = String.Format("Invalid Details Entered");

                }



            }
            catch (Exception ex) {

                throw ex;
            
            }

            return View("~/Views/UpdateView/UpdateView.cshtml");

        }

        public ActionResult AddSignOff(int ID, SignOff SO, string Home)
        {

            try
            {

                if (ModelState.IsValid)
                {

                    DBFunctions DBF = new DBFunctions();

                    DBF.AddSignOff(ID, SO);

                    List<HomePage> HP = new List<HomePage>();

                    HP = DBF.FetchAll(SetProc());

                    TempData["Message"] = "Approval Successfuly Added!";

                    return View(Home, HP);

                }
                else {

                    ViewBag.Message = String.Format("Invalid Details Entered");

                }

            }
            catch (Exception ex) {

                throw ex;

            }

            return View("~/Views/SignOff/SignOff.cshtml");

        }

        public ActionResult Reject(int ID) {

            DBFunctions DBF = new DBFunctions();

            DBF.SetStatus(ID,1);

            DBF.AddHistory(PopHistory("Rejected Form"));

            TempData["Message"] = "Rejection Successfully";

            return RedirectToAction("ReturnHome","HomePage");
        
        }

        public ActionResult Accept(int ID,string comment)
        {

            SignOff SO = new SignOff();

            SO.SignName = HttpContext.Session.GetString("Username");
            SO.Comments = comment;
            SO.DeptName = HttpContext.Session.GetString("DeptName");
            SO.EmpID = HttpContext.Session.GetString("ID");

            DBFunctions DBF = new DBFunctions();

            DBF.AddSignOff(ID,SO);

            DBF.SetStatus(ID, 2);

            DBF.AddHistory(PopHistory("Approved Form"));
                

            TempData["Message"] = "Approved Successfully";

            return RedirectToAction("ReturnHome", "HomePage");

        }

    }
}

using Mail_X.Models;
using Mail_X.Other_Classes;
using Microsoft.AspNetCore.Mvc;

namespace Mail_X.Controllers
{
    public class FormController : Controller
    {
        public IActionResult Index()
        {
            return View();
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

                    DBF.AddForm(FD);

                    HP = DBF.FetchAll();

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

                if (ModelState.IsValid)
                {
                    

                    DBFunctions DBF = new DBFunctions();

                    List<HomePage> HP = new List<HomePage>();

                    DBF.UpdateForm(FD, ID);

                    HP = DBF.FetchAll();

                    TempData["Message"] = "Form Created Successfuly";

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

                    HP = DBF.FetchAll();

                    TempData["Message"] = "Sign Off Successfuly Added!";

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
    }
}

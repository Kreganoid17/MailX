using Mail_X.Models;
using Mail_X.Other_Classes;
using Microsoft.AspNetCore.Mvc;

namespace Mail_X.Controllers
{
    public class MarkAsDoneController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult ValidateDetailsDevOps(MarkAsDoneDetails MD, int ID) {

            try
            {

                if (ModelState.IsValid)
                {


                    ValidateDetails VD = new ValidateDetails();

                    VD.DetailsValid(MD.DevOpsID, MD.Password);

                    if (VD.DetailsValid(MD.DevOpsID, MD.Password) == true)
                    {

                        DBFunctions DBF = new DBFunctions();

                        List<HomePage> HP = new List<HomePage>();

                        DBF.SetStatus(ID);

                        HP = DBF.FetchAll();

                        return View("~/Views/HomePage/HomePageDevOps.cshtml", HP);

                    }
                    else
                    {

                        ViewBag.Message = String.Format("Invalid Details Entered");
                        return View("~/Views/MarkAsDone/MarkAsDone.cshtml");

                    }

                }
                else {

                    ViewBag.Message = String.Format("Invalid Details Entered");

                }

            }
            catch (Exception ex) {

                throw ex;
            
            }

            return View("~/Views/MarkAsDone/MarkAsDone.cshtml");
        
        }
    }
}

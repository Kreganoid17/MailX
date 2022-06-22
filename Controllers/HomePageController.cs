using Mail_X.Models;
using System.Linq;
using Mail_X.Other_Classes;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace Mail_X.Controllers  
{

    public class HomePageController : Controller
    {

        public IActionResult HomePage()
        {
            return View();
        }

        public ActionResult ViewDoc(int ID) { 
        
            PopulateViewDoc PVD = new PopulateViewDoc();

            FormDetails FD = new FormDetails();

            FD = PVD.Populate(ID);

            return View("~/Views/ViewDoc/ViewDoc.cshtml", FD);
        
        }

        public ActionResult UpdateDoc(int ID) {

            PopulateViewDoc PVD = new PopulateViewDoc();

            FormDetails FD = new FormDetails();

            FD = PVD.Populate(ID);

            ViewBag.ID = ID;

            List<string> Environment = new List<string> { "UAT", "Pre-Live", "Live" };
            ViewBag.list = Environment;

            return View("~/Views/UpdateView/UpdateView.cshtml", FD);

        }

        public ActionResult Send(int ID, string FormName) {

            DBFunctions DBF = new DBFunctions();

            List<Senders> send = new List<Senders>();

            send = DBF.FetchSendData();

            ViewBag.FormID = ID;
            ViewBag.FormName = FormName;
            

            return View("~/Views/SendPage/SendPage.cshtml", send);
        
        }

        public ActionResult MarkAsDone(int ID) {

            ViewBag.ID = ID;
            return View("~/Views/MarkAsDone/MarkAsDone.cshtml" );
        
        }

        public ActionResult DeleteForm(int ID) {

            DBFunctions DBF = new DBFunctions();

            DBF.DeleteForm(ID);

            List<HomePage> HP = new List<HomePage>();

            HP = DBF.FetchAll();

            TempData["Message"] = "Form Deleted Successfuly!";

            return RedirectToAction("ReturnHome", HP);

        }

        public ActionResult ReturnHome() {

            DBFunctions DBF = new DBFunctions();

            List<HomePage> HP = new List<HomePage>();

            HP = DBF.FetchAll();

            string Home = HttpContext.Session.GetString("Home");

            return View(Home, HP);

        }

        public ActionResult SignOff(int ID) {

            ViewBag.ID = ID;

            return View("~/Views/SignOff/SignOff.cshtml");

        }

    }
}

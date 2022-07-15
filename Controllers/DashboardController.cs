using Microsoft.AspNetCore.Mvc;
using Mail_X.Other_Classes;
using Mail_X.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace Mail_X.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard() { 

            DBFunctions DBF = new DBFunctions();

            Dashboard dashboard = new Dashboard();

            List<Activity> activity = new List<Activity>();

            dashboard = DBF.GetDashboardData(HttpContext.Session.GetString("DeptID"));

            ViewBag.StatusCount = JsonConvert.SerializeObject(dashboard.StatusCount);
            ViewBag.StatusLabel = JsonConvert.SerializeObject(dashboard.StatusLabel);
            ViewBag.StatusColor = JsonConvert.SerializeObject(dashboard.Color);

            activity = DBF.GetRecentActivity();
        
            return View("~/Views/Dashboard/DashboardDev.cshtml", activity);
        
        }

        public ActionResult AuditPage() {

            DBFunctions DBF = new DBFunctions();

            List<Activity> activity = new List<Activity>();

            activity = DBF.GetAudit();

            return View("~/Views/Audit/Audit.cshtml", activity);
            
        }

        public ActionResult ArchivePage() {

            DBFunctions DBF = new DBFunctions();

            List<HomePage> HP = new List<HomePage>();

            HP = DBF.FetchAllArchive();

            return View("~/Views/Archive/Archive.cshtml", HP);


        }

    }
}

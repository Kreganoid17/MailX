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

            dashboard = DBF.GetDashboardData(HttpContext.Session.GetString("DeptID"));

            ViewBag.StatusCount = JsonConvert.SerializeObject(dashboard.StatusCount);
            ViewBag.StatusLabel = JsonConvert.SerializeObject(dashboard.StatusLabel);
            ViewBag.StatusColor = JsonConvert.SerializeObject(dashboard.Color);
        
            return View("~/Views/Dashboard/DashboardDev.cshtml");
        
        }
    }
}

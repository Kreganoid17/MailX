﻿using Mail_X.Models;
using System.Linq;
using Mail_X.Other_Classes;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Mail_X.Controllers;

namespace Mail_X.Controllers  
{

    public class HomePageController : Controller
    {

        public string SetProc()
        {

            return HttpContext.Session.GetString("Proc");

        }

        public IActionResult HomePage()
        {
            return View();
        }

        public ActionResult StatusPage(int StatusID) {

            string Home = HttpContext.Session.GetString("Home");

            DBFunctions DBF = new DBFunctions();

            List<HomePage> HP = new List<HomePage>();

            HP = DBF.GetHomePageByStatus(StatusID);    
            
            return View(Home,HP);

        }

        public ActionResult ViewDoc(int ID) { 
        
            PopulateViewDoc PVD = new PopulateViewDoc();

            FormDetails FD = new FormDetails();

            FD = PVD.Populate(ID);

            ViewBag.ID = ID;

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

        public ActionResult ArchiveForm(int ID, string PID, string Pname) {

            History history = new History();

            history.EmpID = HttpContext.Session.GetString("ID");
            history.EmpName = HttpContext.Session.GetString("Username");
            history.DeptID = HttpContext.Session.GetString("DeptID");
            history.Description = "Deleted Deployment Form with Project ID: " + PID + " and Project Name: " + Pname;

            DBFunctions DBF = new DBFunctions();

            DBF.ArchiveForm(ID);

            DBF.AddHistory(history);

            List<HomePage> HP = new List<HomePage>();

            HP = DBF.FetchAll(SetProc());

            TempData["Message"] = "Form Archived Successfuly!";

            return RedirectToAction("ReturnHome", HP);

        }

        public ActionResult RestoreForm(int ID, string PID, string Pname)
        {

            History history = new History();

            history.EmpID = HttpContext.Session.GetString("ID");
            history.EmpName = HttpContext.Session.GetString("Username");
            history.DeptID = HttpContext.Session.GetString("DeptID");
            history.Description = "Restored Deployment Form with Project ID: " + PID + " and Project Name: " + Pname;

            DBFunctions DBF = new DBFunctions();

            DBF.RestoreForm(ID);

            DBF.AddHistory(history);

            List<HomePage> HP = new List<HomePage>();

            HP = DBF.FetchAllArchive();

            TempData["Message"] = "Form Restored Successfuly!";

            return View("~/Views/Archive/Archive.cshtml", HP);

        }

        public ActionResult ReturnHome() {

            DBFunctions DBF = new DBFunctions();

            List<HomePage> HP = new List<HomePage>();

            HP = DBF.FetchAll(SetProc());

            string Home = HttpContext.Session.GetString("Home");

            return View(Home, HP);

        }

        public ActionResult SignOff(int ID) {

            ViewBag.ID = ID;

            SignOff SO  = new SignOff();

            SO.SignName = HttpContext.Session.GetString("Username");
            SO.EmpID = HttpContext.Session.GetString("ID");
            SO.DeptName = HttpContext.Session.GetString("DeptName");

            return View("~/Views/SignOff/SignOff.cshtml", SO);

        }

    }
}

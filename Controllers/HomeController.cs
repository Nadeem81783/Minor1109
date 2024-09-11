using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Forensiclab.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Registration()
        {
            return View();
        }
        public ActionResult LabReport()
        {
            return View();
        }
        public ActionResult DptDirectorReport()
        {
            return View();
        }
        public ActionResult ScientReport()
        {
            return View();
        }
        public ActionResult Graph()
        {
            return View();
        }
        public ActionResult DistrictWiseReport()
        {
            return View();
        }
        public ActionResult ScientistWiseReport()
        {
            return View();
        }
        public ActionResult LabWisePerformance()
        {
            return View();
        }
        public ActionResult ScientistWisePerformance()
        {
            return View();
        }
        public ActionResult LaboratoryMaster()
        {
            return View();
        }
        public ActionResult CaseTypeMaster()
        {
            return View();
        }
        public ActionResult DesignationMaster()
        {
            return View();
        }
        public ActionResult UserType()
        {
            return View();
        }
        public ActionResult ScientistMaster()
        {
            return View();
        }
        public ActionResult LabSectionMaster()
        {
            return View();
        }
    }
}
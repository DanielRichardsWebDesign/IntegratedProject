using HealthApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HealthApp.Controllers
{
    public class MessagePageController : Controller
    {
        private HealthAppEntities db = new HealthAppEntities();
        public ActionResult DoctorMessage(string search)
        {
            ViewBag.Title = "Messages";
            int currentDoctor = (Convert.ToInt32(Session["LoggedDoctorID"]));
            var DPMatch = from dpm in db.DoctorPatientMatches.Where(x => x.DoctorID == currentDoctor)
                          select dpm;

            if (!String.IsNullOrEmpty(search))
            {
                DPMatch = DPMatch.Where(p => p.Patient.PatientFName.Contains(search)
                                              || p.Patient.PatientSName.Contains(search)
                                              || p.PatientID.ToString().Contains(search));
            }
            return View(DPMatch.ToList());

        }

        public ActionResult PatientMessage()
        {
            ViewBag.Title = "Messages";
            int currentPatient = (Convert.ToInt32(Session["LoggedPatientID"]));
            var PDMatch = from pdm in db.DoctorPatientMatches.Where(x => x.PatientID == currentPatient)
                          select pdm;

            return View(PDMatch.ToList());
        }
        public ActionResult ToDoctorMessage()
        {
            return RedirectToAction("DoctorMessage", "MessagePage");
        }

        public ActionResult ToPatientMessage()
        {
            return RedirectToAction("PatientMessage", "MessagePage");
        }
    }
}
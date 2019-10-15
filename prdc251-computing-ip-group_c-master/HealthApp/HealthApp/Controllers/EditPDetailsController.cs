using HealthApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HealthApp.Controllers
{
    public class EditPDetailsController : Controller
    {
        private HealthAppEntities db = new HealthAppEntities();
        // GET: EditDetails
        /*public ActionResult Index()
        {
            return View();
        }*/

        //Getting patient information
        // public ActionResult Index()
        //  {
        //    var patientDetails = db.Patients.Include(p => p.PatientFName).Include(p => p.PatientSName).Include
        //      (p => p.PatientEmail).Include(p => p.PatientPass);
        //return View(patientDetails.ToList());
        //}
        public ActionResult EditPatient(int? id)
        {
            id = Convert.ToInt32(Session["LoggedPatientId"]);
            
            Patient patient = db.Patients.Find(id);
            
            return View(patient);
    }
        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPatient([Bind(Include = "PatientID,PatientFName,PatientSName,PatientEmail,PatientLoginCode,PatientPass")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                db.Entry(patient).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("PatientHome", "Home", new { @area = "Home" });
            }
            return View(patient);
        }

    }


}
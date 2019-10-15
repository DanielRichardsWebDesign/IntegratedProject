using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HealthApp.Models;
using Microsoft.AspNet.Identity;

namespace HealthApp.Controllers
{
    public class HomeController : Controller
    {
        private HealthAppEntities db = new HealthAppEntities();

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

        public ActionResult DoctorLogin()
        {
            return View();
        }

        public ActionResult PatientLogin()
        {
            return View();
        }


        public ActionResult DoctorHome(string search)
        {
            ViewBag.Title = "Dashboard";

            var conditions = db.Conditions.Include(c => c.Patient);
            var patients = from p in db.Patients select p;

            if(!String.IsNullOrEmpty(search))
            {                
                conditions = conditions.Where(p => p.Patient.PatientFName.Contains(search)
                                              || p.Patient.PatientSName.Contains(search)
                                              || p.Condition1.Contains(search)
                                              || p.PatientID.ToString().Contains(search));                
            }
            return View(conditions.ToList());         
        }

        public ActionResult PatientHome()
        {
            ViewBag.Title = "Dashboard";
            return View();
        }

            // GET: Conditions/Edit/5
            public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Condition condition = db.Conditions.Find(id);
            if (condition == null)
            {
                return HttpNotFound();
            }
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "PatientFName", "PatientSName", condition.PatientID);
            return RedirectToAction("Edit", "Conditions", new { id = condition.ConditionID });
        }

        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Condition condition = db.Conditions.Find(id);
            if(condition == null)
            {
                return HttpNotFound();
            }
            
            return RedirectToAction("Delete", "Conditions", new { id = condition.ConditionID });
        }

        public ActionResult Create()
        {
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "PatientFName");
            return RedirectToAction("Create", "Conditions");
        }
    }
}
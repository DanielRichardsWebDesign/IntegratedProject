using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HealthApp.Models;

namespace HealthApp.Views.Conditions
{
    public class ConditionsController : Controller
    {
        private HealthAppEntities db = new HealthAppEntities();

        // GET: Conditions
        public ActionResult Index()
        {
            var conditions = db.Conditions.Include(c => c.Patient);            
            return View(conditions.ToList());
        }

        public ActionResult Dashboard()
        {
            var conditions = db.Conditions.Include(c => c.Patient);
            return View(conditions.ToList());
        }

        // GET: Conditions/Details/5
        public ActionResult Details(int? id)
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
            return View(condition);
        }

        // GET: Conditions/Create
        public ActionResult Create()
        {
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "PatientFName");
            return View();
        }

        // POST: Conditions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConditionID,PatientID,Condition1,ConditionNotes")] Condition condition)
        {
            if (ModelState.IsValid)
            {
                db.Conditions.Add(condition);
                db.SaveChanges();
                return RedirectToAction("Dashboard", "Home", new { area = "Home" });
            }

            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "PatientFName", condition.PatientID);
            return View(condition);
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
            return View(condition);
        }

        // POST: Conditions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConditionID,PatientID,Condition1,ConditionNotes")] Condition condition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(condition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Dashboard", "Home", new { area = "" });
            }
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "PatientFName", condition.PatientID);
            return View(condition);
        }

        // GET: Conditions/Delete/5
        public ActionResult Delete(int? id)
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
            return View(condition);
        }

        // POST: Conditions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Condition condition = db.Conditions.Find(id);
            db.Conditions.Remove(condition);
            db.SaveChanges();
            return RedirectToAction("Dashboard", "Home", new { area = "Home" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HealthApp.Models;
using HealthApp.ViewModel;

namespace HealthApp.Views.Surveys
{
    public class SurveysController : Controller
    {
        private HealthAppEntities db = new HealthAppEntities();

        // GET: Surveys
        public ActionResult Index()
        {
            var surveys = db.Surveys.Include(s => s.Doctor).Include(s => s.Patient);
            return View(surveys.ToList());
        }

        public ActionResult CreatedSurveys(string doctorID)
        {
            doctorID = Convert.ToString(Session["LoggedDoctorId"]);

            var surveys = db.Surveys.Include(s => s.Patient).Where(s => s.DoctorID.ToString() == doctorID);

            return View(surveys.ToList());
        }

        public ActionResult SurveyDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var survey = db.Surveys.Where(s => s.SurveyID == id)
                                   .Include(s => s.Questions);

            if (survey == null)
            {
                return HttpNotFound();
            }

            return View(survey);
        }

        public ActionResult ReviewSurvey(string id)
        {
            id = Convert.ToString(Session["SurveyID"]);            

            var surveys = db.Surveys.Where(s => s.SurveyID.ToString() == id)
                                    .Include(s => s.Questions).ToList();

            return View(surveys);            
        }

        
        public ActionResult FinaliseSurvey()
        {
            var id = Convert.ToString(Session["SurveyID"]);

            //Update Survey status
            string mainConn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection sqlConn = new SqlConnection(mainConn);
            string query = "UPDATE [dbo].Survey SET SurveyStatus = 'Awaiting Completion' WHERE SurveyID = @SurveyID";
            SqlCommand sqlComm = new SqlCommand(query, sqlConn);
            sqlConn.Open();
            sqlComm.Parameters.AddWithValue("@SurveyID", id);
            sqlComm.ExecuteNonQuery();
            sqlConn.Close();

            //Delete sessions for created survey
            Session.Remove("SurveyID");
            Session.Remove("SurveyTitle");
            Session.Remove("SurveyQuestionAmount");
            Session.Remove("QuestionAmount");

            return RedirectToAction("CreatedSurveys");
        }        

        // GET: Surveys/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // GET: Surveys/Create
        public ActionResult Create()
        {
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "DoctorFName");
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "PatientFName");
            return View();
        }

        // POST: Surveys/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SurveyID,DoctorID,PatientID,SurveyTitle,SurveyFreq,SurveyDesc,SurveyQuestionAmount")] Survey survey)
        {
            if (ModelState.IsValid)
            {
                db.Surveys.Add(survey);
                db.SaveChanges();

                //Creates Session based on Survey ID - 
                //Used to ensure that you are creating questions for right survey
                Session["SurveyID"] = survey.SurveyID.ToString();
                Session["SurveyTitle"] = survey.SurveyTitle.ToString();

                //Creates Session based on Survey Question Amount
                Session["SurveyQuestionAmount"] = survey.SurveyQuestionAmount.ToString();

                return RedirectToAction("Create", "Questions", new { area = "Questions" });
            }

            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "DoctorFName", survey.DoctorID);
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "PatientFName", survey.PatientID);
            return View(survey);
        }

        // GET: Surveys/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "DoctorFName", survey.DoctorID);
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "PatientFName", survey.PatientID);
            return View(survey);
        }

        // POST: Surveys/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SurveyID,DoctorID,PatientID,SurveyTitle,SurveyDesc,SurveyQuestionAmount")] Survey survey)
        {
            if (ModelState.IsValid)
            {
                db.Entry(survey).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DoctorID = new SelectList(db.Doctors, "DoctorID", "DoctorFName", survey.DoctorID);
            ViewBag.PatientID = new SelectList(db.Patients, "PatientID", "PatientFName", survey.PatientID);
            return View(survey);
        }

        // GET: Surveys/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Survey survey = db.Surveys.Find(id);
            if (survey == null)
            {
                return HttpNotFound();
            }
            return View(survey);
        }

        // POST: Surveys/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Survey survey = db.Surveys.Find(id);
            db.Surveys.Remove(survey);
            db.SaveChanges();
            return RedirectToAction("Index");
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

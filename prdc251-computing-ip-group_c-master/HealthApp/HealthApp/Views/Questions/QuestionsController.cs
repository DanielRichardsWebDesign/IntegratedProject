using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HealthApp.Models;

namespace HealthApp.Views.Questions
{
    public class QuestionsController : Controller
    {
        private HealthAppEntities db = new HealthAppEntities();

        // GET: Questions
        public ActionResult Index()
        {
            var questions = db.Questions.Include(q => q.Survey);
            return View(questions.ToList());
        }

        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Questions/Create
        public ActionResult Create()
        {   
            if(Session["SurveyID"] != null)
            {
                if (Session["QuestionAmount"] == null)
                {
                    Session["QuestionAmount"] = 0;
                }

                if (Convert.ToInt32(Session["QuestionAmount"]) == Convert.ToInt32(Session["SurveyQuestionAmount"]))
                {
                    Session.Remove("QuestionAmount");

                    return RedirectToAction("ReviewSurvey", "Surveys", new { area = "Surveys" });
                }

                return View();
            }
            else
            {
                return RedirectToAction("Dashboard", "Home", new { area = "Home" });
            }
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuestionID,SurveyID,Question1,QuestionType")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Questions.Add(question);
                db.SaveChanges();

                //Counts amount of Questions Created
                Session["QuestionAmount"] = Convert.ToInt32(Session["QuestionAmount"]) + 1;               
                
                //Redirect to AnswersController if the QuestionType isn't these specified question types
                if(question.QuestionType == "Free Text" || question.QuestionType == "True Or False")
                {
                    Session["QuestionID"] = Convert.ToString(question.QuestionID);

                    //Update QuestionAnswer table with dummy data since these types of questions don't need specific answers
                    string mainConn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    SqlConnection sqlConn = new SqlConnection(mainConn);
                    string query = "INSERT INTO [dbo].QuestionAnswers VALUES (@QuestionID, @QuestionAnswer)";
                    SqlCommand sqlComm = new SqlCommand(query, sqlConn);
                    sqlConn.Open();
                    sqlComm.Parameters.AddWithValue("@QuestionID", Convert.ToInt32(Session["QuestionID"]));
                    sqlComm.Parameters.AddWithValue("@QuestionAnswer", "N/A");
                    sqlComm.ExecuteNonQuery();
                    sqlConn.Close();

                    return RedirectToAction("Create");
                }

                if(question.QuestionType == "Numeric Answer")
                {
                    Session["QuestionID"] = Convert.ToString(question.QuestionID);

                    //Update QuestionAnswer table with dummy data since these types of questions don't need specific answers
                    string mainConn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    SqlConnection sqlConn = new SqlConnection(mainConn);
                    string query = "INSERT INTO [dbo].QuestionAnswers VALUES (@QuestionID, @QuestionAnswer)";
                    SqlCommand sqlComm = new SqlCommand(query, sqlConn);
                    sqlConn.Open();
                    sqlComm.Parameters.AddWithValue("@QuestionID", Convert.ToInt32(Session["QuestionID"]));
                    sqlComm.Parameters.AddWithValue("@QuestionAnswer", "N/A");
                    sqlComm.ExecuteNonQuery();
                    sqlConn.Close();

                    return RedirectToAction("RuleParameters");
                }

                //Redirect to a page where you can upload an audio or video file
                if(question.QuestionType == "Audio")
                {
                    Session["QuestionType"] = Convert.ToString(question.QuestionType);
                    Session["QuestionID"] = Convert.ToString(question.QuestionID);
                    Session["Question"] = Convert.ToString(question.Question1);

                    return RedirectToAction("UploadAudio");
                }

                if(question.QuestionType == "Video")
                {
                    Session["QuestionType"] = Convert.ToString(question.QuestionType);
                    Session["QuestionID"] = Convert.ToString(question.QuestionID);
                    Session["Question"] = Convert.ToString(question.Question1);

                    return RedirectToAction("UploadVideo");
                }

                else
                {
                    //Create session type in order to modify the appearance of the QuestionAnswers view
                    Session["QuestionType"] = Convert.ToString(question.QuestionType);
                    Session["QuestionID"] = Convert.ToString(question.QuestionID);
                    Session["Question"] = Convert.ToString(question.Question1);

                    return RedirectToAction("Create", "QuestionAnswers", new { area = "QuestionAnswers" });                    
                }                
            }

            if (Convert.ToInt32(Session["QuestionAmount"]) == Convert.ToInt32(Session["SurveyQuestionAmount"]))
            {
                return RedirectToAction("ReviewSurvey", "Surveys", new { area = "Surveys" });
            }

            ViewBag.SurveyID = new SelectList(db.Surveys, "SurveyID", "SurveyTitle", question.SurveyID);
            return View(question);
        }

        // GET: Questions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            ViewBag.SurveyID = new SelectList(db.Surveys, "SurveyID", "SurveyTitle", question.SurveyID);
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuestionID,SurveyID,Question1,QuestionType")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.SurveyID = new SelectList(db.Surveys, "SurveyID", "SurveyTitle", question.SurveyID);
            return View(question);
        }

        // GET: Questions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
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

        public ActionResult UploadAudio()
        {
            return View();
        }

        //[HttpPost]
        //public ActionResult UploadAudio(HttpPostedFileBase audioFile)
        //{

        //}

        public ActionResult UploadVideo()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadVideo(HttpPostedFileBase videoFile)
        {
            if(videoFile != null)
            {
                string fileName = Path.GetFileName(videoFile.FileName);

                //Only accepts if file size is below this value
                if(videoFile.ContentLength < 104857600)
                {
                    videoFile.SaveAs(Server.MapPath("/Videos/"+fileName));

                    string mainConn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                    SqlConnection sqlConn = new SqlConnection(mainConn);
                    string query = "INSERT INTO [dbo].Videos VALUES (@QuestionID, @VideoName, @VideoPath)";
                    SqlCommand sqlComm = new SqlCommand(query, sqlConn);
                    sqlConn.Open();
                    sqlComm.Parameters.AddWithValue("@QuestionID", Convert.ToInt32(Session["QuestionID"]));
                    sqlComm.Parameters.AddWithValue("@VideoName", fileName);
                    sqlComm.Parameters.AddWithValue("@VideoPath","/Videos/"+fileName);
                    sqlComm.ExecuteNonQuery();
                    sqlConn.Close();

                    ViewData["Message"] = "Video Uploaded Successfully.";
                }
            }
            return RedirectToAction("Create", "QuestionAnswers", new { @area = "QuestionAnswers" });
        }

        public ActionResult RuleParameters()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RuleParameters([Bind(Include = "QuestionID,RuleParam")] QuestionRule rules)
        {
            db.QuestionRules.Add(rules);
            db.SaveChanges();

            Session.Remove("QuestionID");

            return RedirectToAction("Create");
        }
    }
}

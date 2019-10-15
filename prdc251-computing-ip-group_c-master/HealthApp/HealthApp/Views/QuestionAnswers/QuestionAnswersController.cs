using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HealthApp.Models;

namespace HealthApp.Views.QuestionAnswers
{
    public class QuestionAnswersController : Controller
    {
        private HealthAppEntities db = new HealthAppEntities();

        // GET: QuestionAnswers
        public ActionResult Index()
        {
            var questionAnswers = db.QuestionAnswers.Include(q => q.Question);
            return View(questionAnswers.ToList());
        }

        // GET: QuestionAnswers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionAnswer questionAnswer = db.QuestionAnswers.Find(id);
            if (questionAnswer == null)
            {
                return HttpNotFound();
            }
            return View(questionAnswer);
        }

        // GET: QuestionAnswers/Create
        public ActionResult Create()
        {
            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Question1");
            return View();
        }

        // POST: QuestionAnswers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "QuestionAnswerID,QuestionID,QuestionAnswer1")] QuestionAnswer questionAnswer)
        {
            if (ModelState.IsValid)
            {
                db.QuestionAnswers.Add(questionAnswer);
                db.SaveChanges();

                if(Session["QuestionType"].ToString() == "Multiple Choice")
                {
                    Session["QuestionAnswerAmount"] = Convert.ToInt32(Session["QuestionAnswerAmount"]) + 1;

                    if(Convert.ToInt32(Session["QuestionAnswerAmount"]) < 4)
                    {
                        return RedirectToAction("Create");
                    }
                    else
                    {
                        Session.Remove("QuestionAnswerAmount");
                        Session.Remove("QuestionID");
                        Session.Remove("Question");

                        return RedirectToAction("Create", "Questions", new { @area = "Questions" });
                    }
                }

                //if(Session["QuestionType"].ToString() == "Audio")
                //{
                //    Session["QuestionAnswerAmount"] = Convert.ToInt32(Session["QuestionAnswerAmount"]) + 1;

                //    if (Request.Files.Count > 0)
                //    {
                //        var file = Request.Files[0];

                //        if (file != null && file.ContentLength > 0)
                //        {
                //            var fileName = Path.GetFileName(file.FileName);
                //            var filePath = Path.Combine(Server.MapPath("~/Audio/"), fileName);
                //            file.SaveAs(filePath);

                //            Session.Remove("QuestionAnswerAmount");
                //            Session.Remove("QuestionID");
                //            Session.Remove("Question");

                //            return RedirectToAction("Create", "Questions", new { @area = "Questions" });
                //        }
                //    }
                //}

                //if(Session["QuestionType"].ToString() == "Video")
                //{
                //    Session["QuestionAnswerAmount"] = Convert.ToInt32(Session["QuestionAnswerAmount"]) + 1;

                //    if (Request.Files.Count > 0)
                //    {
                //        var file = Request.Files[0];

                //        if (file != null && file.ContentLength > 0)
                //        {
                //            var fileName = Path.GetFileName(file.FileName);
                //            var filePath = Path.Combine(Server.MapPath("~/Video/"), fileName);
                //            file.SaveAs(filePath);

                //            Session.Remove("QuestionAnswerAmount");
                //            Session.Remove("QuestionID");
                //            Session.Remove("Question");

                //            return RedirectToAction("Create", "Questions", new { @area = "Questions" });
                //        }
                //    }
                //}

                return RedirectToAction("Index");
            }

            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Question1", questionAnswer.QuestionID);
            return View(questionAnswer);
        }

        // POST: QuestionAnswers/Upload Video/Audio File
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload()
        {
            if(Session["QuestionType"].ToString() == "Audio")
            {
                if(Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if(file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var filePath = Path.Combine(Server.MapPath("~/Audio/"), fileName);
                        file.SaveAs(filePath);
                    }
                }
            }
            
            if(Session["QuestionType"].ToString() == "Video")
            {
                if(Request.Files.Count > 0)
                {
                    var file = Request.Files[0];

                    if(file != null && file.ContentLength > 0)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var filePath = Path.Combine(Server.MapPath("~/Video/"), fileName);
                        file.SaveAs(filePath);
                    }
                }
            }

            return RedirectToAction("Create", "Questions", new { @area = "Questions" });
        }

        // GET: QuestionAnswers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionAnswer questionAnswer = db.QuestionAnswers.Find(id);
            if (questionAnswer == null)
            {
                return HttpNotFound();
            }
            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Question1", questionAnswer.QuestionID);
            return View(questionAnswer);
        }

        // POST: QuestionAnswers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "QuestionAnswerID,QuestionID,QuestionAnswer1")] QuestionAnswer questionAnswer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(questionAnswer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.QuestionID = new SelectList(db.Questions, "QuestionID", "Question1", questionAnswer.QuestionID);
            return View(questionAnswer);
        }

        // GET: QuestionAnswers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QuestionAnswer questionAnswer = db.QuestionAnswers.Find(id);
            if (questionAnswer == null)
            {
                return HttpNotFound();
            }
            return View(questionAnswer);
        }

        // POST: QuestionAnswers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            QuestionAnswer questionAnswer = db.QuestionAnswers.Find(id);
            db.QuestionAnswers.Remove(questionAnswer);
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

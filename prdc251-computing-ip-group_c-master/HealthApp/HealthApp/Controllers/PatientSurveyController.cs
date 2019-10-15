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

namespace HealthApp.Controllers
{
    public class PatientSurveyController : Controller
    {
        private HealthAppEntities db = new HealthAppEntities();

        // GET: PatientSurvey
        public ActionResult SurveyPreview(string id)
        {
            Session["TimeStarted"] = DateTime.Now;

            id = Convert.ToString(Session["LoggedPatientId"]);

            var survey = db.Surveys.Where(s => s.SurveyStatus.ToString() == "Awaiting Completion" &&
                                               s.PatientID.ToString() == id);

            return View(survey);
        }

        public ActionResult PatientSurvey(int? id)
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

            Session["PatientSurveyID"] = id;

            return View(survey);
        }
                
        public ActionResult PatientSurveySubmit(int? id)
        {

            //Attempts at posting answers script below\\

            //Session["SurveyID"] = survey.SurveyID.ToString();
            //var surveyID = Session["SurveyID"].ToString();
            //var patientID = Convert.ToString(Session["LoggedPatientId"]);
            //Session["QuestionID"] = question.QuestionID.ToString();
            //var questionID = Convert.ToString(Session["QuestionID"]);

            ////Insert Into PatientSurveyAnswers table
            //string mainConn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //SqlConnection sqlConn = new SqlConnection(mainConn);
            //string query = "INSERT INTO [dbo].PatientSurveyAnswers VALUES (@PatientID, @QuestionID, @QuestionAnswerID, " +
            //                                                              "@TimeStarted, @TimeSubmitted)";
            //SqlCommand sqlComm = new SqlCommand(query, sqlConn);
            //sqlComm.Parameters.AddWithValue("@SurveyID", surveyID);
            //sqlComm.Parameters.AddWithValue("@PatientID", patientSurveyAnswer.PatientID);
            //sqlComm.Parameters.AddWithValue("@QuestionID", patientSurveyAnswer.QuestionID);
            //sqlComm.Parameters.AddWithValue("@QuestionAnswerID", patientSurveyAnswer.QuestionAnswerID);
            //sqlComm.Parameters.AddWithValue("@TimeStarted", timeStarted);
            //sqlComm.Parameters.AddWithValue("@TimeSubmitted", timeSubmitted);
            //sqlComm.ExecuteNonQuery();
            //sqlConn.Close();

            //Time variables for specific times when started and submitted.
            //var timeStarted = Convert.ToDateTime(Session["TimeStarted"]);
            //Session["TimeSubmitted"] = DateTime.Now;
            //var timeSubmitted = Convert.ToDateTime(Session["TimeSubmitted"]);

            //PatientSurveyAnswer psa = new PatientSurveyAnswer();
            //psa.SurveyID = survey.SurveyID;
            //psa.PatientID = survey.PatientID;
            //psa.QuestionID =
            //psa.QuestionAnswerID = questionAnswer.QuestionAnswerID;
            //psa.PatientSurveyAnswer1 = questionAnswer.QuestionAnswer1;
            //psa.TimeStarted = timeStarted;
            //psa.TimeSubmitted = timeSubmitted;

            ////db.PatientSurveyAnswers.Add(psa);
            ////db.SaveChanges();
            ///

            id = Convert.ToInt32(Session["PatientSurveyID"]);

            //Update Survey status
            string mainConn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection sqlConn = new SqlConnection(mainConn);
            string query = "UPDATE [dbo].Survey SET SurveyStatus = 'Completed' WHERE SurveyID = @SurveyID";
            SqlCommand sqlComm = new SqlCommand(query, sqlConn);
            sqlConn.Open();
            sqlComm.Parameters.AddWithValue("@SurveyID", id);
            sqlComm.ExecuteNonQuery();
            sqlConn.Close();

            Session.Remove("PatientSurveyID");

            return RedirectToAction("SubmitSuccessful");
        }

        public ActionResult SubmitSuccessful()
        {
            return View();
        }
    }
}
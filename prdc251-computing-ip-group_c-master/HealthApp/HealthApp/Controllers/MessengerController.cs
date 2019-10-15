using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HealthApp.Controllers
{
    public class MessengerController : Controller
    {
        public ActionResult Messenger()
        {
            //Session["MessageMatch"] = match;
            //ViewBag.Chatroom = match;
            return View();
        }
        [HttpPost]
        public ActionResult DoctorMessenger(string match)
        {
            var matchid = match;
            Session["MessageMatch"] = matchid;
            //Session["MessageTarget"] = DPM.PatientFName;
            //Creates a session called MessageMatch
            //var matchid = db.DoctorPatientMatches.Where(item => item.MatchID == DPM.MatchID);
            //Session["MessageMatch"] = matchid;
            //Not sure if this is useful anymore
            //int currentDPMatch = (Convert.ToInt32(Session["MessageMatch"]));
            //var messagematch = from mm in db.DoctorPatientMatches.Where(m => m.MatchID == currentDPMatch)
            //select mm;           
            if (Session["LoggedDoctorID"] != null) // if Doctor isn't logged in
            {
                var name = (Convert.ToString(Session["LoggedDoctorFName"]));
                //var matchid = db.DoctorPatientMatches.Where(item => item.MatchID == DPM.MatchID);
                Session["name"] = name;
            }

            return RedirectToAction("Messenger", "Messenger");
        }

        [HttpPost]
        public ActionResult PatientMessenger(string match)
        {
            var matchid = match;
            Session["MessageMatch"] = matchid;
            //Creates a session called MessageMatch
            //Session["MessageMatch"] = db.DoctorPatientMatches.Where(item => item.MatchID == DPM.MatchID);
            //Not sure if this is useful anymore
            //int currentDPMatch = (Convert.ToInt32(Session["MessageMatch"]));
            //var messagematch = from mm in db.DoctorPatientMatches.Where(m => m.MatchID == currentDPMatch)
            //select mm;

            if (Session["LoggedPatientID"] != null) // if patient is logged in
            {
                var name = (Convert.ToString(Session["LoggedPatientFName"]));
                Session["name"] = name;
            }
            return RedirectToAction("Messenger", "Messenger");
        }
    }
}
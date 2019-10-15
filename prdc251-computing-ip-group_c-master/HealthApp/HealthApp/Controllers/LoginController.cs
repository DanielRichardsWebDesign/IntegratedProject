using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using HealthApp.Models;

namespace HealthApp.Views.Login
{
    public class LoginController : Controller
    {

        public ActionResult DoctorLogin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoctorLogin(HealthApp.Models.Doctor da)
        {
            using (HealthAppEntities dl = new HealthAppEntities())
            {
                var dlv = dl.Doctors.Where(d => d.DoctorEmail == da.DoctorEmail && d.DoctorPass == da.DoctorPass).FirstOrDefault();
                if (dlv == null)
                {
                    da.LoginErrorMessage = "Incorrect username or password";
                    return View("DoctorLogin", da);
                }
                else
                {
                    Session["LoggedDoctorId"] = dlv.DoctorID;
                    Session["LoggedDoctorFName"] = dlv.DoctorFName;
                    Session["LoggedDoctorLName"] = dlv.DoctorSName;
                    return RedirectToAction("DoctorHome", "Home");
                }
            }

        }

        public ActionResult PatientLogin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PatientLogin(HealthApp.Models.Patient pa)
        {
            using (HealthAppEntities pl = new HealthAppEntities())
            {
                var plv = pl.Patients.Where(p => p.PatientEmail == pa.PatientEmail && p.PatientPass == pa.PatientPass).FirstOrDefault();
                if (plv == null)
                {
                    pa.LoginErrorMessage = "Incorrect username or password";
                    return View("PatientLogin", pa);
                }
                else
                {
                    Session["LoggedPatientId"] = plv.PatientID;
                    Session["LoggedPatientFName"] = plv.PatientFName;
                    Session["LoggedPatientLName"] = plv.PatientSName;
                    return RedirectToAction("PatientHome", "Home");
                }
            }

        }

        public ActionResult PatientLoginCode()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PatientLoginCode(HealthApp.Models.Patient pal)
        {
            using (HealthAppEntities pl = new HealthAppEntities())
            {
                var plcv = pl.Patients.Where(p => p.PatientLoginCode == pal.PatientLoginCode).FirstOrDefault();
                if (plcv == null)
                {
                    pal.LoginErrorMessage = "Incorrect username or password";
                    return View("PatientLoginCode", pal);
                }
                else
                {
                    Session["LoggedPatientId"] = plcv.PatientID;
                    Session["LoggedPatientFName"] = plcv.PatientFName;
                    Session["LoggedPatientLName"] = plcv.PatientSName;
                    return RedirectToAction("PatientHome", "Home");
                }
            }

        }

        public ActionResult ToDoctor()
        {
            return RedirectToAction("DoctorLogin", "Login");
        }

        public ActionResult ToPatient()
        {
            return RedirectToAction("PatientLogin", "Login");
        }

        public ActionResult ToPatientCode()
        {
            return RedirectToAction("PatientLoginCode", "Login");
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("DoctorLogin", "Login");
        }

        // POST: /Account/Login
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Login(Doctor model, string returnUrl)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    // Require the user to have a confirmed email before they can log on.
        //    // var user = await UserManager.FindByNameAsync(model.Email);
        //    var user = UserManager.Find(model.Email, model.Password);
        //    if (user != null)
        //    {
        //        if (!await UserManager.IsEmailConfirmedAsync(user.Id))
        //        {
        //            string callbackUrl = await SendEmailConfirmationTokenAsync(user.Id, "Confirm your account-Resend");

        //            // Uncomment to debug locally  
        //            // ViewBag.Link = callbackUrl;
        //            ViewBag.errorMessage = "You must have a confirmed email to log on. "
        //                                 + "The confirmation token has been resent to your email account.";
        //            return View("Error");
        //        }
        //    }

        //    // This doesn't count login failures towards account lockout
        //    // To enable password failures to trigger account lockout, change to shouldLockout: true
        //    var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
        //    switch (result)
        //    {
        //        case SignInStatus.Success:
        //            return RedirectToLocal(returnUrl);
        //        case SignInStatus.LockedOut:
        //            return View("Lockout");
        //        case SignInStatus.RequiresVerification:
        //            return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
        //        case SignInStatus.Failure:
        //        default:
        //            ModelState.AddModelError("", "Invalid login attempt.");
        //            return View(model);
        //    }
        //}
    }
}

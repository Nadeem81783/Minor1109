using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Forensiclab.DBLayer;
using Forensiclab.Models;

namespace Forensiclab.Controllers
{
    public class LoginController : Controller
    {
        DB_Layer DB = new DB_Layer();
        // GET: Login
        public ActionResult Logout()
        {
            Session.Abandon();
            Session.Clear();
            Response.Cookies.Clear();
            return RedirectToAction("UserLogin", "Login");
        }
        public ActionResult ChangePassword()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(UserLogin Model)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            try
            {
                if (!ModelState.IsValid)
                {
                    Model.UserId = Convert.ToInt32(Session["UserId"].ToString());
                    Model.ProcId = 3;
                    UserLogin Log = new UserLogin();
                    Log.UserId = Model.UserId;
                    Log.UPR = Model.ConfimPassword;
                    Log.UPassword = Model.NewPassword;
                    Model.IULogin = DB.GetAdminLogin(Model).ToList();
                    if (Model.IULogin[0].msg == "Success")
                    {

                        if (Log.UPassword == Log.UPR)
                        {
                            Log.ProcId = 2;
                            var s = DB.GetAdminLogin(Log).ToList();
                            TempData["Message"] = "Password Changed Successfully";
                        }
                        else
                        {
                            TempData["Message"] = "New Password and Confirmed Password does not matched";
                        }
                    }
                    else
                    {
                        TempData["Message"] = "Your Password is Invalid!";
                    }
                    ModelState.Clear();
                    //return View();
                    return RedirectToAction("UserLogin", "Login");

                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    return RedirectToAction("ChangePassword", "Login");
                }
            }
            catch (Exception e)
            {
                TempData["Message"] = " Error!!! contact Administrator" + e.Message;
                return View();
            }
            //return View();
        }
        public ActionResult CaptchaIndex()
        {
            string[] strArray = new string[36];
            strArray = new string[] { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };//, "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            Random autoRand = new Random();
            string strCaptcha = string.Empty;
            for (int i = 0; i < 6; i++)
            {
                int j = Convert.ToInt32(autoRand.Next(0, 26));
                strCaptcha += strArray[j].ToString();
            }
            Session["Captcha"] = strCaptcha;
            ImageConverter converter = new ImageConverter();
            Response.BinaryWrite((byte[])converter.ConvertTo(CaptchaGeneration(strCaptcha), typeof(byte[])));
            return View();
        }
        public Bitmap CaptchaGeneration(string captchatxt)
        {
            Bitmap bmp = new Bitmap(133, 48);
            using (Graphics graphics = Graphics.FromImage(bmp))
            {
                Font font = new Font("Tahoma", 14);
                graphics.FillRectangle(new SolidBrush(Color.LightYellow), 0, 0, bmp.Width, bmp.Height);
                graphics.DrawString(captchatxt, font, new SolidBrush(Color.Black), 25, 10);
                graphics.Flush();
                font.Dispose();
                graphics.Dispose();
            }
            return bmp;
        }
        public ActionResult UserLogin()
        {
            UserLogin Model = new UserLogin();
            return View(Model);
        }
        [HttpPost]
        public ActionResult UserLogin(UserLogin Model, string command)
        {
            return RedirectToAction("Dashboard", "Home");
            ////if (command == "Login")
            ////{
            //try
            //{
            //    //if (Model.txtCaptcha == Session["Captcha"].ToString())
            //    //{

            //    if (Model.UserName != null)
            //    {
            //        Model.ProcId = 1;
            //        Model.IULogin = DB.GetPasswrdDetails(Model).ToList();
            //        if (Model.IULogin.Count > 0)
            //        {
            //            if (Model.IULogin[0].UserId > 0)
            //            {
            //                Session["UserName"] = Model.IULogin[0].UserName;
            //                Session["UserId"] = Model.IULogin[0].UserId;
            //                Session["OfficeId"] = Model.IULogin[0].OfficeId;
            //                Session["UserType"] = Model.IULogin[0].UtypeId;
            //                Session["OfficeCode"] = Model.IULogin[0].AgencyCode;
            //                if (Model.IULogin[0].UtypeId == 1)
            //                    return RedirectToAction("Dashboard", "Home");
            //                else
            //                {
            //                    return RedirectToAction("Dashboard", "Home");
            //                }
            //            }
            //            else
            //            {
            //                TempData["Message"] = "Invalid User";
            //            }
            //        }
            //        else
            //        {
            //            TempData["Message"] = "Invalid Username or Password";
            //        }
            //    }
            //    else
            //    {
            //        TempData["Message"] = "Please Provide User Details !";
            //        return RedirectToAction("DashboardAdmin", "Login");
            //    }
            //    //}
            //    //else
            //    //{
            //    //    TempData["Message"] = "Capthca Not Valid !";
            //    //    return View();
            //    //}
            //}
            //catch (Exception e)
            //{
            //    TempData["Message"] = " Error!!! contact info@mectoi.in";
            //    return View();
            //}
            ////}
            ////else
            ////{
            ////    UserLogin M = new UserLogin();
            ////    M.Email = Model.Email;
            ////    M.UserType = Model.UserType;
            ////    M.UPassword = Model.UPassword;
            ////    return View(M);
            ////}

            //return View();
        }
        public ActionResult FirstChangePassword()
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            TempData["Message"] = "1";
            return View();
        }
        [HttpPost]
        public ActionResult FirstChangePassword(UserLogin Model)
        {
            if (Session["UserId"] == null)
            {
                return RedirectToAction("UserLogin", "Login");
            }
            try
            {
                if (!ModelState.IsValid)
                {
                    Model.UserId = Convert.ToInt32(Session["UserId"].ToString());
                    Model.ProcId = 3;
                    UserLogin Log = new UserLogin();
                    Log.UserId = Model.UserId;
                    Log.UPR = Model.ConfimPassword;
                    Log.UPassword = Model.NewPassword;
                    Model.IULogin = DB.GetAdminLogin(Model).ToList();
                    if (Model.IULogin[0].msg == "Success")
                    {

                        if (Log.UPassword == Log.UPR)
                        {
                            Log.ProcId = 2;
                            var s = DB.GetAdminLogin(Log).ToList();
                            TempData["Message"] = "Password Changed Successfully";
                        }
                        else
                        {
                            TempData["Message"] = "New Password and Confirmed Password does not matched";
                        }
                    }
                    else
                    {
                        TempData["Message"] = "Your Password is Invalid!";
                    }
                    ModelState.Clear();
                    //return View();
                    return RedirectToAction("UserLogin", "Login");

                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    return RedirectToAction("ChangePassword", "Login");
                }
            }
            catch (Exception e)
            {
                TempData["Message"] = " Error!!! contact Administrator";
                return View();
            }
            //return View();
        }
    }
}
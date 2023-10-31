using MimeKit;
using Sample1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace Sample1.Controllers
{
    public class UserRegistrationController : Controller
    {

        int count = 1;// GET: UserRegistration
        DevBoxEntities1 db = new DevBoxEntities1();
        public ActionResult Index()
        {

            return View();
        }

        // GET: UserRegistration/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: UserRegistration/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserRegistration/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                if (collection != null)
                {
                    var model = new UsersRegistration
                    {

                        
                        Name = collection["Name"],
                        MobileNumber=collection["MobileNumber"],
                        Email=collection["Email"],
                        

                    };
                    db.Entry(model).State = EntityState.Added;
                    db.SaveChanges();
                    String email = collection["Email"];
                    Session["Email"] = email;
                }
                MailVerification();
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                ViewBag.Message = "An error occurred while adding data User already exsists.";
                return View();
            }
        }

        // GET: UserRegistration/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserRegistration/Edit/5
       [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UserRegistration/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserRegistration/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public String MailVerification()
        {


            /*            string fromMail = "venki.nani2001@gmail.com";
                        string toMail = "venkynani.maddineni@gmail.com";
                        string pass = "E.pranav@123"; // Use the app password you generated

                        MailAddress to = new MailAddress(toMail);
                        MailAddress from = new MailAddress(fromMail);

                        using (MailMessage mail = new MailMessage(from, to))
                        {
                            mail.Subject = "Verify your email within 10 mins";
                            mail.Body = "Verify your email and set your password";

                            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com"))
                            {
                                smtp.Port = 587;
                                smtp.Credentials = new NetworkCredential(fromMail, pass);
                                smtp.EnableSsl = true;

                                try
                                {
                                    smtp.Send(mail);
                                    return "Email sent successfully.";
                                }
                                catch (Exception ex)
                                {
                                    return "Email sending failed: " + ex.Message;
                                }
                            }
                        }*/
            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("venki.nani2001@gmail.com");
                mail.To.Add("vasudev.maddineni@domain.com");
                mail.Subject = "Hello World";
                mail.Body = "<h1>Hello</h1>";
                mail.IsBodyHtml = true;
               // mail.Attachments.Add(new Attachment("C:\\file.zip"));

                using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtp.Credentials = new NetworkCredential("venkinani.maddineni@gmail.com", "E.pranav@123");
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
                return "chance";
            }


            /*            var mailMessage = new MimeMessage();
                        mailMessage.From.Add(new MailboxAddress("from name", "from email"));
                        mailMessage.To.Add(new MailboxAddress("to name", "to email"));
                        mailMessage.Subject = "subject";
                        mailMessage.Body = new TextPart("plain")
                        {
                            Text = "Hello"
                        };

                        using (var smtpClient = new SmtpClient())
                        {
                            smtpClient.Connect("smtp.gmail.com", 587, true);
                            smtpClient.Authenticate("user", "password");
                            smtpClient.Send(mailMessage);
                            smtpClient.Disconnect(true);
                            return "Every thing is Well";
                        }*/

        }
    }
}

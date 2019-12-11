using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using Twilio.AspNet.Common;
using Twilio.AspNet.Mvc;
using WebApplication4.Models.EntityModel;
using Twilio.TwiML;
using Twilio.Clients;
using System.Threading.Tasks;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;

namespace WebApplication4.Controllers
{
    public class Subscriber
    {
        public int Id { get; set; }
        public String PhoneNumber { get; set; }
        public bool Subscribed { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
    public class TwillioHomeController : TwilioController
    {
        TwilioRestClient _client;
        public ActionResult Index()
        {
            string accountId = "ACbb74778527e41e10f3e72889970add73";
            string AuthToken = "d51c821ef251c96453c512396b47be8f";
            _client = new TwilioRestClient(accountId, AuthToken);
            //ACbb74778527e41e10f3e72889970add73
            var response = new VoiceResponse();
            response.Say("hello world");
            return TwiML(response);
        }
        public async Task<MessageResource> SendAsync(string phoneNumber, string message)
        {
            string accountId = "ACbb74778527e41e10f3e72889970add73";
            string AuthToken = "d51c821ef251c96453c512396b47be8f";
            _client = new TwilioRestClient(accountId, AuthToken);
            
            string fromUser = "+14808450833";
            var to = new PhoneNumber(phoneNumber);
            return await MessageResource.CreateAsync(
                to,
                accountId,
                from: new PhoneNumber(fromUser),
                body: message,
                client: _client
            );
        }
    }
    public class HomeController : Controller
    {
        DB_APIEntities db = new DB_APIEntities();
        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";
            return View();
        }
        [HttpPost]
        public ActionResult userLogin(string strEmail, string strPassword)
        {
            TBL_USER_INFO oTBL_USER_INFO = new TBL_USER_INFO();
            oTBL_USER_INFO = db.TBL_USER_INFO
                .FirstOrDefault(user => user.EMail.ToLower() == strEmail.ToLower() && user.Password == strPassword);

            GetSesstionReady(oTBL_USER_INFO);
            return Json(new { success = 1 }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult sendEmailClick(string Ids, string txtSubject, string txtBody)
        {
            if (string.IsNullOrEmpty(Ids) || string.IsNullOrEmpty(txtSubject) || string.IsNullOrEmpty(txtBody))
            {
                return Json(new { success = 0 }, JsonRequestBehavior.AllowGet);
            }
            var splittedIds = Ids.Split(',');
            foreach (var item in splittedIds)
            {
                int _id = -1;
                if (int.TryParse(item, out _id))
                {
                    TBL_USER_INFO obj = db.TBL_USER_INFO.FirstOrDefault(c => c.Id == _id);

                    sendEmail(obj.EMail, txtSubject, txtBody);
                }
            }
            return Json(new { success = 1 }, JsonRequestBehavior.AllowGet);
        }

        public int GetSesstionReady(TBL_USER_INFO user)
        {
            try
            {
                Session["UserProfile"] = user;
                return 1;
            }
            catch (Exception e)
            {
                e.ToString();
                return -1;
            }
        }

        public static bool sendEmail(string receiver, string subject, string message)
        {
            try
            {
                var senderEmail = new MailAddress("apiappapiapp@gmail.com", "api Mail");
                var receiverEmail = new MailAddress(receiver, "Receiver");
                var password = "PASS00966";
                var sub = subject;
                var body = message;
                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = true,
                    Credentials = new NetworkCredential(senderEmail.Address, password)
                };
                
                using (var mess = new MailMessage(senderEmail, receiverEmail)
                {
                    Subject = subject,
                    Body = body
                })
                {
                    smtp.Send(mess);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

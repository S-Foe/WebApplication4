using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Twilio.AspNet.Common;
using Twilio.AspNet.Mvc;
using Twilio.TwiML;

namespace WebApplication4.Controllers
{
    public class SmsController : TwilioController
    {
        public TwiMLResult Index(SmsRequest incomingMessage)
        {
            string accountId = "ACbb74778527e41e10f3e72889970add73";
            string AuthToken = "d51c821ef251c96453c512396b47be8f";
            incomingMessage.AccountSid = accountId;
            incomingMessage.From = "+14808450833";
            incomingMessage.To = "00966599921463";
            
            var messagingResponse = new MessagingResponse();
            
            messagingResponse.Message("The copy cat says: " +
                                      incomingMessage.Body);

            return TwiML(messagingResponse);
        }
    }
}
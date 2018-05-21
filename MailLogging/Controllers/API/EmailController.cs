using System;
using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MailLogging.Helpers;
using MailLogging.Models;
using MailLogging.Services;

namespace MailLogging.Controllers.API
{
    [RoutePrefix("api/emailapi")]
    public class EmailController : ApiController
    {
        [Route("log4netemailsender")]
        [HttpPost]
        public HttpResponseMessage Log4NetEmailSender(EmailViewModel emvm)
        {
            try
            {
                if (!ModelState.IsValid) return this.Request.CreateResponse(HttpStatusCode.BadRequest, "Your fields are not valid");
                string key = ConfigurationManager.AppSettings["Sendgrid.Key"];

                EmailService service = new EmailService(key);

                EmailMessage msg = EmailMessage.CreateMessage(emvm.Email, emvm.Subject, emvm.Message);

                var fileName = FileToByteArrayConverter.GetFilePath("Log4net\\log-file.txt");
                var fileData = FileToByteArrayConverter.Convert(fileName);

                service.SendMail(msg, false, fileName, fileData);

                return this.Request.CreateResponse(HttpStatusCode.OK, "Mail successfully sent");

            }
            catch (Exception ex)
            {

                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        [Route("nlogemailsender")]
        [HttpPost]
        public HttpResponseMessage NlogEmailSender(EmailViewModel emvm)
        {
            try
            {
                if (!ModelState.IsValid) return this.Request.CreateResponse(HttpStatusCode.BadRequest, "Your fields are not valid");
                string key = ConfigurationManager.AppSettings["Sendgrid.Key"];

                EmailService service = new EmailService(key);

                EmailMessage msg = EmailMessage.CreateMessage(emvm.Email, emvm.Subject, emvm.Message);

                var fileName = FileToByteArrayConverter.GetFilePath("logs\\2018-05-21.log");
                var fileData = FileToByteArrayConverter.Convert(fileName);

                service.SendMail(msg, false, fileName, fileData);

                return this.Request.CreateResponse(HttpStatusCode.OK, "Mail successfully sent");

            }
            catch (Exception ex)
            {

                return this.Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
    }
}
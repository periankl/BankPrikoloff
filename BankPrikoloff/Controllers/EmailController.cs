using BankPrikoloff.Contracts;
using BusinessLogic.Interfaces;
using BusinessLogic.Servises;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;


namespace BankPrikoloff.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : ControllerBase
    {
        [HttpPost("send")]
        public IActionResult SendEmail([FromBody] EmailRequest request)
        {

            MailAddress from = new MailAddress("support@bank-prikoloff.ru");
            MailAddress to = new MailAddress(request.To);
            MailMessage message = new MailMessage(from, to);
            message.IsBodyHtml = true;
            message.Subject = request.Subject;
            message.Body = request.Body;

            SmtpClient mailClient = new SmtpClient("smtp.yandex.ru");
            mailClient.Credentials = new NetworkCredential("support@bank-prikoloff.ru", "kcimpuflixjznfjg");
            mailClient.Port = 587;
            mailClient.EnableSsl = true;
            mailClient.UseDefaultCredentials = false;
            mailClient.Send(message);

            return Ok("Email sent successfully.");
        }
    }

    public class EmailRequest
    {
        public string To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }

}
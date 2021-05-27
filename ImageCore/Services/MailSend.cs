using System.Net.Mail;
using ImageCore.Services.Interfaces;

namespace ImageCore.Services
{
    public class MailSend : IMailSend
    {
        public void SendEmail(string message,string subject,string to)
        {
            MailMessage mail = new MailMessage();
           /// mail.To.Add("d.waelsch@gmail.com");
           mail.To.Add(to ?? "");
            mail.From = new MailAddress("imagecore23@gmail.com");
            mail.Subject = subject ?? "";
            mail.Body = message ?? "";
            mail.IsBodyHtml = true;
            
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Credentials = new System.Net.NetworkCredential("imagecore23@gmail.com","netcorecv23");
            smtp.UseDefaultCredentials = false;
            smtp.EnableSsl = true;
            smtp.Send(mail);
        }
    }
}
namespace ImageCore.Services.Interfaces
{
    public interface IMailSend
    {
        public void SendEmail(string message,string subject,string to,string from = "");
    }
}
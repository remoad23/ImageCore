using System.Threading.Tasks;
using ImageCore.Models;
using ImageCore.Options;
using ImageCore.Services.Interfaces;
using Microsoft.Extensions.Options;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ImageCore.Services
{
    public class SmsAuth :  ISmsSender
    {
        public SmsAuth(IOptions<SmsOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public SmsOptions Options { get; }  // set only via Secret Manager

        public Task SendSmsAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            // Your Account SID from twilio.com/console
            var accountSid = Options.AccountIdentification;
            // Your Auth Token from twilio.com/console
            var authToken = Options.AccountPassword;

         //   TwilioClient.Init(accountSid, authToken);
            TwilioClient.Init("AC5b7f2ca8dba289271c417b3c734d0466","9ca430108447c6a895d73b3047f70a9e");
         // +491775704230"
            return MessageResource.CreateAsync(
                to:number,
                from:"+12403187686",
                body: message);
        }
        
    }
}
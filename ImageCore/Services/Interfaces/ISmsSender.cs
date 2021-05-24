using System.Threading.Tasks;

namespace ImageCore.Services.Interfaces
{
    public interface ISmsSender
    {
        public Task SendSmsAsync(string number, string message);
    }
}
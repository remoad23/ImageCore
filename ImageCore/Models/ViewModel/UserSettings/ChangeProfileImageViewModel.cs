using Microsoft.AspNetCore.Http;

namespace ImageCore.ViewModel.UserSettings
{
    public class ChangeProfileImageViewModel
    {
        public IFormFile File { get; set; }
    }
}
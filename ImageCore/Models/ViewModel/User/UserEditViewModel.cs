using Microsoft.AspNetCore.Http;

namespace ImageCore.Models.ViewModel.User
{
    public class UserEditViewModel
    {
        public string Username;
        public string Email;
        public string Role;
        public string Password;
        public IFormFile File;
    }
}
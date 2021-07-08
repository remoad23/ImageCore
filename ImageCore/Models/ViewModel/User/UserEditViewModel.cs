using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ImageCore.Models.ViewModel.User
{
    public class UserEditViewModel
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string Password { get; set; }
        public IFormFile File { get; set; }
        
        public string UserImage { get; set; }
    }
}
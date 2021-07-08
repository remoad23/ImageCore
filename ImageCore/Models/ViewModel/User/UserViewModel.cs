using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImageCore.Models.ViewModel.User
{
    public class UserViewModel
    {
        [Required]
        public string UserId { get; set; }
        
        [Required]
        public string Username { get; set; }
        
        // if the user visits his own profile 
        // to not get specific content to show like add contact
        [Required]
        public bool isUser { get; set; }
        
        // if the User is a contact with the queries user then dont show certain things 
        [Required]
        public bool isContact { get; set; }
        
        public List<string> ProjectNames { get; set; }
        
    }
}

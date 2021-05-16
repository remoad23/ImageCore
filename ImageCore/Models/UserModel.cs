using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ImageCore.Models
{
    public class UserModel : IdentityUser
    {
  
        public UserModel()
        {
        }
        
        public List<ProjectParticipatorModel> ProjectParticipators { get; set; }
        public List<ProjectModel> Projects { get; set; }
    }
}
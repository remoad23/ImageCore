using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ImageCore.Models
{
    public class UserRoleModel : IdentityUserRole<string>
    {
        [Key] public string UserRoleModelId;
        
        public UserRoleModel()
        {
            
        }
    }
}
using Microsoft.AspNetCore.Identity;

namespace ImageCore.Models
{
    public class UserProjectModel : IdentityUser
    {
        public string TestUserProjectColmun { get; set; }
        
        UserProjectModel() : base()
        {
            
        }
    }
}
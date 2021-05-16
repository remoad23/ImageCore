using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;

namespace ImageCore.Models
{
    public class RoleModel : IdentityRole<string>
    {
        public string Description { get; set; }

    }
}
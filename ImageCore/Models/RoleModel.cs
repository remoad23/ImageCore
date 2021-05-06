using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ImageCore.Models
{
    public class RoleModel : IdentityRole<string>
    {
        public string Description { get; set; }
        
        
        public RoleModel() : base() { }
        public RoleModel(string name) : base(name) { }
    }
}
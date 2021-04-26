using Microsoft.AspNetCore.Identity;

namespace ImageCore.Models
{
    public class RoleModel : IdentityRole<string>
    {
        public RoleModel() : base() { }
        public RoleModel(string name) : base(name) { }
        public string Description { get; set; }
    }
}
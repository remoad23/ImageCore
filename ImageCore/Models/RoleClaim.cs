using Microsoft.AspNetCore.Identity;

namespace ImageCore.Models
{
    public class RoleClaim : IdentityRoleClaim<string>
    {
        #nullable enable
        public string? ClaimValue2 { get; set; }
        #nullable disable

    }
}
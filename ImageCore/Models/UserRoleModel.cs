﻿using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ImageCore.Models
{
    public class UserRoleModel : IdentityUserRole<int>
    {
        public UserRoleModel()
        {
            
        }
    }
}
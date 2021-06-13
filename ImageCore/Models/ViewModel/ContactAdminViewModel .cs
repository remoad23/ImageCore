using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImageCore.Models.ViewModel
{
    public class ContactAdminViewModel
    {
        public string Topic { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Message { get; set; }
    }
}

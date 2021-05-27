using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ImageCore.Models
{
    public class ContactModel
    {
        [Key]
        public int ContactId { get; private set; }
        
   
        public string UserId { get; set; }
        
        [ForeignKey("UserId")]
        public string ContactUserId { get; set; }
        
        public bool RequestValidated { get; set; }
        
        public UserModel User { get; set; }
        public UserModel ContactUser { get; set; }

    }
}

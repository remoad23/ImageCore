using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ImageCore.Models.ViewModel
{
    public class FileUploadViewModel
    {
        [Required]
        public IFormFile File { get; set; }
        [Required]
        public string ProjectId { get;set;}
    }
}

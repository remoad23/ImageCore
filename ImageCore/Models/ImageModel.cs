using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageCore.Models
{
    public class ImageModel
    {
        [Key]
        public Guid ImageId { get; private set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public string ProjectId { get; set; }
        
        public ProjectModel Project { get; set; }

    }
}
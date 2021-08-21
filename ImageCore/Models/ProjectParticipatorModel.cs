using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageCore.Models
{
    public class ProjectParticipatorModel
    {
        [Key]
        public string ProjectParticipatorId { get; set; }
        public string ProjectId { get; set; }
        public string UserId { get; set; }
        
        public UserModel User { get; set; }
        public ProjectModel Project { get; set; }
    }
}
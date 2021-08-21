using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace ImageCore.Models.ViewModel
{
    public class ProjectEditViewModel
    {
        [MinLength(6)]
        [MaxLength(30)]
        public string Name { get; set; }
        
        public string OwnerName { get; set; }

        public List<string> Participators { get; set; }
        
        public int ParticipatorCount { get; set; }
    }
}
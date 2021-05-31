using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ImageCore.Models.ViewModel
{
    public class ProjectGetViewModel
    {
        public int ProjectId { get;  set; }
        public string ProjectName { get; set; }
    }
}

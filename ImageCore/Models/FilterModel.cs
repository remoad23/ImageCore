using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageCore.Models
{
    public class FilterModel
    {
        [Key]
        public Guid FilterId { get; private set; }
        public string FilterType { get; set; }
        public int LayerIndex { get; set; }
        public int Brightness { get; set; }
        public float Contrast { get; set; }
        public int Hue { get; set; }
        public float Saturation { get; set; }
        public float Value { get; set; }
        
        
        public int ProjectId { get; set; }
        
        public ProjectModel Project { get; set; }

    }
}
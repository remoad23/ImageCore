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
        public int ImageLayerId { get; set; }
        public int ProjectId { get; set; }
        
        public ImageLayerModel ImageLayer{ get; set; }
        public ProjectModel Project { get; set; }

    }
}
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageCore.Models
{
    public class ImageComponentModel
    {
        [Key]
        public string ImageComponentId { get; private set; }
        public float LocalX { get; set; }
        public float LocalY { get; set; }
        public float LocalZ { get; set; }
        public string ColorMat { get; set; }
        public string ImageLayerId { get; set; }
        public string ProjectId { get; set; }
        
        public ImageLayerModel ImageLayer { get; set; }
        public ProjectModel Project { get; set; }
    }
}
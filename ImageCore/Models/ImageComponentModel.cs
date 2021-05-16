using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageCore.Models
{
    public class ImageComponentModel
    {
        [Key]
        public int ImageComponentId { get; private set; }
        public float LocalX { get; set; }
        public float LocalY { get; set; }
        public float LocalZ { get; set; }
        public string ColorMat { get; set; }
        public int ImageLayerId { get; set; }
        public int ProjectId { get; set; }
        
        public ImageLayerModel ImageLayer { get; set; }
        public ProjectModel Project { get; set; }
    }
}
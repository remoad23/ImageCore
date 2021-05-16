using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageCore.Models
{
    public class ImageLayerModel
    {
        [Key]
        public int ImageLayerId { get; private set; }
        
        public string Name { get; set; }
        public string MaskMat { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public byte Opacity { get; set; }
        public bool Visible { get; set; }
        public int ProjectId { get; set; }
        public string LayerType { get; set; }
        
        public ProjectModel Project{ get; set; }
    }
}
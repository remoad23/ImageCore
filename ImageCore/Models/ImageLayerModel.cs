using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageCore.Models
{
    public class ImageLayerModel
    {
        [Key]
        public Guid ImageLayerId { get; private set; }
        
        public string Name { get; set; }
        public string MaskMat { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public byte Opacity { get; set; }
        public bool Visible { get; set; }
        public string ProjectId { get; set; }
        public string LayerType { get; set; }
        
        public ProjectModel Project{ get; set; }
    }
}
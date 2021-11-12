using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageCore.Models
{
    public class ImageLayerModel
    {
        [Key]
        public Guid ImageLayerId { get; private set; }
        public string OriginalImageMat { get; set; }
        public string ProcessedImageMat { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool Masked { get; set; }
        public float Rotation { get; set; }
        public string LayerColor { get; set; }
        public int FontSize { get; set; }
        public int FontStrength { get; set; }
        public string Text { get; set; }
        public int FilterId { get; set; }

        public string Name { get; set; }
        public string MaskMat { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }
        public byte Opacity { get; set; }
        public bool Visible { get; set; }
        public string LayerType { get; set; }
        
        public string ProjectId { get; set; }
        
        public ProjectModel Project{ get; set; }
    }
}
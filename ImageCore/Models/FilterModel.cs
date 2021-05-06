using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageCore.Models
{
    public class FilterModel
    {
        [Key] 
        public int FilterId { get; private set; }
        
        [ForeignKey("ImageLayerId")]
        public int ImageLayerId { get; set; }

        public string FilterType { get; set; }

    }
}
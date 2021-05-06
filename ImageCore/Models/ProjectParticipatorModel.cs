using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ImageCore.Models
{
    public class ProjectParticipatorModel
    {
        [Key]
        public int ProjectParticipatorId { get; private set; }
        
        [ForeignKey("ProjectId")]
        public int ProjectId { get; set; }
        
        
    }
}
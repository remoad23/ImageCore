using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ImageCore.Models
{
    public class ProjectModel
    {
        [Key]
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public int Views { get; set; }
        
        [Required] // <-- oncascade aktivieren
        public string UserId { get; set; }
        
        public UserModel User { get; set; }
        
        public List<ProjectParticipatorModel> ProjectParticipators { get; set; }
        public List<ImageLayerModel> ImageLayers { get; set; }
        public List<ImageComponentModel> ImageComponents { get; set; }
        public List<FilterModel> Filters { get; set; }

    }
}

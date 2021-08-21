using System.Collections.Generic;

namespace ImageCore.Models.ViewModel
{
    public class ProjectListViewModel
    {
        public string Owner { get; set; }
        public string Name { get; set; }
        public string ProjectId { get; set; }
        
        public int ParticipatorCount { get; set; }
    }
}
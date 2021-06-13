using System.Collections.Generic;

namespace ImageCore.Models.ViewModel
{
    public class ProjectStoreViewModel
    {
        public string ProjectName { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public List<string> UserIds { get; set; }

    }
}
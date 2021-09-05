using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageCore.Models.ViewModel
{
    public class ProjectViewModel
    {
        public List<string> Projectname = new List<string>();
        public List<string> ProjectIds = new List<string>();
        public List<int> ProjectViews = new List<int>();
        public List<int> ParticipatorCounts = new List<int>();
    }
}

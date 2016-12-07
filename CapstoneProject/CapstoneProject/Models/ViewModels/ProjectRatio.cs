using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneProject.Models.ViewModels
{
    public class ProjectRatio
    {
        public int Approved { get; set; }
        public int Rejected { get; set; }
        public int Completed { get; set; }
        public int Pending { get; set; }
        public int Assigned { get; set; }

        public int Total { get; set; }

        public ProjectRatio()
        {
            Approved = 0;
            Rejected = 0;
            Completed = 0;
            Pending = 0;
            Assigned = 0;
        }

        public int GetTotal()
        {
            return Approved + Rejected + Completed + Pending + Assigned;
        }
    }
}
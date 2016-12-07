using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneProject.Models.ViewModels
{
    public class ProjectRatio
    {
        public double Approved { get; set; }
        public double Rejected { get; set; }
        public double Completed { get; set; }
        public double Pending { get; set; }
        public double Assigned { get; set; }

        public double Total { get; set; }

        public double ApprovedPct;
        public double RejectedPct;
        public double CompletedPct;
        public double PendingPct;
        public double AssignedPct;

        public ProjectRatio()
        {
            Approved = 0;
            Rejected = 0;
            Completed = 0;
            Pending = 0;
            Assigned = 0;
            Total = 0;
        }

        public double GetTotal()
        {
            return Approved + Rejected + Completed + Pending + Assigned;
        }
        public double GetApprovedPct()
        {
            double value = (Approved / Total) * 100;
            return Math.Round(value);
        }
        public double GetRejectedPct()
        {
            double value = (Rejected / Total) * 100;
            return Math.Round(value);
        }
        public double GetCompletedPct()
        {
            double value = (Completed / Total) * 100;
            return Math.Round(value);
        }
        public double GetPendingPct()
        {
            double value = (Pending / Total) * 100;
            return Math.Round(value);
        }
        public double GetAssignedPct()
        {
            double value = (Assigned / Total) * 100;
            return Math.Round(value);
        }

    }
}
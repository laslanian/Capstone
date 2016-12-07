using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneProject.Models.ViewModels
{
    public class GroupRatio
    {
        public double Assigned { get; set; }
        public double NotAssigned { get; set; }

        public double Total { get; set; }
        public double AssignedPct;

        public double NotAssignedPct;

        public GroupRatio()
        {
            Assigned = 0;
            NotAssigned = 0;
            Total = 0;
        }

        public double GetAssignedPercent()
        {
            double value = (Assigned / Total) * 100;
            return Math.Round(value, 2);
        }

        public double GetNotAssignedPercent()
        {
            double value = (NotAssigned / Total) * 100;
            return Math.Round(value, 2);
        }

        public double GetTotal()
        {
            return Assigned + NotAssigned;
        }
    }
}
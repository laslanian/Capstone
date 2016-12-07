using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneProject.Models.ViewModels
{
    public class UserRatio
    {
        public double Students { get; set; }
        public double Admins { get; set; }
        public double Management { get; set; }
        public double Clients { get; set; }
        public double Advisors { get; set; }
        public double Total { get; set; }

        public double StudentPct { get; set; }
        public double AdminPct { get; set; }
        public double MngPct { get; set; }
        public double ClientPct { get; set; }
        public double AdvisorPct { get; set; }

        public UserRatio() {
            Students = 0;
            Admins = 0;
            Management = 0;
            Clients = 0;
            Advisors = 0;
            Total = 0;
        }

        public double GetStudentRatio()
        {
            double value = (Students / Total) * 100;
            return Math.Round(value);
        }
        public double GetClientRatio()
        {
            double value = (Clients / Total) * 100;
            return Math.Round(value);
        }
        public double GetAdminRatio()
        {
            double value = (Admins / Total) * 100;
            return Math.Round(value);
        }
        public double GetMngRatio()
        {
            double value = (Management / Total) * 100;
            return Math.Round(value);
        }
        public double GetAdvRatio()
        {
            double value = (Advisors / Total) * 100;
            return Math.Round(value);
        }
    }
}
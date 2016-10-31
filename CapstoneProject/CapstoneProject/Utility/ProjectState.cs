using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneProject.Utility
{
    public class ProjectState
    {
        public static string Pending { get { return "Pending"; } }
        public static string Approved { get { return "Approved"; } }
        public static string Rejected { get { return "Rejected"; } }
        public static string Assigned { get { return "Assigned"; } }
        public static string All { get { return "All"; } }
    }
}
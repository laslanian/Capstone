using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneProject.Models.ViewModels
{
    public class ProjectForm
    {
        public ProjectForm()
        {
            this.criteria = new Criteria();
        }
        public Project project { get; set; }
        public Criteria criteria { get; set; }
    }
}
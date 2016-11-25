﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CapstoneProject.Models.ViewModels
{
    public class GroupProject
    {
        public Group Group { get; set; }
        public List<Project> Projects { get; set; }      
        public GroupProject()
        {
            Projects = new List<Project>();
        }
    }
}
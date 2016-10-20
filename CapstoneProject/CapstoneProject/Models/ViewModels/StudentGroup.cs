using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CapstoneProject.Models
{
    public class StudentGroup
    {
        public Student Student { get; set; }
        public List<Group> Groups { get; set; } 
        public bool isOwner { get; set; }

        public StudentGroup()
        {
            Groups = new List<Group>();
        }
    }
}
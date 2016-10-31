using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CapstoneProject.Models
{
    public class GroupStudent
    {
        public Group Group { get; set; }
        public List<Student> Students { get; set; }
        public GroupStudent()
        {

            Students = new List<Student>();
        }
    }
}
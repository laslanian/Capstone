using CapstoneProject.Models.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CapstoneProject
{
    [MetadataType(typeof(ProgramMetadata))]
    public partial class Program { }

    [MetadataType(typeof(SkillsetMetadata))]
    public partial class Skillset { }

    [MetadataType(typeof(CoopMetadata))]
    public partial class Coop { }


    [MetadataType(typeof(ClientMetadata))]
    public partial class Client : User { }


    [MetadataType(typeof(CriteriaMetadata))]
    public partial class Criteria { }

    [MetadataType(typeof(GroupMetadata))]
    public partial class Group { }
    [MetadataType(typeof(UserMetadata))]
    public partial class User { }

    [MetadataType(typeof(ProjectMetadata))]
    public partial class Project { }

    [MetadataType(typeof(StudentMetadata))]
    public partial class Student : User  { }
}
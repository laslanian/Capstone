//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace CapstoneProject
{
    using System;
    using System.Collections.Generic;
    
    public partial class Coop
    {
        public int CoopId { get; set; }
        public string CompanyName { get; set; }
        public string JobTitle { get; set; }
        public string Description { get; set; }
        public System.DateTime StartDate { get; set; }
        public System.DateTime EndDate { get; set; }
        public string Comments { get; set; }
        public int StudentUserId { get; set; }
    
        public virtual Student Student { get; set; }
    }
}

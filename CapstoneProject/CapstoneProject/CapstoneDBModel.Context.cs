﻿

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
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class CapstoneDBModel : DbContext
{
    public CapstoneDBModel()
        : base("name=CapstoneDBModel")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<Coop> Coops { get; set; }

    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Program> Programs { get; set; }

    public virtual DbSet<Project> Projects { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Criteria> Criteria { get; set; }

    public virtual DbSet<Skillset> Skillsets { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

}

}


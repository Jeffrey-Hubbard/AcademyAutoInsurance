﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AcademyAutoInsurance.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class QuoteDBEntities : DbContext
    {
        public QuoteDBEntities()
            : base("name=QuoteDBEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cost> Costs { get; set; }
        public virtual DbSet<MakeCost> MakeCosts { get; set; }
        public virtual DbSet<ModelCost> ModelCosts { get; set; }
        public virtual DbSet<Quote> Quotes { get; set; }
    }
}
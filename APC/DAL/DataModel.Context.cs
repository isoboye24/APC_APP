﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace APC.DAL
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class APCEntities : DbContext
    {
        public APCEntities()
            : base("name=APCEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<ATTENDANCE_STATUS> ATTENDANCE_STATUS { get; set; }
        public virtual DbSet<CHILD> Children { get; set; }
        public virtual DbSet<COMMENT> COMMENTs { get; set; }
        public virtual DbSet<CONSTITUTION> CONSTITUTIONs { get; set; }
        public virtual DbSet<COUNTRY> COUNTRies { get; set; }
        public virtual DbSet<DOCUMENT> DOCUMENTs { get; set; }
        public virtual DbSet<DUAL_NATIONALITY> DUAL_NATIONALITY { get; set; }
        public virtual DbSet<EMPLOYMENT_STATUS> EMPLOYMENT_STATUS { get; set; }
        public virtual DbSet<EVENT_IMAGE> EVENT_IMAGE { get; set; }
        public virtual DbSet<EVENT> EVENTS { get; set; }
        public virtual DbSet<EXPENDITURE> EXPENDITUREs { get; set; }
        public virtual DbSet<FINANCIAL_REPORT> FINANCIAL_REPORT { get; set; }
        public virtual DbSet<FINED_MEMBER> FINED_MEMBER { get; set; }
        public virtual DbSet<GENDER> GENDERs { get; set; }
        public virtual DbSet<GENERAL_ATTENDANCE> GENERAL_ATTENDANCE { get; set; }
        public virtual DbSet<MARITAL_STATUS> MARITAL_STATUS { get; set; }
        public virtual DbSet<MEMBER> MEMBERs { get; set; }
        public virtual DbSet<MEMBERSHIP_STATUS> MEMBERSHIP_STATUS { get; set; }
        public virtual DbSet<MONTH> MONTHs { get; set; }
        public virtual DbSet<NATIONALITY> NATIONALITies { get; set; }
        public virtual DbSet<NEXT_OF_KIN_RELATIONSHIP> NEXT_OF_KIN_RELATIONSHIP { get; set; }
        public virtual DbSet<PERMISSION> PERMISSIONs { get; set; }
        public virtual DbSet<PERSONAL_ATTENDANCE> PERSONAL_ATTENDANCE { get; set; }
        public virtual DbSet<POSITION> POSITIONs { get; set; }
        public virtual DbSet<PROFESSION> PROFESSIONs { get; set; }
    }
}

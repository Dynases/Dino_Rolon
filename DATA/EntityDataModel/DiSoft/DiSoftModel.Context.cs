﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DATA.EntityDataModel.DiSoft
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class BDDistBHF_RolonEntities : DbContext
    {
        public BDDistBHF_RolonEntities()
            : base("name=BDDistBHF_RolonEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<TC0051> TC0051 { get; set; }
        public virtual DbSet<TL001> TL001 { get; set; }
        public virtual DbSet<TC004B> TC004B { get; set; }
        public virtual DbSet<TC004> TC004 { get; set; }
        public virtual DbSet<TC0042> TC0042 { get; set; }
        public virtual DbSet<TC0041> TC0041 { get; set; }
    }
}

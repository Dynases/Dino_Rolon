﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DATA.EntityDataModel.DiAvi
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class DiAviEntities : DbContext
    {
        public DiAviEntities()
            : base("name=DiAviEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Libreria> Libreria { get; set; }
        public virtual DbSet<Compra> Compra { get; set; }
        public virtual DbSet<Compra_01> Compra_01 { get; set; }
        public virtual DbSet<CompraIng> CompraIng { get; set; }
        public virtual DbSet<CompraIng_01> CompraIng_01 { get; set; }
        public virtual DbSet<Proveed> Proveed { get; set; }
        public virtual DbSet<Proveed_01> Proveed_01 { get; set; }
        public virtual DbSet<Seleccion> Seleccion { get; set; }
        public virtual DbSet<Seleccion_01> Seleccion_01 { get; set; }
        public virtual DbSet<Plantilla> Plantilla { get; set; }
        public virtual DbSet<Plantilla_01> Plantilla_01 { get; set; }
        public virtual DbSet<TB001> TB001 { get; set; }
        public virtual DbSet<TI001> TI001 { get; set; }
        public virtual DbSet<TI002> TI002 { get; set; }
        public virtual DbSet<TI0021> TI0021 { get; set; }
        public virtual DbSet<ZY0021> ZY0021 { get; set; }
        public virtual DbSet<Almacen> Almacen { get; set; }
        public virtual DbSet<Sucursal> Sucursal { get; set; }
        public virtual DbSet<TipoAlmacen> TipoAlmacen { get; set; }
        public virtual DbSet<Transformacion> Transformacion { get; set; }
        public virtual DbSet<Transformacion_01> Transformacion_01 { get; set; }
        public virtual DbSet<Traspaso> Traspaso { get; set; }
        public virtual DbSet<Traspaso_01> Traspaso_01 { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Precio> Precio { get; set; }
        public virtual DbSet<PrecioCat> PrecioCat { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Venta> Venta { get; set; }
        public virtual DbSet<Venta_01> Venta_01 { get; set; }
        public virtual DbSet<Estaticos> Estaticos { get; set; }
        public virtual DbSet<SY000> SY000 { get; set; }
        public virtual DbSet<V_NotaCompraIngreso> V_NotaCompraIngreso { get; set; }
        public virtual DbSet<Vr_CompraIngreso> Vr_CompraIngreso { get; set; }
        public virtual DbSet<Vr_TransformacionIngreso> Vr_TransformacionIngreso { get; set; }
        public virtual DbSet<Vr_TransformacionSalida> Vr_TransformacionSalida { get; set; }
    }
}

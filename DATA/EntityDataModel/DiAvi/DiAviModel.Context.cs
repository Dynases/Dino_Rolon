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
    using System.Data.Entity.Core.Objects;
    using System.Linq;
    
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
        public virtual DbSet<CompraIng_01> CompraIng_01 { get; set; }
        public virtual DbSet<CompraIng_02> CompraIng_02 { get; set; }
        public virtual DbSet<Proveed> Proveed { get; set; }
        public virtual DbSet<Proveed_01> Proveed_01 { get; set; }
        public virtual DbSet<Seleccion> Seleccion { get; set; }
        public virtual DbSet<Seleccion_01> Seleccion_01 { get; set; }
        public virtual DbSet<Plantilla> Plantilla { get; set; }
        public virtual DbSet<Plantilla_01> Plantilla_01 { get; set; }
        public virtual DbSet<TB001> TB001 { get; set; }
        public virtual DbSet<ZY0021> ZY0021 { get; set; }
        public virtual DbSet<Almacen> Almacen { get; set; }
        public virtual DbSet<Sucursal> Sucursal { get; set; }
        public virtual DbSet<TipoAlmacen> TipoAlmacen { get; set; }
        public virtual DbSet<Transformacion> Transformacion { get; set; }
        public virtual DbSet<Transformacion_01> Transformacion_01 { get; set; }
        public virtual DbSet<Traspaso> Traspaso { get; set; }
        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Precio> Precio { get; set; }
        public virtual DbSet<PrecioCat> PrecioCat { get; set; }
        public virtual DbSet<Estaticos> Estaticos { get; set; }
        public virtual DbSet<SY000> SY000 { get; set; }
        public virtual DbSet<V_NotaCompraIngreso> V_NotaCompraIngreso { get; set; }
        public virtual DbSet<Vr_CompraIngreso> Vr_CompraIngreso { get; set; }
        public virtual DbSet<Vr_TransformacionIngreso> Vr_TransformacionIngreso { get; set; }
        public virtual DbSet<Vr_TransformacionSalida> Vr_TransformacionSalida { get; set; }
        public virtual DbSet<Producto> Producto { get; set; }
        public virtual DbSet<Compra> Compra { get; set; }
        public virtual DbSet<Compra_01> Compra_01 { get; set; }
        public virtual DbSet<CompraIng> CompraIng { get; set; }
        public virtual DbSet<TI001> TI001 { get; set; }
        public virtual DbSet<TI002> TI002 { get; set; }
        public virtual DbSet<TI0021> TI0021 { get; set; }
        public virtual DbSet<Traspaso_01> Traspaso_01 { get; set; }
        public virtual DbSet<Venta> Venta { get; set; }
        public virtual DbSet<Venta_01> Venta_01 { get; set; }
        public virtual DbSet<Programa> Programa { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Rol_01> Rol_01 { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Usuario_01> Usuario_01 { get; set; }
    
        public virtual ObjectResult<sp_dg_TC0051_Result> sp_dg_TC0051(Nullable<int> tipo, Nullable<int> cncod1, Nullable<int> cncod2, Nullable<int> cnnum, string cndesc1, string cndesc2, string cnuact)
        {
            var tipoParameter = tipo.HasValue ?
                new ObjectParameter("tipo", tipo) :
                new ObjectParameter("tipo", typeof(int));
    
            var cncod1Parameter = cncod1.HasValue ?
                new ObjectParameter("cncod1", cncod1) :
                new ObjectParameter("cncod1", typeof(int));
    
            var cncod2Parameter = cncod2.HasValue ?
                new ObjectParameter("cncod2", cncod2) :
                new ObjectParameter("cncod2", typeof(int));
    
            var cnnumParameter = cnnum.HasValue ?
                new ObjectParameter("cnnum", cnnum) :
                new ObjectParameter("cnnum", typeof(int));
    
            var cndesc1Parameter = cndesc1 != null ?
                new ObjectParameter("cndesc1", cndesc1) :
                new ObjectParameter("cndesc1", typeof(string));
    
            var cndesc2Parameter = cndesc2 != null ?
                new ObjectParameter("cndesc2", cndesc2) :
                new ObjectParameter("cndesc2", typeof(string));
    
            var cnuactParameter = cnuact != null ?
                new ObjectParameter("cnuact", cnuact) :
                new ObjectParameter("cnuact", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_dg_TC0051_Result>("sp_dg_TC0051", tipoParameter, cncod1Parameter, cncod2Parameter, cnnumParameter, cndesc1Parameter, cndesc2Parameter, cnuactParameter);
        }
    
        public virtual ObjectResult<sp_Mam_SaldosProducto_Result> sp_Mam_SaldosProducto(Nullable<int> tipo, string yduact, Nullable<int> almacen, Nullable<int> linea)
        {
            var tipoParameter = tipo.HasValue ?
                new ObjectParameter("tipo", tipo) :
                new ObjectParameter("tipo", typeof(int));
    
            var yduactParameter = yduact != null ?
                new ObjectParameter("yduact", yduact) :
                new ObjectParameter("yduact", typeof(string));
    
            var almacenParameter = almacen.HasValue ?
                new ObjectParameter("almacen", almacen) :
                new ObjectParameter("almacen", typeof(int));
    
            var lineaParameter = linea.HasValue ?
                new ObjectParameter("linea", linea) :
                new ObjectParameter("linea", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Mam_SaldosProducto_Result>("sp_Mam_SaldosProducto", tipoParameter, yduactParameter, almacenParameter, lineaParameter);
        }
    
        public virtual int sp_Mam_TA001()
        {
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Mam_TA001");
        }
    
        public virtual int sp_Mam_TA002(Nullable<int> tipo, Nullable<int> abnumi, string abdesc, string abdir, string abtelf, Nullable<decimal> ablat, Nullable<decimal> ablong, string abimg, Nullable<int> abest, string abuact)
        {
            var tipoParameter = tipo.HasValue ?
                new ObjectParameter("tipo", tipo) :
                new ObjectParameter("tipo", typeof(int));
    
            var abnumiParameter = abnumi.HasValue ?
                new ObjectParameter("abnumi", abnumi) :
                new ObjectParameter("abnumi", typeof(int));
    
            var abdescParameter = abdesc != null ?
                new ObjectParameter("abdesc", abdesc) :
                new ObjectParameter("abdesc", typeof(string));
    
            var abdirParameter = abdir != null ?
                new ObjectParameter("abdir", abdir) :
                new ObjectParameter("abdir", typeof(string));
    
            var abtelfParameter = abtelf != null ?
                new ObjectParameter("abtelf", abtelf) :
                new ObjectParameter("abtelf", typeof(string));
    
            var ablatParameter = ablat.HasValue ?
                new ObjectParameter("ablat", ablat) :
                new ObjectParameter("ablat", typeof(decimal));
    
            var ablongParameter = ablong.HasValue ?
                new ObjectParameter("ablong", ablong) :
                new ObjectParameter("ablong", typeof(decimal));
    
            var abimgParameter = abimg != null ?
                new ObjectParameter("abimg", abimg) :
                new ObjectParameter("abimg", typeof(string));
    
            var abestParameter = abest.HasValue ?
                new ObjectParameter("abest", abest) :
                new ObjectParameter("abest", typeof(int));
    
            var abuactParameter = abuact != null ?
                new ObjectParameter("abuact", abuact) :
                new ObjectParameter("abuact", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Mam_TA002", tipoParameter, abnumiParameter, abdescParameter, abdirParameter, abtelfParameter, ablatParameter, ablongParameter, abimgParameter, abestParameter, abuactParameter);
        }
    
        public virtual int sp_Mam_TI002(Nullable<int> tipo, Nullable<int> ibid, Nullable<System.DateTime> ibfdoc, Nullable<int> ibconcep, string ibobs, Nullable<int> ibest, Nullable<int> ibalm, Nullable<int> ibdepdest, Nullable<int> ibiddc, string ibuact, Nullable<int> producto, Nullable<System.DateTime> fechaI, Nullable<System.DateTime> fechaF, Nullable<int> almacen, Nullable<decimal> cantidad, Nullable<int> ibidOrigen, string lote, Nullable<System.DateTime> fechaVenc, Nullable<int> numi, string obs, Nullable<int> cbnumi, Nullable<System.DateTime> fact, string hact, string uact, Nullable<int> cbty5prod, Nullable<decimal> cbcmin, string cblote, Nullable<System.DateTime> cbfechavenc, Nullable<int> depositoInventario)
        {
            var tipoParameter = tipo.HasValue ?
                new ObjectParameter("tipo", tipo) :
                new ObjectParameter("tipo", typeof(int));
    
            var ibidParameter = ibid.HasValue ?
                new ObjectParameter("ibid", ibid) :
                new ObjectParameter("ibid", typeof(int));
    
            var ibfdocParameter = ibfdoc.HasValue ?
                new ObjectParameter("ibfdoc", ibfdoc) :
                new ObjectParameter("ibfdoc", typeof(System.DateTime));
    
            var ibconcepParameter = ibconcep.HasValue ?
                new ObjectParameter("ibconcep", ibconcep) :
                new ObjectParameter("ibconcep", typeof(int));
    
            var ibobsParameter = ibobs != null ?
                new ObjectParameter("ibobs", ibobs) :
                new ObjectParameter("ibobs", typeof(string));
    
            var ibestParameter = ibest.HasValue ?
                new ObjectParameter("ibest", ibest) :
                new ObjectParameter("ibest", typeof(int));
    
            var ibalmParameter = ibalm.HasValue ?
                new ObjectParameter("ibalm", ibalm) :
                new ObjectParameter("ibalm", typeof(int));
    
            var ibdepdestParameter = ibdepdest.HasValue ?
                new ObjectParameter("ibdepdest", ibdepdest) :
                new ObjectParameter("ibdepdest", typeof(int));
    
            var ibiddcParameter = ibiddc.HasValue ?
                new ObjectParameter("ibiddc", ibiddc) :
                new ObjectParameter("ibiddc", typeof(int));
    
            var ibuactParameter = ibuact != null ?
                new ObjectParameter("ibuact", ibuact) :
                new ObjectParameter("ibuact", typeof(string));
    
            var productoParameter = producto.HasValue ?
                new ObjectParameter("producto", producto) :
                new ObjectParameter("producto", typeof(int));
    
            var fechaIParameter = fechaI.HasValue ?
                new ObjectParameter("fechaI", fechaI) :
                new ObjectParameter("fechaI", typeof(System.DateTime));
    
            var fechaFParameter = fechaF.HasValue ?
                new ObjectParameter("fechaF", fechaF) :
                new ObjectParameter("fechaF", typeof(System.DateTime));
    
            var almacenParameter = almacen.HasValue ?
                new ObjectParameter("almacen", almacen) :
                new ObjectParameter("almacen", typeof(int));
    
            var cantidadParameter = cantidad.HasValue ?
                new ObjectParameter("cantidad", cantidad) :
                new ObjectParameter("cantidad", typeof(decimal));
    
            var ibidOrigenParameter = ibidOrigen.HasValue ?
                new ObjectParameter("ibidOrigen", ibidOrigen) :
                new ObjectParameter("ibidOrigen", typeof(int));
    
            var loteParameter = lote != null ?
                new ObjectParameter("lote", lote) :
                new ObjectParameter("lote", typeof(string));
    
            var fechaVencParameter = fechaVenc.HasValue ?
                new ObjectParameter("fechaVenc", fechaVenc) :
                new ObjectParameter("fechaVenc", typeof(System.DateTime));
    
            var numiParameter = numi.HasValue ?
                new ObjectParameter("numi", numi) :
                new ObjectParameter("numi", typeof(int));
    
            var obsParameter = obs != null ?
                new ObjectParameter("obs", obs) :
                new ObjectParameter("obs", typeof(string));
    
            var cbnumiParameter = cbnumi.HasValue ?
                new ObjectParameter("cbnumi", cbnumi) :
                new ObjectParameter("cbnumi", typeof(int));
    
            var factParameter = fact.HasValue ?
                new ObjectParameter("fact", fact) :
                new ObjectParameter("fact", typeof(System.DateTime));
    
            var hactParameter = hact != null ?
                new ObjectParameter("hact", hact) :
                new ObjectParameter("hact", typeof(string));
    
            var uactParameter = uact != null ?
                new ObjectParameter("uact", uact) :
                new ObjectParameter("uact", typeof(string));
    
            var cbty5prodParameter = cbty5prod.HasValue ?
                new ObjectParameter("cbty5prod", cbty5prod) :
                new ObjectParameter("cbty5prod", typeof(int));
    
            var cbcminParameter = cbcmin.HasValue ?
                new ObjectParameter("cbcmin", cbcmin) :
                new ObjectParameter("cbcmin", typeof(decimal));
    
            var cbloteParameter = cblote != null ?
                new ObjectParameter("cblote", cblote) :
                new ObjectParameter("cblote", typeof(string));
    
            var cbfechavencParameter = cbfechavenc.HasValue ?
                new ObjectParameter("cbfechavenc", cbfechavenc) :
                new ObjectParameter("cbfechavenc", typeof(System.DateTime));
    
            var depositoInventarioParameter = depositoInventario.HasValue ?
                new ObjectParameter("depositoInventario", depositoInventario) :
                new ObjectParameter("depositoInventario", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction("sp_Mam_TI002", tipoParameter, ibidParameter, ibfdocParameter, ibconcepParameter, ibobsParameter, ibestParameter, ibalmParameter, ibdepdestParameter, ibiddcParameter, ibuactParameter, productoParameter, fechaIParameter, fechaFParameter, almacenParameter, cantidadParameter, ibidOrigenParameter, loteParameter, fechaVencParameter, numiParameter, obsParameter, cbnumiParameter, factParameter, hactParameter, uactParameter, cbty5prodParameter, cbcminParameter, cbloteParameter, cbfechavencParameter, depositoInventarioParameter);
        }
    
        public virtual ObjectResult<sp_Marco_TI002_Result> sp_Marco_TI002(Nullable<int> tipo, Nullable<int> ibid, Nullable<System.DateTime> ibfdoc, Nullable<int> ibconcep, string ibobs, Nullable<int> ibest, Nullable<int> ibalm, Nullable<int> ibdepdest, Nullable<int> ibiddc, string ibuact, Nullable<int> producto, Nullable<System.DateTime> fechaI, Nullable<System.DateTime> fechaF, Nullable<int> almacen)
        {
            var tipoParameter = tipo.HasValue ?
                new ObjectParameter("tipo", tipo) :
                new ObjectParameter("tipo", typeof(int));
    
            var ibidParameter = ibid.HasValue ?
                new ObjectParameter("ibid", ibid) :
                new ObjectParameter("ibid", typeof(int));
    
            var ibfdocParameter = ibfdoc.HasValue ?
                new ObjectParameter("ibfdoc", ibfdoc) :
                new ObjectParameter("ibfdoc", typeof(System.DateTime));
    
            var ibconcepParameter = ibconcep.HasValue ?
                new ObjectParameter("ibconcep", ibconcep) :
                new ObjectParameter("ibconcep", typeof(int));
    
            var ibobsParameter = ibobs != null ?
                new ObjectParameter("ibobs", ibobs) :
                new ObjectParameter("ibobs", typeof(string));
    
            var ibestParameter = ibest.HasValue ?
                new ObjectParameter("ibest", ibest) :
                new ObjectParameter("ibest", typeof(int));
    
            var ibalmParameter = ibalm.HasValue ?
                new ObjectParameter("ibalm", ibalm) :
                new ObjectParameter("ibalm", typeof(int));
    
            var ibdepdestParameter = ibdepdest.HasValue ?
                new ObjectParameter("ibdepdest", ibdepdest) :
                new ObjectParameter("ibdepdest", typeof(int));
    
            var ibiddcParameter = ibiddc.HasValue ?
                new ObjectParameter("ibiddc", ibiddc) :
                new ObjectParameter("ibiddc", typeof(int));
    
            var ibuactParameter = ibuact != null ?
                new ObjectParameter("ibuact", ibuact) :
                new ObjectParameter("ibuact", typeof(string));
    
            var productoParameter = producto.HasValue ?
                new ObjectParameter("producto", producto) :
                new ObjectParameter("producto", typeof(int));
    
            var fechaIParameter = fechaI.HasValue ?
                new ObjectParameter("fechaI", fechaI) :
                new ObjectParameter("fechaI", typeof(System.DateTime));
    
            var fechaFParameter = fechaF.HasValue ?
                new ObjectParameter("fechaF", fechaF) :
                new ObjectParameter("fechaF", typeof(System.DateTime));
    
            var almacenParameter = almacen.HasValue ?
                new ObjectParameter("almacen", almacen) :
                new ObjectParameter("almacen", typeof(int));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<sp_Marco_TI002_Result>("sp_Marco_TI002", tipoParameter, ibidParameter, ibfdocParameter, ibconcepParameter, ibobsParameter, ibestParameter, ibalmParameter, ibdepdestParameter, ibiddcParameter, ibuactParameter, productoParameter, fechaIParameter, fechaFParameter, almacenParameter);
        }
    }
}

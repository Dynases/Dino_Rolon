using ENTITY.inv.TI001.VIew;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UTILITY.Enum;

namespace REPOSITORY.Clase
{
    public class RTI001 : BaseConexion, ITI001
    {
        #region Trasancciones

        public bool ActualizarInventario(string idProducto,
                                         int idAlmacen,
                                         EnAccionEnInventario accionEnInventario,
                                         decimal cantidad,
                                         string lote,
                                         DateTime? fechaVen)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var inventario = db.TI001.Where(i => i.icalm == idAlmacen
                                                    && i.iccprod.Equals(idProducto)
                                                    && i.iclot.Equals(lote)
                                                    && i.icfven == fechaVen)
                                             .FirstOrDefault();

                    var stock = inventario.iccven;
                    if (accionEnInventario.Equals(EnAccionEnInventario.Incrementar))
                    { inventario.iccven = stock +  cantidad; }
                    else if (accionEnInventario.Equals(EnAccionEnInventario.Descontar))
                    { inventario.iccven = stock -cantidad; }

                    db.TI001.Attach(inventario);
                    db.Entry(inventario).State = EntityState.Modified;
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ActualizarInventarioModificados(string idProducto,
                                        int idAlmacen,                                      
                                        decimal cantidadAnterior,
                                        decimal cantidadNueva,
                                        string lote,
                                        DateTime? fechaVen)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var inventario = db.TI001.Where(i => i.icalm == idAlmacen
                                                    && i.iccprod.Equals(idProducto)
                                                    && i.iclot.Equals(lote)
                                                    && i.icfven == fechaVen)
                                             .FirstOrDefault();
                    var stock = inventario.iccven;
                    inventario.iccven = stock- (cantidadAnterior - cantidadNueva);               
                    db.TI001.Attach(inventario);
                    db.Entry(inventario).State = EntityState.Modified;
                    db.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VTI001> Listar(int IdProducto)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var compraIng = db.TI001.Where(c => c.iccprod.Equals(IdProducto)).ToList();
                    var listResult = (from a in db.TI001
                                      where a.iccprod.Equals(IdProducto) && a.iccven > 0
                                      select new VTI001
                                      {
                                          id = a.id,
                                          IdAlmacen = a.icalm,
                                          IdProducto = a.iccprod,
                                          Cantidad = a.iccven,
                                          Unidad = a.icuven,
                                          Lote = a.iclot,
                                          FechaVen = a.icfven                                         
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public decimal? StockActual(string IdProducto, int? idAlmacen, string lote, DateTime? fecha)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var cantidad = db.TI001.Where(c => c.icalm == idAlmacen &&
                                                       c.iccprod.Equals(IdProducto) &&                                                       
                                                       c.iclot.Equals(lote) &&
                                                       c.icfven == fecha ).Select(c=>c.iccven).FirstOrDefault();
                    return cantidad;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Verificacion
        public bool ExisteProducto(string IdProducto, int? idAlmacen, string lote, DateTime? fecha)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var resultado = db.TI001.Where(c => c.icalm == idAlmacen &&
                                                       c.iccprod.Equals(IdProducto) &&
                                                       c.iclot.Equals(lote) &&
                                                       c.icfven == fecha).Count();
                    return resultado != 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}

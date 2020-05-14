using DATA.EntityDataModel.DiAvi;
using ENTITY.ven.view;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace REPOSITORY.Clase
{
    public class RVenta : BaseConexion, IVenta
    {
        #region Trasanciones

        public bool Guardar(VVenta VVenta, ref int id)
        {
            try
            {
                using (var db = this.GetEsquema())
                {

                    var idAux = id;
                    Venta venta;
                    if (id > 0)
                    {
                        venta = db.Venta.Where(a => a.Id == idAux).FirstOrDefault();
                        if (venta == null)
                            throw new Exception("No existe la Venta con id " + idAux);
                    }
                    else
                    {
                        venta = new Venta();
                        db.Venta.Add(venta);
                    }
                    venta.IdAlmacen = VVenta.IdAlmacen;
                    venta.IdCliente = VVenta.IdCliente;
                    venta.FechaVenta = VVenta.FechaVenta;
                    venta.Estado = VVenta.Estado;
                    venta.Tipo = VVenta.Tipo;
                    venta.Observaciones = VVenta.Observaciones;
                    venta.EncPrVenta = VVenta.EncPrVenta;
                    venta.EncTransporte = VVenta.EncTransporte;
                    venta.EncEntrega = VVenta.EncEntrega;
                    venta.EncRecepcion = VVenta.EncRecepcion;
                    venta.EncVenta = VVenta.EncVenta;
                    venta.Fecha = VVenta.Fecha;
                    venta.Hora = VVenta.Hora;
                    venta.Usuario = VVenta.Usuario;                    
                    db.SaveChanges();
                    id = venta.Id;
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Consultas

        public List<VVenta> Listar()
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    return db.Venta
                             .Select(v => new VVenta
                             {
                                 DescripcionAlmacen = v.Almacen.Descrip,
                                 DescripcionCliente = v.Cliente.Descrip,
                                 EncEntrega = v.EncEntrega,
                                 EncPrVenta = v.EncPrVenta,
                                 EncRecepcion = v.EncRecepcion,
                                 EncTransporte = v.EncTransporte,
                                 EncVenta = v.EncVenta,
                                 Estado = v.Estado,
                                 Fecha = v.Fecha,
                                 FechaVenta = v.FechaVenta,
                                 Id = v.Id,
                                 IdAlmacen = v.IdAlmacen,
                                 IdCliente = v.IdCliente,
                                 NitCliente = v.Cliente.Nit,
                                 Observaciones = v.Observaciones,
                                 Tipo = v.Tipo,
                                 Usuario = v.Usuario,
                                 Hora = v.Hora,
                                 IdCategoriaCliente=  v.Cliente.IdCategoria
                             }).ToList();
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

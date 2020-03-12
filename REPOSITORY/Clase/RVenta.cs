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
                    var venta = new Venta
                    {
                        EncEntrega = VVenta.EncEntrega,
                        EncPrVenta = VVenta.EncPrVenta,
                        EncRecepcion = VVenta.EncRecepcion,
                        EncTransporte = VVenta.EncTransporte,
                        EncVenta = VVenta.EncVenta,
                        FechaRegistro = VVenta.FechaRegistro,
                        Estado = VVenta.Estado,
                        FechaVenta = VVenta.FechaVenta,
                        IdAlmacen = VVenta.IdAlmacen,
                        IdCliente = VVenta.IdCliente,
                        Observaciones = VVenta.Observaciones,
                        Tipo = VVenta.Tipo,
                        Usuario = VVenta.Usuario
                    };

                    db.Venta.Add(venta);
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
                                 FechaRegistro = v.FechaRegistro,
                                 FechaVenta = v.FechaVenta,
                                 Id = v.Id,
                                 IdAlmacen = v.IdAlmacen,
                                 IdCliente = v.IdCliente,
                                 NitCliente = v.Cliente.Nit,
                                 Observaciones = v.Observaciones,
                                 Tipo = v.Tipo,
                                 Usuario = v.Usuario
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

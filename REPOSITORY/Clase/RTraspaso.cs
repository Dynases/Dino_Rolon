using DATA.EntityDataModel.DiAvi;
using ENTITY.inv.Traspaso.View;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using UTILITY.Enum.ENConcepto;
using UTILITY.Enum.EnEstaticos;

namespace REPOSITORY.Clase
{
    public class RTraspaso : BaseConexion, ITraspaso
    {
        private readonly ITI002 tI002;
        private readonly ITraspaso_01 traspaso_01;

        public RTraspaso(ITI002 tI002, ITraspaso_01 traspaso_01)
        {
            this.tI002 = tI002;
            this.traspaso_01 = traspaso_01;
        }

        #region CONSULTAS

        public List<VTraspaso> ListarTraspasos()
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var listResult = db.Traspaso
                        .Select(ti => new VTraspaso
                        {
                            Destino = ti.AlmacenDestino.Value,
                            Estado = ti.Estado.Value,
                            Fecha = ti.FechaEnvio.Value,
                            Hora = ti.FechaEnvio.Value.ToString(),
                            Id = ti.Id,
                            Observaciones = ti.Observaciones,
                            Origen = ti.AlmacenOrigen.Value,
                            Usuario = ti.UsuarioEnvio
                        }).ToList();

                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<VTListaProducto> ListarInventarioXAlmacenId(int AlmacenId)
        {
            try
            {
                var grupo = Convert.ToInt32(ENEstaticosGrupo.PRODUCTO);
                var Orden1 = Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO1);
                var Orden2 = Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO2);
                var Orden3 = Convert.ToInt32(ENEstaticosOrden.PRODUCTO_GRUPO3);
                var Unidad = Convert.ToInt32(ENEstaticosOrden.PRODUCTO_UN_VENTA);

                using (var db = this.GetEsquema())
                {
                    var listResult = new List<VTListaProducto>();
                    var listSearch = db.TI001.Where(i => i.icalm == AlmacenId);

                    foreach (var i in listSearch)
                    {
                        var producto = db.Producto.Find(Convert.ToInt32(i.iccprod));

                        var grupo1 = db.Libreria.Where(l => l.IdGrupo == grupo &&
                                                       l.IdOrden == Orden1 &&
                                                       l.IdLibrer == producto.Grupo1)
                                                .FirstOrDefault();

                        var grupo2 = db.Libreria.Where(l => l.IdGrupo == grupo &&
                                                       l.IdOrden == Orden2 &&
                                                       l.IdLibrer == producto.Grupo2)
                                                .FirstOrDefault();

                        var grupo3 = db.Libreria.Where(l => l.IdGrupo == grupo &&
                                                       l.IdOrden == Orden3 &&
                                                       l.IdLibrer == producto.Grupo3)
                                                .FirstOrDefault();

                        var grupo4 = db.Libreria.Where(l => l.IdGrupo == grupo &&
                                                      l.IdOrden == Unidad &&
                                                      l.IdLibrer == producto.UniVen)
                                               .FirstOrDefault();

                        var item = new VTListaProducto
                        {
                            AlmacenId = i.icalm.Value,
                            Division = grupo1.Descrip,
                            Marca = grupo2.Descrip,
                            Categoria = grupo3.Descrip,
                            Descripcion = producto.Descrip,
                            Existencia = Convert.ToInt32(i.iccven.Value),
                            InventarioId = i.id,
                            ProductoId = Convert.ToInt32(i.iccprod),
                            UnidadVenta = producto.UniVen,
                            UnidadVentaDisplay = grupo4.Descrip
                        };

                        listResult.Add(item);
                    }

                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region TRANSACCIONES

        public bool Guardar(VTraspaso vTraspaso, ref int idTI2, ref int id)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var traspaso = new Traspaso
                    {
                        Estado = vTraspaso.Estado,
                        AlmacenDestino = vTraspaso.Destino,
                        AlmacenOrigen = vTraspaso.Origen,
                        FechaEnvio = vTraspaso.Fecha,
                        FechaRecepcion = null,
                        Observaciones = vTraspaso.Observaciones,
                        UsuarioEnvio = vTraspaso.Usuario,
                        UsuarioRecepcion = null
                    };

                    db.Traspaso.Add(traspaso);
                    db.SaveChanges();
                    id = traspaso.Id;

                    traspaso.Almacen = db.Almacen.Find(vTraspaso.Origen);
                    traspaso.Almacen1 = db.Almacen.Find(vTraspaso.Destino);

                    //ACTUALIZACION EN TI002: CABECERA
                    if (!this.tI002.Guardar(traspaso.AlmacenOrigen.Value, traspaso.Almacen.Descrip,
                                            traspaso.AlmacenDestino.Value, traspaso.Almacen1.Descrip,
                                            id, traspaso.UsuarioEnvio,
                                            " TRASPASO DE SALIDA DESDE: " + traspaso.Almacen.Descrip + " - HACIA: " + traspaso.Almacen1.Descrip,
                                            (int)ENConcepto.TRASPASO_SALIDA,
                                            ref idTI2))
                    {
                        return false;
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ConfirmarRecepcion(int TraspasoId, string usuarioRecepcion)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var traspaso = db.Traspaso.Find(TraspasoId);

                    if (traspaso == null)
                    {
                        return false;
                    }

                    int idTI2 = 0;
                    //REGISTRAMOS LA CABECERA DE LA RECEPCION EN LA TI002
                    if (this.tI002.Guardar(traspaso.AlmacenOrigen.Value, traspaso.Almacen.Descrip, traspaso.AlmacenDestino.Value, traspaso.Almacen1.Descrip,
                        traspaso.Id, usuarioRecepcion,
                        " TRASPASO DE INGRESO DESDE " + traspaso.Almacen.Descrip + " - HACIA: " + traspaso.Almacen1.Descrip,
                        (int)ENConcepto.TRASPASO_SALIDA,
                        ref idTI2))
                    {
                        //AHORA SE REGISTRA EL DETALLE DE LA RECEPCION EN LA TABLA TI0021 y 
                        //A SU VES EN EL METODO SE ACTUALIZA EL INVENTARIO DE DESTINO EN LA TI001
                        var detalle = db.Traspaso_01.Where(tp => tp.TraspasoId == traspaso.Id).ToList();
                        if (this.traspaso_01.ConfirmarRecepcionDetalle(detalle, idTI2))
                        {
                            //ACTUALIZAMOS EL TRASPASO CAMBIANDO SU ESTADO A RECEPCIONADO = 3
                            traspaso.Estado = 3;
                            db.Traspaso.Attach(traspaso);
                            db.Entry(traspaso).State = EntityState.Modified;
                            db.SaveChanges();

                            return true;
                        }
                        else
                        { return false; }
                    }
                    else
                    { return false; }
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

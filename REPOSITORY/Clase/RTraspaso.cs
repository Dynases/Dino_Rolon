using DATA.EntityDataModel.DiAvi;
using ENTITY.inv.Traspaso.View;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http.Headers;
using UTILITY.Enum.ENConcepto;
using UTILITY.Enum.EnEstado;
using UTILITY.Enum.EnEstaticos;

namespace REPOSITORY.Clase
{
    public class RTraspaso : BaseConexion, ITraspaso
    {

        #region Consulta
        /******** VALOR/REGISTRO ÚNICO *********/
        /********** VARIOS REGISTROS ***********/
        public List<VTraspaso> TraerTraspasos(int usuarioId)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var listResult = db.Traspaso
                        .OrderBy(x => x.Id)
                        .Where(x => x.Estado != (int)ENEstado.ELIMINAR &&
                               (db.Usuario_01
                               .Where(b => b.IdUsuario == usuarioId &&
                                           b.Acceso == true)
                               .Select(d => d.IdAlmacen)).Contains(x.IdAlmacenDestino) &&
                               (db.Usuario_01
                               .Where(b => b.IdUsuario == usuarioId &&
                                           b.Acceso == true)
                               .Select(d => d.IdAlmacen)).Contains(x.IdAlmacenOrigen))
                        .Select(ti => new VTraspaso
                        {
                            Id = ti.Id,
                            IdAlmacenOrigen = ti.IdAlmacenOrigen,
                            IdAlmacenDestino = ti.IdAlmacenDestino,
                            AlamacenOrigen = db.Almacen.FirstOrDefault(a => a.Id == ti.IdAlmacenOrigen).Descrip,
                            AlmacenDestino = db.Almacen.FirstOrDefault(a => a.Id == ti.IdAlmacenDestino).Descrip,
                            Estado = ti.Estado,
                            Observaciones = ti.Observaciones,
                            UsuarioEnvio = ti.UsuarioEnvio,
                            UsuarioRecepcion = ti.UsuarioRecepcion,
                            FechaRecepcion = ti.FechaRecepcion,
                            FechaEnvio = ti.FechaEnvio,
                            EstadoEnvio = ti.EstadoEnvio,
                            EstadoEnvioDescripcion = ti.EstadoEnvio == 1 ? "SIN RECEPCION" : "CON RECEPCION",
                            TotalUnidad = ti.TotalUnidad,
                            Total = ti.Total,
                            Fecha = ti.FechaEnvio,
                            Hora = ti.Hora,
                            Usuario = ti.Usuario
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
                            AlmacenId = i.icalm,
                            Division = grupo1.Descrip,
                            Marca = grupo2.Descrip,
                            Categoria = grupo3.Descrip,
                            Descripcion = producto.Descrip,
                            Existencia = Convert.ToInt32(i.iccven),
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
        /********** REPORTE ***********/
        #endregion
        #region Transacciones
        public bool Guardar(VTraspaso vTraspaso, ref int id)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var idAux = id;
                    Traspaso traspaso;
                    if (id > 0)
                    {
                        traspaso = db.Traspaso.Where(a => a.Id == idAux).FirstOrDefault();
                        if (traspaso == null)
                            throw new Exception("No existe el Traspaso con id " + idAux);
                    }
                    else
                    {
                        traspaso = new Traspaso();
                        db.Traspaso.Add(traspaso);
                    }
                    traspaso.Estado = vTraspaso.Estado;
                    traspaso.IdAlmacenOrigen = vTraspaso.IdAlmacenOrigen;
                    traspaso.IdAlmacenDestino = vTraspaso.IdAlmacenDestino;
                    traspaso.FechaEnvio = vTraspaso.FechaEnvio;
                    traspaso.FechaRecepcion = vTraspaso.FechaRecepcion;
                    traspaso.Observaciones = vTraspaso.Observaciones;
                    traspaso.UsuarioEnvio = vTraspaso.UsuarioEnvio;
                    traspaso.UsuarioRecepcion = vTraspaso.UsuarioRecepcion;
                    traspaso.EstadoEnvio = vTraspaso.EstadoEnvio;
                    traspaso.TotalUnidad = vTraspaso.TotalUnidad;
                    traspaso.Total = vTraspaso.Total;
                    traspaso.Fecha = vTraspaso.Fecha;
                    traspaso.Hora = vTraspaso.Hora;
                    traspaso.Usuario = vTraspaso.Usuario;
                    db.SaveChanges();
                    id = traspaso.Id;
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
                    traspaso.UsuarioRecepcion = usuarioRecepcion;
                    traspaso.EstadoEnvio = 2;
                    traspaso.Estado = (int)ENEstado.COMPLETADO;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void GuardarDetalleDisoft(int idTraspaso)
        {
            try
            {
                using (var db = this.GetEsquema())
                {                 
                    Traspaso traspaso = db.Traspaso.Where(a => a.Id == idTraspaso).FirstOrDefault();
                    if (traspaso == null)
                    {
                        throw new Exception("No se encontro el registro");
                    }
                    Traspaso_02 detalleDisoft = new Traspaso_02();
                    detalleDisoft.AlmacenId = traspaso.IdAlmacenDestino;
                    detalleDisoft.TraspasoId = traspaso.Id;
                    detalleDisoft.Estado = 0;
                    db.Traspaso_02.Add(detalleDisoft);
                    db.SaveChanges();                   
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

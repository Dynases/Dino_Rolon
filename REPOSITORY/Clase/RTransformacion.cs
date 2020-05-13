using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.inv.Transformacion.View;
using DATA.EntityDataModel.DiAvi;
using UTILITY.Enum.EnEstado;
using ENTITY.inv.Transformacion.Report;
using ENTITY.com.Seleccion.Report;
using System.Data.Entity;
using UTILITY.Enum.ENConcepto;
using UTILITY.Enum;

namespace REPOSITORY.Clase
{
    public class RTransformacion : BaseConexion, ITransformacion
    {
        private readonly ITI001 tI001;
        private readonly ITI002 tI002;
        private readonly ITI0021 tI0021;
        public RTransformacion(ITI001 tI001, ITI002 tI002, ITI0021 tI0021)
        {
            this.tI001 = tI001;
            this.tI002 = tI002;
            this.tI0021 = tI0021;
        }
        #region TRANSACCIONES
        public bool Guardar(VTransformacion vTransformacion, ref int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var idAux = id;
                    Transformacion transformacion;
                    if (id > 0)
                    {
                        transformacion = db.Transformacion.Where(a => a.Id == idAux).FirstOrDefault();
                        if (transformacion == null)
                            throw new Exception("No existe la transformacion con id " + idAux);
                    }
                    else
                    {
                        transformacion = new Transformacion();
                        db.Transformacion.Add(transformacion);
                    }
                    transformacion.IdAlmacenSalida = vTransformacion.IdAlmacenSalida;
                    transformacion.IdAlmacenIngreso = vTransformacion.IdAlmacenIngreso;
                    transformacion.Estado = (int)ENEstado.GUARDADO;
                    transformacion.Observ = vTransformacion.Observ;          
                    transformacion.Fecha = vTransformacion.Fecha;
                    transformacion.Hora = vTransformacion.Hora;
                    transformacion.Usuario = vTransformacion.Usuario;                    
                    db.SaveChanges();
                    id = transformacion.Id;
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ModificarEstado(int IdTransformacion, int estado,ref List<string> lMensaje)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    #region Validacion
                    DateTime fechaVen = Convert.ToDateTime("2017-01-01");
                    string lote = "20170101";

                    var transformacion = db.Transformacion.Where(c => c.Id.Equals(IdTransformacion)).FirstOrDefault();
                    var transformacion_01 = db.Transformacion_01.Where(c => c.IdTransformacion.Equals(IdTransformacion)).ToList();
                    //Verifica si existe stock para todos los productos a Eliminar
                    foreach (var item in transformacion_01)
                    {
                        var StockActual_MatPrima = this.tI001.StockActual(item.IdProducto_Mat, transformacion.IdAlmacenSalida, lote, fechaVen);
                        var StockActual_Comercial = this.tI001.StockActual(item.IdProducto, transformacion.IdAlmacenIngreso, lote, fechaVen);
                        //if (StockActual_MatPrima < item.Total)
                        //{
                        //    var producto = db.Producto.Where(p => p.Id == item.IdProducto_Mat).Select(p => p.Descrip).FirstOrDefault();
                        //    lMensaje.Add("No existe stock actual suficiente para el producto de materia prima: " + producto);
                        //}
                        if (StockActual_Comercial < item.TotalProd)
                        {
                            var producto = db.Producto.Where(p => p.Id == item.IdProducto).Select(p => p.Descrip).FirstOrDefault();
                            lMensaje.Add("No existe stock actual suficiente para el producto comercial: " + producto);
                        }
                    }
                    if (lMensaje.Count > 0)
                    {
                        var mensaje = "";
                        foreach (var item in lMensaje)
                        {
                            mensaje = mensaje + "- " + item + "\n";
                        }
                        return false;
                    }
                    #endregion
                    #region Actualiza TI001, TI002 Y TI0021
                    //Actualizar saldo, Eliminar Movimientos
                    foreach (var i in transformacion_01)
                    {
                        if (i.Total > 0)
                        {
                            if (this.tI001.ExisteProducto(i.IdProducto_Mat, transformacion.IdAlmacenSalida, lote, fechaVen))
                            {
                                if (!this.tI001.ActualizarInventario(i.IdProducto_Mat,
                                                               transformacion.IdAlmacenSalida,
                                                               EnAccionEnInventario.Incrementar,
                                                               Convert.ToDecimal(i.Total),
                                                               lote,
                                                               fechaVen))
                                {
                                    return false;
                                }
                                //ELIMINA EL DETALLE DE MOVIMIENTO
                                if (!this.tI0021.Eliminar(i.Id, (int)ENConcepto.TRANSFORMACION_SALIDA))
                                {
                                    return false;
                                }
                                //ELIMINA EL MOVIMIENTO
                                if (!this.tI002.Eliminar(i.Id, (int)ENConcepto.TRANSFORMACION_SALIDA))
                                {
                                    return false;
                                }                                
                            }
                            else
                            {
                                //ELIMINA EL DETALLE DE MOVIMIENTO
                                if (!this.tI0021.Eliminar(i.Id, (int)ENConcepto.TRANSFORMACION_SALIDA))
                                {
                                    return false;
                                }
                                //ELIMINA EL MOVIMIENTO
                                if (!this.tI002.Eliminar(i.Id, (int)ENConcepto.TRANSFORMACION_SALIDA))
                                {
                                    return false;
                                }
                            }
                        }
                        if (i.TotalProd > 0)
                        {
                            if (this.tI001.ExisteProducto(i.IdProducto, transformacion.IdAlmacenIngreso, lote, fechaVen))
                            {
                                if (!this.tI001.ActualizarInventario(i.IdProducto,
                                                               transformacion.IdAlmacenIngreso,
                                                               EnAccionEnInventario.Descontar,
                                                               Convert.ToDecimal(i.TotalProd),
                                                               lote,
                                                               fechaVen))
                                {
                                    return false;
                                }
                                //ELIMINA EL DETALLE DE MOVIMIENTO
                                if (!this.tI0021.Eliminar(i.Id, (int)ENConcepto.TRANSFORMACION_INGRESO))
                                {
                                    return false;
                                }
                                //ELIMINA EL MOVIMIENTO
                                if (!this.tI002.Eliminar(i.Id, (int)ENConcepto.TRANSFORMACION_INGRESO))
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                //ELIMINA EL DETALLE DE MOVIMIENTO
                                if (!this.tI0021.Eliminar(i.Id, (int)ENConcepto.TRANSFORMACION_INGRESO))
                                {
                                    return false;
                                }
                                //ELIMINA EL MOVIMIENTO
                                if (!this.tI002.Eliminar(i.Id, (int)ENConcepto.TRANSFORMACION_INGRESO))
                                {
                                    return false;
                                }
                            }
                        }

                    }                  
                    #endregion
                    transformacion.Estado = (int)ENEstado.ELIMINAR;
                    db.Transformacion.Attach(transformacion);
                    db.Entry(transformacion).State = EntityState.Modified;
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region CONSULTAS
        public List<VTransformacion> Listar()
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Transformacion
                                      join b in db.Almacen on a.IdAlmacenIngreso equals b.Id
                                      join c in db.Almacen on a.IdAlmacenSalida equals c.Id
                                      where a.Estado != (int)ENEstado.ELIMINAR
                                      select new VTransformacion
                                      {
                                          Id = a.Id,
                                          IdAlmacenSalida = a.IdAlmacenSalida,
                                          Almacen2 = c.Descrip,
                                          IdAlmacenIngreso = a.IdAlmacenIngreso,
                                          Almacen1 = b.Descrip,
                                          Observ = a.Observ,
                                          Fecha = a.Fecha,
                                          Hora = a.Hora,
                                          Usuario = a.Usuario
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VTransformacionReport> ListarIngreso(int Id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Vr_TransformacionIngreso
                                      where a.Id.Equals(Id)
                                      select new VTransformacionReport
                                      {
                                          Id = a.Id,
                                          AlmacenIngreso = a.AlmacenIngreso,
                                          AlmacenSalida = a.AlmacenSalida,
                                          IdProducto = a.IdProducto,
                                          Descrip = a.Descrip,
                                          Observ = a.Observ,
                                          TotalProd = a.TotalProd,
                                          Fecha = a.Fecha
                                      }).ToList();
                    return listResult;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VTransformacionReport> ListarSalida(int Id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Vr_TransformacionSalida
                                      where a.Id.Equals(Id)
                                      select new VTransformacionReport
                                      {
                                          Id = a.Id,
                                          AlmacenIngreso = a.AlmacenIngreso,
                                          AlmacenSalida = a.AlmacenSalida,
                                          IdProducto_Mat = a.IdProducto_Mat,
                                          Descrip = a.Descrip,
                                          Observ = a.Observ,
                                          Total = a.Total,
                                          Fecha = a.Fecha
                                      }).ToList();
                    return listResult;
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

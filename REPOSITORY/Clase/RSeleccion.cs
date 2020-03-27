using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITY.com.Seleccion.View;
using DATA.EntityDataModel.DiAvi;
using ENTITY.com.Seleccion.Report;
using UTILITY.Enum.EnEstaticos;
using System.Data.Entity;

namespace REPOSITORY.Clase
{

    public class RSeleccion : BaseConexion, ISeleccion
    {
        private readonly ITI001 tI001;

        public RSeleccion(ITI001 tI001)
        {
            this.tI001 = tI001;
        }
        #region tRANSACCIONES
        public bool Guardar(VSeleccion vSeleccion, ref int id)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    var idAux = id;
                    Seleccion seleccion;
                    if (id > 0)
                    {
                        seleccion = db.Seleccion.Where(a => a.Id == idAux).FirstOrDefault();
                        if (seleccion == null)
                            throw new Exception("No existe la seleccion con id " + idAux);
                    }
                    else
                    {
                        seleccion = new Seleccion();
                        db.Seleccion.Add(seleccion);
                    }
                    seleccion.IdAlmacen = vSeleccion.IdAlmacen;
                    seleccion.IdCompraIng = vSeleccion.IdCompraIng;
                    seleccion.Estado = vSeleccion.Estado;
                    seleccion.FechaReg = vSeleccion.FechaReg;
                    seleccion.Cantidad = vSeleccion.Cantidad ;
                    seleccion.Precio = vSeleccion.Precio;
                    seleccion.Total = vSeleccion.Total;                  
                    seleccion.Fecha = vSeleccion.Fecha;
                    seleccion.Hora = vSeleccion.Hora;
                    seleccion.Usuario = vSeleccion.Usuario;
                    seleccion.Merma = vSeleccion.Merma;
                    db.SaveChanges();
                    id = seleccion.Id;
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ModificarEstado(int IdSeleccion, int estado)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    List<string> lMensaje = new List<string>();
                    var seleccion = db.Seleccion.Where(c => c.Id.Equals(IdSeleccion)).FirstOrDefault();
                    var seleccion_01 = db.Seleccion_01.Where(c => c.IdSeleccion.Equals(IdSeleccion)).ToList();
                    //Verifica si existe stock para todos los productos a Eliminar
                    foreach (var item in seleccion_01)
                    {
                        var StockActual = this.tI001.Listar(item.IdProducto).
                                                   Where(c => c.IdAlmacen.Equals(seleccion.IdAlmacen) &&
                                                             c.Lote.Equals("20170101") &&
                                                             c.FechaVen.Equals("2017-01-01")).Select(c => c.Cantidad).FirstOrDefault();
                        if (StockActual >= item.Cantidad)
                        {
                            lMensaje.Add("No existe stock actual suficiente para el producto con Cod: " + item.IdProducto);  
                        }
                    }
                    if (lMensaje.Count > 0)
                    {
                        var mensaje = "";
                        foreach (var item in lMensaje)
                        {
                            mensaje = mensaje + "- " + item + "\n";
                        }
                        throw new Exception(mensaje);
                    }
                    seleccion.Estado = estado;
                    db.Seleccion.Attach(seleccion);
                    db.Entry(seleccion).State = EntityState.Modified;
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
        public List<VSeleccionLista> Listar()
        {
            try
            {
                //var idGrupo = (int)ENEstaticosGrupo.COMPRA_INGRESO;
                //var idOrden = (int)ENEstaticosOrden.COMPRA_INGRESO_PLACA;
                using (var db = GetEsquema())
                {
                    var listResult = (from a in db.Seleccion
                                      join b in db.CompraIng on
                                      new { idCompraIng = a.IdCompraIng }
                                        equals new { idCompraIng = b.Id }
                                      join c in db.Proveed on
                                      new { idProve = b.IdProvee }
                                            equals new { idProve = c.Id }                                      
                                      select new VSeleccionLista
                                      {
                                          Id = a.Id,
                                          IdCompraIng = a.IdCompraIng,
                                          Granja = b.NumNota,
                                          FechaReg = a.FechaReg,
                                          FechaEntrega = b.FechaEnt,
                                          FechaRecepcion = b.FechaRec,
                                          Proveedor = c.Descrip,
                                          Placa = b.Placa,
                                          Tipo = b.Tipo,
                                          Edad = b.EdadSemana,
                                          Fecha = a.Fecha,
                                          Hora = a.Hora,
                                          Usuario = a.Usuario,
                                          IdAlmacen = b.IdAlmacen,
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

﻿using ENTITY.com.CompraIngreso.View;
using ENTITY.com.CompraIngreso_01;
using ENTITY.com.CompraIngreso_03.View;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using UTILITY.Enum;
using UTILITY.Enum.ENConcepto;
using UTILITY.Enum.EnEstado;
using UTILITY.Global;

namespace LOGIC.Class
{
   public class LCompraIngreso
    {
        protected ICompraIngreso iCompraIngreso;
        protected ICompraIngreso_01 iCompraIngreso_01;
        protected IProducto iProducto;
        public LCompraIngreso()        
        {           
            iCompraIngreso = new RCompraIngreso();
            iCompraIngreso_01 = new RCompraIngreso_01();
            iProducto = new RProducto();
        }
        #region Transacciones
        public bool Guardar(VCompraIngresoLista vCompraIngreso, List<VCompraIngreso_01> vCompraIngreso_01, ref int idCompraIngreso, bool EsDevolucion, List<VCompraIngreso_03> vCompraIngreso_03)
        {
            try
            {
                bool result = false;
                using (var scope =new TransactionScope())
                {
                    int aux = idCompraIngreso;
                    result = iCompraIngreso.Guardar(vCompraIngreso, ref idCompraIngreso);
                    if (aux == 0) //Nuevo
                    {
                        new LCompraIngreso_01().Nuevo(vCompraIngreso_01, idCompraIngreso, vCompraIngreso.IdAlmacen);
                    }
                    else
                    {
                        foreach (var i in vCompraIngreso_01)
                        {
                            if (i.Estado == (int)ENEstado.NUEVO)
                            {
                                List<VCompraIngreso_01> detalleNuevo = new List<VCompraIngreso_01>();
                                detalleNuevo.Add(i);
                                new LCompraIngreso_01().Nuevo(detalleNuevo, idCompraIngreso, vCompraIngreso.IdAlmacen);
                                //if (!new LCompraIngreso_01().Nuevo(detalleNuevo, idCompraIngreso, vCompraIngreso.IdAlmacen))
                                //{
                                //    return false;
                                //}
                            }
                            if (i.Estado == (int)ENEstado.MODIFICAR)
                            {
                                new LCompraIngreso_01().Modificar(i, idCompraIngreso, vCompraIngreso.IdAlmacen);
                                //if (!new LCompraIngreso_01().Modificar(i, idCompraIngreso, vCompraIngreso.IdAlmacen))
                                //{
                                //    return false;
                                //}
                            }
                        }
                    }
                    if (!EsDevolucion)
                    {
                        new LCompraIngreso_03().Guardar(vCompraIngreso_03, idCompraIngreso);
                    }
                    scope.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ModificarEstado(int IdCompraIng, int estado, ref List<string> lMensaje)
        {
            try
            {
                bool resultado = false;
                using (var scope = new TransactionScope())
                {
                    //Trae el detalle de venta completo
                    var compraIng = this.TraerCompraIngreso(IdCompraIng);
                    var compraIng_01 = this.iCompraIngreso_01.ListarXId(IdCompraIng);
                    foreach (var vCompraIng_01 in compraIng_01)
                    {
                        if (compraIng_01 == null) { return false; }

                        var StockActual = new LInventario().TraerStockActual(vCompraIng_01.IdProduc, compraIng.IdAlmacen, UTGlobal.lote, UTGlobal.fechaVencimiento);
                        if (StockActual < vCompraIng_01.Cantidad)
                        {
                            var producto = iProducto.ListarXId(vCompraIng_01.IdProduc);
                            lMensaje.Add("No existe stock actual suficiente para el producto: " + producto.Id + " - " + producto.Descripcion);
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

                        //Elimina el movimiento de inventario y actualiza el stock
                        new LInventario().EliminarMovimientoInventario(vCompraIng_01.Id, vCompraIng_01.IdProduc, compraIng.IdAlmacen,
                                                                              UTGlobal.lote, UTGlobal.fechaVencimiento, vCompraIng_01.TotalCant,
                                                                             (int)ENConcepto.COMPRA_INGRES0, EnAccionEnInventario.Descontar);                     
                    }
                    resultado = iCompraIngreso.ModificarEstado(IdCompraIng, estado);                    
                    scope.Complete();
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Consulta
        /******** VALOR/REGISTRO ÚNICO *********/
        public VCompraIngresoLista TraerCompraIngreso(int id)
        {
            try
            {
                return iCompraIngreso.TraerCompraIngreso(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /********** VARIOS REGISTROS ***********/
        public List<VCompraIngreso> TraerComprasIngreso()
        {
            try
            {
                return iCompraIngreso.TraerComprasIngreso();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable BuscarCompraIngreso(int estado)
        {
            try
            {
                return iCompraIngreso.BuscarCompraIngreso(estado);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /********** REPORTES ***********/
        public List<VCompraIngresoNota> NotaCompraIngreso(int id)
        {
            try
            {
                return iCompraIngreso.NotaCompraIngreso(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VCompraIngresoNota> NotaCompraIngresoDevolucion(int id)
        {
            try
            {
                return iCompraIngreso.NotaCompraIngresoDevolucion(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Reporte de inventario de compra ingreso
        public DataTable ReporteCompraIngreso(DateTime? fechaDesde, DateTime? fechaHasta, int estado)
        {
            try
            {
                return iCompraIngreso.ReporteCompraIngreso(fechaDesde, fechaHasta, estado);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Verificaciones
        public bool ExisteEnSeleccion(int idCompraIng)
        {
            try
            {
                return iCompraIngreso.ExisteEnSeleccion(idCompraIng);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}

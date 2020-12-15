using ENTITY.com.CompraIngreso.Filter;
using ENTITY.com.CompraIngreso.View;
using ENTITY.com.CompraIngreso_01;
using ENTITY.com.CompraIngreso_03.View;
using ENTITY.inv.Ajuste.View;
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
        protected ITI002 iInventario;
        protected IAjuste iAjuste;
        protected ITI001 iTI001;
        public LCompraIngreso()        
        {           
            iCompraIngreso = new RCompraIngreso();
            iCompraIngreso_01 = new RCompraIngreso_01();
            iProducto = new RProducto();
            iInventario = new RTI002();
            iAjuste = new RAjuste();
            iTI001 = new RTI001();
        }
        #region Transacciones
        public bool Guardar(VCompraIngresoLista vCompraIngreso, List<VCompraIngreso_01> vCompraIngreso_01, 
                            ref int idCompraIngreso, bool EsDevolucion, List<VCompraIngreso_03> vCompraIngreso_03, int totalMapleDetalle, int totalMapleDevolucion)
        {
            try
            {
                bool result = false;
                using (var scope =new TransactionScope())
                {
                    int aux = idCompraIngreso;
                    result = iCompraIngreso.Guardar(vCompraIngreso, ref idCompraIngreso);
                    if (EsDevolucion) //Sin devoluion
                    {
                        if (aux == 0) //Nuevo
                        {
                            new LCompraIngreso_01().Nuevo(vCompraIngreso_01, idCompraIngreso, vCompraIngreso.IdAlmacen, totalMapleDetalle);
                        }
                        else
                        {
                            foreach (var i in vCompraIngreso_01)
                            {
                                if (i.Estado == (int)ENEstado.NUEVO)
                                {
                                    List<VCompraIngreso_01> detalleNuevo = new List<VCompraIngreso_01>();
                                    detalleNuevo.Add(i);
                                    new LCompraIngreso_01().Nuevo(detalleNuevo, idCompraIngreso, vCompraIngreso.IdAlmacen, totalMapleDetalle);
                                }
                                if (i.Estado == (int)ENEstado.MODIFICAR)
                                {
                                    new LCompraIngreso_01().Modificar(i, idCompraIngreso, vCompraIngreso.IdAlmacen, totalMapleDetalle);
                                }
                            }
                        }
                    }
                    else//Con devoluciob
                    {
                        if (aux == 0) //Nuevo
                        {
                            new LCompraIngreso_01().NuevoDevolucion(vCompraIngreso_01, idCompraIngreso, vCompraIngreso.IdAlmacen, vCompraIngreso_03, totalMapleDetalle);
                            new LCompraIngreso_03().Guardar(vCompraIngreso_03, idCompraIngreso, totalMapleDevolucion);
                        }
                        else
                        {

                            foreach (var i in vCompraIngreso_01)
                            {
                                if (i.Estado == (int)ENEstado.NUEVO && vCompraIngreso_03.FirstOrDefault(a => a.IdProduc == i.IdProduc).Estado == (int)ENEstado.NUEVO)
                                {
                                    List<VCompraIngreso_01> detalleNuevo = new List<VCompraIngreso_01>();
                                    detalleNuevo.Add(i);
                                    new LCompraIngreso_01().NuevoDevolucion(detalleNuevo, idCompraIngreso, vCompraIngreso.IdAlmacen, vCompraIngreso_03, totalMapleDetalle);
                                }
                                if (i.Estado == (int)ENEstado.MODIFICAR ||
                                    vCompraIngreso_03.FirstOrDefault(a => a.IdProduc == i.IdProduc).Estado == (int)ENEstado.NUEVO ||
                                    vCompraIngreso_03.FirstOrDefault(a => a.IdProduc == i.IdProduc).Estado == (int)ENEstado.MODIFICAR)
                                {
                                    var devolucion = vCompraIngreso_03.Where(a => a.IdProduc == i.IdProduc).FirstOrDefault();
                                    new LCompraIngreso_01().ModificarDevolucion(i, idCompraIngreso, vCompraIngreso.IdAlmacen, devolucion, totalMapleDetalle);
                                }
                            }
                            new LCompraIngreso_03().Guardar(vCompraIngreso_03, idCompraIngreso, totalMapleDevolucion);
                        }

                    }
                    //Ingrso por ajuste
                    IngresoPorAjusteCompra(vCompraIngreso, idCompraIngreso, aux);
                    scope.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void IngresoPorAjusteCompra(VCompraIngresoLista vCompraIngreso, int idCompraIngreso, int idAux)
        {
            if (vCompraIngreso.TotalVendido > 0)
            {
                VAjuste ajusteInventario = null;
                List<VAjusteDetalle> ajusteDetalle = null;

                //Ajuste Inventario TI002
                var idInventario = iInventario.TraerMovimiento(idCompraIngreso, (int)ENConcepto.INGRESO) == null ? 0 : iInventario.TraerMovimiento(idCompraIngreso, (int)ENConcepto.INGRESO).IdManual;

                ajusteInventario = LlenarAjuste(vCompraIngreso, idInventario, idCompraIngreso);
                ajusteDetalle = LlenarAjusteDetalle(vCompraIngreso.TotalVendido, idInventario);


                iAjuste.Guardar(ajusteInventario, ref idInventario, vCompraIngreso.Usuario);
                iAjuste.GuardarDetalle(ajusteDetalle, idInventario);

                if (idAux == 0)//Nuevo
                {
                    iTI001.ActualizarInventario((int)ENProducto.SEGUNDA,
                                                vCompraIngreso.IdAlmacen,
                                                vCompraIngreso.TotalVendido,
                                                UTGlobal.lote,
                                                UTGlobal.fechaVencimiento);
                }
                else//Modificar
                {

                    iTI001.ActualizarInventario((int)ENProducto.SEGUNDA,
                                                vCompraIngreso.IdAlmacen,
                                                vCompraIngreso.TotalVendido * -1,
                                                UTGlobal.lote, UTGlobal.fechaVencimiento);

                    iTI001.ActualizarInventario((int)ENProducto.SEGUNDA,
                                                vCompraIngreso.IdAlmacen,
                                                vCompraIngreso.TotalVendido,
                                                UTGlobal.lote, UTGlobal.fechaVencimiento);
                }
            }
        }

        private List< VAjusteDetalle> LlenarAjusteDetalle(decimal TotalVendido, int idInventario)
        {
            VAjusteDetalle itemDetalle = new VAjusteDetalle();
            itemDetalle.Estado = idInventario == 0 ? (int)ENEstado.NUEVO : (int)ENEstado.MODIFICAR;
            itemDetalle.IdAjuste = idInventario;
            itemDetalle.Id = 0;
            itemDetalle.IdProducto = (int)ENProducto.SEGUNDA;
            itemDetalle.Cantidad = TotalVendido;
            itemDetalle.Lote = UTGlobal.lote;
            itemDetalle.FechaVen = UTGlobal.fechaVencimiento;
            List<VAjusteDetalle> detalle = new List<VAjusteDetalle>();
            detalle.Add(itemDetalle);
            return detalle;
        }

        private VAjuste LlenarAjuste(VCompraIngresoLista compraIngreso, int idInventario, int IdAjuste)
        {
            var NConcepto ="Ajuste de Compra por diferencia del Total Vendido";

            var ajuste = new VAjuste();
            ajuste.Id = idInventario;
            ajuste.IdAlmacen = compraIngreso.IdAlmacen;
            ajuste.IdConcepto = (int)ENConcepto.INGRESO;
            ajuste.Fecha = compraIngreso.Fecha;
            ajuste.Obs = IdAjuste.ToString() + "- " + NConcepto;
            ajuste.IdAjusteFisico = IdAjuste;
            return ajuste;
        }
        public bool ModificarEstado(int IdCompraIng, int estado, ref List<string> lMensaje, bool existeDevolucion)
        {
            try
            {
                bool resultado = false;
                using (var scope = new TransactionScope())
                {
                    decimal cantidadDevolucion = 0;
                    //Trae el detalle de venta completo
                    var compraIng = this.TraerCompraIngreso(IdCompraIng);
                    var compraIng_01 = this.iCompraIngreso_01.ListarXId(IdCompraIng);
                    var devolucion = new LCompraIngreso_03().TraerDevoluciones(IdCompraIng).ToList();                   
                    foreach (var vCompraIng_01 in compraIng_01)
                    {
                        if (compraIng_01 == null) { return false; }

                        var StockActual = new LInventario().TraerStockActual(vCompraIng_01.IdProduc, compraIng.IdAlmacen, UTGlobal.lote, UTGlobal.fechaVencimiento);
                        var cantidadDetalle = vCompraIng_01.TotalCant;
                        if (!existeDevolucion)
                        {
                            cantidadDevolucion = devolucion.FirstOrDefault(a => a.IdProduc == vCompraIng_01.IdProduc).TotalCant;
                        }
                        var totalCantidad = cantidadDetalle - cantidadDevolucion;
                        if (StockActual < totalCantidad)
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
                                                                              UTGlobal.lote, UTGlobal.fechaVencimiento, totalCantidad,
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
        public List<VCompraIngreso> TraerComprasIngreso(int usuarioId)
        {
            try
            {
                return iCompraIngreso.TraerComprasIngreso(usuarioId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable BuscarCompraIngreso(int estado, int usuarioId)
        {
            try
            {
                return iCompraIngreso.BuscarCompraIngreso(estado, usuarioId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VCompraIngresoCombo> TraerCompraIngresoCombo( int usuarioId)
        {
            try
            {
                return iCompraIngreso.TraerCompraIngresoCombo(usuarioId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VCompraIngresoCombo> TraerCompraIngresoComboCompleto()
        {
            try
            {
                return iCompraIngreso.TraerCompraIngresoComboCompleto();
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
        public List<VCompraIngresoNota> NotaCompraIngresoResultado(int id)
        {
            try
            {
                return iCompraIngreso.NotaCompraIngresoResultado(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //Reporte de inventario de compra ingreso
        public DataTable ReporteCompraIngreso(FCompraIngreso fcompraIngreso)
        {
            try
            {
                return iCompraIngreso.ReporteCompraIngreso(fcompraIngreso);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ReporteTotalMaple(FCompraIngreso fcompraIngreso)
        {
            try
            {
                return iCompraIngreso.ReporteTotalMaple(fcompraIngreso);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ReporteCriterioCompraIngreso(FCompraIngreso fcompraIngreso)
        {
            try
            {
                return iCompraIngreso.ReporteCriterioCompraIngreso(fcompraIngreso);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ReporteCriterioCompraIngresoDevolucion(FCompraIngreso fcompraIngreso)
        {
            try
            {
                return iCompraIngreso.ReporteCriterioCompraIngresoDevolucion(fcompraIngreso);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ReporteCriterioCompraIngresoResultado(FCompraIngreso fcompraIngreso)
        {
            try
            {
                return iCompraIngreso.ReporteCriterioCompraIngresoResultado(fcompraIngreso);
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
        public bool ExisteEnDevolucion(int idCompraIng)
        {
            try
            {
                return iCompraIngreso.ExisteEnDevolucion(idCompraIng);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}

using ENTITY.com.Seleccion_01.View;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using UTILITY.Enum;
using UTILITY.Enum.ENConcepto;
using UTILITY.Global;

namespace LOGIC.Class
{
    public class LSeleccion_01
    {
        protected ISeleccion_01 iSeleccion_01;
        public LSeleccion_01()
        {
            iSeleccion_01 = new RSeleccion_01();
        }
        #region TRANSACCIONES
        public bool Guardar(List<VSeleccion_01_Lista> lista, int Id)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = iSeleccion_01.Guardar(lista, Id);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool GuardarModificar(List<VSeleccion_01_Lista> lista, int Id)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = iSeleccion_01.GuardarModificar(lista, Id);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool GuardarModificar_CompraIngreso(List<VSeleccion_01_Lista> lista, int IdCompraIngreso)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = iSeleccion_01.GuardarModificar_CompraIngreso(lista, IdCompraIngreso);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool NuevoMovimientoSelecciom(List<VSeleccion_01_Lista> lSeleccion_01,int idCompra, int idSeleccion)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var idAlmacen = new LCompraIngreso().TraerCompraIngreso(idCompra).IdAlmacen;
                    var DetalleCompra = new LCompraIngreso_01().ListarXId(idCompra).ToList();
                    foreach (var vSeleccion_01 in lSeleccion_01)
                    {
                        //Registra el detalle de venta
                        var idDetalleCompra = DetalleCompra.FirstOrDefault(a => a.IdProduc == vSeleccion_01.IdProducto).Id;
                        if (idDetalleCompra != 0)
                        {
                            var producto = new LProducto().ListarXId(vSeleccion_01.IdProducto);
                            //Registra el movimiento de inventario y actualiza el stock
                            var Observacion = "Compra Salida: " + idSeleccion + " - IdProducto: " + vSeleccion_01.IdProducto + " | " + producto.Descripcion;
                            if (!new LInventario().NuevoMovimientoInventario(idDetalleCompra,
                                                                           vSeleccion_01.IdProducto,
                                                                           idAlmacen,
                                                                           UTGlobal.lote, UTGlobal.fechaVencimiento,
                                                                           vSeleccion_01.Cantidad,
                                                                           (int)ENConcepto.COMPRA_SALIDA,
                                                                           Observacion,
                                                                           EnAccionEnInventario.Descontar,
                                                                           UTGlobal.Usuario))
                            {
                                return false;
                            }
                        }                        
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
        #endregion
        #region CONSULTAS
        /******** VALOR/REGISTRO ÚNICO *********/
        public List<VSeleccion_01_Lista> TraerSeleccion_01(int idSeleccion)
        {
            try
            {
                return iSeleccion_01.TraerSeleccion_01(idSeleccion);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /********** VARIOS REGISTROS ***********/
        public List<VSeleccion_01_Lista> Listar()
        {
            try
            {
                return iSeleccion_01.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VSeleccion_01_Lista> ListarXId_CompraIng(int Id, int tipo)
        {
            try
            {
                return iSeleccion_01.ListarXId_CompraIng(Id, tipo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VSeleccion_01_Lista> ListarXId_CompraIng_XSeleccion(int Id)
        {
            try
            {
                return iSeleccion_01.ListarXId_CompraIng_XSeleccion(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
    #endregion

}

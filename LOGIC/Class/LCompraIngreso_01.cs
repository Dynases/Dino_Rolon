using ENTITY.com.CompraIngreso_01;
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
  public  class LCompraIngreso_01
    {
        protected ICompraIngreso_01 iCompraIngreso_01;
        protected IProducto iProducto;
        public LCompraIngreso_01()
        {
            iProducto = new RProducto();
            iCompraIngreso_01 = new RCompraIngreso_01();
        }
        #region Transacciones
        public bool Nuevo(List<VCompraIngreso_01> lCompra_01, int idCompra, int idAlmacen)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    foreach (var vCompraIngreso_01 in lCompra_01)
                    {
                        var idCompraDetalle = 0;                   
                        //Registra el detalle de venta
                        if (!this.iCompraIngreso_01.Nuevo(vCompraIngreso_01, idCompra, ref idCompraDetalle)) { return false; }

                        var producto = iProducto.ListarXId(vCompraIngreso_01.IdProduc);
                        //Registra el movimiento de inventario y actualiza el stock
                        var Observacion = "Compra Ingreso: " + idCompra + " - IdProducto: " + vCompraIngreso_01.IdProduc + " | " + producto.Descripcion;
                        if (!new LInventario().NuevoMovimientoInventario(idCompraDetalle,
                                                                       vCompraIngreso_01.IdProduc  ,
                                                                       idAlmacen,
                                                                       UTGlobal.lote, UTGlobal.fechaVencimiento,
                                                                       vCompraIngreso_01.TotalCant,
                                                                       (int)ENConcepto.COMPRA_INGRES0,
                                                                       Observacion,
                                                                       EnAccionEnInventario.Incrementar,
                                                                       UTGlobal.Usuario))
                        {
                            return false;
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
        public bool Modificar(VCompraIngreso_01 vCompraIngreso_01, int idCompra, int idAlmacen)
        {
            try
            {
                using (var scope = new TransactionScope())
                {                   
                    //Trae el detalle de antiguo
                    var compraIngreso_01Antiguo = this.iCompraIngreso_01.TraerCompraIngreso_01(vCompraIngreso_01.Id);
                    if (compraIngreso_01Antiguo == null) { return false; }

                    var producto = iProducto.ListarXId(vCompraIngreso_01.IdProduc);
                    var Observacion = "Compra Ingreso: " + idCompra + " - IdProducto: " + vCompraIngreso_01.IdProduc + " | " + producto.Descripcion;
                    //Modifica el movimiento de inventario y actualiza el stock
                    new LInventario().ModificarMovimientoInventario(vCompraIngreso_01.Id,
                                                                        vCompraIngreso_01.IdProduc,
                                                                        idAlmacen,
                                                                         UTGlobal.lote, UTGlobal.fechaVencimiento,
                                                                        compraIngreso_01Antiguo.TotalCant, vCompraIngreso_01.TotalCant,
                                                                        (int)ENConcepto.VENTAS,
                                                                        Observacion, UTGlobal.Usuario,
                                                                        UTGlobal.lote, UTGlobal.fechaVencimiento);         

                    //Modifica el detalle de venta
                    if (!this.iCompraIngreso_01.Modificar(vCompraIngreso_01)) { return false; }
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
        #region Consulta
        /******** VALOR/REGISTRO ÚNICO *********/

        public List<VCompraIngreso_01> ListarXId(int id)
        {
            try
            {
                return iCompraIngreso_01.ListarXId(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VCompraIngreso_01> ListarXId2(int IdGrupo2, int idAlmacen)
        {
            try
            {
                return iCompraIngreso_01.ListarXId2(IdGrupo2, idAlmacen);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /********** VARIOS REGISTROS ***********/
        #endregion
    }
}

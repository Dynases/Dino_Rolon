using ENTITY.inv.Almacen.View;
using ENTITY.Producto.View;
using REPOSITORY.Clase;
using REPOSITORY.Clase.DiSoft;
using REPOSITORY.Interface;
using REPOSITORY.Interface.DiSoft;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace LOGIC.Class
{
    public class LProducto
    {
        protected IProducto iProducto;
        protected IProductoD iProductoD;
        protected ITI0021 iTI0021;

        public LProducto()
        {
            iProducto = new RProducto();
            iProductoD = new RProductoD();
            iTI0021 = new RTI0021();
        }

        #region Transaciones
        public bool Guardar(VProducto vProducto, ref int id)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = false;
                    result = iProducto.Guardar(vProducto, ref id);
                    //Guarda en Disoft
                    result = iProductoD.Guardar(vProducto, ref id);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Modificar(VProducto vProducto, int idProducto)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = false;
                    result = iProducto.Modificar(vProducto, idProducto);
                    //modifica en Disoft
                    result = iProductoD.Guardar(vProducto, ref idProducto);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Eliminar(int idProducto)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = false;
                    result = iProducto.Eliminar(idProducto);
                    //Elimina en Disoft
                    result = iProductoD.Eliminar(idProducto);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Consultas
        /******** VALOR/REGISTRO ÚNICO *********/

        public List<VProducto> ListarXId(int id)
        {
            try
            {
                return iProducto.ListarXId(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /********** VARIOS REGISTROS ***********/
        public List<VProductoLista> Listar()
        {
            try
            {
                return iProducto.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }        

        public DataTable ListarEncabezado(int IdSucursal, int IdAlmacen,
                                          int IdCategoriaPrecio)
        {
            try
            {
                return iProducto.ListarEncabezado(IdSucursal, IdAlmacen, IdCategoriaPrecio);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       
        public List<VProductoListaStock> ListarProductosStock(int IdSucursal, int IdAlmacen,
                                          int IdCategoriaPrecio)
        {
            try
            {
                return iProducto.ListarProductoStock(IdSucursal, IdAlmacen, IdCategoriaPrecio);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /************** REPORTES ***************/
        public List<VDetalleKardex> ListarDetalleKardex(DateTime inicio, DateTime fin, int IdAlmacen)
        {
            try
            {
                return this.iTI0021.ListarDetalleKardex(inicio, fin, IdAlmacen);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Transacciones
        public bool ExisteEnCompra(int idProducto)
        {
            try
            {
                return iProducto.ExisteEnCompra(idProducto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ExisteEnCompraNormal(int idProducto)
        {
            try
            {
                return iProducto.ExisteEnCompraNormal(idProducto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ExisteEnVenta(int idProducto)
        {
            try
            {
                return iProducto.ExisteEnVenta(idProducto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ExisteEnSeleccion(int idProducto)
        {
            try
            {
                return iProducto.ExisteEnSeleccion(idProducto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ExisteEnTransformacion(int idProducto)
        {
            try
            {
                return iProducto.ExisteEnTransformacion(idProducto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ExisteEnMovimiento(int idProducto)
        {
            try
            {
                return iProducto.ExisteEnMovimiento(idProducto);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}

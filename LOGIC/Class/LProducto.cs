using ENTITY.Producto.View;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace LOGIC.Class
{
    public class LProducto
    {
        protected IProducto iProducto;
        public LProducto()
        {
            iProducto = new RProducto();
        }
        #region Transaciones
        public bool Guardar(VProducto vProducto, ref int id)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = iProducto.Guardar(vProducto, ref id);
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
                    var result = iProducto.Modificar(vProducto, idProducto);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Eliminar( int idProducto)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = iProducto.Eliminar(idProducto);
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
        #endregion
    }
}

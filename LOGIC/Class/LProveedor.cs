using ENTITY.Proveedor.View;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace LOGIC.Class
{
    public class LProveedor
    {
        protected IProveedor iProveedor;
        public LProveedor()
        {
            iProveedor = new RProvedor();            
        }
        #region Transaciones
        public bool Guardar(VProveedor vProveedor, List<VProveedor_01Lista> detalle, ref int id, string usuario)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = iProveedor.Guardar(vProveedor, ref id);
                    if (detalle!=null)
                    {
                        var resultDetalle = new LProveedor_01().Guardar(detalle, id, usuario);
                    }
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool Eliminar(int idProveedor)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    bool result = false;
                    result = iProveedor.Eliminar(idProveedor);                                
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
        public List<VProveedor> ListarXId(int id)
        {
            try
            {
                return iProveedor.ListarXId(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VProveedorLista>Listar()
        {
            try
            {
                return iProveedor.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ListarEncabezado()
        {
            try
            {
                return iProveedor.ListarEncabezado();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VProveedorCombo> TraerProveedoresEdadSemana()
        {
            try
            {
                return iProveedor.TraerProveedoresEdadSemana();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Verificaciones
        public bool ExisteEnCompra(int idProveedor)
        {
            try
            {
                return iProveedor.ExisteEnCompra(idProveedor);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ExisteEnCompraIng(int idProveedor)
        {
            try
            {
                return iProveedor.ExisteEnCompraIng(idProveedor);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}

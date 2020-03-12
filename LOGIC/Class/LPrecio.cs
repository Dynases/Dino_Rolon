using ENTITY.reg.Precio.View;
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
    public class LPrecio
    {

        protected IPrecio iPrecio;
        public LPrecio()
        {
            iPrecio = new RPrecio();
        }
        #region Transacciones
        public bool Guardar(List<VPrecioLista> vPrecio, int idSucural, string usuario)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    foreach (var item in vPrecio)
                    {
                        if (item.Estado == 3 )
                        {
                            iPrecio.Nuevo(item,idSucural,usuario);
                        }
                        else
                        {
                            if (item.Estado == 2)
                            {

                                iPrecio.Modificar(item, usuario);
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
        #region Consulta
        public List<VPrecioLista> ListarProductoPrecio(int idSucursal)
        {
            try
            {
                return iPrecio.ListarProductoPrecio(idSucursal);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public DataTable ListarProductoPrecio2(int idSucursal)
        {
            try
            {
                return iPrecio.ListarProductoPrecio2(idSucursal);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}

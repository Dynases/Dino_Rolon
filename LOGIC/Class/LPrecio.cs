using ENTITY.reg.Precio.View;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGIC.Class
{
    public class LPrecio
    {

        protected IPrecio iPrecio;
        public LPrecio()
        {
            iPrecio = new RPrecio();
        }
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
        #endregion
    }
}

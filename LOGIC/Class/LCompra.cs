using ENTITY.com.Compra.View;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGIC.Class
{
    public class LCompra
    {
        protected ICompra iCompra;
        public LCompra()
        {
            iCompra = new RCompra();
        }
        #region Consulta
        public List<VCompraLista> Listar()
        {
            try
            {
                return iCompra.Lista();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}

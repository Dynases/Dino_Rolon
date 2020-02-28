using ENTITY.reg.PrecioCategoria.View;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGIC.Class
{
   public class LPrecioCategoria
    {
        protected IPrecioCategoria iPrecioCat;
        public LPrecioCategoria()
        {
            iPrecioCat = new RPrecioCategoria();
        }
        #region Consulta
        public List<VPrecioCategoria> Listar()
        {
            try
            {
                return iPrecioCat.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}

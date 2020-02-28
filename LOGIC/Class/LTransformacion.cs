using ENTITY.inv.Transformacion.View;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGIC.Class
{
   public class LTransformacion
    {
        protected ITransformacion ITransformacion;
        public LTransformacion()
        {
            ITransformacion = new RTransformacion();
        }
        #region Consulta
        public List<VTransformacion> Listar()
        {
            try
            {
                return ITransformacion.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}

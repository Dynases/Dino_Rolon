using ENTITY.inv.Transformacion_01.View;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGIC.Class
{
   public  class LTransformacion_01
    {

        protected ITransformacion_01 ITransformacion_01;
        public LTransformacion_01()
        {
            ITransformacion_01 = new RTransformacion_01();
        }
        #region Consulta
        public List<VTransformacion_01_Lista> Listar()
        {
            try
            {
                return ITransformacion_01.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}

using ENTITY.inv.Transformacion.View;
using ENTITY.inv.Transformacion_01.View;
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
   public class LTransformacion
    {
        protected ITransformacion iTransformacion;
        public LTransformacion()
        {
            iTransformacion = new RTransformacion();
        }
        #region TRANSACCIONES
        public bool Guardar(VTransformacion vSeleccion, List<VTransformacion_01> detalle, ref int Id)
        {
            try
            {
                bool result = false;
                using (var scope = new TransactionScope())
                {
                    int aux = Id;
                    result = iTransformacion.Guardar(vSeleccion, ref Id);
                    if (aux == 0)//Nuevo 
                    {

                        var resultDetalle = new LTransformacion_01().Nuevo(detalle, Id);
                    }
                    else//Modificar          
                    { 
                        var resultDetalle = new LTransformacion_01().Modificar(detalle, Id);
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
        #endregion
        #region Consulta
        public List<VTransformacion> Listar()
        {
            try
            {
                return iTransformacion.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}

using ENTITY.inv.Transformacion.Report;
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
        protected ITI001 iTi001;
        protected ITI002 iTi002;
        protected ITI0021 iTi0021;
        public LTransformacion()
        {
            iTi001 = new RTI001(iTi002, iTi0021);
            iTi002 = new RTI002();
            iTi0021 = new RTI0021();
            iTransformacion = new RTransformacion(iTi001, iTi002, iTi0021);
        }
        #region TRANSACCIONES
        public bool Guardar(VTransformacion vSeleccion, List<VTransformacion_01> detalle, ref int Id, ref List<string> lMensaje)
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
                        var resultDetalle = new LTransformacion_01().Modificar(detalle, Id, vSeleccion.Usuario, ref lMensaje);
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

        public bool ModificarEstado(int IdTransformacion, int estado, ref List<string> lMensaje)
        {
            try
            {
                bool result = false;
                using (var scope = new TransactionScope())
                {
                    result = iTransformacion.ModificarEstado(IdTransformacion, estado, ref lMensaje);
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
        public List<VTransformacion> Listar(int usuarioId)
        {
            try
            {
                return iTransformacion.Listar(usuarioId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VTransformacionReport> ListarIngreso(int Id)
        {
            try
            {
                return iTransformacion.ListarIngreso(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VTransformacionReport> ListarSalida(int Id)
        {
            try
            {
                return iTransformacion.ListarSalida(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}

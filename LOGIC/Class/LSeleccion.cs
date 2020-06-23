using ENTITY.com.Seleccion.Report;
using ENTITY.com.Seleccion.View;
using ENTITY.com.Seleccion_01.View;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using UTILITY.Enum.EnEstado;

namespace LOGIC.Class
{
    public class LSeleccion
    {
        protected ISeleccion iSeleccion;
        protected ITI001 iTi001;    
        protected ITI002 iTi002;
        protected ITI0021 iTi0021;
        public LSeleccion()
        {
            iTi001 = new RTI001(iTi002, iTi0021);
            iTi002 = new RTI002();
            iTi0021 = new RTI0021();
            iSeleccion = new RSeleccion(iTi001, iTi002, iTi0021);
        }
        #region TRANSACCIONES
        public bool Guardar(VSeleccion vSeleccion, List<VSeleccion_01_Lista> detalle_Seleecion, List<VSeleccion_01_Lista> detalle_Ingreso, ref int idSeleccion)
        {
            try
            {
                bool result = false;          
                using (var scope = new TransactionScope())
                {
                    if (idSeleccion == 0) //Nuevo
                    {
                        result = iSeleccion.Guardar(vSeleccion, ref idSeleccion);                       
                        var resultIngreso = new LSeleccion_01().GuardarModificar_CompraIngreso(detalle_Ingreso,vSeleccion.IdCompraIng);
                        if (!new LSeleccion_01().NuevoMovimientoSelecciom(detalle_Ingreso,vSeleccion.IdCompraIng, idSeleccion))
                        {
                            return false;
                        }
                        var resultDetalle = new LSeleccion_01().Guardar(detalle_Seleecion, idSeleccion);
                    }
                    else
                    {
                        result = iSeleccion.Guardar(vSeleccion, ref idSeleccion);
                        var resultDetalle = new LSeleccion_01().GuardarModificar(detalle_Seleecion, idSeleccion);
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
        public bool ModificarEstado(int IdSeleccion,int estado, ref List<string> lMensaje)
        {
            try
            {
                bool result = false;
                using (var scope = new TransactionScope())
                {
                    result = iSeleccion.ModificarEstado(IdSeleccion, estado, ref lMensaje);
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
        /******** VALOR/REGISTRO ÚNICO *********/
        public VSeleccionLista TraerSeleccion(int idSeleccion)
        {
            try
            {
                return iSeleccion.TraerSeleccion(idSeleccion);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<RSeleccionNota> NotaSeleccion( int idSeleccion)
        {
            try
            {
                return iSeleccion.NotaSeleccion(idSeleccion);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /********** VARIOS REGISTROS ***********/
        public List<VSeleccionEncabezado> TraerSelecciones()
        {
            try
            {
                return iSeleccion.TraerSelecciones();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }       
        #endregion
    }
}

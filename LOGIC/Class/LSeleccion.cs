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

namespace LOGIC.Class
{
    public class LSeleccion
    {
        protected ISeleccion iSeleccion;
        public LSeleccion()
        {
            iSeleccion = new RSeleccion();
        }
        #region TRANSACCIONES
        public bool Guardar(VSeleccion vSeleccion, List<VSeleccion_01_Lista> detalle_Seleecion, List<VSeleccion_01_Lista> detalle_Ingreso, ref int Id)
        {
            try
            {
                bool result = false;          
                using (var scope = new TransactionScope())
                {
                    if (Id == 0) //Nuevo
                    {
                        result = iSeleccion.Guardar(vSeleccion, ref Id);
                        var resultIngreso = new LSeleccion_01().GuardarModificar_CompraIngreso(detalle_Ingreso,vSeleccion.IdCompraIng);
                        var resultDetalle = new LSeleccion_01().Guardar(detalle_Seleecion, Id);
                    }
                    else
                    {
                        result = iSeleccion.Guardar(vSeleccion, ref Id);
                     
                        var resultDetalle = new LSeleccion_01().GuardarModificar(detalle_Seleecion, Id);
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

        public List<VSeleccionLista> Listar()
        {
            try
            {
                return iSeleccion.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}

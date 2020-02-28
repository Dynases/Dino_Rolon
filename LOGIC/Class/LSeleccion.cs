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
        #region Consulta
        public bool Guardar(VSeleccion vSeleccion, List<VSeleccion_01> detalle, ref int Id, string usuario)
        {
            try
            {
                using (var scope = new TransactionScope())

                {
                    var result = iSeleccion.Guardar(vSeleccion, ref Id);

                    var resultDetalle = new LSeleccion_01().Guardar(detalle, Id);

                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
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

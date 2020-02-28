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
    public class LSeleccion_01
    {
        protected ISeleccion_01 iSeleccion_01;
        public LSeleccion_01()
        {
            iSeleccion_01 = new RSeleccion_01();
        }
        #region TRANSACCIONES
        public bool Guardar(List<VSeleccion_01> lista, int Id)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = iSeleccion_01.Guardar(lista, Id);
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
        #region CONSULTAS
        public List<VSeleccion_01_Lista> Listar()
        {
            try
            {
                return iSeleccion_01.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VSeleccion_01_Lista> ListarXId_Vacio(int Id)
        {
            try
            {
                return iSeleccion_01.ListarXId_Vacio(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
    #endregion

}

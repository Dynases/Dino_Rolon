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
        public bool Guardar(List<VSeleccion_01_Lista> lista, int Id)
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
        public bool GuardarModificar(List<VSeleccion_01_Lista> lista, int Id)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = iSeleccion_01.GuardarModificar(lista, Id);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool GuardarModificar_CompraIngreso(List<VSeleccion_01_Lista> lista, int IdCompraIngreso)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = iSeleccion_01.GuardarModificar_CompraIngreso(lista, IdCompraIngreso);
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
        public List<VSeleccion_01_Lista> ListarXId_CompraIng(int Id, int tipo)
        {
            try
            {
                return iSeleccion_01.ListarXId_CompraIng(Id, tipo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VSeleccion_01_Lista> ListarXId_CompraIng_XSeleccion(int Id)
        {
            try
            {
                return iSeleccion_01.ListarXId_CompraIng_XSeleccion(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
    #endregion

}

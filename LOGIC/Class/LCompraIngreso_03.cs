using ENTITY.com.CompraIngreso_01;
using ENTITY.com.CompraIngreso_03.View;
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
    public class LCompraIngreso_03
    {
        protected ICompraIngreso_03 iCompraIngreso_03;
        
        public LCompraIngreso_03()
        {
            iCompraIngreso_03 = new RCompraIngreso_03();
        }
        #region Transacciones
        public bool Guardar(List<VCompraIngreso_03> lista, int Id)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    foreach (var fila in lista)
                    {
                        if (fila.Estado == (int)ENEstado.NUEVO)
                        {
                            iCompraIngreso_03.Guardar(fila, Id);
                           
                        }
                        if (fila.Estado == (int)ENEstado.MODIFICAR)
                        {
                            iCompraIngreso_03.Modificar(fila, Id);
                        }
                    }
                    scope.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }      
        #endregion

        #region Consultas
        /******** VALOR/REGISTRO ÚNICO *********/
        public List<VCompraIngreso_03> TraerDevoluciones(int id)
        {
            try
            {
                return iCompraIngreso_03.TraerDevoluciones(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VCompraIngreso_03> TraerDevolucionesTipoProducto(int IdGrupo2, int idAlmacen)
        {
            try
            {
                return iCompraIngreso_03.TraerDevolucionesTipoProducto(IdGrupo2, idAlmacen);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /********** VARIOS REGISTROS ***********/
        #endregion
    }
}

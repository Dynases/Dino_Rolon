using ENTITY.com.CompraIngreso.View;
using ENTITY.com.CompraIngreso_01;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace LOGIC.Class
{
   public class LCompraIngreso
    {
        protected ICompraIngreso iCompraIngreso;
        public LCompraIngreso()
        {
            iCompraIngreso = new RCompraIngreso();
        }

        #region Transacciones
        public bool Guardar(VCompraIngresoLista vCompraIngreso, List<VCompraIngreso_01> detalle, ref int Id,string usuario)
        {
            try
            {
                using (var scope =new TransactionScope())
                {
                    var result = iCompraIngreso.Guardar(vCompraIngreso, ref Id);

                    var resultDetalle = new LCompraIngreso_01().Guardar(detalle, Id,usuario);

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

        public List<VCompraIngresoLista> ListarXId(int id)
        {
            try
            {
                return iCompraIngreso.ListarXId(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VCompraIngreso> Listar()
        {
            try
            {
                return iCompraIngreso.Listar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VCompraIngresoNota> ListarNotaXId(int id)
        {
            try
            {
                return iCompraIngreso.ListarNotaXId(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public DataTable ListarEncabezado()
        {
            try
            {
                return iCompraIngreso.ListarEncabezado();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}

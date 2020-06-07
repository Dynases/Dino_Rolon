using ENTITY.com.CompraIngreso.View;
using ENTITY.com.CompraIngreso_01;
using ENTITY.com.CompraIngreso_03.View;
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
        protected ITI001 iTi001;
        protected ITI002 iTi002;
        protected ITI0021 iTi0021;
        public LCompraIngreso()
        {
            iTi001 = new RTI001(iTi002, iTi0021);
            iTi002 = new RTI002();
            iTi0021 = new RTI0021();
            iCompraIngreso = new RCompraIngreso(iTi001, iTi002, iTi0021);
        }
        #region Transacciones
        public bool Guardar(VCompraIngresoLista vCompraIngreso, List<VCompraIngreso_01> vCompraIngreso_01, ref int Id,string usuario, bool EsDevolucion, List<VCompraIngreso_03> vCompraIngreso_03)
        {
            try
            {               
                using (var scope =new TransactionScope())
                {
                    if (Id == 0) //Nuevo
                    {
                        iCompraIngreso.Guardar(vCompraIngreso, ref Id);
                        new LCompraIngreso_01().Guardar(vCompraIngreso_01, Id, usuario);                        
                    }
                    else
                    {
                        iCompraIngreso.Guardar(vCompraIngreso, ref Id);
                        new LCompraIngreso_01().GuardarModificado(vCompraIngreso_01, Id, usuario);                       
                    }
                    if (!EsDevolucion)
                    {
                        new LCompraIngreso_03().Guardar(vCompraIngreso_03, Id);
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
        public bool ModificarEstado(int IdCompraIng, int estado, ref List<string> lMensaje)
        {
            try
            {
                bool result = false;
                using (var scope = new TransactionScope())
                {                     
                    result= iCompraIngreso.ModificarEstado(IdCompraIng, estado, ref lMensaje);                    
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
        /********** VARIOS REGISTROS ***********/
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
        public DataTable BuscarCompraIngreso(int estado)
        {
            try
            {
                return iCompraIngreso.BuscarCompraIngreso(estado);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /********** REPORTES ***********/
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
        public DataTable ReporteCompraIngreso(DateTime? fechaDesde, DateTime? fechaHasta, int estado)
        {
            try
            {
                return iCompraIngreso.ReporteCompraIngreso(fechaDesde, fechaHasta, estado);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Verificaciones
        public bool ExisteEnSeleccion(int idCompraIng)
        {
            try
            {
                return iCompraIngreso.ExisteEnSeleccion(idCompraIng);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}

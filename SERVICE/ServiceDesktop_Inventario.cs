using ENTITY.inv.Ajuste.View;
using ENTITY.inv.Concepto;
using LOGIC.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SERVICE
{
    public partial class Service1 : IServiceDesktop
    {
        /********** Ajuste ******************/
        #region Ajuste
        #region Transacciones
        public int Ajuste_Guardar(VAjuste ajuste, List<VAjusteDetalle> detalle, string usuario)
        {
            try
            {
                var result = new LAjuste().Guardar(ajuste, detalle, usuario);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Consulta
        public List<VAjusteLista> Ajuste_Lista()
        {
            try
            {
                var listResult = new LAjuste().Listar();
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VAjusteDetalle> AjusteDetalle_Lista(int id)
        {
            try
            {
                var listResult = new LAjuste().ListaDetalle(id);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #endregion

        /********** Concepto ******************/
        #region Concepto
        #region Consulta
        public List<VConceptoCombo> Concepto_ListaComboAjuste()
        {
            try
            {
                var listResult = new LConcepto().ListaComboAjuste();
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion        
        #endregion
    }
}
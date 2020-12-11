using ENTITY.inv.Ajuste.Report;
using ENTITY.inv.Ajuste.View;
using ENTITY.inv.Concepto.View;
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
        public void Ajuste_Eliminar(int ajusteId)
        {
            try
            {
                new LAjuste().Eliminar(ajusteId);                
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
        #region Transaciones
        public int Concepto_Guardar(VConcepto concepto)
        {
            try
            {
                var conceptoId = new LConcepto().Guardar(concepto);
                return conceptoId;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Concepto_Eliminar(int Id)
        {
            try
            {
                var result = false;
                return result = new LConcepto().Eliminar(Id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
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
        public List<VConcepto> Concepto_Lista()
        {
            try
            {
                var listResult = new LConcepto().ListaConcepto();
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VConceptoLista> ObtenerListaConcepto()
        {
            try
            {
                var listResult = new LConcepto().ObtenerListaConcepto();
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #endregion

        /********** Ajuste ******************/
        #region Ajuste Fisico
        #region Transacciones
        public int AjusteFisico_Guardar(VAjusteFisico ajuste, List<VAjusteFisicoProducto> detalle, string usuario)
        {
            try
            {
                var result = new LAjusteFisico().Guardar(ajuste, detalle, usuario);
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void AjusteFisico_Eliminar(int ajusteId)
        {
            try
            {
                new LAjusteFisico().Eliminar(ajusteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Consulta
        public List<VAjusteLista> AjusteFisico_Lista()
        {
            try
            {
                var listResult = new LAjusteFisico().Listar();
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VAjusteTicket> ReporteAjuste(int ajusteId)
        {
            try
            {
                var listResult = new LAjusteFisico().ReporteAjuste(ajusteId);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VAjusteFisicoProducto> AjusteFisicoDetalle_Lista(int id)
        {
            try
            {
                var listResult = new LAjusteFisico().ListaDetalle(id);
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
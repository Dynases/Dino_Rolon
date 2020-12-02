using ENTITY.adm.ValidacioinPrograma;
using ENTITY.inv.Concepto.View;
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
   public class LConcepto
    {
        protected IConcepto iConcepto;

        public LConcepto()
        {
            iConcepto = new RConcepto();
        }
        #region Transaciones
        public void Guardar(VConcepto concepto, ref int Id )
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    iConcepto.Guardar(concepto,ref Id);
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Eliminar(int Id)
        {
            try
            {
                FValidacionPrograma validacionPrograma = new FValidacionPrograma();
                validacionPrograma.tablaOrigen = "TCI001";
                List<string> mensaje = new List<string>();
                if (new LValidacionPrograma().ValidadrEliminacion(Id, validacionPrograma, ref mensaje, false))
                {
                    using (var scope = new TransactionScope())
                    {
                        iConcepto.Eliminar(Id);
                        scope.Complete();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Consulta
        public List<VConceptoCombo> ListaComboAjuste()
        {
            try
            {
                return iConcepto.ListaComboAjuste();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VConcepto> ListaConcepto()
        {
            try
            {
                return iConcepto.ListaConcepto();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}

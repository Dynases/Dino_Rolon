using ENTITY.inv.Concepto;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOGIC.Class
{
    public class LConcepto
    {
        protected IConcepto iConcepto;

        public LConcepto()
        {
            iConcepto = new RConcepto();
        }

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
        #endregion
    }
}

using ENTITY.inv.Concepto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface
{
    public interface IConcepto
    {
        #region Consulta
        List<VConceptoCombo> ListaComboAjuste();
        VConceptoCombo ObternerPorId(int id);
        #endregion
    }
}

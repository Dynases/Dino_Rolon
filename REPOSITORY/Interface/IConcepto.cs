using ENTITY.inv.Concepto.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface
{
 public interface IConcepto
    {
        #region Transacciones
        void Guardar(VConcepto concepto, ref int id);
        void Eliminar(int id);
        #endregion
        #region Consulta
        List<VConcepto> ListaConcepto();
        List<VConceptoCombo> ListaComboAjuste();
        VConceptoCombo ObternerPorId(int id);
        #endregion
    }
}

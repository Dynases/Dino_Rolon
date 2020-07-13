using ENTITY.inv.Ajuste.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface
{
   public interface IAjuste
    {
        #region Ajuste
        List<VAjusteLista> Lista();
        VAjuste ObtenerPorId(int id);
        void Guardar(VAjuste ajuste, ref int id, string usuario);
        void Eliminar(int IdAjuste);
        #endregion

        #region AjusteDetalle
        List<VAjusteDetalle> ListaDetalle(int id);
        void GuardarDetalle(List<VAjusteDetalle> detalle, int id);
        #endregion
    }
}

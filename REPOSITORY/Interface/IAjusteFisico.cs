using ENTITY.inv.Ajuste.Report;
using ENTITY.inv.Ajuste.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Interface
{
   public interface IAjusteFisico
    {
        #region Ajuste
        List<VAjusteLista> Lista();
        VAjusteFisico ObtenerPorId(int id);
        void Guardar(VAjusteFisico ajuste, ref int id, string usuario);
        void Eliminar(int IdAjuste);
        List<VAjusteTicket> ReporteAjuste(int ajusteId);
        #endregion

        #region AjusteDetalle
        List<VAjusteFisicoProducto> ListaDetalle(int id);
        void GuardarDetalle(List<VAjusteFisicoProducto> detalle, int id);
        #endregion
    }
}

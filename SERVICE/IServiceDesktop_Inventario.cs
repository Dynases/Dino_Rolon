using ENTITY.inv.Ajuste.View;
using ENTITY.inv.Concepto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace SERVICE
{
    public partial interface IServiceDesktop
    {
        /********** Ajuste ******************/
        #region Ajuste
        #region Transacciones
        [OperationContract]
        int Ajuste_Guardar(VAjuste ajuste, List<VAjusteDetalle> detalle, string usuario);
        #endregion
        #region Consulta
        [OperationContract]
        List<VAjusteLista> Ajuste_Lista();
        [OperationContract]
        List<VAjusteDetalle> AjusteDetalle_Lista(int id);
        #endregion
        #endregion


        /********** Concepto ******************/
        #region Concepto
        #region Consulta
        [OperationContract]
        List<VConceptoCombo> Concepto_ListaComboAjuste();
        #endregion        
        #endregion
    }
}

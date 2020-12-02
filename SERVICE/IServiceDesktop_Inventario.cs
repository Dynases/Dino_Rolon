using ENTITY.inv.Ajuste.View;
using ENTITY.inv.Concepto;
using ENTITY.inv.Concepto.View;
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
        [OperationContract]
        void Ajuste_Eliminar(int ajusteId);
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
        #region Transacciones
        [OperationContract]
        void Guardar(VConcepto concepto, ref int Id);
        [OperationContract]
        bool Eliminar(int Id);
        #endregion
        #region Consulta
        [OperationContract]
        List<VConceptoCombo> Concepto_ListaComboAjuste();
        [OperationContract]
        List<VConcepto> Concepto_Listar();
        #endregion        
        #endregion
    }
}
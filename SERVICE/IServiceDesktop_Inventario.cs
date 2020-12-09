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
        int Concepto_Guardar(VConcepto concepto);
        [OperationContract]
        bool Concepto_Eliminar(int Id);
        #endregion
        #region Consulta
        [OperationContract]
        List<VConceptoCombo> Concepto_ListaComboAjuste();
        [OperationContract]
        List<VConcepto> Concepto_Lista();
        [OperationContract]
        List<VConceptoLista> ObtenerListaConcepto();
        #endregion
        #endregion


        /********** Ajuste fisico ******************/
        #region Ajuste Fisico
        #region Transacciones
        [OperationContract]
        int AjusteFisico_Guardar(VAjusteFisico ajuste, List<VAjusteFisicoProducto> detalle, string usuario);
        [OperationContract]
        void AjusteFisico_Eliminar(int ajusteId);
        #endregion
        #region Consulta
        [OperationContract]
        List<VAjusteLista> AjusteFisico_Lista();

        [OperationContract]
        List<VAjusteFisicoProducto> AjusteFisicoDetalle_Lista(int id);
        #endregion
        #endregion


       
    }
}
using ENTITY.inv.Traspaso.Report;
using ENTITY.inv.Traspaso.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;

namespace SERVICE
{
    public partial interface IServiceDesktop
    {
        /**********   Traspaso ******************/
  
        #region Consulta

        /******** VALOR/REGISTRO ÚNICO *********/
        [OperationContract]
        VTraspaso_01 TraerTraspass_01(int idDetalle);

        /********** VARIOS REGISTROS ***********/        
        [OperationContract]
        List<VTraspaso> TraerTraspasos(int usuarioId);

        [OperationContract]
        List<VTraspaso_01> TraerTraspasos_01(int idTraspaso);

        [OperationContract]
        List<VTListaProducto> ListarInventarioXAlmacenId(int AlmacenId);
        [OperationContract]
        List<VTraspaso_01> TraerTraspasos_01Vacio(int idTraspaso);

        /********** REPORTE ***********/
        [OperationContract]
        List<VTraspasoTicket> ReporteTraspaso(int traspasoId);
        #endregion

        #region Transacciones

        [OperationContract]
        bool GuardarTraspaso(VTraspaso vTraspaso, List<VTraspaso_01> detalle, ref int idTraspaso, ref List<string> lMensaje);

        [OperationContract]
        bool TraspasoDetalleGuardar(List<VTraspaso_01> lista, int TraspasoId, int almacenId, int idTI2);

        [OperationContract]
        bool TraspasoConfirmarRecepcion(int traspasoId, string usuarioRecepcion);
        [OperationContract]
        void EliminarTraspaso(int ajusteId);
        #endregion

        #region Verificaciones

        #endregion


    }
}
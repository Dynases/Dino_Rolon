using ENTITY.inv.Traspaso.Report;
using ENTITY.inv.Traspaso.View;
using LOGIC.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SERVICE
{
    public partial class Service1 : IServiceDesktop
    {
        /********** TRASPASO ****************/

    
        #region Consulta
        /******** VALOR/REGISTRO ÚNICO *********/
        /********** VARIOS REGISTROS ***********/
        public List<VTraspaso> TraerTraspasos(int usuarioId)
        {
            try
            {
                return new LTraspaso().TraerTraspasos(usuarioId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VTListaProducto> ListarInventarioXAlmacenId(int AlmacenId)
        {
            try
            {
                return new LTraspaso().ListarInventarioXAlmacenId(AlmacenId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool TraspasoConfirmarRecepcion(int traspasoId, string usuarioRecepcion)
        {
            try
            {
                return new LTraspaso().ConfirmarRecepcion(traspasoId, usuarioRecepcion);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /********** REPORTE ***********/
        public List<VTraspasoTicket> ReporteTraspaso(int traspasoId)
        {
            try
            {
                var listResult = new LTraspaso().ReporteTraspaso(traspasoId);
                return listResult;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Transacciones
        public bool GuardarTraspaso(VTraspaso vTraspaso, List<VTraspaso_01> detalle, ref int idTraspaso, ref List<string> lMensaje)
        {
            try
            {
                return new LTraspaso().Guardar(vTraspaso, detalle, ref idTraspaso, ref lMensaje);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void EliminarTraspaso(int traspasoId)
        {
            try
            {
                new LTraspaso().Eliminar(traspasoId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Verificaciones

        #endregion

        /********** TRASPASO DETALLE ****************/

        #region Consulta

        /******** VALOR/REGISTRO ÚNICO *********/
        public VTraspaso_01 TraerTraspass_01(int idDetalle)
        {
            try
            {
                return new LTraspaso_01().TraerTraspass_01(idDetalle);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        
        /********** VARIOS REGISTROS ***********/
        public List<VTraspaso_01> TraerTraspasos_01(int idTraspaso)
        {
            try
            {
                return new LTraspaso_01().ListaDetalle(idTraspaso);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VTraspaso_01> TraerTraspasos_01Vacio(int idTraspaso)
        {
            try
            {
                return new LTraspaso_01().TraerTraspasos_01Vacio(idTraspaso);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /********** REPORTE ***********/
        #endregion

        #region Transacciones
        public bool TraspasoDetalleGuardar(List<VTraspaso_01> lista, int TraspasoId, int almacenId, int idTI2)
        {
            try
            {
                return new LTraspaso_01().Nuevo(lista, TraspasoId, almacenId, idTI2);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}
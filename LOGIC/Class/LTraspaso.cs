using ENTITY.inv.Traspaso.View;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Transactions;

namespace LOGIC.Class
{
    public class LTraspaso
    {
        protected ITraspaso iTraspaso;
        protected ITI002 iTI002;
        protected ITI0021 iTI0021;
        protected ITI001 iTI001;
        protected ITraspaso_01 iTraspaso_01;
        public LTraspaso()
        {
            iTI002 = new RTI002();
            iTI001 = new RTI001(iTI002, iTI0021);
            iTI0021 = new RTI0021();
            iTraspaso_01 = new RTraspaso_01(iTI001, iTI0021);
            iTraspaso = new RTraspaso(iTI002, iTraspaso_01);
        }

        #region Transacciones

        public bool Guardar(VTraspaso vTraspaso, ref int idTI2, ref int id)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var result = iTraspaso.Guardar(vTraspaso, ref idTI2, ref id);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ConfirmarRecepcion(int traspasoId, string usuarioRecepcion)
        {
            try
            {
                if (!this.iTraspaso.ConfirmarRecepcion(traspasoId, usuarioRecepcion))
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Consulta

        public List<VTraspaso> Listar()
        {
            try
            {
                return this.iTraspaso.ListarTraspasos();
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
                return this.iTraspaso.ListarInventarioXAlmacenId(AlmacenId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}

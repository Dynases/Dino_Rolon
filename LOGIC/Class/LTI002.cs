using ENTITY.inv.TI002.View;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace LOGIC.Class.DiSoft
{
    public class LTI002
    {
        protected ITI002 iTi002;
        public LTI002()
        {
            iTi002 = new RTI002();   
        }

        #region Consulta
        /******** VALOR/REGISTRO ÚNICO *********/
        public VTI002 TraerMovimiento(int idDetalle, int idConcepto)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var resultado = this.iTi002.TraerMovimiento(idDetalle,idConcepto);
                    scope.Complete();
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        /********** VARIOS REGISTROS ***********/

        /********** REPORTE ***********/
        #endregion
        #region Transacciones
        public bool Nuevo(VTI002 VMovimiento, ref int IdMovimiento)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var resultado = this.iTi002.Guardar(VMovimiento.IdAlmacenOrigen,
                                                               VMovimiento.idAlmacenDestino,
                                                               VMovimiento.IdDetalle,
                                                               VMovimiento.Usuario,
                                                               VMovimiento.Observacion,
                                                               VMovimiento.IdConecpto,
                                                               ref IdMovimiento,
                                                               VMovimiento.IdDestino);
                    scope.Complete();
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Modificar(VTI002 VMovimiento, ref int IdMovimiento)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var resultado = this.iTi002.Modificar(VMovimiento.IdAlmacenOrigen,
                                                               VMovimiento.idAlmacenDestino,
                                                               VMovimiento.IdDetalle,
                                                               VMovimiento.Usuario,
                                                               VMovimiento.Observacion,
                                                               VMovimiento.IdConecpto,
                                                               VMovimiento.IdDestino);
                    scope.Complete();
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Eliminar(int IdDetalle, int concepto)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var resultado = this.iTi002.Eliminar(IdDetalle, concepto);
                    scope.Complete();
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ModificarCampoDestinoTraspaso(int idTraspaso)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var resultado = this.iTi002.ModificarCampoDestinoTraspaso(idTraspaso);
                    scope.Complete();
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Verificaciones
        public bool ExisteEnMovimiento(int idDetalle, int concepto)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    var resultado = this.iTi002.ExisteEnMovimiento(idDetalle, concepto);
                    scope.Complete();
                    return resultado;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

    }
}

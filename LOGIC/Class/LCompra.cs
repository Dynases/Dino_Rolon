using ENTITY.com.Compra.View;
using ENTITY.com.Compra_01.View;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using UTILITY.Enum.EnEstado;

namespace LOGIC.Class
{
    public class LCompra
    {
        protected ICompra iCompra;
        protected ITI001 iTi001;
        protected ITI002 iTi002;
        protected ITI0021 iTi0021;
        public LCompra()
        {
            iTi002 = new RTI002();
            iTi0021 = new RTI0021();
            iTi001 = new RTI001(iTi002, iTi0021);           
            iCompra = new RCompra(iTi001);
        }
        #region Transaccion
        public bool Guardar(VCompra vCompra, List<VCompra_01> detalle, ref int IdCompra, ref List<string> lMensaje, string usuario)
        {
            try
            {
                bool result = false;
                using (var scope = new TransactionScope())
                {
                    int aux = IdCompra;
                    result = iCompra.Guardar(vCompra, ref IdCompra);
                    if (aux == 0)//Nuevo 
                    {
                        var resultDetalle = new LCompra_01().Nuevo(detalle, IdCompra, usuario);
                    }
                    else//Modificar          
                    {
                        foreach (var i in detalle)
                        {
                            if (i.Estado == (int)ENEstado.NUEVO)
                            {
                                List<VCompra_01> detalleNuevo = new List<VCompra_01>();
                                detalleNuevo.Add(i);
                                var resultDetalle = new LCompra_01().Nuevo(detalleNuevo, IdCompra, usuario);
                            }
                            if (i.Estado == (int)ENEstado.MODIFICAR)
                            {
                                var resultDetalle = new LCompra_01().Modificar(i, IdCompra, usuario);
                                if (resultDetalle == false)
                                {
                                    return false;
                                }
                            }
                            if (i.Estado == (int)ENEstado.ELIMINAR)
                            {
                                var resultDetalle = new LCompra_01().Eliminar(IdCompra, i.Id, ref lMensaje);
                                if (resultDetalle == false)
                                {
                                    return false;
                                }
                            }
                        }
                    }
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool ModificarEstado(int IdCompra, int estado, ref List<string> lMensaje)
        {
            try
            {
                bool result = false;
                using (var scope = new TransactionScope())
                {
                    result = iCompra.ModificarEstado(IdCompra, estado, ref lMensaje);
                    scope.Complete();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region Consulta

        public List<VCompraLista> Listar()
        {
            try
            {
                return iCompra.Lista();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}

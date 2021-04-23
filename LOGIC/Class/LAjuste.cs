using ENTITY.inv.Ajuste.View;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using UTILITY.Enum.ENConcepto;
using UTILITY.Enum.EnEstado;
namespace LOGIC.Class
{
  public  class LAjuste
    {
        protected IAjuste iAjuste;
        protected IConcepto iConcepto;
        protected ITI001 iTI001;     

        public LAjuste()
        {
            iAjuste = new RAjuste();
            iConcepto = new RConcepto();
            iTI001 = new RTI001();      
        }
        #region Transaccion
        public int Guardar(VAjuste ajuste, List<VAjusteDetalle> detalle, string usuario)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    bool EsAjusteFisico = false;
                    VAjuste ajusteAnterior = null;
                    List<VAjusteDetalle> detalleAnterior = null;
                    if (ajuste.Id > 0)
                    {
                        ajusteAnterior = iAjuste.ObtenerPorId(ajuste.Id);
                        detalleAnterior = iAjuste.ListaDetalle(ajuste.Id);
                    }

                    int id = ajuste.Id;
                    //Ajuste
                    iAjuste.Guardar(ajuste, ref id, usuario);
                    iAjuste.GuardarDetalle(detalle, id, EsAjusteFisico);

                    //Saldo
                    var accionAnterior = 0;
                    var accionActual = iConcepto.ObternerPorId(ajuste.IdConcepto).TipoMovimiento;
                    if (ajusteAnterior != null)
                    {
                        accionAnterior = iConcepto.ObternerPorId(ajusteAnterior.IdConcepto).TipoMovimiento;
                    }

                    foreach (var item in detalle)
                    {
                        var cantidadActual = item.Cantidad * accionActual;
                        VAjusteDetalle itemAnterior;
                        switch (item.Estado)
                        {
                            case (int)ENEstado.NUEVO:
                                iTI001.ActualizarInventario(item.IdProducto, ajuste.IdAlmacen, cantidadActual, item.Lote, item.FechaVen);
                                break;
                            case (int)ENEstado.MODIFICAR:
                                itemAnterior = detalleAnterior.Where(a => a.Id == item.Id).FirstOrDefault();
                                if (itemAnterior != null)
                                {
                                    var cantidadAnterior = itemAnterior.Cantidad * accionAnterior * -1;
                                    iTI001.ActualizarInventario(item.IdProducto, ajuste.IdAlmacen, cantidadAnterior, item.Lote, item.FechaVen);
                                }
                                iTI001.ActualizarInventario(item.IdProducto, ajuste.IdAlmacen, cantidadActual, item.Lote, item.FechaVen);
                                break;
                            case (int)ENEstado.ELIMINAR:
                                itemAnterior = detalleAnterior.Where(a => a.Id == item.Id).FirstOrDefault();
                                if (itemAnterior != null)
                                {
                                    var cantidadAnterior = itemAnterior.Cantidad * accionAnterior * -1;
                                    iTI001.ActualizarInventario(item.IdProducto, ajuste.IdAlmacen, cantidadAnterior, item.Lote, item.FechaVen);
                                }
                                break;
                        }
                    }
                    scope.Complete();
                    return id;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Eliminar( int ajusteId)
        {
            try
            {
                VAjuste ajuste = null;
                List<VAjusteDetalle> detalle = null;

                //Ajuste
                ajuste = iAjuste.ObtenerPorId(ajusteId);
                detalle = iAjuste.ListaDetalle(ajusteId);

                //Saldo
                var accion = 0;
                if (ajuste != null)
                {
                    accion = iConcepto.ObternerPorId(ajuste.IdConcepto).TipoMovimiento;
                }

                //Actualizar Stock
                foreach (var item in detalle)
                {
                    VAjusteDetalle itemAnterior;
                    itemAnterior = detalle.Where(a => a.Id == item.Id).FirstOrDefault();
                    if (itemAnterior != null)
                    {
                        var cantidad = itemAnterior.Cantidad * accion * -1;
                        iTI001.ActualizarInventario(item.IdProducto, ajuste.IdAlmacen, cantidad, item.Lote, item.FechaVen);
                    }
                }

                //Elimina 
                iAjuste.Eliminar(ajusteId);                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region Consulta
        public List<VAjusteLista> Listar()
        {
            try
            {
                return iAjuste.Lista();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VAjusteDetalle> ListaDetalle(int id)
        {
            try
            {
                return iAjuste.ListaDetalle(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}

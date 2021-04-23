using ENTITY.inv.Ajuste.Report;
using ENTITY.inv.Ajuste.View;
using ENTITY.inv.Concepto.View;
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
    public class LAjusteFisico
    {
        protected IAjusteFisico iAjusteFisico;
        protected IAjuste iAjuste;
        protected IConcepto iConcepto;
        protected ITI001 iTI001;
        protected ITI002 iInventario;
        public LAjusteFisico()
        {
            iAjusteFisico = new RAjusteFisico();
            iAjuste = new RAjuste();
            iConcepto = new RConcepto();
            iTI001 = new RTI001();
            iInventario = new RTI002();
        }
        #region Transaccion
        public int Guardar(VAjusteFisico ajusteFisico, List<VAjusteFisicoProducto> detalleFisico, string usuario)
        {
            try
            {
                using (var scope = new TransactionScope())
                {
                    
                    int idCOncepto = 0;
                    bool EsAjusteFisico = true;
                    VAjusteFisico ajusteAnterior = null;
                    List<VAjusteFisicoProducto> detalleAnterior = null;
                    VAjuste ajusteInventario = null;
                    List<VAjusteDetalle> ajusteDetalle = null;

                    idCOncepto = ajusteFisico.IdConcepto;
                    if (ajusteFisico.Id > 0)
                    {
                        ajusteAnterior = iAjusteFisico.ObtenerPorId(ajusteFisico.Id);
                        detalleAnterior = iAjusteFisico.ListaDetalle(ajusteFisico.Id);
                        idCOncepto = ajusteAnterior.IdConcepto;
                    }

                    int id = ajusteFisico.Id;
                    //AjusteFisico
                    iAjusteFisico.Guardar(ajusteFisico, ref id, usuario);
                    iAjusteFisico.GuardarDetalle(detalleFisico, id);                    

                    //Saldo
                    var accionAnterior = 0;
                    var accionActual = iConcepto.ObternerPorId(ajusteFisico.IdConcepto).TipoMovimiento;

                    //Ajuste Inventario TI002
                    var idInventario = iInventario.TraerMovimiento(id, idCOncepto) == null ? 0 : iInventario.TraerMovimiento(id, idCOncepto).IdManual;

                    ajusteInventario = LlenarAjuste(ajusteFisico, idInventario, id);
                    ajusteDetalle = LlenarAjusteDetalle(detalleFisico, idInventario, accionActual);


                    iAjuste.Guardar(ajusteInventario, ref idInventario, usuario);
                    iAjuste.GuardarDetalle(ajusteDetalle, idInventario, EsAjusteFisico);
                    if (ajusteAnterior != null)
                    {
                        accionAnterior = iConcepto.ObternerPorId(ajusteAnterior.IdConcepto).TipoMovimiento;
                    }

                    foreach (var item in detalleFisico)
                    {
                        var cantidadActual = accionActual == 1?  item.Diferencia * accionActual: item.Diferencia * accionActual * -1;
                        VAjusteFisicoProducto itemAnterior;
                        switch (item.Estado)
                        {
                            case (int)ENEstado.NUEVO:
                                iTI001.ActualizarInventario(item.IdProducto, ajusteFisico.IdAlmacen, cantidadActual, item.Lote, item.FechaVen);
                                break;
                            case (int)ENEstado.MODIFICAR:
                                itemAnterior = detalleAnterior.Where(a => a.Id == item.Id).FirstOrDefault();
                                if (itemAnterior != null)
                                {
                                    var cantidadAnterior = accionAnterior == 1 ?  itemAnterior.Diferencia * accionAnterior * -1 : itemAnterior.Diferencia * accionAnterior;
                                    iTI001.ActualizarInventario(item.IdProducto, ajusteFisico.IdAlmacen, cantidadAnterior, item.Lote, item.FechaVen);
                                }
                                iTI001.ActualizarInventario(item.IdProducto, ajusteFisico.IdAlmacen, cantidadActual, item.Lote, item.FechaVen);
                                break;
                            case (int)ENEstado.ELIMINAR:
                                itemAnterior = detalleAnterior.Where(a => a.Id == item.Id).FirstOrDefault();
                                if (itemAnterior != null)
                                {
                                    var cantidadAnterior = itemAnterior.Diferencia * accionAnterior * -1;
                                    iTI001.ActualizarInventario(item.IdProducto, ajusteFisico.IdAlmacen, cantidadAnterior, item.Lote, item.FechaVen);
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

        private  List<VAjusteDetalle> LlenarAjusteDetalle(List<VAjusteFisicoProducto> detalleFisico, int idInventario, int accionActual)
        {
            List<VAjusteDetalle> lista = new List<VAjusteDetalle>();
            foreach (var item in detalleFisico)
            {
                VAjusteDetalle itemDetalle = new VAjusteDetalle();
                itemDetalle.Estado =  idInventario == 0? (int)ENEstado.NUEVO: (int)ENEstado.MODIFICAR;
                itemDetalle.IdAjuste = idInventario;
                itemDetalle.Id = 0;
                itemDetalle.IdProducto = item.IdProducto;
                itemDetalle.Cantidad = accionActual == 1 ? item.Diferencia : item.Diferencia * -1;
                itemDetalle.Lote = item.Lote;
                itemDetalle.FechaVen = item.FechaVen;
                lista.Add(itemDetalle);
            }
            return lista;
        }

        private  VAjuste LlenarAjuste(VAjusteFisico ajusteFisico, int idInventario, int IdAjuste)
        {
            var NConcepto = iConcepto.ObtenerListaConcepto().ToList().Where(a => a.Id == ajusteFisico.IdConcepto).First().Descripcion;
                                                
            var ajuste = new VAjuste();
            ajuste.Id = idInventario;
            ajuste.IdAlmacen = ajusteFisico.IdAlmacen;
            ajuste.IdConcepto = ajusteFisico.IdConcepto;
            ajuste.Fecha = ajusteFisico.FechaReg;
            ajuste.Obs = IdAjuste.ToString()+ "- "+ NConcepto;
            ajuste.IdAjusteFisico = IdAjuste;
            return ajuste;
        }

        public void Eliminar(int ajusteId)
        {
            try
            {
                VAjusteFisico ajuste = null;
                List<VAjusteFisicoProducto> detalle = null;

                //Ajuste
                ajuste = iAjusteFisico.ObtenerPorId(ajusteId);
                detalle = iAjusteFisico.ListaDetalle(ajusteId);

                //Saldo
                var accion = 0;
                if (ajuste != null)
                {
                    accion = iConcepto.ObternerPorId(ajuste.IdConcepto).TipoMovimiento;
                }

                //Actualizar Stock
                foreach (var item in detalle)
                {
                    VAjusteFisicoProducto itemAnterior;
                    itemAnterior = detalle.Where(a => a.Id == item.Id).FirstOrDefault();
                    if (itemAnterior != null)
                    {
                        var cantidad = itemAnterior.Diferencia * accion * -1;
                        iTI001.ActualizarInventario(item.IdProducto, ajuste.IdAlmacen, cantidad, item.Lote, item.FechaVen);
                    }
                }

                //Elimina 
                iAjusteFisico.Eliminar(ajusteId);
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
                return iAjusteFisico.Lista();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<VAjusteFisicoProducto> ListaDetalle(int id)
        {
            try
            {
                return iAjusteFisico.ListaDetalle(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VAjusteTicket> ReporteAjuste(int ajusteId)
        {
            try
            {
                return iAjusteFisico.ReporteAjuste(ajusteId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
    }
}

using ENTITY.inv.TI002.View;
using ENTITY.inv.Traspaso.Report;
using ENTITY.inv.Traspaso.View;
using LOGIC.Class.DiSoft;
using REPOSITORY.Clase;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Transactions;
using UTILITY.Enum.ENConcepto;
using UTILITY.Enum.EnEstado;
using UTILITY.Global;

namespace LOGIC.Class
{
    public class LTraspaso
    {
        protected ITraspaso iTraspaso;
        protected IProducto iProducto;
        protected ITraspaso_01 iDetalle;
        protected IConcepto iConcepto;
        protected ITI001 iTI001;
        public LTraspaso()
        {
            iProducto = new RProducto();
            iDetalle = new RTraspaso_01();
            iTraspaso = new RTraspaso();
            iConcepto = new RConcepto();
            iTI001 = new RTI001();
        }

        #region Transacciones

        public bool Guardar(VTraspaso vTraspaso, List<VTraspaso_01> detalle, ref int idTraspaso,ref List<string> lMensaje)
        {
            try
            {                
                using (var scope = new TransactionScope())
                {
                    VTI002 vMovimiento = new VTI002();
                    int IdMovimiento = 0;
                    int auxiliarId = idTraspaso;

                    iTraspaso.Guardar(vTraspaso, ref idTraspaso);                 
                    LlenarDatosParaTI002(ref vMovimiento, vTraspaso,idTraspaso);

                    if (auxiliarId == 0)//Nuevo
                    {
                        //Ingresa el movimiento en la TI002
                        new LTI002().Nuevo(vMovimiento, ref IdMovimiento);
                        //Ingresa el detalle de movimiento y el detall de Traspaso
                        var resultDetalle = new LTraspaso_01().Nuevo(detalle, idTraspaso, vTraspaso.IdAlmacenOrigen, IdMovimiento);
                        if (iTraspaso.EsTraspasoDirecto(vTraspaso.IdAlmacenDestino))
                        {
                            vTraspaso.EstadoEnvio = 2;
                            vTraspaso.FechaEnvio = vTraspaso.FechaRecepcion;
                            vTraspaso.UsuarioRecepcion = vTraspaso.UsuarioRecepcion;
                            iTraspaso.Guardar(vTraspaso, ref idTraspaso);
                            LlenarDatosParaTI002(ref vMovimiento, vTraspaso, idTraspaso);
                            IngresoTraspasoDeSalida(vMovimiento, IdMovimiento, idTraspaso, vTraspaso, detalle);
                        }
                    }
                    else//Modificar
                    {
                        IdMovimiento = new LTI002().TraerMovimiento(idTraspaso, (int)ENConcepto.TRASPASO_SALIDA).IdManual;                        
                        foreach (var i in detalle)
                        {
                            if (i.Estado == (int)ENEstado.NUEVO)
                            {
                                List<VTraspaso_01> detalleNuevo = new List<VTraspaso_01>();
                                detalleNuevo.Add(i);
                                if (!new LTraspaso_01().Nuevo(detalle, idTraspaso, vTraspaso.IdAlmacenOrigen, IdMovimiento))
                                {
                                    return false;
                                }
                            }
                            if (i.Estado == (int)ENEstado.MODIFICAR)
                            {
                                if (!new LTraspaso_01().Modificar(i, idTraspaso, vTraspaso.IdAlmacenOrigen))
                                {
                                    return false;
                                }
                            }
                            if (i.Estado == (int)ENEstado.ELIMINAR)
                            {
                                if (!new LTraspaso_01().Eliminar(i.Id, vTraspaso.IdAlmacenOrigen, IdMovimiento, ref lMensaje))
                                {
                                    return false;
                                }
                            }                            
                        }                       
                    }
                    if (vTraspaso.EstadoEnvio == 2 && !new LTI002().ExisteEnMovimiento(idTraspaso, (int)ENConcepto.TRASPASO_INGRESO))
                    {
                        IngresoTraspasoDeSalida(vMovimiento, IdMovimiento, idTraspaso, vTraspaso, detalle);
                    }
                    scope.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private void IngresoTraspasoDeSalida(VTI002 vMovimiento, int IdMovimiento, int idTraspaso, VTraspaso vTraspaso, List<VTraspaso_01> detalle)
        {
            //Ingresa el movimiento en la TI002
            new LTI002().Nuevo(vMovimiento, ref IdMovimiento);

            //Modifica, hace el cruce de IdDestino entre los movimientos.
            new LTI002().ModificarCampoDestinoTraspaso(idTraspaso);
            new LTraspaso_01().NuevoMovimiento(detalle, vTraspaso.IdAlmacenDestino, IdMovimiento);

            //Registra el detalle de traspaso para ser utilizado en Disoft para controlar las salidas y recargado de producto.
            iTraspaso.GuardarDetalleDisoft(idTraspaso);
        }

        private void LlenarDatosParaTI002(ref VTI002 movimiento,VTraspaso vTraspaso, int idTraspaso)
        {
            if (vTraspaso.EstadoEnvio == 1)
            {
                movimiento = new VTI002()
                {
                    IdAlmacenOrigen = vTraspaso.IdAlmacenOrigen,
                    idAlmacenDestino = vTraspaso.IdAlmacenDestino,
                    IdDetalle = idTraspaso,
                    Usuario = vTraspaso.Usuario,
                    Observacion = "TRASPASO SALIDA | DESTINO: " +
                                       vTraspaso.IdAlmacenDestino +
                                       new LAlmacen().ListarAlmacenes()
                                                     .FirstOrDefault(A => A.Id == vTraspaso.IdAlmacenDestino).Descripcion,
                    IdConecpto = (int)ENConcepto.TRASPASO_SALIDA,
                    IdDestino = 0
                };
            }
            else
            {
                movimiento = new VTI002()
                {
                    IdAlmacenOrigen = vTraspaso.IdAlmacenDestino,
                    idAlmacenDestino = vTraspaso.IdAlmacenOrigen,
                    IdDetalle = idTraspaso,
                    Usuario = vTraspaso.Usuario,
                    Observacion = "TRASPASO INGRESO | ORIGEN: " +
                                      vTraspaso.IdAlmacenOrigen +
                                      new LAlmacen().ListarAlmacenes()
                                                    .FirstOrDefault(A => A.Id == vTraspaso.IdAlmacenOrigen).Descripcion,
                    IdConecpto = (int)ENConcepto.TRASPASO_INGRESO,
                    IdDestino = 0
                };
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
        public void Eliminar(int ajusteId)
        {
            try
            {
                VTraspaso traspaso = null;
                List<VTraspaso_01> detalle = null;

                //Ajuste
                traspaso = iTraspaso.ObtenerPorId(ajusteId);
                detalle = iDetalle.ListaDetalle(ajusteId);

                //Saldo
                var accionIngreso = 0;
                var accionSalida = 0;
                var estadoEnvio = traspaso.EstadoEnvio;
                if (estadoEnvio == (int)ENEstado.SINRECEPCION)
                {
                    if (traspaso != null)
                    {
                        accionIngreso = iConcepto.ObternerPorId((int)ENConcepto.TRASPASO_INGRESO).TipoMovimiento;
                    }
                }
                else
                {
                    if (traspaso != null)
                    {
                        accionIngreso = iConcepto.ObternerPorId((int)ENConcepto.TRASPASO_INGRESO).TipoMovimiento;
                        accionSalida = iConcepto.ObternerPorId((int)ENConcepto.TRANSFORMACION_SALIDA).TipoMovimiento;
                    }
                }               
                //Actualizar Stock
                foreach (var item in detalle)
                {
                    VTraspaso_01 itemAnterior;
                    itemAnterior = detalle.Where(a => a.Id == item.Id).FirstOrDefault();
                    if (itemAnterior != null)
                    {
                        if (estadoEnvio == (int)ENEstado.SINRECEPCION)
                        {
                            var cantidad = itemAnterior.Cantidad * accionIngreso * -1;
                            iTI001.ActualizarInventario(item.IdProducto, traspaso.IdAlmacenOrigen, cantidad, item.Lote, item.FechaVencimiento);
                        }
                        else
                        {
                            var cantidad = itemAnterior.Cantidad * accionIngreso * -1;
                            iTI001.ActualizarInventario(item.IdProducto, traspaso.IdAlmacenOrigen, cantidad, item.Lote, item.FechaVencimiento);

                            cantidad = itemAnterior.Cantidad * accionSalida * -1;
                            iTI001.ActualizarInventario(item.IdProducto, traspaso.IdAlmacenDestino, cantidad, item.Lote, item.FechaVencimiento);
                        }                        
                    }
                }
                //Elimina 
                iTraspaso.ModificarEstado(ajusteId, (int)ENEstado.ELIMINAR);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion

        #region Consulta
        /******** VALOR/REGISTRO ÚNICO *********/
        /********** VARIOS REGISTROS ***********/
        public List<VTraspasoTicket> ReporteTraspaso(int traspasoId)
        {
            try
            {
                return this.iTraspaso.ReporteTraspaso(traspasoId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<VTraspaso> TraerTraspasos(int usuarioId)
        {
            try
            {
                return this.iTraspaso.TraerTraspasos(usuarioId);
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

using DATA.EntityDataModel.DiAvi;
using ENTITY.inv.Traspaso.View;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using UTILITY.Enum;

namespace REPOSITORY.Clase
{
    public class RTraspaso_01 : BaseConexion, ITraspaso_01
    {
        private readonly ITI001 tI001;
        private readonly ITI0021 tI0021;

        public RTraspaso_01(ITI001 tI001, ITI0021 tI0021)
        {
            this.tI001 = tI001;
            this.tI0021 = tI0021;
        }

        #region TRANSACCIONES

        public bool Guardar(List<VTraspaso_01> lista, int TraspasoId, int almacenId, int idTI2)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    foreach (var i in lista)
                    {
                        var detalle = new Traspaso_01
                        {
                            Cantidad = i.Cantidad,
                            Estado = i.Estado,
                            Id = i.Id,
                            Observaciones = "",
                            ProductId = i.ProductoId,
                            TraspasoId = TraspasoId,
                            Marca = i.Marca,
                            Unidad = i.Unidad
                        };

                        db.Traspaso_01.Add(detalle);

                        if (!this.tI001.ActualizarInventario(detalle.ProductId.ToString(),
                                                            almacenId,
                                                            EnAccionEnInventario.Descontar,
                                                            Convert.ToDecimal(detalle.Cantidad)))
                        {
                            return false;
                        }

                        if (!this.tI0021.Guardar(idTI2, detalle.ProductId.Value, detalle.Cantidad.Value))
                        {
                            return false;
                        }
                    }

                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public bool ConfirmarRecepcionDetalle(List<Traspaso_01> detalle, int idTI2)
        {
            try
            {
                using (var db = GetEsquema())
                {
                    foreach (var i in detalle)
                    {
                        //ACTUALIZAMOS EL INVENTARIO
                        if (!this.tI001.ActualizarInventario(i.ProductId.ToString(),
                                                            i.Traspaso.AlmacenDestino.Value,
                                                            EnAccionEnInventario.Incrementar,
                                                            Convert.ToDecimal(i.Cantidad.Value)))
                        {
                            return false;
                        }

                        //REGISTRAMOS LA CONFIRMACION COMO UN REGISTRO DONDE SE ESPEFICICA LA RECEPCION DE ESE TRASPASO
                        if (!this.tI0021.Guardar(idTI2, i.ProductId.Value, i.Cantidad.Value))
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion

        #region CONSULTAS

        public List<VTraspaso_01> ListarDetalleTraspaso(int TraspasoId)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var listResult = db.Traspaso_01
                       .Where(d => d.TraspasoId == TraspasoId)
                       .Select(d => new VTraspaso_01
                       {
                           Cantidad = d.Cantidad.Value,
                           Estado = d.Estado.Value,
                           Fecha = DateTime.Now,
                           Id = d.Id,
                           Marca = d.Marca,
                           Unidad = d.Unidad,
                           ProductoId = d.ProductId.Value,
                           ProductoDescripcion = d.Producto.Descrip,
                           TraspasoId = d.TraspasoId.Value,
                       }).ToList();

                    return listResult;
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

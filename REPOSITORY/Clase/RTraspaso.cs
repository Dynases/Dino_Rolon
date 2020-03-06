using DATA.EntityDataModel.DiAvi;
using ENTITY.inv.Traspaso.View;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace REPOSITORY.Clase
{
    public class RTraspaso : BaseConexion, ITraspaso
    {
        #region CONSULTAS

        public List<VTraspaso> ListarTraspasos()
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var listResult = db.Traspaso
                        .Select(ti => new VTraspaso
                        {
                            Destino = ti.AlmacenDestino.Value,
                            Estado = ti.Estado.Value,
                            Fecha = ti.FechaEnvio.Value,
                            Hora = ti.FechaEnvio.Value.ToString(),
                            Id = ti.Id,
                            Observaciones = ti.Observaciones,
                            Origen = ti.AlmacenOrigen.Value,
                            Usuario = ti.UsuarioEnvio
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

        #region TRANSACCIONES

        public bool Guardar(VTraspaso vTraspaso)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var traspaso = new Traspaso
                    {
                        Estado = vTraspaso.Estado,
                        AlmacenDestino = vTraspaso.Destino,
                        AlmacenOrigen = vTraspaso.Origen,
                        FechaEnvio = vTraspaso.Fecha,
                        FechaRecepcion = null,
                        Id = vTraspaso.Id,
                        Observaciones = vTraspaso.Observaciones,
                        UsuarioEnvio = vTraspaso.Usuario,
                        UsuarioRecepcion = null
                    };

                    db.Traspaso.Add(traspaso);
                    db.SaveChanges();

                    return true;
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

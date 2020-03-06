using DATA.EntityDataModel.DiAvi;
using ENTITY.inv.Traspaso.View;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REPOSITORY.Clase
{
    public class RTraspaso_01 : BaseConexion, ITraspaso_01
    {
        #region TRANSACCIONES

        public bool Guardar(List<VTraspaso_01> lista, int TraspasoId)
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
                            TraspasoId = i.TraspasoId
                        };

                        db.Traspaso_01.Add(detalle);
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
                           Id = d.Id,
                           ProductoId = d.ProductId.Value,
                           TraspasoId = d.TraspasoId.Value,
                           Fecha = DateTime.Now
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

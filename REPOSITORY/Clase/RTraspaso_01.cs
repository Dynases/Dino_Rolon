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
                        var detalle = new TI0021
                        {
                            iccant = i.Cantidad,
                            iccprod = i.ProductoId,
                            icfvenc = i.Fecha,
                            icibid = TraspasoId,
                            iclot = i.Lote
                        };

                        db.TI0021.Add(detalle);
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
                    var listResult = db.TI0021
                       .Where(d => d.icibid == TraspasoId)
                       .Select(d => new VTraspaso_01
                       {
                           Cantidad = d.iccant.Value,
                           Fecha = d.icfvenc.Value,
                           Id = d.icid,
                           Lote = d.iclot,
                           ProductoId = d.iccprod.Value,
                           TraspasoId = d.icibid.Value
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

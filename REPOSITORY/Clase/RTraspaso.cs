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
    public class RTraspaso : BaseConexion, ITraspaso
    {
        #region CONSULTAS

        public List<VTraspaso> ListarTraspasos()
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var listResult = db.TI002
                        .Where(ti => ti.ibconcep == 11)
                        .Select(ti => new VTraspaso
                        {
                            Concepto = ti.ibconcep.Value,
                            Destino = ti.ibdepdest.Value,
                            Estado = ti.ibest.Value,
                            Fecha = ti.ibfact.Value,
                            Hora = ti.ibhact,
                            Id = ti.ibid,
                            Observaciones = ti.ibobs,
                            Origen = ti.ibalm.Value,
                            Usuario = ti.ibuact
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
                    var traspaso = new TI002
                    {
                        ibalm = vTraspaso.Origen,
                        ibconcep = vTraspaso.Concepto,
                        ibdepdest = vTraspaso.Destino,
                        ibest = vTraspaso.Estado,
                        ibfact = vTraspaso.Fecha,
                        ibfdoc = vTraspaso.Fecha,
                        ibhact = vTraspaso.Hora,
                        ibiddc = vTraspaso.Id,
                        ibobs = vTraspaso.Observaciones,
                        ibuact = vTraspaso.Usuario,
                        ididdestino = 0,
                        ibid = vTraspaso.Id
                    };

                    db.TI002.Add(traspaso);
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

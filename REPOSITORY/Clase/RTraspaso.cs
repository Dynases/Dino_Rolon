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
                        ibhact = vTraspaso.Hora.ToShortTimeString(),
                        ibiddc = vTraspaso.Id,
                        ibobs = vTraspaso.Observaciones,
                        ibuact = vTraspaso.Usuario,
                        ididdestino = 0
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

        #region TRANSACCIONES
        #endregion

    }
}

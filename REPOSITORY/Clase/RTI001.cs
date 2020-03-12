using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Data.Entity;
using System.Linq;
using UTILITY.Enum;

namespace REPOSITORY.Clase
{
    public class RTI001 : BaseConexion, ITI001
    {
        #region Trasancciones

        public bool ActualizarInventario(string idProducto,
                                         int idAlmacen,
                                         EnAccionEnInventario accionEnInventario,
                                         decimal cantidad)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var inventario = db.TI001.Where(i => i.icalm == idAlmacen
                                                    && i.iccprod.Equals(idProducto))
                                             .FirstOrDefault();

                    if (accionEnInventario.Equals(EnAccionEnInventario.Incrementar))
                    { inventario.iccven += cantidad; }
                    else if (accionEnInventario.Equals(EnAccionEnInventario.Descontar))
                    { inventario.iccven -= cantidad; }

                    db.TI001.Attach(inventario);
                    db.Entry(inventario).State = EntityState.Modified;
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

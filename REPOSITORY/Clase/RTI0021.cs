using DATA.EntityDataModel.DiAvi;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Linq;

namespace REPOSITORY.Clase
{
    public class RTI0021 : BaseConexion, ITI0021
    {
        #region Trasancciones

        public bool Guardar(int idTI002,
                            int idProducto,
                            int cantidad)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var ti0021 = new TI0021
                    {
                        iccant = cantidad,
                        iccprod = idProducto,
                        icfvenc = null,
                        icibid = idTI002,
                        iclot = "",
                        icid = db.TI0021.Count() + 1
                    };

                    db.TI0021.Add(ti0021);
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

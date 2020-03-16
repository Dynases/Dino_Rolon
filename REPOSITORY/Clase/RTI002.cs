using DATA.EntityDataModel.DiAvi;
using REPOSITORY.Base;
using REPOSITORY.Interface;
using System;
using System.Linq;

namespace REPOSITORY.Clase
{
    public class RTI002 : BaseConexion, ITI002
    {
        #region Trasancciones

        public bool Guardar(int idAlmacenSalida,
                            string almacenSalida,
                            int idAlmacenDestino,
                            string almacenDestino,
                            int idTraspaso,
                            string usuario,
                            string observaciones,
                            ref int idTI2)
        {
            try
            {
                using (var db = this.GetEsquema())
                {
                    var ti002 = new TI002
                    {
                        ibalm = idAlmacenSalida,
                        ibconcep = 6,//FROM TCI001 id 6 TRASPASO SALIDA
                        ibdepdest = idAlmacenDestino,
                        ibest = 2,//NO SE SABE PORQUE
                        ibfact = DateTime.Now,
                        ibfdoc = DateTime.Now,
                        ibhact = DateTime.Now.ToShortTimeString(),
                        ibid = db.TI002.Max(t => t.ibid) + 1,
                        ibiddc = idTraspaso, //JOIN IMPLICITO A TABLA TRASPASO
                        ibobs = observaciones,
                        ibuact = usuario
                    };

                    db.TI002.Add(ti002);
                    db.SaveChanges();
                    idTI2 = ti002.ibid;
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
